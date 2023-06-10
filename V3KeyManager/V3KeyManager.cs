using System.Diagnostics;
using System.Security.Cryptography;

namespace V3KeyManager
{
	public partial class V3KeyManager : Form
	{
		Color DefaultTextColor = Color.Black;
		Color ChangedTextColor = Color.HotPink;

		ConfigFile CurrentConfig = new ConfigFile();
		ConfigFile OpenedConfig = null;

		public V3KeyManager()
		{
			InitializeComponent();
			Init();
		}

		private void Init()
		{
			CurrentFileNameLabel.Parent = BackgroundImage;
			VersionLabel.Parent = BackgroundImage;
			WindowModeLabel.Parent = BackgroundImage;
			WindowLocationLabel.Parent = BackgroundImage;
			WindowSizeLabel.Parent = BackgroundImage;
			RenderResolutionLabel.Parent = BackgroundImage;
			InputTypeLabel.Parent = BackgroundImage;
			MouseSpeedLabel.Parent = BackgroundImage;
			FXAALabel.Parent = BackgroundImage;

			CurrentFileNameLabel.ForeColor = DefaultTextColor;
			VersionLabel.ForeColor = DefaultTextColor;
			WindowModeLabel.ForeColor = DefaultTextColor;
			WindowLocationLabel.ForeColor = DefaultTextColor;
			WindowSizeLabel.ForeColor = DefaultTextColor;
			RenderResolutionLabel.ForeColor = DefaultTextColor;
			InputTypeLabel.ForeColor = DefaultTextColor;
			MouseSpeedLabel.ForeColor = DefaultTextColor;
			FXAALabel.ForeColor = DefaultTextColor;

			CurrentFileNameLabel.Text = "Current File: None";
		}

		private void UpdateGUIStrings()
		{
			string window_mode = ConfigFile.GetHR(CurrentConfig.Window.WindowMode, false);

			VersionLabel.Text = "Version: " + CurrentConfig.Version.GameVersion;
			WindowModeLabel.Text = "Window Mode: " + window_mode;
			WindowLocationLabel.Text = "Window Location: [" + CurrentConfig.Window.WindowTopX + ", " + CurrentConfig.Window.WindowTopY + "]";
			WindowSizeLabel.Text = "Window Size: " + CurrentConfig.Window.WindowSizeX + "x" + CurrentConfig.Window.WindowSizeY;
			RenderResolutionLabel.Text = "Render Resolution: " + (window_mode == "Fullscreen" ?
				CurrentConfig.Window.FullscreenRenderSizeX.ToString() + "x" + CurrentConfig.Window.FullscreenRenderSizeY.ToString()
				: CurrentConfig.Window.RenderSizeX.ToString() + "x" + CurrentConfig.Window.RenderSizeY.ToString());
			InputTypeLabel.Text = "Input: " + ConfigFile.GetHR(CurrentConfig.Pad.PadHelpType, false);
			MouseSpeedLabel.Text = "Mouse Speed: " + ConfigFile.PadString(ConfigFile.MakeDouble(CurrentConfig.Mouse.MouseSpeed.ToString()), 8, '0');
			FXAALabel.Text = "FXAA: " + ConfigFile.GetHR(CurrentConfig.Effect.FXAA, false);
		}

