namespace V3KeyManager.Core;

public sealed record BindingDefinition(string Id, string Label, string Group);
public sealed record KeyDefinition(string Token, string Label);

/// <summary>The game's action names and supported key labels, independent of the editor UI.</summary>
public static class BindingCatalog
{
    public static IReadOnlyList<BindingDefinition> Actions { get; } = Array.AsReadOnly<BindingDefinition>([
        new("pad_up", "D-pad up", "Controller"),
        new("pad_down", "D-pad down", "Controller"),
        new("pad_left", "D-pad left", "Controller"),
        new("pad_right", "D-pad right", "Controller"),
        new("pad_r1", "Right shoulder / R1", "Controller"),
        new("pad_r2", "Right trigger / R2", "Controller"),
        new("pad_r3", "Right stick / R3", "Controller"),
        new("pad_l1", "Left shoulder / L1", "Controller"),
        new("pad_l2", "Left trigger / L2", "Controller"),
        new("pad_l3", "Left stick / L3", "Controller"),
        new("pad_cross", "Cross / A", "Controller"),
        new("pad_circle", "Circle / B", "Controller"),
        new("pad_square", "Square / X", "Controller"),
        new("pad_triangle", "Triangle / Y", "Controller"),
        new("pad_start", "Start / Menu", "Controller"),
        new("pad_select", "Select / View", "Controller"),
        new("pad_any", "Any button", "Controller"),
        new("move_front", "Move forward", "Movement"),
        new("move_back", "Move backward", "Movement"),
        new("move_left", "Move left", "Movement"),
        new("move_right", "Move right", "Movement"),
        new("move_camera_up", "Camera up", "Camera"),
        new("move_camera_down", "Camera down", "Camera"),
        new("move_camera_left", "Camera left", "Camera"),
        new("move_camera_right", "Camera right", "Camera"),
        new("move_camera_left_dr", "Camera left (trial)", "Camera"),
        new("move_camera_right_dr", "Camera right (trial)", "Camera"),
        new("move_dash", "Dash", "Movement"),
        new("message_skip", "Skip dialogue", "Actions"),
        new("push_w", "W prompt", "Actions"),
        new("push_s", "S prompt", "Actions"),
        new("push_a", "A prompt", "Actions"),
        new("push_d", "D prompt", "Actions"),
        new("mouse_mode", "Mouse mode", "Actions"),
        new("return_button", "Confirm / Return", "Actions")
    ]);

    // Keep the game's historical "return" token mapped to Backspace, as in the original editor.
    public static IReadOnlyList<KeyDefinition> Keys { get; } = Array.AsReadOnly<KeyDefinition>([
        new("escape", "Escape"), new("f1", "F1"), new("f2", "F2"), new("f3", "F3"),
        new("key_a", "A"), new("key_d", "D"), new("key_e", "E"), new("key_q", "Q"),
        new("key_r", "R"), new("key_s", "S"), new("key_w", "W"), new("key_x", "X"), new("key_z", "Z"),
        new("l_ctrl", "Left Ctrl"), new("l_shift", "Left Shift"), new("return", "Backspace"),
        new("slash", "/"), new("space", "Space"), new("down_arrow", "Down arrow"),
        new("left_arrow", "Left arrow"), new("right_arrow", "Right arrow"), new("up_arrow", "Up arrow")
    ]);

    public static string DisplayKey(string token) => Keys.FirstOrDefault(key => key.Token == token)?.Label ?? token;
}
