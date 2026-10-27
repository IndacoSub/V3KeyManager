using System.ComponentModel;
using System.Text;
using System.Text.Json;
using System.Security.Cryptography;
using V3KeyManager.Core;

namespace V3KeyManager.UI;

internal sealed class MainForm : Form
{
    private GameConfiguration configuration = new();
    private string? filePath;
    private byte[]? diskHash;
    private bool dirty;
    private string savedFingerprint = "";
    private readonly Panel content = new() { Name = "pageContent", Dock = DockStyle.Fill, Padding = new Padding(24, 20, 24, 16), Margin = Padding.Empty };
    private readonly ToolTip tooltips = new();
    private readonly Label title = Theme.Label("Keyboard bindings", 22, true);
    private readonly Label subtitle = Theme.Label("Find an action. Make it yours.", 10, color: Theme.Muted);
    private readonly Label documentName = Theme.Label("Untitled configuration", 11, true);
    private readonly Label documentPath = new() { UseMnemonic = false, AutoEllipsis = true, Dock = DockStyle.Fill, ForeColor = Theme.Muted, TextAlign = ContentAlignment.MiddleLeft, Margin = Padding.Empty };
    private readonly Label status = new() { UseMnemonic = false, AutoEllipsis = true, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft, ForeColor = Theme.Muted, Margin = new Padding(0, 0, 16, 0) };
    private readonly Label savedBadge = Theme.Label("NEW FILE", 9, true, Theme.Accent);
    private readonly List<Button> navigation = [];
    private readonly PictureBox artwork = new() { Dock = DockStyle.Fill, SizeMode = PictureBoxSizeMode.Zoom, BackColor = Color.FromArgb(35, 43, 57) };
    private readonly Button saveButton;
    private readonly Button revertButton;
    private KeyboardPage keyboardPage = null!;
    private int activePage;
    private readonly string? initialPath;

