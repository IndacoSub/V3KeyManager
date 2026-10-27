using System.Diagnostics;

namespace V3KeyManager.Core;

public static class GameLauncher
{
    public static IReadOnlyList<string> ResolveExecutables(string configPath)
    {
        var directory = Path.GetDirectoryName(Path.GetFullPath(configPath))!;
        return new[] { "Dangan3Win.exe", "Dangan3Desktop.exe" }
            .Select(name => Path.Combine(directory, name)).Where(File.Exists).ToArray();
    }

    public static void Start(string executablePath)
    {
        if (!OperatingSystem.IsWindows())
            throw new PlatformNotSupportedException("Launching Danganronpa V3 requires Windows.");
        var path = Path.GetFullPath(executablePath);
        if (!File.Exists(path))
            throw new FileNotFoundException("The game executable could not be found.", path);
        Process.Start(new ProcessStartInfo(path)
        {
            WorkingDirectory = Path.GetDirectoryName(path)!,
            UseShellExecute = true
        });
    }
}
