using System.Text.RegularExpressions;
using V3KeyManager.Core;

namespace V3KeyManager.UI;

internal sealed class BindingDialog : Form
{
    private readonly ComboBox[] editors = new ComboBox[3];
    private readonly Label error;

    public IReadOnlyList<string> Bindings { get; private set; } = [];

    public BindingDialog(BindingDefinition action, IReadOnlyList<string> bindings)
    {
        Text = "Edit keyboard binding";
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        ShowInTaskbar = false;
        AutoScaleDimensions = new SizeF(96, 96);
        AutoScaleMode = AutoScaleMode.Dpi;
        ClientSize = new Size(510, 430);
        Font = new Font("Segoe UI", 10);
        BackColor = Theme.Background;
        ForeColor = Theme.Text;
        Padding = new Padding(24);

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 7,
            AutoSize = true, BackColor = Theme.Background
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 88));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        var title = Theme.Label(action.Label, 18, true);
        title.Dock = DockStyle.Fill;
        title.Margin = new Padding(0, 0, 0, 10);
        layout.Controls.Add(title, 0, 0);
        layout.SetColumnSpan(title, 2);
        var help = Theme.Label("Choose up to three keys, or select Unbound to remove a key.\nYou can also type a game token, such as key_b.", 9, color: Theme.Muted);
        help.AutoSize = true;
        help.Margin = new Padding(0, 0, 0, 18);
        layout.Controls.Add(help, 0, 1);
        layout.SetColumnSpan(help, 2);
        for (var index = 0; index < editors.Length; index++)
        {
            var label = Theme.Label($"Key {index + 1}");
            label.Anchor = AnchorStyles.Left;
            label.Margin = new Padding(0, 6, 0, 12);
            layout.Controls.Add(label, 0, index + 2);
            var editor = new ComboBox
            {
                Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDown,
                AutoCompleteMode = AutoCompleteMode.SuggestAppend,
                AutoCompleteSource = AutoCompleteSource.ListItems,
                Margin = new Padding(0, 0, 0, 12), AccessibleName = $"Assigned key {index + 1}",
                TabIndex = index
            };
            editor.Items.Add(new KeyChoice("", "Unbound"));
            foreach (var key in BindingCatalog.Keys)
                editor.Items.Add(new KeyChoice(key.Token, key.Label));
            var token = bindings.ElementAtOrDefault(index) ?? "";
            var choice = editor.Items.Cast<KeyChoice>().FirstOrDefault(choice => choice.Token == token);
            if (choice is not null) editor.SelectedItem = choice;
            else editor.Text = token;
            editors[index] = editor;
            layout.Controls.Add(editor, 1, index + 2);
        }
        error = Theme.Label("", 9, color: Theme.Accent);
        error.AutoSize = false;
        error.Dock = DockStyle.Fill;
        error.MinimumSize = new Size(0, 44);
        error.AccessibleName = "Binding validation error";
        layout.Controls.Add(error, 0, 5);
        layout.SetColumnSpan(error, 2);
        var buttons = new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.RightToLeft, Dock = DockStyle.Fill,
            AutoSize = true, Margin = Padding.Empty
        };
        var save = Theme.Button("Apply binding", (_, _) => Apply(), true);
        var cancel = Theme.Button("Cancel", (_, _) => { DialogResult = DialogResult.Cancel; Close(); });
        buttons.TabIndex = 3;
        save.TabIndex = 0;
        cancel.TabIndex = 1;
        cancel.DialogResult = DialogResult.Cancel;
        buttons.Controls.Add(save);
        buttons.Controls.Add(cancel);
        layout.Controls.Add(buttons, 0, 6);
        layout.SetColumnSpan(buttons, 2);
        Controls.Add(layout);
        AcceptButton = save;
        CancelButton = cancel;
        Shown += (_, _) => editors[0].Focus();
    }

    private void Apply()
    {
        var bindings = new List<string>();
        foreach (var editor in editors)
        {
            var text = editor.Text.Trim();
            var known = editor.Items.Cast<KeyChoice>().FirstOrDefault(choice =>
                string.Equals(choice.Label, text, StringComparison.OrdinalIgnoreCase));
            var token = known?.Token ?? text.ToLowerInvariant();
            if (token.Length == 0) continue;
            if (!Regex.IsMatch(token, "\\A[a-z0-9_]+\\z", RegexOptions.CultureInvariant))
            {
                error.Text = "Use a listed key or a game token containing letters, numbers and underscores.";
                editor.Focus();
                return;
            }
            if (bindings.Contains(token, StringComparer.Ordinal))
            {
                error.Text = "Each key can appear only once in this action. Choose another key or Unbound.";
                editor.Focus();
                return;
            }
            bindings.Add(token);
        }
        Bindings = bindings;
        DialogResult = DialogResult.OK;
        Close();
    }

    private sealed record KeyChoice(string Token, string Label)
    {
        public override string ToString() => Label;
    }
}
