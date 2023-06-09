using System.Diagnostics;

namespace V3KeyManager
{
	public partial class V3KeyManager : Form
	{
		ConfigFile CurrentConfig = new ConfigFile();
		ConfigFile OpenedConfig;

		public V3KeyManager()
		{
			InitializeComponent();
			Init();
		}

		private void Init()
		{
			VersionLabel.Parent = BackgroundImage;
			WindowModeLabel.Parent = BackgroundImage;
			WindowLocationLabel.Parent = BackgroundImage;
			WindowSizeLabel.Parent = BackgroundImage;
			RenderResolutionLabel.Parent = BackgroundImage;
			InputTypeLabel.Parent = BackgroundImage;
			MouseSpeedLabel.Parent = BackgroundImage;
			FXAALabel.Parent = BackgroundImage;
		}

		private void UpdateGUIStrings()
		{
			string window_mode = CurrentConfig.GetHR(CurrentConfig.Window.WindowMode);

			VersionLabel.Text = "Version: " + CurrentConfig.Version.GameVersion;
			WindowModeLabel.Text = "Window Mode: " + window_mode;
			WindowLocationLabel.Text = "Window Location: [" + CurrentConfig.Window.WindowTopX + ", " + CurrentConfig.Window.WindowTopY + "]";
			WindowSizeLabel.Text = "Window Size: " + CurrentConfig.Window.WindowSizeX + "x" + CurrentConfig.Window.WindowSizeY;
			RenderResolutionLabel.Text = "Render Resolution: " + (window_mode == "Fullscreen" ?
				CurrentConfig.Window.FullscreenRenderSizeX.ToString() + "x" + CurrentConfig.Window.FullscreenRenderSizeY.ToString()
				: CurrentConfig.Window.RenderSizeX.ToString() + "x" + CurrentConfig.Window.RenderSizeY.ToString());
			InputTypeLabel.Text = "Input: " + CurrentConfig.GetHR(CurrentConfig.Pad.PadHelpType);
			MouseSpeedLabel.Text = "Mouse Speed: " + CurrentConfig.PadString(CurrentConfig.MakeDouble(CurrentConfig.Mouse.MouseSpeed.ToString()), 8, '0');
			FXAALabel.Text = "FXAA: " + CurrentConfig.GetHR(CurrentConfig.Effect.FXAA);
		}

		private void UpdateGUIColors()
		{
			bool different_version = CurrentConfig.Version.GameVersion != OpenedConfig.Version.GameVersion;
			Debug.WriteLine(CurrentConfig.Version.GameVersion + " == " + OpenedConfig.Version.GameVersion);

			bool different_window_mode = CurrentConfig.Window.WindowMode != OpenedConfig.Window.WindowMode;
			bool different_window_top = CurrentConfig.Window.WindowTopX != OpenedConfig.Window.WindowTopX ||
							CurrentConfig.Window.WindowTopY != OpenedConfig.Window.WindowTopY;
			bool different_window_size = CurrentConfig.Window.WindowSizeX != OpenedConfig.Window.WindowSizeX ||
										CurrentConfig.Window.WindowSizeY != OpenedConfig.Window.WindowSizeY;
			bool different_render_size = CurrentConfig.Window.RenderSizeX != OpenedConfig.Window.RenderSizeX ||
										CurrentConfig.Window.RenderSizeY != OpenedConfig.Window.RenderSizeY;
			bool different_render_fs_size = CurrentConfig.Window.FullscreenRenderSizeX != OpenedConfig.Window.FullscreenRenderSizeX ||
										CurrentConfig.Window.FullscreenRenderSizeY != OpenedConfig.Window.FullscreenRenderSizeY;

			bool different_input_type = CurrentConfig.Pad.PadHelpType != OpenedConfig.Pad.PadHelpType;

			bool different_mouse_speed = CurrentConfig.Mouse.MouseSpeed != OpenedConfig.Mouse.MouseSpeed;

			bool different_fxaa = CurrentConfig.Effect.FXAA != OpenedConfig.Effect.FXAA;

			VersionLabel.ForeColor = different_version ? Color.HotPink : Color.Black;

			WindowModeLabel.ForeColor = different_window_mode ? Color.HotPink : Color.Black;
			WindowLocationLabel.ForeColor = different_window_top ? Color.HotPink : Color.Black;
			WindowSizeLabel.ForeColor = different_window_size ? Color.HotPink : Color.Black;
			RenderResolutionLabel.ForeColor = different_render_size || different_render_fs_size ? Color.HotPink : Color.Black;

			InputTypeLabel.ForeColor = different_input_type ? Color.HotPink : Color.Black;

			MouseSpeedLabel.ForeColor = different_mouse_speed ? Color.HotPink : Color.Black;

			FXAALabel.ForeColor = different_fxaa ? Color.HotPink : Color.Black;

			BackgroundImage.Refresh();
		}

		private void UpdateGUIFromConfig()
		{
			UpdateGUIStrings();
			UpdateGUIColors();
		}

		private void OpenConfigButton_Click(object sender, EventArgs e)
		{

			OpenFileDialog ofd = new OpenFileDialog();
			if (ofd.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			CurrentConfig = new ConfigFile();
			OpenedConfig = new ConfigFile();
			CurrentConfig.Load(ofd.FileName);
			OpenedConfig.Load(ofd.FileName);
			UpdateGUIFromConfig();
		}

		private void SaveConfigButton_Click(object sender, EventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog();
			if (sfd.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			CurrentConfig.Save(sfd.FileName);
		}

		private void EditVersionButton_Click(object sender, EventArgs e)
		{
			if (OpenedConfig == null)
			{
				return;
			}

			EditVersion editVersion = new EditVersion();
			DialogResult res = editVersion.ShowDialog();
			if (res == DialogResult.OK)
			{
				CurrentConfig.Version.GameVersion = editVersion.InsertVersionTextbox.Text;
			}
			editVersion.Close();
			editVersion.Dispose();
			UpdateGUIFromConfig();
		}

		private void OpenBackgroundImageButton_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "PNG files|*.png|JPEG files|*.jpg";
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				Bitmap b = new Bitmap(ofd.FileName);
				BackgroundImage.Image = b;
			}
		}
	}
}