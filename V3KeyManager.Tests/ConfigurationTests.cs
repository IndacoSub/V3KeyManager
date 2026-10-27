using System.Globalization;
using V3KeyManager.Core;
using Xunit;

namespace V3KeyManager.Tests;

public sealed class ConfigurationTests
{
    [Fact]
    public void DefaultsMatchTheOriginalGameConfiguration()
    {
        var config = new GameConfiguration();
        Assert.Equal("1.01", config.GameVersion);
        Assert.Equal("windowed", config.WindowMode);
        Assert.Equal(0, config.WindowX);
        Assert.Equal(0, config.WindowY);
        Assert.Equal(1920, config.WindowWidth);
        Assert.Equal(1061, config.WindowHeight);
        Assert.Equal(1920, config.RenderWidth);
        Assert.Equal(1080, config.RenderHeight);
        Assert.Equal(1920, config.FullscreenWidth);
        Assert.Equal(1080, config.FullscreenHeight);
        Assert.Equal("pad", config.InputPrompts);
        Assert.Equal(1m, config.MouseSpeed);
        Assert.True(config.Fxaa);
        Assert.Empty(config.Validate());

        var expected = new Dictionary<string, string>
        {
            ["pad_up"] = "key_w,up_arrow", ["pad_down"] = "key_s,down_arrow",
            ["pad_left"] = "key_a,left_arrow", ["pad_right"] = "key_d,right_arrow",
            ["pad_r1"] = "key_e,space", ["pad_r2"] = "", ["pad_r3"] = "",
            ["pad_l1"] = "key_q", ["pad_l2"] = "", ["pad_l3"] = "",
            ["pad_cross"] = "key_z", ["pad_circle"] = "escape,key_r,key_x",
            ["pad_square"] = "f1", ["pad_triangle"] = "return",
            ["pad_start"] = "f3", ["pad_select"] = "f2",
            ["pad_any"] = "return,space,escape", ["move_front"] = "key_w",
            ["move_back"] = "key_s", ["move_left"] = "key_a", ["move_right"] = "key_d",
            ["move_camera_up"] = "up_arrow", ["move_camera_down"] = "down_arrow",
            ["move_camera_left"] = "left_arrow", ["move_camera_right"] = "right_arrow",
            ["move_camera_left_dr"] = "left_arrow,key_q", ["move_camera_right_dr"] = "right_arrow,key_e",
            ["move_dash"] = "l_shift", ["message_skip"] = "l_ctrl,escape,key_x",
            ["push_w"] = "key_w", ["push_s"] = "key_s", ["push_a"] = "key_a",
            ["push_d"] = "key_d", ["mouse_mode"] = "slash", ["return_button"] = "key_z,return"
        };
        Assert.Equal(expected.Count, config.Bindings.Count);
        foreach (var (key, value) in expected)
            Assert.Equal(value, string.Join(',', config.Bindings[key]));
        Assert.Equal(expected.Keys.Order(), BindingCatalog.Actions.Select(action => action.Id).Order());
    }

    [Fact]
    public void ParseFindsSectionsAndKeysRegardlessOfOrderOrBlankLines()
    {
        var config = ConfigurationFile.Parse(ReorderedConfig);
        Assert.Equal("1.02", config.GameVersion);
        Assert.Equal("borderless", config.WindowMode);
        Assert.Equal(-1920, config.WindowX);
        Assert.Equal(40, config.WindowY);
        Assert.Equal(2560, config.WindowWidth);
        Assert.Equal(1440, config.WindowHeight);
        Assert.Equal(1280, config.RenderWidth);
        Assert.Equal(720, config.RenderHeight);
        Assert.Equal(3840, config.FullscreenWidth);
        Assert.Equal(2160, config.FullscreenHeight);
        Assert.Equal("keyboard_fr", config.InputPrompts);
        Assert.Equal(1.25m, config.MouseSpeed);
        Assert.False(config.Fxaa);
        Assert.Equal(["key_z", "up_arrow"], config.Bindings["pad_up"]);
        Assert.Empty(config.Bindings["pad_r2"]);
    }