		private void UpdateGUIColors()
		{
			bool different_version = CurrentConfig.Version.GameVersion != OpenedConfig.Version.GameVersion;
			Debug.WriteLine("(1)" + CurrentConfig.Version.GameVersion + " == " + OpenedConfig.Version.GameVersion);

			bool different_window_mode = CurrentConfig.Window.WindowMode != OpenedConfig.Window.WindowMode;
			bool different_window_top = CurrentConfig.Window.WindowTopX != OpenedConfig.Window.WindowTopX ||
							CurrentConfig.Window.WindowTopY != OpenedConfig.Window.WindowTopY;
			bool different_window_size = CurrentConfig.Window.WindowSizeX != OpenedConfig.Window.WindowSizeX ||
										CurrentConfig.Window.WindowSizeY != OpenedConfig.Window.WindowSizeY;
			Debug.WriteLine("(2)" + CurrentConfig.Window.WindowSizeX + "x" + CurrentConfig.Window.WindowSizeY +
							" != " + OpenedConfig.Window.WindowSizeX + "x" + OpenedConfig.Window.WindowSizeY);
			bool different_render_size = CurrentConfig.Window.RenderSizeX != OpenedConfig.Window.RenderSizeX ||
										CurrentConfig.Window.RenderSizeY != OpenedConfig.Window.RenderSizeY;
			Debug.WriteLine("(3)" + CurrentConfig.Window.RenderSizeX + "x" + CurrentConfig.Window.RenderSizeY +
				" != " + OpenedConfig.Window.RenderSizeX + "x" + OpenedConfig.Window.RenderSizeY);
			bool different_render_fs_size = CurrentConfig.Window.FullscreenRenderSizeX != OpenedConfig.Window.FullscreenRenderSizeX ||
										CurrentConfig.Window.FullscreenRenderSizeY != OpenedConfig.Window.FullscreenRenderSizeY;

			bool different_input_type = CurrentConfig.Pad.PadHelpType != OpenedConfig.Pad.PadHelpType;

			bool different_mouse_speed = CurrentConfig.Mouse.MouseSpeed != OpenedConfig.Mouse.MouseSpeed;

			bool different_fxaa = CurrentConfig.Effect.FXAA != OpenedConfig.Effect.FXAA;

			VersionLabel.ForeColor = different_version ? ChangedTextColor : DefaultTextColor;

			WindowModeLabel.ForeColor = different_window_mode ? ChangedTextColor : DefaultTextColor;
			WindowLocationLabel.ForeColor = different_window_top ? ChangedTextColor : DefaultTextColor;
			WindowSizeLabel.ForeColor = different_window_size ? ChangedTextColor : DefaultTextColor;
			RenderResolutionLabel.ForeColor = different_render_size || different_render_fs_size ? ChangedTextColor : DefaultTextColor;

			InputTypeLabel.ForeColor = different_input_type ? ChangedTextColor : DefaultTextColor;

			MouseSpeedLabel.ForeColor = different_mouse_speed ? ChangedTextColor : DefaultTextColor;

			FXAALabel.ForeColor = different_fxaa ? ChangedTextColor : DefaultTextColor;

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
			ofd.Filter = "Text Files|*.txt|All files|*.*";
			if (ofd.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			string CurrentConfigFile = ofd.FileName;
			CurrentConfig = new ConfigFile();
			bool load_current = CurrentConfig.Load(CurrentConfigFile);
			if (load_current)
			{
				CurrentFileNameLabel.Text = "Current File: " + CurrentConfig.Filename;
				OpenedConfig = new ConfigFile();
				bool load_opened = OpenedConfig.Load(CurrentConfigFile);
				if (load_opened) // Should not matter, but just in case
				{
					UpdateGUIFromConfig();
				}
			}
		}

		private void SaveConfigButton_Click(object sender, EventArgs e)
		{
			if (OpenedConfig == null)
			{
				return;
			}

			SaveFileDialog sfd = new SaveFileDialog();
			if (sfd.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			CurrentConfig.Save(sfd.FileName);
		}

		private void OpenBackgroundImageButton_Click(object sender, EventArgs e)
		{

			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "PNG files|*.png|JPEG files|*.jpg|All files|*.*";
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				try
				{
					Bitmap b = new Bitmap(ofd.FileName);
					if (b != null && b.Width > 0 && b.Height > 0)
					{
						BackgroundImage.Image = b;
					}
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.Message);
				}
			}
		}