    public MainForm(string? initialPath)
    {
        this.initialPath = initialPath;
        Text = "V3 Key Manager";
        Font = new Font("Segoe UI", 10);
        ForeColor = Theme.Text;
        BackColor = Theme.Background;
        AutoScaleDimensions = new SizeF(96, 96);
        AutoScaleMode = AutoScaleMode.Dpi;
        ClientSize = new Size(1180, 760);
        MinimumSize = new Size(1024, 620);
        StartPosition = FormStartPosition.CenterScreen;

        var root = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1, Margin = Padding.Empty };
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 240));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        Controls.Add(root);
        root.Controls.Add(BuildSidebar(), 0, 0);

        var workspace = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 4, Margin = Padding.Empty };
        workspace.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        workspace.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        workspace.RowStyles.Add(new RowStyle(SizeType.Absolute, 88));
        workspace.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        workspace.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        root.Controls.Add(workspace, 1, 0);

        var heading = Theme.Stack();
        heading.Padding = new Padding(24, 22, 24, 16);
        subtitle.Margin = Padding.Empty;
        Theme.AddRow(heading, title);
        Theme.AddRow(heading, subtitle);
        workspace.Controls.Add(heading, 0, 0);

        var fileBand = new TableLayoutPanel
        {
            Name = "documentBar", Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1,
            BackColor = Theme.Surface, Padding = new Padding(16, 14, 16, 14), Margin = new Padding(24, 0, 24, 0)
        };
        fileBand.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        fileBand.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        fileBand.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        var documentInfo = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, Margin = new Padding(0, 0, 16, 0) };
        documentInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        documentInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
        documentInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        var nameRow = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1, Margin = Padding.Empty };
        nameRow.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        nameRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        nameRow.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        savedBadge.AutoSize = true;
        savedBadge.Padding = new Padding(6, 3, 6, 3);
        savedBadge.Margin = new Padding(0, 0, 10, 0);
        savedBadge.Anchor = AnchorStyles.Left;
        savedBadge.BackColor = Color.FromArgb(250, 233, 238);
        documentName.AutoSize = false;
        documentName.AutoEllipsis = true;
        documentName.Dock = DockStyle.Fill;
        documentName.TextAlign = ContentAlignment.MiddleLeft;
        documentName.Margin = Padding.Empty;
        nameRow.Controls.Add(savedBadge, 0, 0);
        nameRow.Controls.Add(documentName, 1, 0);
        documentInfo.Controls.Add(nameRow, 0, 0);
        documentInfo.Controls.Add(documentPath, 0, 1);
        fileBand.Controls.Add(documentInfo, 0, 0);
        var fileActions = new FlowLayoutPanel { AutoSize = true, Anchor = AnchorStyles.Right, WrapContents = false, Margin = Padding.Empty };
        var newButton = Theme.Button("New", (_, _) => NewDocument());
        var openButton = Theme.Button("Open…", (_, _) => OpenDocument());
        newButton.Margin = new Padding(0, 0, 8, 0);
        openButton.Margin = Padding.Empty;
        fileActions.Controls.AddRange([newButton, openButton]);
        fileBand.Controls.Add(fileActions, 1, 0);
        workspace.Controls.Add(fileBand, 0, 1);
        workspace.Controls.Add(content, 0, 2);

        var footer = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, BackColor = Theme.Surface, AutoSize = true, Padding = new Padding(24, 12, 24, 12), Margin = Padding.Empty };
        footer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        footer.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        footer.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        footer.Controls.Add(status, 0, 0);
        var actions = new FlowLayoutPanel { AutoSize = true, WrapContents = false, Anchor = AnchorStyles.Right, Margin = Padding.Empty };
        revertButton = Theme.Button("Revert", (_, _) => Revert());
        actions.Controls.Add(revertButton);
        actions.Controls.Add(Theme.Button("Save as…", (_, _) => SaveDocument(true)));
        saveButton = Theme.Button("Save", (_, _) => SaveDocument(), true);
        saveButton.Margin = Padding.Empty;
        actions.Controls.Add(saveButton);
        footer.Controls.Add(actions, 1, 0);
        workspace.Controls.Add(footer, 0, 3);

        savedFingerprint = Fingerprint();
        RebuildPage();
        UpdateDocumentState();
        FormClosing += OnFormClosing;
        Shown += (_, _) =>
        {
            var area = Screen.FromControl(this).WorkingArea;
            if (Width > area.Width || Height > area.Height) WindowState = FormWindowState.Maximized;
            if (!string.IsNullOrWhiteSpace(this.initialPath)) LoadDocument(this.initialPath);
        };
    }

    private Control BuildSidebar()
    {
        var sidebar = new TableLayoutPanel
        {
            Name = "sidebar", Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 6,
            BackColor = Color.FromArgb(25, 32, 44), Padding = new Padding(20, 24, 20, 18), Margin = Padding.Empty
        };
        sidebar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        sidebar.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        sidebar.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        sidebar.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        sidebar.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        sidebar.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        sidebar.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        var brand = Theme.Stack();
        brand.Padding = new Padding(8, 0, 0, 28);
        Theme.AddRow(brand, Theme.Label("V3", 28, true, Color.FromArgb(245, 129, 155)));
        Theme.AddRow(brand, Theme.Label("KEY MANAGER", 10, true, Color.White));
        sidebar.Controls.Add(brand, 0, 0);
        var sectionLabel = Theme.Label("CONFIGURATION", 8, true, Color.FromArgb(151, 163, 183));
        sectionLabel.Margin = new Padding(12, 0, 0, 12);
        sidebar.Controls.Add(sectionLabel, 0, 1);
        var nav = Theme.Stack();
        string[] names = ["Keyboard bindings", "Display & graphics", "Input & game"];
        for (var i = 0; i < names.Length; i++)
        {
            var page = i;
            var button = Theme.Button(names[i], (_, _) => ShowPage(page));
            button.Name = $"navigatePage{i}";
            button.TextAlign = ContentAlignment.MiddleLeft;
            button.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            button.MinimumSize = new Size(0, 46);
            button.Padding = new Padding(12, 4, 4, 4);
            button.Margin = new Padding(0, 0, 0, 6);
            button.FlatAppearance.BorderSize = 0;
            button.ForeColor = Color.FromArgb(203, 212, 227);
            button.BackColor = sidebar.BackColor;
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(49, 58, 75);
            button.FlatAppearance.MouseDownBackColor = Color.FromArgb(61, 47, 65);
            navigation.Add(button);
            Theme.AddRow(nav, button);
        }
        sidebar.Controls.Add(nav, 0, 2);
        // Artwork is optional. Keep an intentional, quiet empty state rather than a large text box.
        var artPanel = new Panel { Name = "artworkPanel", Dock = DockStyle.Fill, Margin = new Padding(8, 24, 8, 20) };
        artwork.BackColor = sidebar.BackColor;
        artwork.AccessibleName = "Optional sidebar artwork";
        var placeholder = Theme.Label("Your game.\nYour controls.", 16, true, Color.FromArgb(151, 163, 183));
        placeholder.AutoSize = false;
        placeholder.Dock = DockStyle.Fill;
        placeholder.TextAlign = ContentAlignment.BottomLeft;
        placeholder.Margin = Padding.Empty;
        artwork.Controls.Add(placeholder);
        artPanel.Controls.Add(artwork);
        artPanel.SizeChanged += (_, _) =>
        {
            var requiredHeight = TextRenderer.MeasureText(placeholder.Text, placeholder.Font).Height;
            placeholder.Visible = artwork.Image is null && artPanel.ClientSize.Height >= requiredHeight;
        };
        sidebar.Controls.Add(artPanel, 0, 3);
        var artActions = Theme.Stack();
        artActions.Padding = new Padding(0, 0, 0, 24);
        var chooseArt = Theme.Button("Choose artwork…", (_, _) => ChooseArtwork());
        chooseArt.BackColor = Color.FromArgb(35, 43, 57);
        chooseArt.ForeColor = Color.FromArgb(203, 212, 227);
        chooseArt.FlatAppearance.BorderSize = 0;
        chooseArt.FlatAppearance.MouseOverBackColor = Color.FromArgb(49, 58, 75);
        chooseArt.FlatAppearance.MouseDownBackColor = Color.FromArgb(61, 47, 65);
        chooseArt.Margin = Padding.Empty;
        Theme.AddRow(artActions, chooseArt);
        sidebar.Controls.Add(artActions, 0, 4);
        var launch = Theme.Stack();
        var launchButton = Theme.Button("Save & launch game", (_, _) => LaunchGame(), true);
        launchButton.Name = "launchGame";
        launchButton.Margin = new Padding(0, 0, 0, 12);
        Theme.AddRow(launch, launchButton);
        var versionLabel = Theme.Label("V3 Key Manager  /  2.0", 8, color: Color.FromArgb(151, 163, 183));
        versionLabel.Margin = Padding.Empty;
        Theme.AddRow(launch, versionLabel);
        sidebar.Controls.Add(launch, 0, 5);
        return sidebar;
    }

    private void ShowPage(int page)
    {
        CommitPendingEditors();
        activePage = page;
        RebuildPage();
    }

    private void RebuildPage()
    {
        content.SuspendLayout();
        foreach (Control child in content.Controls.Cast<Control>().ToArray()) child.Dispose();
        content.Controls.Clear();
        for (var i = 0; i < navigation.Count; i++)
        {
            navigation[i].BackColor = i == activePage ? Color.FromArgb(54, 43, 59) : Color.FromArgb(25, 32, 44);
            navigation[i].ForeColor = i == activePage ? Color.FromArgb(255, 177, 196) : Color.FromArgb(203, 212, 227);
        }
        Control page;
        if (activePage == 0)
        {
            title.Text = "Keyboard bindings";
            subtitle.Text = "Find an action. Make it yours.";
            keyboardPage = new KeyboardPage(() => configuration, MarkChanged) { Dock = DockStyle.Fill };
            page = keyboardPage;
        }
        else if (activePage == 1)
        {
            title.Text = "Display & graphics";
            subtitle.Text = "A comfortable view, down to the pixel.";
            page = BuildDisplayPage();
        }
        else
        {
            title.Text = "Input & game";
            subtitle.Text = "The finishing touches for your setup.";
            page = BuildInputPage();
        }
        content.Controls.Add(page);
        content.ResumeLayout(true);
        UpdateDocumentState();
    }

    private Control SettingsPage(params Control[] cards)
    {
        var scroll = new Panel { Dock = DockStyle.Fill, AutoScroll = true };
        var stack = Theme.Stack();
        stack.Padding = new Padding(0, 0, 18, 0);
        foreach (var card in cards) Theme.AddRow(stack, card);
        scroll.Controls.Add(stack);
        return scroll;
    }

    private static TableLayoutPanel Card(string heading, string description)
    {
        var card = Theme.Stack();
        card.BackColor = Theme.Surface;
        card.Padding = new Padding(20);
        card.Margin = new Padding(0, 0, 0, 16);
        Theme.AddRow(card, Theme.Label(heading, 13, true));
        Theme.AddRow(card, Theme.Label(description, 9, color: Theme.Muted));
        return card;
    }

    private static void Field(TableLayoutPanel card, string label, Control editor)
    {
        var row = new TableLayoutPanel { Dock = DockStyle.Top, AutoSize = true, ColumnCount = 2, Padding = new Padding(0, 8, 0, 4) };
        row.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 205));
        row.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        var caption = Theme.Label(label);
        caption.Anchor = AnchorStyles.Left;
        caption.Margin = new Padding(0, 6, 6, 4);
        row.Controls.Add(caption, 0, 0);
        editor.AccessibleName = label;
        editor.Dock = DockStyle.Top;
        row.Controls.Add(editor, 1, 0);
        Theme.AddRow(card, row);
    }

    private ComboBox Choice(string value, (string Value, string Label)[] choices, Action<string> set)
    {
        var combo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 270, FlatStyle = FlatStyle.Standard };
        foreach (var option in choices) combo.Items.Add(new ChoiceItem(option.Value, option.Label));
        combo.SelectedIndex = Array.FindIndex(choices, x => x.Value == value);
        combo.SelectedIndexChanged += (_, _) => { if (combo.SelectedItem is ChoiceItem item) { set(item.Value); MarkChanged(); } };
        return combo;
    }

    private NumericUpDown Number(decimal value, decimal min, decimal max, Action<decimal> set, int decimals = 0)
    {
        var number = new SettingNumber { TrimFraction = decimals > 0, CommittedValue = value, Minimum = min, Maximum = max, DecimalPlaces = decimals > 0 ? 28 : 0, Value = value, Width = 120, ThousandsSeparator = false, Increment = decimals > 0 ? 0.1m : 1m };
        number.ValueChanged += (_, _) => { var current = number.Value; number.CommittedValue = current; set(current); MarkChanged(); };
        number.TextChanged += (_, _) => MarkChanged();
        return number;
    }

    private Control Pair(int first, int second, Action<int> setFirst, Action<int> setSecond, bool position = false)
    {
        var pair = new FlowLayoutPanel { AutoSize = true, WrapContents = false, Margin = Padding.Empty };
        var a = Number(first, position ? int.MinValue : 1, position ? int.MaxValue : 32768, v => setFirst((int)v));
        var b = Number(second, position ? int.MinValue : 1, position ? int.MaxValue : 32768, v => setSecond((int)v));
        a.AccessibleName = position ? "X position" : "Width";
        b.AccessibleName = position ? "Y position" : "Height";
        pair.Controls.AddRange([a, new Label { UseMnemonic = false, Text = position ? "," : "×", AutoSize = true, Margin = new Padding(4, 5, 4, 0) }, b]);
        return pair;
    }

    private Control BuildDisplayPage()
    {
        var window = Card("Window", "Choose how the game appears on your display.");
        Field(window, "Display mode", Choice(configuration.WindowMode,
            [("windowed", "Windowed"), ("borderless", "Borderless"), ("fullscreen", "Fullscreen")], v => configuration.WindowMode = v));
        Field(window, "Window position (X, Y)", Pair(configuration.WindowX, configuration.WindowY, v => configuration.WindowX = v, v => configuration.WindowY = v, true));
        Field(window, "Window size (px)", Pair(configuration.WindowWidth, configuration.WindowHeight, v => configuration.WindowWidth = v, v => configuration.WindowHeight = v));
        var rendering = Card("Rendering", "Windowed and fullscreen resolutions are stored separately.");
        Field(rendering, "Windowed resolution (px)", Pair(configuration.RenderWidth, configuration.RenderHeight, v => configuration.RenderWidth = v, v => configuration.RenderHeight = v));
        Field(rendering, "Fullscreen resolution (px)", Pair(configuration.FullscreenWidth, configuration.FullscreenHeight, v => configuration.FullscreenWidth = v, v => configuration.FullscreenHeight = v));
        var fxaa = new CheckBox { UseMnemonic = false, Text = "Smooth jagged edges (FXAA)", Checked = configuration.Fxaa, AutoSize = true };
        fxaa.CheckedChanged += (_, _) => { configuration.Fxaa = fxaa.Checked; MarkChanged(); };
        Field(rendering, "Anti-aliasing", fxaa);
        return SettingsPage(window, rendering);
    }

    private Control BuildInputPage()
    {
        var input = Card("Input preferences", "Match the on-screen prompts to the way you play.");
        Field(input, "Button prompts", Choice(configuration.InputPrompts,
            [("pad", "Controller"), ("keyboard", "Keyboard & mouse · QWERTY"), ("keyboard_fr", "Keyboard & mouse · AZERTY")], v => configuration.InputPrompts = v));
        Field(input, "Mouse sensitivity", Number(configuration.MouseSpeed, 0.0000000000000000000000000001m, 1000, v => configuration.MouseSpeed = v, 6));
        var game = Card("Game configuration", "This is the game's config version, not the editor version.");
        var version = new TextBox { Text = configuration.GameVersion, MaxLength = 80 };
        version.TextChanged += (_, _) => { configuration.GameVersion = version.Text.Trim(); MarkChanged(); };
        Field(game, "Game version", version);
        var launch = Card("Ready to play?", "Save & launch finds the Steam or Xbox executable beside config.txt.");
        Theme.AddRow(launch, Theme.Label("You can also choose the game executable in another folder.\nYour configuration is saved there only after confirmation.", 10, color: Theme.Muted));
        Theme.AddRow(launch, Theme.Button("Save & launch game", (_, _) => LaunchGame(), true));
        return SettingsPage(input, game, launch);
    }

    private void MarkChanged()
    {
        UpdateDocumentState();
    }

    private string Fingerprint() => JsonSerializer.Serialize(configuration);

    private static IEnumerable<SettingNumber> NumericEditors(Control parent)
    {
        foreach (Control control in parent.Controls)
        {
            if (control is SettingNumber number) yield return number;
            foreach (var child in NumericEditors(control)) yield return child;
        }
    }

    private void CommitPendingEditors()
    {
        foreach (var number in NumericEditors(content).ToArray()) number.Commit();
        ValidateChildren();
        UpdateDocumentState();
    }

    private void UpdateDocumentState(string? message = null)
    {
        dirty = Fingerprint() != savedFingerprint || NumericEditors(content).Any(number => number.HasPendingEdit);
        Text = $"{(dirty ? "* " : "")}{(filePath is null ? "Untitled" : Path.GetFileName(filePath))} — V3 Key Manager";
        documentName.Text = filePath is null ? "Untitled configuration" : Path.GetFileName(filePath);
        documentPath.Text = filePath ?? "Start with the original game defaults, or open an existing config.txt.";
        documentPath.AccessibleName = "Configuration path";
        tooltips.SetToolTip(documentPath, documentPath.Text);
        tooltips.SetToolTip(documentName, documentName.Text);
        savedBadge.Text = dirty ? "UNSAVED" : filePath is null ? "NEW FILE" : "SAVED";
        savedBadge.ForeColor = dirty || filePath is null ? Theme.Accent : Color.FromArgb(37, 104, 80);
        savedBadge.BackColor = dirty || filePath is null ? Color.FromArgb(250, 233, 238) : Color.FromArgb(231, 244, 237);
        saveButton.Enabled = dirty || filePath is null;
        revertButton.Enabled = dirty;
        status.Text = message ?? (dirty ? "Unsaved changes" : filePath is null ? "Original game defaults · ready to customize" : "All changes saved");
    }

    private bool ConfirmDiscard()
    {
        CommitPendingEditors();
        if (!dirty) return true;
        var answer = MessageBox.Show(this, "Save your changes before continuing?", "Unsaved changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        return answer == DialogResult.No || answer == DialogResult.Yes && SaveDocument();
    }

    private void NewDocument()
    {
        if (!ConfirmDiscard()) return;
        configuration = new GameConfiguration();
        filePath = null;
        diskHash = null;
        savedFingerprint = Fingerprint();
        RebuildPage();
        UpdateDocumentState();
    }

    private void OpenDocument()
    {
        if (!ConfirmDiscard()) return;
        using var dialog = new OpenFileDialog { Title = "Open game configuration", Filter = "Game configuration (*.txt)|*.txt|All files (*.*)|*.*", FileName = "config.txt" };
        if (dialog.ShowDialog(this) == DialogResult.OK) LoadDocument(dialog.FileName);
    }

    private void LoadDocument(string path)
    {
        try
        {
            // Parse before replacing the document, so a bad file never loses the current work.
            var bytes = File.ReadAllBytes(path);
            using var reader = new StreamReader(new MemoryStream(bytes), detectEncodingFromByteOrderMarks: true);
            var next = ConfigurationFile.Parse(reader.ReadToEnd());
            var hash = SHA256.HashData(bytes);
            configuration = next;
            filePath = Path.GetFullPath(path);
            diskHash = hash;
            savedFingerprint = Fingerprint();
            RebuildPage();
            UpdateDocumentState("Configuration opened");
        }
        catch (Exception ex) when (IsFileError(ex)) { ShowError("Couldn't open the configuration", ex); }
    }

    private bool SaveDocument(bool saveAs = false)
    {
        var target = filePath;
        if (saveAs || target is null)
        {
            using var dialog = new SaveFileDialog { Title = "Save game configuration", Filter = "Game configuration (*.txt)|*.txt|All files (*.*)|*.*", FileName = target is null ? "config.txt" : Path.GetFileName(target), InitialDirectory = target is null ? "" : Path.GetDirectoryName(target), DefaultExt = "txt", AddExtension = true, OverwritePrompt = true };
            if (dialog.ShowDialog(this) != DialogResult.OK) return false;
            target = dialog.FileName;
        }
        return SaveTo(target);
    }

    private bool SaveTo(string target)
    {
        try
        {
            CommitPendingEditors();
            var errors = configuration.Validate();
            if (errors.Count > 0)
            {
                MessageBox.Show(this, string.Join(Environment.NewLine, errors), "Check your settings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (filePath is not null && Path.GetFullPath(target).Equals(filePath, StringComparison.OrdinalIgnoreCase) && diskHash is not null && File.Exists(target)
                && !SHA256.HashData(File.ReadAllBytes(target)).AsSpan().SequenceEqual(diskHash))
            {
                if (MessageBox.Show(this, "This file changed outside the editor. Replace it with your current settings? The previous file will be kept as a .bak backup.", "File changed", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return false;
            }
            var writtenHash = SHA256.HashData(Encoding.UTF8.GetBytes(ConfigurationFile.Serialize(configuration)));
            ConfigurationFile.Save(configuration, target);
            filePath = Path.GetFullPath(target);
            diskHash = writtenHash;
            savedFingerprint = Fingerprint();
            UpdateDocumentState("Configuration saved · previous file kept as .bak when replaced");
            return true;
        }
        catch (Exception ex) when (IsFileError(ex)) { ShowError("Couldn't save the configuration", ex); return false; }
    }

    private void Revert()
    {
        if (MessageBox.Show(this, "Discard unsaved changes and restore the last saved configuration?", "Revert changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
        if (filePath is not null) LoadDocument(filePath);
        else
        {
            configuration = new GameConfiguration();
            savedFingerprint = Fingerprint();
            RebuildPage();
            UpdateDocumentState("Original defaults restored");
        }
    }

    private void LaunchGame()
    {
        try
        {
            var candidates = filePath is null ? [] : GameLauncher.ResolveExecutables(filePath);
            string? executable = candidates.Count == 1 ? candidates[0] : candidates.Count > 1 ? ChooseExecutable(candidates) : BrowseExecutable();
            if (executable is null) return;
            var target = Path.Combine(Path.GetDirectoryName(executable)!, "config.txt");
            if (MessageBox.Show(this, $"Save these settings to:\n{target}\n\nand launch {Path.GetFileName(executable)}?\nAn existing configuration will be backed up as config.txt.bak.", "Save & launch game", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK) return;
            if (!SaveTo(target)) return;
            GameLauncher.Start(executable);
            UpdateDocumentState("Game launched");
        }
        catch (Exception ex) when (IsFileError(ex) || ex is Win32Exception or PlatformNotSupportedException)
        {
            ShowError("Couldn't launch the game", ex);
        }
    }

    private string? BrowseExecutable()
    {
        using var dialog = new OpenFileDialog { Title = "Find the Danganronpa V3 game executable", Filter = "Danganronpa V3|Dangan3Win.exe;Dangan3Desktop.exe", CheckFileExists = true };
        return dialog.ShowDialog(this) == DialogResult.OK ? dialog.FileName : null;
    }

    private string? ChooseExecutable(IReadOnlyList<string> candidates)
    {
        using var dialog = new Form { Text = "Choose game edition", ClientSize = new Size(430, 180), StartPosition = FormStartPosition.CenterParent, FormBorderStyle = FormBorderStyle.FixedDialog, MinimizeBox = false, MaximizeBox = false, Font = Font, BackColor = Theme.Background, Padding = new Padding(20) };
        var stack = Theme.Stack();
        Theme.AddRow(stack, Theme.Label("Both game editions were found. Which should launch?"));
        var combo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
        combo.Items.AddRange(candidates.Select(Path.GetFileName).Cast<object>().ToArray());
        combo.SelectedIndex = 0;
        Theme.AddRow(stack, combo);
        var buttons = new FlowLayoutPanel { AutoSize = true, Padding = new Padding(0, 16, 0, 0) };
        var choose = Theme.Button("Continue", (_, _) => dialog.DialogResult = DialogResult.OK, true);
        var cancel = Theme.Button("Cancel", (_, _) => dialog.DialogResult = DialogResult.Cancel);
        buttons.Controls.AddRange([choose, cancel]);
        Theme.AddRow(stack, buttons);
        dialog.Controls.Add(stack);
        dialog.AcceptButton = choose;
        dialog.CancelButton = cancel;
        return dialog.ShowDialog(this) == DialogResult.OK ? candidates[combo.SelectedIndex] : null;
    }

    private void ChooseArtwork()
    {
        using var dialog = new OpenFileDialog { Title = "Choose sidebar artwork", Filter = "Images|*.png;*.jpg;*.jpeg;*.bmp|All files|*.*" };
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        try
        {
            using var image = Image.FromFile(dialog.FileName);
            var copy = new Bitmap(image);
            var previous = artwork.Image;
            artwork.Image = copy;
            previous?.Dispose();
            foreach (Control overlay in artwork.Controls) overlay.Visible = false;
        }
        catch (Exception ex) when (IsFileError(ex) || ex is ArgumentException or OutOfMemoryException) { ShowError("Couldn't load this image", ex); }
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        switch (keyData)
        {
            case Keys.Control | Keys.S: SaveDocument(); return true;
            case Keys.Control | Keys.Shift | Keys.S: SaveDocument(true); return true;
            case Keys.Control | Keys.O: OpenDocument(); return true;
            case Keys.Control | Keys.N: NewDocument(); return true;
        }
        return base.ProcessCmdKey(ref msg, keyData);
    }

    private void OnFormClosing(object? sender, FormClosingEventArgs e) { if (!ConfirmDiscard()) e.Cancel = true; }
    private static bool IsFileError(Exception ex) => ex is IOException or UnauthorizedAccessException or FormatException or ArgumentException or InvalidOperationException or System.Security.SecurityException;
    private void ShowError(string title, Exception ex) => MessageBox.Show(this, ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            artwork.Image?.Dispose();
            tooltips.Dispose();
        }
        base.Dispose(disposing);
    }
    private sealed record ChoiceItem(string Value, string Label) { public override string ToString() => Label; }
}