    [Fact]
    public void RoundTripPreservesEveryKnownFieldAndBinding()
    {
        var original = ConfigurationFile.Parse(ReorderedConfig);
        var saved = ConfigurationFile.Parse(ConfigurationFile.Serialize(original));
        Assert.Equal(ConfigurationFile.Serialize(original), ConfigurationFile.Serialize(saved));
        foreach (var action in BindingCatalog.Actions)
            Assert.Equal(original.Bindings[action.Id], saved.Bindings[action.Id]);
    }

    [Theory]
    [InlineData("it-IT")]
    [InlineData("fr-FR")]
    [InlineData("en-US")]
    public void NumericFormatIsIndependentOfCurrentCulture(string culture)
    {
        var previous = CultureInfo.CurrentCulture;
        try
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(culture);
            var config = ConfigurationFile.Parse(ReorderedConfig);
            config.MouseSpeed = 2.125m;
            var text = ConfigurationFile.Serialize(config);
            Assert.Contains("2.125", text);
            Assert.DoesNotContain("2,125", text);
            Assert.Equal(2.125m, ConfigurationFile.Parse(text).MouseSpeed);
            Assert.Equal(culture, CultureInfo.CurrentCulture.Name);
        }
        finally { CultureInfo.CurrentCulture = previous; }
    }

    [Fact]
    public void CloneDoesNotShareBindingListsOrDocumentChanges()
    {
        var original = ConfigurationFile.Parse(ReorderedConfig);
        var originalText = ConfigurationFile.Serialize(original);
        var clone = original.Clone();
        clone.Bindings["pad_up"].Clear();
        clone.GameVersion = "9.99";
        Assert.Equal(["key_z", "up_arrow"], original.Bindings["pad_up"]);
        Assert.Equal(originalText, ConfigurationFile.Serialize(original));
        Assert.NotEqual(originalText, ConfigurationFile.Serialize(clone));
    }

    [Fact]
    public void EditingRetainsUnknownSettingsSectionsCommentsAndKeyTokens()
    {
        var input = ReorderedConfig + "\n; custom section\n[mod]\ntexture_pack = high\n";
        input = input.Replace("pad_up= key_z , up_arrow", "pad_up= custom_key , up_arrow");
        input = input.Replace("[mouse]", "[mouse]\n# sensitivity note\nexperimental_mouse = untouched");
        var config = ConfigurationFile.Parse(input);
        config.MouseSpeed = 3m;
        var text = ConfigurationFile.Serialize(config);
        Assert.Contains("; custom section", text);
        Assert.Contains("[mod]", text);
        Assert.Contains("texture_pack = high", text);
        Assert.Contains("# sensitivity note", text);
        Assert.Contains("experimental_mouse = untouched", text);
        Assert.Equal(["custom_key", "up_arrow"], ConfigurationFile.Parse(text).Bindings["pad_up"]);
        Assert.Equal(3m, ConfigurationFile.Parse(text).MouseSpeed);
    }

    [Theory]
    [InlineData("window_size_x=2560", "window_size_x=not-a-number")]
    [InlineData("window_size_x=2560", "window_size_x=0")]
    [InlineData("window_mode=borderless", "window_mode=invalid")]
    [InlineData("mouse_speed = 1.25", "mouse_speed = NaN")]
    [InlineData("fxaa=off", "fxaa=perhaps")]
    [InlineData("pad_up= key_z , up_arrow", "pad_up=key_z,key_z")]
    [InlineData("pad_up= key_z , up_arrow", "pad_up=key_z,key_a,key_b,key_c")]
    public void MalformedValuesFailWithAnActionableError(string from, string to)
    {
        Assert.Contains(from, ReorderedConfig);
        var exception = Assert.Throws<FormatException>(() => ConfigurationFile.Parse(ReorderedConfig.Replace(from, to)));
        Assert.False(string.IsNullOrWhiteSpace(exception.Message));
    }

    [Theory]
    [InlineData("")]
    [InlineData("This is not a game configuration")]
    [InlineData("[window]\nwindow_mode=windowed")]
    public void UnrelatedOrIncompleteFilesAreRejected(string input)
    {
        Assert.Throws<FormatException>(() => ConfigurationFile.Parse(input));
    }

    [Fact]
    public void DuplicateSettingsAreRejectedRatherThanSilentlyTakingOneValue()
    {
        Assert.Throws<FormatException>(() => ConfigurationFile.Parse(ReorderedConfig.Replace("window_size_x=2560", "window_size_x=2560\nwindow_size_x=1920")));
    }

    [Fact]
    public void MissingKeysInRecognizedSectionsUseGameDefaults()
    {
        const string input = "[version]\n[window]\n[pad]\n[mouse]\n[effect]\n[keyboard]\n";
        var config = ConfigurationFile.Parse(input);
        Assert.Equal("1.01", config.GameVersion);
        Assert.Equal(1920, config.RenderWidth);
        Assert.Equal(1m, config.MouseSpeed);
        Assert.Equal(["key_w", "up_arrow"], config.Bindings["pad_up"]);
        var roundTrip = ConfigurationFile.Parse(ConfigurationFile.Serialize(config));
        Assert.Equal("windowed", roundTrip.WindowMode);
        Assert.Equal(35, roundTrip.Bindings.Count);
    }

    [Theory]
    [InlineData("")]
    [InlineData("1.01\n[mod]")]
    [InlineData("1.01; comment")]
    [InlineData("1.01=other")]
    public void VersionCannotInjectConfigurationSyntax(string version)
    {
        var config = new GameConfiguration { GameVersion = version };
        Assert.NotEmpty(config.Validate());
    }

    [Theory]
    [InlineData("key_a,key_a")]
    [InlineData("key_a,key_b,key_c,key_d")]
    [InlineData("key_a,invalid token")]
    [InlineData("key_a,foo=bar")]
    public void ValidationRejectsUnsafeBindings(string bindings)
    {
        var config = new GameConfiguration();
        config.Bindings["pad_up"] = bindings.Split(',').ToList();
        Assert.NotEmpty(config.Validate());
    }

    [Fact]
    public void ValidationAcceptsUnboundActionsAndUnknownSafeTokens()
    {
        var config = new GameConfiguration();
        config.Bindings["pad_up"] = [];
        config.Bindings["pad_down"] = ["custom_key"];
        Assert.Empty(config.Validate());
    }

    [Fact]
    public void InvalidStateCannotBeSerialized()
    {
        var config = new GameConfiguration { WindowHeight = 0 };
        Assert.ThrowsAny<ArgumentException>(() => ConfigurationFile.Serialize(config));
    }

    internal const string ReorderedConfig = """
        ; V3 settings exported by a different editor
        [keyboard]
        pad_up= key_z , up_arrow
        pad_down=key_s,down_arrow
        pad_left=key_a,left_arrow
        pad_right=key_d,right_arrow
        pad_r1=key_e,space
        pad_r2=
        pad_r3=
        pad_l1=key_q
        pad_l2=
        pad_l3=
        pad_cross=key_z
        pad_circle=escape,key_r,key_x
        pad_square=f1
        pad_triangle=return
        pad_start=f3
        pad_select=f2
        pad_any=return,space,escape
        move_front=key_w
        move_back=key_s
        move_left=key_a
        move_right=key_d
        move_camera_up=up_arrow
        move_camera_down=down_arrow
        move_camera_left=left_arrow
        move_camera_right=right_arrow
        move_camera_left_dr=left_arrow,key_q
        move_camera_right_dr=right_arrow,key_e
        move_dash=l_shift
        message_skip=l_ctrl,escape,key_x
        push_w=key_w
        push_s=key_s
        push_a=key_a
        push_d=key_d
        mouse_mode=slash
        return_button=key_z,return
        [effect]
        fxaa=off
        [mouse]
        mouse_speed = 1.25
        [window]
        fullscreen_render_size_y=2160
        window_top_y=40
        render_size_x=1280
        window_size_y=1440
        window_mode=borderless
        window_top_x=-1920
        fullscreen_render_size_x=3840
        window_size_x=2560
        render_size_y=720
        [version]
        version_num=1.02
        [pad]
        pad_help_type=keyboard_fr
        """;
}