		private void ResetChangesButton_Click(object sender, EventArgs e)
		{
			if (OpenedConfig == null)
			{
				return;
			}

			CurrentConfig.Load(CurrentConfig.FullFilename);
			UpdateGUIFromConfig();
		}

		private void LaunchGameButton_Click(object sender, EventArgs e)
		{
			if (OpenedConfig == null)
			{
				return;
			}

			bool xbox_version = false;
			string config_loc = Directory.GetParent(CurrentConfig.FullFilename).FullName;
			string xbox_version_file = "Dangan3Desktop.exe";
			string steam_version_file = "Dangan3Win.exe";
			string xbox_config = Path.Combine(config_loc, "config.txt");
			string xbox_path = Path.Combine(config_loc, xbox_version_file);
			string steam_config = Path.Combine(config_loc, "config.txt");
			string steam_path = Path.Combine(config_loc, steam_version_file);

			if (File.Exists(xbox_path) && File.Exists(steam_path))
			{
				Debug.WriteLine("(1) " + xbox_path + " / " + steam_path);
				return;
			}

			if (!File.Exists(xbox_path) && !File.Exists(steam_path))
			{
				Debug.WriteLine("(2) " + xbox_path + " / " + steam_path);
				return;
			}

			if (File.Exists(xbox_path))
			{
				xbox_version = true;
			}

			if (xbox_version)
			{
				CurrentConfig.Save(xbox_config);
				Debug.WriteLine(xbox_path);
				Process.Start(xbox_path, "");
			}
			else
			{
				CurrentConfig.Save(steam_config);
				Debug.WriteLine(steam_path);
				Process.Start(steam_path, "");
			}
		}

		private void CreateDefaultConfigFile_Click(object sender, EventArgs e)
		{

			ConfigFile default_config = new ConfigFile();
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				default_config.Save(saveFileDialog.FileName);
			}
		}

		private void EditVersionButton_Click(object sender, EventArgs e)
		{

			if (OpenedConfig == null)
			{
				return;
			}

			EditVersion editVersion = new EditVersion();

			editVersion.CurrentVersionLabel.Text = "Current Version: " + CurrentConfig.Version.GameVersion;

			editVersion.InsertVersionTextbox.Text = CurrentConfig.Version.GameVersion;

			DialogResult res = editVersion.ShowDialog();
			if (res == DialogResult.OK)
			{
				CurrentConfig.Version.GameVersion = editVersion.InsertVersionTextbox.Text;
			}
			editVersion.Close();
			editVersion.Dispose();
			UpdateGUIFromConfig();
		}

