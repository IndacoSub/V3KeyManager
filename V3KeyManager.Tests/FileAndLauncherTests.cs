using V3KeyManager.Core;
using Xunit;

namespace V3KeyManager.Tests;

public sealed class FileAndLauncherTests : IDisposable
{
    private readonly string directory = Path.Combine(Path.GetTempPath(), "V3KeyManager.Tests", Guid.NewGuid().ToString("N"));

    public FileAndLauncherTests() => Directory.CreateDirectory(directory);

    [Fact]
    public void FirstSaveCreatesAReadableConfigurationWithoutAnUnnecessaryBackup()
    {
        var path = Path.Combine(directory, "config.txt");
        var config = new GameConfiguration { GameVersion = "2.00" };
        ConfigurationFile.Save(config, path);
        Assert.Equal("2.00", ConfigurationFile.Load(path).GameVersion);
        Assert.False(File.Exists(path + ".bak"));
    }

    [Fact]
    public void SaveMakesAnExactBackupAndReplacesTheDestination()
    {
        var path = Path.Combine(directory, "config.txt");
        var original = ConfigurationTests.ReorderedConfig;
        File.WriteAllText(path, original);
        var config = ConfigurationFile.Load(path);
        config.MouseSpeed = 4m;
        ConfigurationFile.Save(config, path);
        Assert.Equal(original, File.ReadAllText(path + ".bak"));
        Assert.Equal(4m, ConfigurationFile.Load(path).MouseSpeed);
        Assert.Equal(["key_z", "up_arrow"], ConfigurationFile.Load(path).Bindings["pad_up"]);
        Assert.Equal(2, Directory.GetFiles(directory).Length);
    }

    [Fact]
    public void RepeatedSavesBackUpTheImmediatelyPreviousFile()
    {
        var path = Path.Combine(directory, "config.txt");
        var config = new GameConfiguration();
        ConfigurationFile.Save(config, path);
        config.MouseSpeed = 2m;
        ConfigurationFile.Save(config, path);
        var secondVersion = File.ReadAllText(path);
        config.MouseSpeed = 3m;
        ConfigurationFile.Save(config, path);
        Assert.Equal(secondVersion, File.ReadAllText(path + ".bak"));
        Assert.Equal(3m, ConfigurationFile.Load(path).MouseSpeed);
    }

    [Fact]
    public void InvalidSaveLeavesExistingFileAndBackupUntouched()
    {
        var path = Path.Combine(directory, "config.txt");
        File.WriteAllText(path, ConfigurationTests.ReorderedConfig);
        File.WriteAllText(path + ".bak", "existing backup");
        var invalid = new GameConfiguration { MouseSpeed = -1m };
        Assert.ThrowsAny<ArgumentException>(() => ConfigurationFile.Save(invalid, path));
        Assert.Equal(ConfigurationTests.ReorderedConfig, File.ReadAllText(path));
        Assert.Equal("existing backup", File.ReadAllText(path + ".bak"));
        Assert.Equal(2, Directory.GetFiles(directory).Length);
    }

    [Fact]
    public void BackupFailureLeavesTheOriginalConfigurationUntouched()
    {
        var path = Path.Combine(directory, "config.txt");
        File.WriteAllText(path, ConfigurationTests.ReorderedConfig);
        Directory.CreateDirectory(path + ".bak");
        Assert.ThrowsAny<IOException>(() => ConfigurationFile.Save(new GameConfiguration(), path));
        Assert.Equal(ConfigurationTests.ReorderedConfig, File.ReadAllText(path));
        Assert.Single(Directory.GetFiles(directory));
    }

    [Fact]
    public void MissingInputFileReportsAnIoError()
    {
        Assert.Throws<FileNotFoundException>(() => ConfigurationFile.Load(Path.Combine(directory, "missing.txt")));
    }

    [Fact]
    public void LauncherFindsBothSupportedExecutablesNextToTheConfigurationOnly()
    {
        var configPath = Path.Combine(directory, "config.txt");
        var steamPath = Path.Combine(directory, "Dangan3Win.exe");
        var desktopPath = Path.Combine(directory, "Dangan3Desktop.exe");
        File.WriteAllText(steamPath, "test executable placeholder");
        File.WriteAllText(desktopPath, "test executable placeholder");
        File.WriteAllText(Path.Combine(directory, "unrelated.exe"), "test executable placeholder");
        var found = GameLauncher.ResolveExecutables(configPath);
        Assert.Equal([steamPath, desktopPath], found);
    }

    [Fact]
    public void LauncherReportsNoSupportedExecutableWhenTheFolderContainsNone()
    {
        Assert.Empty(GameLauncher.ResolveExecutables(Path.Combine(directory, "config.txt")));
    }

    [Fact]
    public void LauncherReportsTheSingleAvailableEdition()
    {
        var executable = Path.Combine(directory, "Dangan3Desktop.exe");
        File.WriteAllText(executable, "test executable placeholder");
        Assert.Equal([executable], GameLauncher.ResolveExecutables(Path.Combine(directory, "config.txt")));
    }

    public void Dispose()
    {
        if (Directory.Exists(directory))
            Directory.Delete(directory, recursive: true);
    }
}
