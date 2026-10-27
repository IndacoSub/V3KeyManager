using System.Globalization;
using System.Text;

namespace V3KeyManager.Core;

/// <summary>Reads by section/key rather than line number and retains text the editor does not understand.</summary>
public static class ConfigurationFile
{
    private static readonly string[] Sections = ["version", "window", "pad", "mouse", "effect", "keyboard"];
    private static readonly CultureInfo Invariant = CultureInfo.InvariantCulture;

    public static GameConfiguration Load(string path) => Parse(File.ReadAllText(path));

    public static GameConfiguration Parse(string text)
    {
        ArgumentNullException.ThrowIfNull(text);
        text = text.TrimStart('\uFEFF');
        var config = new GameConfiguration { OriginalText = text };
        var sections = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var keys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var section = "";
        var lines = SplitLines(text);
        for (var index = 0; index < lines.Length; index++)
        {
            var line = lines[index].Trim();
            if (line.Length == 0 || line.StartsWith(';') || line.StartsWith('#'))
                continue;
            if (line.StartsWith('['))
            {
                var end = line.IndexOf(']');
                if (end <= 1 || !IsCommentOrEmpty(line[(end + 1)..].TrimStart()))
                    throw Invalid(index, "Malformed section header.");
                section = line[1..end].Trim().ToLowerInvariant();
                if (!sections.Add(section))
                    throw Invalid(index, $"Duplicate section [{section}].");
                continue;
            }
            var equals = line.IndexOf('=');
            if (equals <= 0 || section.Length == 0)
                throw Invalid(index, "Expected a section header or key = value setting.");
            var key = line[..equals].Trim().ToLowerInvariant();
            if (!keys.Add(section + "/" + key))
                throw Invalid(index, $"Duplicate setting [{section}] {key}.");
            var value = SeparateComment(line[(equals + 1)..]).Value.Trim();
            try
            {
                Assign(config, section, key, value);
            }
            catch (Exception ex) when (ex is FormatException or OverflowException)
            {
                throw Invalid(index, $"Invalid value for [{section}] {key}: {value}.");
            }
        }
        var missing = Sections.Where(sectionName => !sections.Contains(sectionName)).ToArray();
        if (missing.Length > 0)
            throw new FormatException("Missing configuration sections: " + string.Join(", ", missing.Select(name => $"[{name}]")) + ".");
        var errors = config.Validate();
        if (errors.Count > 0)
            throw new FormatException(string.Join(Environment.NewLine, errors));
        return config;
    }

    public static string Serialize(GameConfiguration config)
    {
        ArgumentNullException.ThrowIfNull(config);
        var errors = config.Validate();
        if (errors.Count > 0)
            throw new ArgumentException(string.Join(Environment.NewLine, errors), nameof(config));
        var values = Values(config);
        if (config.OriginalText is null)
        {
            var result = new StringBuilder();
            foreach (var sectionName in Sections)
            {
                result.Append('[').Append(sectionName).Append("]\r\n");
                foreach (var (key, value) in values[sectionName])
                    result.Append(key).Append(" = ").Append(value).Append("\r\n");
                result.Append("\r\n");
            }
            return result.ToString();
        }

        var original = config.OriginalText;
        var originalValues = Values(Parse(original));
        var newline = original.Contains("\r\n", StringComparison.Ordinal) ? "\r\n" : original.Contains('\r') ? "\r" : "\n";
        var output = new List<string>();
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var section = "";
        void AddMissing()
        {
            if (!values.TryGetValue(section, out var sectionValues))
                return;
            foreach (var (key, value) in sectionValues)
                if (!seen.Contains(section + "/" + key))
                    output.Add($"{key} = {value}");
        }
        var sourceLines = SplitLines(original);
        // Exclude the artificial final empty entry; re-add the terminal newline below.
        var endedWithNewline = original.EndsWith('\n') || original.EndsWith('\r');
        var length = sourceLines.Length - (endedWithNewline ? 1 : 0);
        for (var index = 0; index < length; index++)
        {
            var raw = sourceLines[index];
            var trimmed = raw.Trim();
            if (trimmed.StartsWith('['))
            {
                AddMissing();
                section = trimmed[1..trimmed.IndexOf(']')].Trim().ToLowerInvariant();
            }
            var equals = raw.IndexOf('=');
            if (equals > 0 && !trimmed.StartsWith(';') && !trimmed.StartsWith('#') && !trimmed.StartsWith('['))
            {
                var key = raw[..equals].Trim().ToLowerInvariant();
                if (values.TryGetValue(section, out var sectionValues) && sectionValues.TryGetValue(key, out var value))
                {
                    seen.Add(section + "/" + key);
                    if (value != originalValues[section][key])
                    {
                        var tail = raw[(equals + 1)..];
                        var leadingSpace = tail.Length - tail.TrimStart().Length;
                        var (_, comment) = SeparateComment(tail);
                        raw = raw[..(equals + 1)] + tail[..leadingSpace] + value + comment;
                    }
                }
            }
            output.Add(raw);
        }
        AddMissing();
        return string.Join(newline, output) + (endedWithNewline ? newline : "");
    }