		private void EditWindowSettingsButton_Click(object sender, EventArgs e)
		{

			if (OpenedConfig == null)
			{
				return;
			}

			string window_mode = ConfigFile.GetHR(CurrentConfig.Window.WindowMode, false);

			EditWindowSettings editWindowSettings = new EditWindowSettings();

			editWindowSettings.CurrentWindowModeLabel.Text = "Current Window Mode: " + window_mode;
			editWindowSettings.CurrentWindowLocationLabel.Text = "Current Window Location: [" + CurrentConfig.Window.WindowTopX.ToString() + ", " + CurrentConfig.Window.WindowTopY.ToString() + "]";
			editWindowSettings.CurrentWindowSizeLabel.Text = "Current Window Size: " + CurrentConfig.Window.WindowSizeX.ToString() + "x" + CurrentConfig.Window.WindowSizeY.ToString();
			editWindowSettings.CurrentRenderSizeLabel.Text = "Current Render Size: " + CurrentConfig.Window.RenderSizeX.ToString() + "x" + CurrentConfig.Window.RenderSizeY.ToString();
			editWindowSettings.CurrentFullscreenRenderSizeLabel.Text = "Current Fullscreen Render Size: " + CurrentConfig.Window.FullscreenRenderSizeX.ToString() + "x" + CurrentConfig.Window.FullscreenRenderSizeY.ToString();

			editWindowSettings.NewWindowModeComboBox.SelectedIndex = editWindowSettings.NewWindowModeComboBox.FindStringExact(window_mode);
			editWindowSettings.NewWindowLocationX.Text = CurrentConfig.Window.WindowTopX.ToString();
			editWindowSettings.NewWindowLocationY.Text = CurrentConfig.Window.WindowTopY.ToString();
			editWindowSettings.NewWindowSizeX.Text = CurrentConfig.Window.WindowSizeX.ToString();
			editWindowSettings.NewWindowSizeY.Text = CurrentConfig.Window.WindowSizeY.ToString();
			editWindowSettings.NewRenderSizeX.Text = CurrentConfig.Window.RenderSizeX.ToString();
			editWindowSettings.NewRenderSizeY.Text = CurrentConfig.Window.RenderSizeY.ToString();
			editWindowSettings.NewFullscreenRenderSizeX.Text = CurrentConfig.Window.FullscreenRenderSizeX.ToString();
			editWindowSettings.NewFullscreenRenderSizeY.Text = CurrentConfig.Window.FullscreenRenderSizeY.ToString();

			if (editWindowSettings.NewWindowModeComboBox.SelectedIndex == -1)
			{
				Debug.WriteLine("Invalid Window Settings combobox index!");
			}

			DialogResult res = editWindowSettings.ShowDialog();
			if (res == DialogResult.OK)
			{
				CurrentConfig.Window.WindowMode = editWindowSettings.NewWindowModeComboBox.Text.ToLower();
				CurrentConfig.Window.WindowTopX = int.Parse(editWindowSettings.NewWindowLocationX.Text);
				CurrentConfig.Window.WindowTopY = int.Parse(editWindowSettings.NewWindowLocationY.Text);
				CurrentConfig.Window.WindowSizeX = int.Parse(editWindowSettings.NewWindowSizeX.Text);
				CurrentConfig.Window.WindowSizeY = int.Parse(editWindowSettings.NewWindowSizeY.Text);
				CurrentConfig.Window.RenderSizeX = int.Parse(editWindowSettings.NewRenderSizeX.Text);
				CurrentConfig.Window.RenderSizeY = int.Parse(editWindowSettings.NewRenderSizeY.Text);
				CurrentConfig.Window.FullscreenRenderSizeX = int.Parse(editWindowSettings.NewFullscreenRenderSizeX.Text);
				CurrentConfig.Window.FullscreenRenderSizeY = int.Parse(editWindowSettings.NewFullscreenRenderSizeY.Text);
			}
			editWindowSettings.Close();
			editWindowSettings.Dispose();
			UpdateGUIFromConfig();
		}

		private void EditPadSettingsButton_Click(object sender, EventArgs e)
		{

			if (OpenedConfig == null)
			{
				return;
			}

			string input_type = ConfigFile.GetHR(CurrentConfig.Pad.PadHelpType, false);

			EditPadSettings editPadSettings = new EditPadSettings();

			editPadSettings.CurrentInputTypeLabel.Text = "Current Input Type: " + input_type;

			editPadSettings.InputTypeComboBox.SelectedIndex = editPadSettings.InputTypeComboBox.FindStringExact(input_type);

			if (editPadSettings.InputTypeComboBox.SelectedIndex == -1)
			{
				Debug.WriteLine("Invalid Pad Settings combobox index!");
			}

			DialogResult res = editPadSettings.ShowDialog();
			if (res == DialogResult.OK)
			{
				CurrentConfig.Pad.PadHelpType = ConfigFile.GetHR(editPadSettings.InputTypeComboBox.Text, true);
			}
			editPadSettings.Close();
			editPadSettings.Dispose();
			UpdateGUIFromConfig();
		}

