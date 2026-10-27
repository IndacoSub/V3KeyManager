using System.Drawing.Imaging;
using System.Reflection;
using System.Runtime.ExceptionServices;
using V3KeyManager.Core;
using V3KeyManager.UI;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace V3KeyManager.UI.Tests;

/// <summary>Runs on the Windows CI runner; Linux builds cannot execute Windows Forms.</summary>
public sealed class LayoutTests
{
    private static readonly Lazy<bool> ApplicationReady = new(() =>
    {
        Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        return true;
    });

    [Fact]
    public void ThemeRendersAmpersandsAsTextRatherThanHiddenMnemonics() => OnSta(() =>
    {
        using var button = Theme.Button("Save & launch game", (_, _) => { });
        using var label = Theme.Label("Display & graphics");
        Assert.False(button.UseMnemonic);
        Assert.False(label.UseMnemonic);
        Assert.Equal("Save & launch game", button.Text);
        Assert.Equal("Display & graphics", label.Text);
    });

    [Theory]
    [InlineData(1180, 760)]
    [InlineData(1024, 640)]
    public void EveryPageKeepsCommandsInsideItsViewportAndProducesAScreenshot(int width, int height) => OnSta(() =>
    {
        using var form = new MainForm(null);
        form.StartPosition = FormStartPosition.Manual;
        form.Location = Point.Empty;
        form.Show();
        form.WindowState = FormWindowState.Normal;
        form.ClientSize = new Size(width, height);
        PumpLayout(form);

        var showPage = typeof(MainForm).GetMethod("ShowPage", BindingFlags.Instance | BindingFlags.NonPublic)!;
        for (var page = 0; page < 3; page++)
        {
            showPage.Invoke(form, [page]);
            PumpLayout(form);
            // Capture before assertions so a failing layout remains reviewable in CI artifacts.
            SaveScreenshot(form, $"page-{page}-{width}x{height}.png");
            AssertCommandsVisible(form);
            AssertLiteralAmpersands(form);
            if (page == 0)
            {
                var grid = Descendants(form).OfType<DataGridView>().Single();
                Assert.Equal(3, grid.Columns.Count);
                Assert.Equal(35, grid.Rows.Count);
                Assert.True(grid.ClientSize.Height >= 120, $"Keyboard list has only {grid.ClientSize.Height}px of height.");
                var searchField = Descendants(form).Single(control => control.Name == "bindingSearchField");
                var groupField = Descendants(form).Single(control => control.Name == "bindingGroupField");
                Assert.True(searchField.Height >= 44, $"Search field has only {searchField.Height}px of height.");
                Assert.Equal(searchField.Height, groupField.Height);
                Assert.Equal(searchField.PointToScreen(Point.Empty).Y, groupField.PointToScreen(Point.Empty).Y);
            }
        }
    });

    [Fact]
    public void ConfigurationNamesAndPathsDisplayLiteralAmpersands() => OnSta(() =>
    {
        var directory = Path.Combine(Path.GetTempPath(), "V3 UI & path tests", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(directory);
        try
        {
            var path = Path.Combine(directory, "custom & profile.txt");
            ConfigurationFile.Save(new GameConfiguration(), path);
            using var form = new MainForm(path);
            form.Show();
            PumpLayout(form);
            var labels = Descendants(form).OfType<Label>().ToArray();
            Assert.Contains(labels, label => label.Text == Path.GetFileName(path));
            Assert.Contains(labels, label => label.Text == path);
            AssertLiteralAmpersands(form);
        }
        finally { Directory.Delete(directory, recursive: true); }
    });

    private static void AssertCommandsVisible(Form form)
    {
        var buttons = Descendants(form).OfType<Button>().Where(button => button.Visible).ToArray();
        Assert.NotEmpty(buttons);
        foreach (var button in buttons)
        {
            // Settings pages intentionally scroll; grid and numeric editors have native child controls.
            if (Ancestors(button).Any(parent => parent is DataGridView or NumericUpDown || parent is ScrollableControl { AutoScroll: true }))
                continue;
            Assert.True(button.Width > 0 && button.Height > 0, $"'{button.Text}' has no visible size.");
            for (Control child = button; child.Parent is not null; child = child.Parent)
            {
                var parent = child.Parent;
                var visibleBounds = Rectangle.Intersect(child.Bounds, parent.ClientRectangle);
                Assert.True(visibleBounds.Width >= child.Width && visibleBounds.Height >= child.Height,
                    $"'{button.Text}' is clipped by {parent.GetType().Name}: child={child.Bounds}, viewport={parent.ClientRectangle}.");
            }
        }
    }

    private static void AssertLiteralAmpersands(Form form)
    {
        foreach (var control in Descendants(form).Where(control => control.Text.Contains('&')))
        {
            if (control is Button button) Assert.False(button.UseMnemonic);
            if (control is Label label) Assert.False(label.UseMnemonic);
        }
    }

    private static void PumpLayout(Form form)
    {
        form.PerformLayout();
        Application.DoEvents();
        form.PerformLayout();
        Application.DoEvents();
    }

    private static void SaveScreenshot(Form form, string fileName)
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null && !File.Exists(Path.Combine(directory.FullName, "V3KeyManager.sln")))
            directory = directory.Parent;
        var destination = Path.Combine(directory?.FullName ?? AppContext.BaseDirectory, "artifacts", "ui");
        Directory.CreateDirectory(destination);
        using var bitmap = new Bitmap(form.Width, form.Height);
        form.DrawToBitmap(bitmap, new Rectangle(Point.Empty, form.Size));
        bitmap.Save(Path.Combine(destination, fileName), ImageFormat.Png);
    }

    private static IEnumerable<Control> Descendants(Control parent)
    {
        foreach (Control child in parent.Controls)
        {
            yield return child;
            foreach (var descendant in Descendants(child)) yield return descendant;
        }
    }

    private static IEnumerable<Control> Ancestors(Control control)
    {
        for (var parent = control.Parent; parent is not null; parent = parent.Parent)
            yield return parent;
    }

    private static void OnSta(Action test)
    {
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            try
            {
                _ = ApplicationReady.Value;
                test();
            }
            catch (Exception exception) { failure = exception; }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(30)), "The Windows UI test did not complete in 30 seconds.");
        if (failure is not null) ExceptionDispatchInfo.Capture(failure).Throw();
    }
}