    /// <summary>Writes a sibling temporary file before replacing the destination and keeping its previous contents as .bak.</summary>
    public static void Save(GameConfiguration config, string path)
    {
        var text = Serialize(config); // Validate everything before touching the destination.
        var destination = Path.GetFullPath(path);
        var directory = Path.GetDirectoryName(destination)!;
        var temporary = Path.Combine(directory, $".{Path.GetFileName(destination)}.{Guid.NewGuid():N}.tmp");
        try
        {
            using (var stream = new FileStream(temporary, FileMode.CreateNew, FileAccess.Write, FileShare.None))
            {
                using var writer = new StreamWriter(stream, new UTF8Encoding(false), leaveOpen: true);
                writer.Write(text);
                writer.Flush();
                stream.Flush(flushToDisk: true);
            }
            if (File.Exists(destination))
                File.Replace(temporary, destination, destination + ".bak");
            else
                File.Move(temporary, destination);
            config.OriginalText = text;
        }
        finally
        {
            if (File.Exists(temporary))
                File.Delete(temporary);
        }
    }

    private static void Assign(GameConfiguration config, string section, string key, string value)
    {
        static int Integer(string number) => int.Parse(number, NumberStyles.Integer, Invariant);
        switch (section + "/" + key)
        {
            case "version/version_num": config.GameVersion = value; break;
            case "window/window_mode": config.WindowMode = value; break;
            case "window/window_top_x": config.WindowX = Integer(value); break;
            case "window/window_top_y": config.WindowY = Integer(value); break;
            case "window/window_size_x": config.WindowWidth = Integer(value); break;
            case "window/window_size_y": config.WindowHeight = Integer(value); break;
            case "window/render_size_x": config.RenderWidth = Integer(value); break;
            case "window/render_size_y": config.RenderHeight = Integer(value); break;
            case "window/fullscreen_render_size_x": config.FullscreenWidth = Integer(value); break;
            case "window/fullscreen_render_size_y": config.FullscreenHeight = Integer(value); break;
            case "pad/pad_help_type": config.InputPrompts = value; break;
            case "mouse/mouse_speed": config.MouseSpeed = decimal.Parse(value, NumberStyles.Float, Invariant); break;
            case "effect/fxaa": config.Fxaa = value switch { "on" => true, "off" => false, _ => throw new FormatException() }; break;
            default:
                if (section == "keyboard" && config.Bindings.ContainsKey(key))
                    config.Bindings[key] = value.Length == 0 ? [] : value.Split(',').Select(token => token.Trim()).ToList();
                break;
        }
    }

    private static Dictionary<string, Dictionary<string, string>> Values(GameConfiguration config) => new(StringComparer.OrdinalIgnoreCase)
    {
        ["version"] = new() { ["version_num"] = config.GameVersion },
        ["window"] = new()
        {
            ["window_mode"] = config.WindowMode,
            ["window_top_x"] = config.WindowX.ToString(Invariant), ["window_top_y"] = config.WindowY.ToString(Invariant),
            ["window_size_x"] = config.WindowWidth.ToString(Invariant), ["window_size_y"] = config.WindowHeight.ToString(Invariant),
            ["render_size_x"] = config.RenderWidth.ToString(Invariant), ["render_size_y"] = config.RenderHeight.ToString(Invariant),
            ["fullscreen_render_size_x"] = config.FullscreenWidth.ToString(Invariant),
            ["fullscreen_render_size_y"] = config.FullscreenHeight.ToString(Invariant)
        },
        ["pad"] = new() { ["pad_help_type"] = config.InputPrompts },
        ["mouse"] = new() { ["mouse_speed"] = config.MouseSpeed.ToString("0.000000######################", Invariant) },
        ["effect"] = new() { ["fxaa"] = config.Fxaa ? "on" : "off" },
        ["keyboard"] = BindingCatalog.Actions.ToDictionary(action => action.Id, action => string.Join(',', config.Bindings[action.Id]))
    };

    private static string[] SplitLines(string text) => text.Replace("\r\n", "\n", StringComparison.Ordinal).Replace('\r', '\n').Split('\n');
    private static bool IsCommentOrEmpty(string value) => value.Length == 0 || value.StartsWith(';') || value.StartsWith('#');
    private static FormatException Invalid(int zeroBasedLine, string message) => new($"Line {zeroBasedLine + 1}: {message}");

    private static (string Value, string Comment) SeparateComment(string value)
    {
        for (var index = 0; index < value.Length; index++)
        {
            if ((value[index] == ';' || value[index] == '#') && (index == 0 || char.IsWhiteSpace(value[index - 1])))
            {
                var start = index;
                while (start > 0 && char.IsWhiteSpace(value[start - 1]))
                    start--;
                return (value[..start], value[start..]);
            }
        }
        // Preserve trailing whitespace when changing the value, too.
        var trimmedLength = value.TrimEnd().Length;
        return (value[..trimmedLength], value[trimmedLength..]);
    }
}