		private void EditMouseSettingsButton_Click(object sender, EventArgs e)
		{

			if (OpenedConfig == null)
			{
				return;
			}

			string current_speed = ConfigFile.PadString(ConfigFile.MakeDouble(CurrentConfig.Mouse.MouseSpeed.ToString()), 8, '0');

			EditMouseSettings editMouseSettings = new EditMouseSettings();

			editMouseSettings.CurrentMouseSpeedLabel.Text = "Current Mouse Speed: " + current_speed;

			editMouseSettings.NewMouseSpeedTextbox.Text = current_speed;

			DialogResult res = editMouseSettings.ShowDialog();
			if (res == DialogResult.OK)
			{
				CurrentConfig.Mouse.MouseSpeed = double.Parse(editMouseSettings.NewMouseSpeedTextbox.Text);
			}
			editMouseSettings.Close();
			editMouseSettings.Dispose();
			UpdateGUIFromConfig();
		}

		private void EditEffectSettingsButton_Click(object sender, EventArgs e)
		{

			if (OpenedConfig == null)
			{
				return;
			}

			string current_fxaa = ConfigFile.GetHR(CurrentConfig.Effect.FXAA, false);

			EditEffectSettings editEffectSettings = new EditEffectSettings();

			editEffectSettings.CurrentFXAALabel.Text = "Current FXAA: " + current_fxaa;

			editEffectSettings.ActivateFXAACheckbox.Checked = current_fxaa == "On";

			DialogResult res = editEffectSettings.ShowDialog();
			if (res == DialogResult.OK)
			{
				CurrentConfig.Effect.FXAA = ConfigFile.GetHR(editEffectSettings.ActivateFXAACheckbox.Checked ? "On" : "Off", true);
			}
			editEffectSettings.Close();
			editEffectSettings.Dispose();
			UpdateGUIFromConfig();
		}

