using System.Text.RegularExpressions;

namespace V3KeyManager.Core;

/// <summary>An editable document, including the original text needed to retain unrecognized settings.</summary>
public sealed class GameConfiguration
{
    public string GameVersion { get; set; } = "1.01";
    public string WindowMode { get; set; } = "windowed";
    public int WindowX { get; set; }
    public int WindowY { get; set; }
    public int WindowWidth { get; set; } = 1920;
    public int WindowHeight { get; set; } = 1061;
    public int RenderWidth { get; set; } = 1920;
    public int RenderHeight { get; set; } = 1080;
    public int FullscreenWidth { get; set; } = 1920;
    public int FullscreenHeight { get; set; } = 1080;
    public string InputPrompts { get; set; } = "pad";
    public decimal MouseSpeed { get; set; } = 1m;
    public bool Fxaa { get; set; } = true;
    public Dictionary<string, List<string>> Bindings { get; } = new(StringComparer.Ordinal)
    {
        ["pad_up"] = ["key_w", "up_arrow"], ["pad_down"] = ["key_s", "down_arrow"],
        ["pad_left"] = ["key_a", "left_arrow"], ["pad_right"] = ["key_d", "right_arrow"],
        ["pad_r1"] = ["key_e", "space"], ["pad_r2"] = [], ["pad_r3"] = [],
        ["pad_l1"] = ["key_q"], ["pad_l2"] = [], ["pad_l3"] = [],
        ["pad_cross"] = ["key_z"], ["pad_circle"] = ["escape", "key_r", "key_x"],
        ["pad_square"] = ["f1"], ["pad_triangle"] = ["return"], ["pad_start"] = ["f3"],
        ["pad_select"] = ["f2"], ["pad_any"] = ["return", "space", "escape"],
        ["move_front"] = ["key_w"], ["move_back"] = ["key_s"],
        ["move_left"] = ["key_a"], ["move_right"] = ["key_d"],
        ["move_camera_up"] = ["up_arrow"], ["move_camera_down"] = ["down_arrow"],
        ["move_camera_left"] = ["left_arrow"], ["move_camera_right"] = ["right_arrow"],
        ["move_camera_left_dr"] = ["left_arrow", "key_q"],
        ["move_camera_right_dr"] = ["right_arrow", "key_e"], ["move_dash"] = ["l_shift"],
        ["message_skip"] = ["l_ctrl", "escape", "key_x"],
        ["push_w"] = ["key_w"], ["push_s"] = ["key_s"], ["push_a"] = ["key_a"], ["push_d"] = ["key_d"],
        ["mouse_mode"] = ["slash"], ["return_button"] = ["key_z", "return"]
    };

    internal string? OriginalText { get; set; }

    public GameConfiguration Clone()
    {
        var result = new GameConfiguration
        {
            GameVersion = GameVersion, WindowMode = WindowMode, WindowX = WindowX, WindowY = WindowY,
            WindowWidth = WindowWidth, WindowHeight = WindowHeight, RenderWidth = RenderWidth,
            RenderHeight = RenderHeight, FullscreenWidth = FullscreenWidth, FullscreenHeight = FullscreenHeight,
            InputPrompts = InputPrompts, MouseSpeed = MouseSpeed, Fxaa = Fxaa, OriginalText = OriginalText
        };
        result.Bindings.Clear();
        foreach (var (key, values) in Bindings)
            result.Bindings[key] = [.. values];
        return result;
    }

    public IReadOnlyList<string> Validate()
    {
        var errors = new List<string>();
        if (string.IsNullOrWhiteSpace(GameVersion) || GameVersion.IndexOfAny(['\r', '\n', '=', '[', ']', ';', '#']) >= 0)
            errors.Add("Game version must be nonempty and contain no configuration delimiters or line breaks.");
        if (WindowMode is not ("windowed" or "borderless" or "fullscreen"))
            errors.Add("Window mode must be windowed, borderless, or fullscreen.");
        if (InputPrompts is not ("pad" or "keyboard" or "keyboard_fr"))
            errors.Add("Input prompts must be pad, keyboard, or keyboard_fr.");
        foreach (var (name, value) in new (string, int)[] {
            ("Window width", WindowWidth), ("Window height", WindowHeight),
            ("Render width", RenderWidth), ("Render height", RenderHeight),
            ("Fullscreen width", FullscreenWidth), ("Fullscreen height", FullscreenHeight) })
            if (value is < 1 or > 32768)
                errors.Add($"{name} must be between 1 and 32768 pixels.");
        if (MouseSpeed is <= 0m or > 1000m)
            errors.Add("Mouse speed must be greater than 0 and at most 1000.");
        foreach (var action in BindingCatalog.Actions)
        {
            if (!Bindings.TryGetValue(action.Id, out var keys) || keys is null)
            {
                errors.Add($"Missing keyboard action: {action.Label}.");
                continue;
            }
            if (keys.Count > 3)
                errors.Add($"{action.Label} has more than three keys.");
            if (keys.Any(key => key is null || !Regex.IsMatch(key, "\\A[a-z0-9_]+\\z", RegexOptions.CultureInvariant)))
                errors.Add($"{action.Label} contains an invalid key token; use lowercase letters, numbers, and underscores.");
            if (keys.Distinct(StringComparer.Ordinal).Count() != keys.Count)
                errors.Add($"{action.Label} contains duplicate keys.");
        }
        return errors;
    }
}
