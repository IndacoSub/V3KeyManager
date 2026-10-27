using V3KeyManager.Core;

namespace V3KeyManager.UI;

internal sealed class KeyboardPage : UserControl
{
    private readonly Func<GameConfiguration> getConfiguration;
    private readonly Action changed;
    private readonly TextBox search = new();
    private readonly ComboBox group = new();
    private readonly BindingGrid grid = new();
    private readonly Label results = new();
    private readonly Button edit;
    private readonly Button reset;
    private readonly Button clear;
    private readonly ContextMenuStrip moreMenu = new();

    public KeyboardPage(Func<GameConfiguration> getConfiguration, Action changed)
    {
        this.getConfiguration = getConfiguration;
        this.changed = changed;
        Dock = DockStyle.Fill;
        AutoScaleMode = AutoScaleMode.Inherit;
        BackColor = Theme.Background;
        Font = new Font("Segoe UI", 10);
        Padding = new Padding(0);

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 4,
            BackColor = Theme.Background, Margin = Padding.Empty
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        var filters = new TableLayoutPanel
        {
            Dock = DockStyle.Top, ColumnCount = 2, AutoSize = true,
            Margin = new Padding(0, 0, 0, 16), Padding = Padding.Empty
        };
        filters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        filters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 188));
        search.Name = "bindingSearch";
        search.BorderStyle = BorderStyle.None;
        search.BackColor = Theme.Surface;
        search.ForeColor = Theme.Text;
        search.PlaceholderText = "Search actions or keys…";
        search.AccessibleName = "Search keyboard bindings";
        search.TabIndex = 0;
        search.Margin = Padding.Empty;
        search.TextChanged += (_, _) => Reload();
        group.Name = "bindingGroup";
        group.BackColor = Theme.Surface;
        group.ForeColor = Theme.Text;
        group.FlatStyle = FlatStyle.Flat;
        group.DropDownStyle = ComboBoxStyle.DropDownList;
        group.AccessibleName = "Filter bindings by group";
        group.TabIndex = 1;
        group.Items.Add("All groups");
        foreach (var name in BindingCatalog.Actions.Select(action => action.Group).Distinct())
            group.Items.Add(name);
        group.SelectedIndex = 0;
        group.Margin = Padding.Empty;
        group.SelectedIndexChanged += (_, _) => Reload();
        var searchField = new InputField(search) { Name = "bindingSearchField", Margin = new Padding(0, 0, 12, 0) };
        var groupField = new InputField(group) { Name = "bindingGroupField", Margin = Padding.Empty };
        filters.Controls.Add(searchField, 0, 0);
        filters.Controls.Add(groupField, 1, 0);
        layout.Controls.Add(filters, 0, 0);

        grid.Name = "bindingGrid";
        grid.Dock = DockStyle.Fill;
        grid.BackgroundColor = Theme.Surface;
        grid.BorderStyle = BorderStyle.None;
        grid.GridColor = Color.FromArgb(232, 235, 240);
        grid.ReadOnly = true;
        grid.AllowUserToAddRows = false;
        grid.AllowUserToDeleteRows = false;
        grid.AllowUserToResizeRows = false;
        grid.RowHeadersVisible = false;
        grid.MultiSelect = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        grid.ColumnHeadersHeight = 42;
        grid.RowTemplate.Height = 42;
        grid.EnableHeadersVisualStyles = false;
        grid.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
        {
            BackColor = Color.FromArgb(234, 237, 242), ForeColor = Theme.Muted,
            Font = new Font("Segoe UI", 9, FontStyle.Bold), Padding = new Padding(12, 0, 0, 0),
            SelectionBackColor = Color.FromArgb(234, 237, 242), SelectionForeColor = Theme.Muted
        };
        grid.DefaultCellStyle = new DataGridViewCellStyle
        {
            BackColor = Theme.Surface, ForeColor = Theme.Text,
            SelectionBackColor = Color.FromArgb(249, 230, 235), SelectionForeColor = Theme.Text,
            Padding = new Padding(12, 10, 8, 10), WrapMode = DataGridViewTriState.True
        };
        grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        grid.AccessibleName = "Keyboard actions and assigned keys";
        grid.TabIndex = 1;
        grid.Margin = Padding.Empty;
        grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Group", HeaderText = "GROUP", FillWeight = 22, SortMode = DataGridViewColumnSortMode.NotSortable });
        grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Action", HeaderText = "ACTION", FillWeight = 37, SortMode = DataGridViewColumnSortMode.NotSortable });
        grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Bindings", HeaderText = "ASSIGNED KEYS", FillWeight = 41, SortMode = DataGridViewColumnSortMode.NotSortable });
        grid.CellDoubleClick += (_, args) => { if (args.RowIndex >= 0) EditSelected(); };
        grid.EditRequested += EditSelected;
        layout.Controls.Add(grid, 0, 1);

        results.UseMnemonic = false;
        results.AutoSize = false;
        results.AutoEllipsis = true;
        results.Height = 22;
        results.MinimumSize = new Size(0, 22);
        results.Dock = DockStyle.Fill;
        results.ForeColor = Theme.Muted;
        results.Margin = new Padding(0, 10, 0, 10);
        layout.Controls.Add(results, 0, 2);
        var tools = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill, AutoSize = true, WrapContents = true,
            Margin = Padding.Empty, Padding = Padding.Empty
        };
        tools.TabIndex = 2;
        edit = Theme.Button("Edit binding…", (_, _) => EditSelected(), true);
        reset = Theme.Button("Reset selected", (_, _) => ChangeSelected(false));
        clear = Theme.Button("Clear selected", (_, _) => ChangeSelected(true));
        tools.Controls.Add(edit);
        tools.Controls.Add(reset);
        tools.Controls.Add(clear);
        var resetAll = new ToolStripMenuItem("Reset all bindings…", null, (_, _) => ResetAll());
        var randomize = new ToolStripMenuItem("Randomize bindings…", null, (_, _) => Randomize());
        moreMenu.Items.AddRange([resetAll, new ToolStripSeparator(), randomize]);
        var more = Theme.Button("More…", (_, _) => { });
        more.AccessibleName = "More keyboard binding actions";
        more.Click += (_, _) => moreMenu.Show(more, new Point(0, more.Height));
        tools.Controls.Add(more);
        foreach (Control tool in tools.Controls) tool.Margin = new Padding(0, 0, 8, 8);
        layout.Controls.Add(tools, 0, 3);
        grid.SelectionChanged += (_, _) => UpdateSelection();
        Controls.Add(layout);
        Reload();
    }

    private string? SelectedId => grid.SelectedRows.Count > 0 ? grid.SelectedRows[0].Tag as string : null;

    public void Reload()
    {
        var selected = SelectedId;
        var configuration = getConfiguration();
        var query = search.Text.Trim();
        grid.Rows.Clear();
        foreach (var action in BindingCatalog.Actions)
        {
            var bindings = configuration.Bindings.GetValueOrDefault(action.Id) ?? [];
            var label = bindings.Count == 0 ? "Unbound" : string.Join("  /  ", bindings.Select(BindingCatalog.DisplayKey));
            if (group.SelectedIndex > 0 && !string.Equals(action.Group, group.Text, StringComparison.Ordinal))
                continue;
            if (query.Length > 0 && !string.Join(" ", action.Id, action.Label, action.Group, label, string.Join(' ', bindings))
                    .Contains(query, StringComparison.OrdinalIgnoreCase))
                continue;
            var index = grid.Rows.Add(action.Group, action.Label, label);
            grid.Rows[index].Tag = action.Id;
            grid.Rows[index].Cells[1].ToolTipText = action.Id;
            grid.Rows[index].Cells[2].ToolTipText = bindings.Count == 0 ? "Unbound" : string.Join(", ", bindings);
            if (bindings.Count == 0)
                grid.Rows[index].Cells[2].Style.ForeColor = Theme.Muted;
        }
        grid.ClearSelection();
        var selection = grid.Rows.Cast<DataGridViewRow>().FirstOrDefault(row => Equals(row.Tag, selected));
        selection ??= grid.Rows.Cast<DataGridViewRow>().FirstOrDefault();
        if (selection is not null)
        {
            selection.Selected = true;
            grid.CurrentCell = selection.Cells[1];
        }
        results.Text = grid.Rows.Count == 0 ? "No actions match your search." : $"{grid.Rows.Count} of {BindingCatalog.Actions.Count} actions · Double-click or press Enter to edit.";
        UpdateSelection();
    }

    private void UpdateSelection()
    {
        var selected = SelectedId is not null;
        edit.Enabled = selected;
        reset.Enabled = selected;
        clear.Enabled = selected;
    }

    private void EditSelected()
    {
        var id = SelectedId;
        if (id is null) return;
        var action = BindingCatalog.Actions.First(action => action.Id == id);
        var bindings = getConfiguration().Bindings.GetValueOrDefault(id) ?? [];
        using var dialog = new BindingDialog(action, bindings);
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        if (bindings.SequenceEqual(dialog.Bindings)) return;
        getConfiguration().Bindings[id] = dialog.Bindings.ToList();
        changed();
        Reload();
    }

    private void ChangeSelected(bool empty)
    {
        var id = SelectedId;
        if (id is null) return;
        var replacement = empty ? new List<string>() : new GameConfiguration().Bindings[id].ToList();
        if (getConfiguration().Bindings[id].SequenceEqual(replacement)) return;
        getConfiguration().Bindings[id] = replacement;
        changed();
        Reload();
    }

    private void ResetAll()
    {
        if (MessageBox.Show(this, "Replace all keyboard bindings with the game defaults? Other settings will stay the same.",
                "Reset keyboard bindings", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK) return;
        var defaults = new GameConfiguration();
        foreach (var action in BindingCatalog.Actions)
            getConfiguration().Bindings[action.Id] = defaults.Bindings[action.Id].ToList();
        changed();
        Reload();
    }

    private void Randomize()
    {
        if (MessageBox.Show(this, "Assign random keys to all currently bound actions? Each action keeps its current number of keys; unbound actions stay unbound. Save only if you want to use this layout.",
                "Randomize keyboard bindings", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK) return;
        var configuration = getConfiguration();
        foreach (var action in BindingCatalog.Actions)
        {
            var keys = BindingCatalog.Keys.Select(key => key.Token).ToArray();
            Random.Shared.Shuffle(keys);
            var count = configuration.Bindings[action.Id].Count;
            configuration.Bindings[action.Id] = keys.Take(count).ToList();
        }
        changed();
        Reload();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing) moreMenu.Dispose();
        base.Dispose(disposing);
    }

    /// <summary>Keeps native edit controls vertically centered in matching padded fields.</summary>
    private sealed class InputField : Panel
    {
        private readonly Control editor;

        internal InputField(Control editor)
        {
            this.editor = editor;
            BackColor = Theme.Surface;
            Dock = DockStyle.Fill;
            Height = 44;
            MinimumSize = new Size(0, 44);
            Padding = new Padding(12, 0, 12, 0);
            TabStop = false;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            Controls.Add(editor);
            editor.Enter += (_, _) => Invalidate();
            editor.Leave += (_, _) => Invalidate();
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);
            if (editor is null) return;
            var height = editor.GetPreferredSize(new Size(Math.Max(1, ClientSize.Width - Padding.Horizontal), 0)).Height;
            editor.SetBounds(Padding.Left, Math.Max(0, (ClientSize.Height - height) / 2),
                Math.Max(1, ClientSize.Width - Padding.Horizontal), height);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using var pen = new Pen(ContainsFocus ? Theme.Accent : Theme.Border);
            e.Graphics.DrawRectangle(pen, 0, 0, Math.Max(0, Width - 1), Math.Max(0, Height - 1));
        }
    }

    private sealed class BindingGrid : DataGridView
    {
        public event Action? EditRequested;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                EditRequested?.Invoke();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
