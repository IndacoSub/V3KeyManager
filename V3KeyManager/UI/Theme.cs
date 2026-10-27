namespace V3KeyManager.UI;

internal static class Theme
{
    public static readonly Color Background = Color.FromArgb(245, 246, 248);
    public static readonly Color Surface = Color.White;
    public static readonly Color Text = Color.FromArgb(28, 34, 44);
    public static readonly Color Muted = Color.FromArgb(100, 111, 125);
    public static readonly Color Accent = Color.FromArgb(178, 52, 82);
    public static readonly Color Border = Color.FromArgb(222, 226, 232);

    public static Label Label(string text, float size = 10, bool bold = false, Color? color = null) => new()
    {
        Text = text, UseMnemonic = false, AutoSize = true, ForeColor = color ?? Text,
        Font = new Font("Segoe UI", size, bold ? FontStyle.Bold : FontStyle.Regular),
        Margin = new Padding(0, 0, 0, 8), BackColor = Color.Transparent
    };

    public static Button Button(string text, EventHandler handler, bool primary = false)
    {
        var button = new Button
        {
            Text = text, UseMnemonic = false, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink,
            MinimumSize = new Size(90, 40), Padding = new Padding(12, 5, 12, 5),
            FlatStyle = FlatStyle.Flat, BackColor = primary ? Accent : Surface,
            ForeColor = primary ? Color.White : Text, Cursor = Cursors.Hand,
            Font = new Font("Segoe UI", 10, primary ? FontStyle.Bold : FontStyle.Regular), Margin = new Padding(0, 0, 8, 0),
            UseVisualStyleBackColor = false
        };
        button.FlatAppearance.BorderColor = primary ? Accent : Border;
        button.FlatAppearance.MouseOverBackColor = primary ? Color.FromArgb(153, 40, 67) : Background;
        button.Click += handler;
        return button;
    }

    public static TableLayoutPanel Stack()
    {
        var stack = new TableLayoutPanel
        {
            Dock = DockStyle.Top, AutoSize = true, ColumnCount = 1, RowCount = 0,
            GrowStyle = TableLayoutPanelGrowStyle.AddRows, Margin = Padding.Empty,
            Padding = Padding.Empty, BackColor = Color.Transparent
        };
        stack.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        return stack;
    }

    public static void AddRow(TableLayoutPanel stack, Control control)
    {
        control.Dock = DockStyle.Top;
        stack.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        stack.Controls.Add(control, 0, stack.RowCount++);
    }
}