		private void EditKeyboardSettingsButton_Click(object sender, EventArgs e)
		{

			if (OpenedConfig == null)
			{
				return;
			}

			bool fill_entries = true;

			DialogResult res = DialogResult.Cancel;
			EditKeyboardSettings editKeyboardSettings = new EditKeyboardSettings();
			do
			{
				editKeyboardSettings.ResetLabels();
				editKeyboardSettings.ResetTextboxes();
				editKeyboardSettings.PadUpKey.LoadKey("Pad Up", CurrentConfig.Keyboard.PadUp, fill_entries);
				editKeyboardSettings.PadDownKey.LoadKey("Pad Down", CurrentConfig.Keyboard.PadDown, fill_entries);
				editKeyboardSettings.PadLeftKey.LoadKey("Pad Left", CurrentConfig.Keyboard.PadLeft, fill_entries);
				editKeyboardSettings.PadRightKey.LoadKey("Pad Right", CurrentConfig.Keyboard.PadRight, fill_entries);
				editKeyboardSettings.PadR1Key.LoadKey("Pad R1", CurrentConfig.Keyboard.PadR1, fill_entries);
				editKeyboardSettings.PadR2Key.LoadKey("Pad R2", CurrentConfig.Keyboard.PadR2, fill_entries);
				editKeyboardSettings.PadR3Key.LoadKey("Pad R3", CurrentConfig.Keyboard.PadR3, fill_entries);
				editKeyboardSettings.PadL1Key.LoadKey("Pad L1", CurrentConfig.Keyboard.PadL1, fill_entries);
				editKeyboardSettings.PadL2Key.LoadKey("Pad L2", CurrentConfig.Keyboard.PadL2, fill_entries);
				editKeyboardSettings.PadL3Key.LoadKey("Pad L3", CurrentConfig.Keyboard.PadL3, fill_entries);
				editKeyboardSettings.PadCrossKey.LoadKey("Pad Cross", CurrentConfig.Keyboard.PadCross, fill_entries);
				editKeyboardSettings.PadCircleKey.LoadKey("Pad Circle", CurrentConfig.Keyboard.PadCircle, fill_entries);
				editKeyboardSettings.PadSquareKey.LoadKey("Pad Square", CurrentConfig.Keyboard.PadSquare, fill_entries);
				editKeyboardSettings.PadTriangleKey.LoadKey("Pad Triangle", CurrentConfig.Keyboard.PadTriangle, fill_entries);
				editKeyboardSettings.PadStartKey.LoadKey("Pad Start", CurrentConfig.Keyboard.PadStart, fill_entries);
				editKeyboardSettings.PadSelectKey.LoadKey("Pad Select", CurrentConfig.Keyboard.PadSelect, fill_entries);
				editKeyboardSettings.PadAnyKey.LoadKey("Pad Any", CurrentConfig.Keyboard.PadAny, fill_entries);
				editKeyboardSettings.MoveFrontKey.LoadKey("Move Front", CurrentConfig.Keyboard.MoveFront, fill_entries);
				editKeyboardSettings.MoveBackKey.LoadKey("Move Back", CurrentConfig.Keyboard.MoveBack, fill_entries);
				editKeyboardSettings.MoveLeftKey.LoadKey("Move Left", CurrentConfig.Keyboard.MoveLeft, fill_entries);
				editKeyboardSettings.MoveRightKey.LoadKey("Move Right", CurrentConfig.Keyboard.MoveRight, fill_entries);
				editKeyboardSettings.MoveCameraUpKey.LoadKey("Camera Up", CurrentConfig.Keyboard.MoveCameraUp, fill_entries);
				editKeyboardSettings.MoveCameraDownKey.LoadKey("Camera Down", CurrentConfig.Keyboard.MoveCameraDown, fill_entries);
				editKeyboardSettings.MoveCameraLeftKey.LoadKey("Camera Left", CurrentConfig.Keyboard.MoveCameraLeft, fill_entries);
				editKeyboardSettings.MoveCameraRightKey.LoadKey("Camera Right", CurrentConfig.Keyboard.MoveCameraRight, fill_entries);
				editKeyboardSettings.MoveCameraLeftDRKey.LoadKey("Camera Left DR", CurrentConfig.Keyboard.MoveCameraLeftDR, fill_entries);
				editKeyboardSettings.MoveCameraRightDRKey.LoadKey("Camera Right DR", CurrentConfig.Keyboard.MoveCameraRightDR, fill_entries);
				editKeyboardSettings.MoveDashKey.LoadKey("Move Dash", CurrentConfig.Keyboard.MoveDash, fill_entries);
				editKeyboardSettings.MessageSkipKey.LoadKey("Message Skip", CurrentConfig.Keyboard.MessageSkip, fill_entries);
				editKeyboardSettings.PushWKey.LoadKey("Push W", CurrentConfig.Keyboard.PushW, fill_entries);
				editKeyboardSettings.PushSKey.LoadKey("Push S", CurrentConfig.Keyboard.PushS, fill_entries);
				editKeyboardSettings.PushAKey.LoadKey("Push A", CurrentConfig.Keyboard.PushA, fill_entries);
				editKeyboardSettings.PushDKey.LoadKey("Push D", CurrentConfig.Keyboard.PushD, fill_entries);
				editKeyboardSettings.MouseModeKey.LoadKey("Mouse Mode", CurrentConfig.Keyboard.MouseMode, fill_entries);
				editKeyboardSettings.ReturnButtonKey.LoadKey("Return Button", CurrentConfig.Keyboard.ReturnButton, fill_entries);
				res = editKeyboardSettings.ShowDialog();
			} while (res == DialogResult.No);
			if (res == DialogResult.OK)
			{
				CurrentConfig.Keyboard.PadUp = editKeyboardSettings.PadUpKey.ToStringList();
				CurrentConfig.Keyboard.PadDown = editKeyboardSettings.PadDownKey.ToStringList();
				CurrentConfig.Keyboard.PadLeft = editKeyboardSettings.PadLeftKey.ToStringList();
				CurrentConfig.Keyboard.PadRight = editKeyboardSettings.PadRightKey.ToStringList();
				CurrentConfig.Keyboard.PadR1 = editKeyboardSettings.PadR1Key.ToStringList();
				CurrentConfig.Keyboard.PadR2 = editKeyboardSettings.PadR2Key.ToStringList();
				CurrentConfig.Keyboard.PadR3 = editKeyboardSettings.PadR3Key.ToStringList();
				CurrentConfig.Keyboard.PadL1 = editKeyboardSettings.PadL1Key.ToStringList();
				CurrentConfig.Keyboard.PadL2 = editKeyboardSettings.PadL2Key.ToStringList();
				CurrentConfig.Keyboard.PadL3 = editKeyboardSettings.PadL3Key.ToStringList();
				CurrentConfig.Keyboard.PadCross = editKeyboardSettings.PadCrossKey.ToStringList();
				CurrentConfig.Keyboard.PadCircle = editKeyboardSettings.PadCircleKey.ToStringList();
				CurrentConfig.Keyboard.PadSquare = editKeyboardSettings.PadSquareKey.ToStringList();
				CurrentConfig.Keyboard.PadTriangle = editKeyboardSettings.PadTriangleKey.ToStringList();
				CurrentConfig.Keyboard.PadStart = editKeyboardSettings.PadStartKey.ToStringList();
				CurrentConfig.Keyboard.PadSelect = editKeyboardSettings.PadSelectKey.ToStringList();
				CurrentConfig.Keyboard.PadAny = editKeyboardSettings.PadAnyKey.ToStringList();
				CurrentConfig.Keyboard.MoveFront = editKeyboardSettings.MoveFrontKey.ToStringList();
				CurrentConfig.Keyboard.MoveBack = editKeyboardSettings.MoveBackKey.ToStringList();
				CurrentConfig.Keyboard.MoveLeft = editKeyboardSettings.MoveLeftKey.ToStringList();
				CurrentConfig.Keyboard.MoveRight = editKeyboardSettings.MoveRightKey.ToStringList();
				CurrentConfig.Keyboard.MoveCameraUp = editKeyboardSettings.MoveCameraUpKey.ToStringList();
				CurrentConfig.Keyboard.MoveCameraDown = editKeyboardSettings.MoveCameraDownKey.ToStringList();
				CurrentConfig.Keyboard.MoveCameraLeft = editKeyboardSettings.MoveCameraLeftKey.ToStringList();
				CurrentConfig.Keyboard.MoveCameraRight = editKeyboardSettings.MoveCameraRightKey.ToStringList();
				CurrentConfig.Keyboard.MoveCameraLeftDR = editKeyboardSettings.MoveCameraLeftDRKey.ToStringList();
				CurrentConfig.Keyboard.MoveCameraRightDR = editKeyboardSettings.MoveCameraRightDRKey.ToStringList();
				CurrentConfig.Keyboard.MoveDash = editKeyboardSettings.MoveDashKey.ToStringList();
				CurrentConfig.Keyboard.MessageSkip = editKeyboardSettings.MessageSkipKey.ToStringList();
				CurrentConfig.Keyboard.PushW = editKeyboardSettings.PushWKey.ToStringList();
				CurrentConfig.Keyboard.PushS = editKeyboardSettings.PushSKey.ToStringList();
				CurrentConfig.Keyboard.PushA = editKeyboardSettings.PushAKey.ToStringList();
				CurrentConfig.Keyboard.PushD = editKeyboardSettings.PushDKey.ToStringList();
				CurrentConfig.Keyboard.MouseMode = editKeyboardSettings.MouseModeKey.ToStringList();
				CurrentConfig.Keyboard.ReturnButton = editKeyboardSettings.ReturnButtonKey.ToStringList();
			}
			editKeyboardSettings.Close();
			editKeyboardSettings.Dispose();
			UpdateGUIFromConfig();
		}
	}
}