namespace V3KeyManager
{
	partial class V3KeyManager
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			OpenConfigButton = new Button();
			SaveConfigButton = new Button();
			EditVersionButton = new Button();
			EditWindowSettingsButton = new Button();
			EditPadSettingsButton = new Button();
			EditMouseSettingsButton = new Button();
			EditEffectSettingsButton = new Button();
			EditKeyboardSettingsButton = new Button();
			ResetChangesButton = new Button();
			LaunchGameButton = new Button();
			BackgroundImage = new PictureBox();
			VersionLabel = new Label();
			InputTypeLabel = new Label();
			WindowModeLabel = new Label();
			RenderResolutionLabel = new Label();
			MouseSpeedLabel = new Label();
			FXAALabel = new Label();
			WindowLocationLabel = new Label();
			WindowSizeLabel = new Label();
			OpenBackgroundImageButton = new Button();
			CreateDefaultConfigFile = new Button();
			((System.ComponentModel.ISupportInitialize)BackgroundImage).BeginInit();
			SuspendLayout();
			// 
			// OpenConfigButton
			// 
			OpenConfigButton.Location = new Point(265, 383);
			OpenConfigButton.Name = "OpenConfigButton";
			OpenConfigButton.Size = new Size(120, 58);
			OpenConfigButton.TabIndex = 0;
			OpenConfigButton.Text = "Open Config File";
			OpenConfigButton.UseVisualStyleBackColor = true;
			OpenConfigButton.Click += OpenConfigButton_Click;
			// 
			// SaveConfigButton
			// 
			SaveConfigButton.Location = new Point(391, 383);
			SaveConfigButton.Name = "SaveConfigButton";
			SaveConfigButton.Size = new Size(120, 58);
			SaveConfigButton.TabIndex = 1;
			SaveConfigButton.Text = "Save Config As...";
			SaveConfigButton.UseVisualStyleBackColor = true;
			SaveConfigButton.Click += SaveConfigButton_Click;
			// 
			// EditVersionButton
			// 
			EditVersionButton.Location = new Point(13, 317);
			EditVersionButton.Name = "EditVersionButton";
			EditVersionButton.Size = new Size(120, 60);
			EditVersionButton.TabIndex = 2;
			EditVersionButton.Text = "Edit Version";
			EditVersionButton.UseVisualStyleBackColor = true;
			EditVersionButton.Click += EditVersionButton_Click;
			// 
			// EditWindowSettingsButton
			// 
			EditWindowSettingsButton.Location = new Point(139, 317);
			EditWindowSettingsButton.Name = "EditWindowSettingsButton";
			EditWindowSettingsButton.Size = new Size(120, 60);
			EditWindowSettingsButton.TabIndex = 3;
			EditWindowSettingsButton.Text = "Edit Window Settings";
			EditWindowSettingsButton.UseVisualStyleBackColor = true;
			EditWindowSettingsButton.Click += EditWindowSettingsButton_Click;
			// 
			// EditPadSettingsButton
			// 
			EditPadSettingsButton.Location = new Point(265, 317);
			EditPadSettingsButton.Name = "EditPadSettingsButton";
			EditPadSettingsButton.Size = new Size(120, 60);
			EditPadSettingsButton.TabIndex = 4;
			EditPadSettingsButton.Text = "Edit Pad Settings";
			EditPadSettingsButton.UseVisualStyleBackColor = true;
			EditPadSettingsButton.Click += EditPadSettingsButton_Click;
			// 
			// EditMouseSettingsButton
			// 
			EditMouseSettingsButton.Location = new Point(391, 317);
			EditMouseSettingsButton.Name = "EditMouseSettingsButton";
			EditMouseSettingsButton.Size = new Size(120, 60);
			EditMouseSettingsButton.TabIndex = 5;
			EditMouseSettingsButton.Text = "Edit Mouse Settings";
			EditMouseSettingsButton.UseVisualStyleBackColor = true;
			EditMouseSettingsButton.Click += EditMouseSettingsButton_Click;
			// 
			// EditEffectSettingsButton
			// 
			EditEffectSettingsButton.Location = new Point(517, 317);
			EditEffectSettingsButton.Name = "EditEffectSettingsButton";
			EditEffectSettingsButton.Size = new Size(120, 60);
			EditEffectSettingsButton.TabIndex = 6;
			EditEffectSettingsButton.Text = "Edit Effect Settings";
			EditEffectSettingsButton.UseVisualStyleBackColor = true;
			EditEffectSettingsButton.Click += EditEffectSettingsButton_Click;
			// 
			// EditKeyboardSettingsButton
			// 
			EditKeyboardSettingsButton.Location = new Point(643, 317);
			EditKeyboardSettingsButton.Name = "EditKeyboardSettingsButton";
			EditKeyboardSettingsButton.Size = new Size(120, 60);
			EditKeyboardSettingsButton.TabIndex = 7;
			EditKeyboardSettingsButton.Text = "Edit Keyboard Settings";
			EditKeyboardSettingsButton.UseVisualStyleBackColor = true;
			// 
			// ResetChangesButton
			// 
			ResetChangesButton.Location = new Point(517, 383);
			ResetChangesButton.Name = "ResetChangesButton";
			ResetChangesButton.Size = new Size(120, 58);
			ResetChangesButton.TabIndex = 8;
			ResetChangesButton.Text = "Reset Changes";
			ResetChangesButton.UseVisualStyleBackColor = true;
			ResetChangesButton.Click += ResetChangesButton_Click;
			// 
			// LaunchGameButton
			// 
			LaunchGameButton.Location = new Point(643, 383);
			LaunchGameButton.Name = "LaunchGameButton";
			LaunchGameButton.Size = new Size(120, 58);
			LaunchGameButton.TabIndex = 9;
			LaunchGameButton.Text = "Launch Game";
			LaunchGameButton.UseVisualStyleBackColor = true;
			LaunchGameButton.Click += LaunchGameButton_Click;
			// 
			// BackgroundImage
			// 
			BackgroundImage.Dock = DockStyle.Top;
			BackgroundImage.Location = new Point(0, 0);
			BackgroundImage.Name = "BackgroundImage";
			BackgroundImage.Size = new Size(775, 299);
			BackgroundImage.TabIndex = 10;
			BackgroundImage.TabStop = false;
			// 
			// VersionLabel
			// 
			VersionLabel.AutoSize = true;
			VersionLabel.BackColor = Color.Transparent;
			VersionLabel.FlatStyle = FlatStyle.Flat;
			VersionLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
			VersionLabel.ImageAlign = ContentAlignment.MiddleLeft;
			VersionLabel.Location = new Point(12, 11);
			VersionLabel.Name = "VersionLabel";
			VersionLabel.Size = new Size(84, 25);
			VersionLabel.TabIndex = 11;
			VersionLabel.Text = "Version:";
			// 
			// InputTypeLabel
			// 
			InputTypeLabel.AutoSize = true;
			InputTypeLabel.BackColor = Color.Transparent;
			InputTypeLabel.FlatStyle = FlatStyle.Flat;
			InputTypeLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
			InputTypeLabel.ImageAlign = ContentAlignment.MiddleLeft;
			InputTypeLabel.Location = new Point(12, 136);
			InputTypeLabel.Name = "InputTypeLabel";
			InputTypeLabel.Size = new Size(66, 25);
			InputTypeLabel.TabIndex = 12;
			InputTypeLabel.Text = "Input:";
			// 
			// WindowModeLabel
			// 
			WindowModeLabel.AutoSize = true;
			WindowModeLabel.BackColor = Color.Transparent;
			WindowModeLabel.FlatStyle = FlatStyle.Flat;
			WindowModeLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
			WindowModeLabel.ImageAlign = ContentAlignment.MiddleLeft;
			WindowModeLabel.Location = new Point(12, 36);
			WindowModeLabel.Name = "WindowModeLabel";
			WindowModeLabel.Size = new Size(149, 25);
			WindowModeLabel.TabIndex = 13;
			WindowModeLabel.Text = "Window Mode:";
			// 
			// RenderResolutionLabel
			// 
			RenderResolutionLabel.AutoSize = true;
			RenderResolutionLabel.BackColor = Color.Transparent;
			RenderResolutionLabel.FlatStyle = FlatStyle.Flat;
			RenderResolutionLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
			RenderResolutionLabel.ImageAlign = ContentAlignment.MiddleLeft;
			RenderResolutionLabel.Location = new Point(12, 111);
			RenderResolutionLabel.Name = "RenderResolutionLabel";
			RenderResolutionLabel.Size = new Size(181, 25);
			RenderResolutionLabel.TabIndex = 14;
			RenderResolutionLabel.Text = "Render Resolution:";
			// 
			// MouseSpeedLabel
			// 
			MouseSpeedLabel.AutoSize = true;
			MouseSpeedLabel.BackColor = Color.Transparent;
			MouseSpeedLabel.FlatStyle = FlatStyle.Flat;
			MouseSpeedLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
			MouseSpeedLabel.ImageAlign = ContentAlignment.MiddleLeft;
			MouseSpeedLabel.Location = new Point(12, 161);
			MouseSpeedLabel.Name = "MouseSpeedLabel";
			MouseSpeedLabel.Size = new Size(137, 25);
			MouseSpeedLabel.TabIndex = 15;
			MouseSpeedLabel.Text = "Mouse Speed:";
			// 
			// FXAALabel
			// 
			FXAALabel.AutoSize = true;
			FXAALabel.BackColor = Color.Transparent;
			FXAALabel.FlatStyle = FlatStyle.Flat;
			FXAALabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
			FXAALabel.ImageAlign = ContentAlignment.MiddleLeft;
			FXAALabel.Location = new Point(12, 186);
			FXAALabel.Name = "FXAALabel";
			FXAALabel.Size = new Size(65, 25);
			FXAALabel.TabIndex = 16;
			FXAALabel.Text = "FXAA:";
			// 
			// WindowLocationLabel
			// 
			WindowLocationLabel.AutoSize = true;
			WindowLocationLabel.BackColor = Color.Transparent;
			WindowLocationLabel.FlatStyle = FlatStyle.Flat;
			WindowLocationLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
			WindowLocationLabel.ImageAlign = ContentAlignment.MiddleLeft;
			WindowLocationLabel.Location = new Point(12, 61);
			WindowLocationLabel.Name = "WindowLocationLabel";
			WindowLocationLabel.Size = new Size(174, 25);
			WindowLocationLabel.TabIndex = 17;
			WindowLocationLabel.Text = "Window Location:";
			// 
			// WindowSizeLabel
			// 
			WindowSizeLabel.AutoSize = true;
			WindowSizeLabel.BackColor = Color.Transparent;
			WindowSizeLabel.FlatStyle = FlatStyle.Flat;
			WindowSizeLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
			WindowSizeLabel.ImageAlign = ContentAlignment.MiddleLeft;
			WindowSizeLabel.Location = new Point(12, 86);
			WindowSizeLabel.Name = "WindowSizeLabel";
			WindowSizeLabel.Size = new Size(132, 25);
			WindowSizeLabel.TabIndex = 18;
			WindowSizeLabel.Text = "Window Size:";
			// 
			// OpenBackgroundImageButton
			// 
			OpenBackgroundImageButton.Location = new Point(12, 383);
			OpenBackgroundImageButton.Name = "OpenBackgroundImageButton";
			OpenBackgroundImageButton.Size = new Size(120, 58);
			OpenBackgroundImageButton.TabIndex = 19;
			OpenBackgroundImageButton.Text = "Open Background Image";
			OpenBackgroundImageButton.UseVisualStyleBackColor = true;
			OpenBackgroundImageButton.Click += OpenBackgroundImageButton_Click;
			// 
			// CreateDefaultConfigFile
			// 
			CreateDefaultConfigFile.Location = new Point(138, 383);
			CreateDefaultConfigFile.Name = "CreateDefaultConfigFile";
			CreateDefaultConfigFile.Size = new Size(120, 58);
			CreateDefaultConfigFile.TabIndex = 20;
			CreateDefaultConfigFile.Text = "Create Default Config File";
			CreateDefaultConfigFile.UseVisualStyleBackColor = true;
			CreateDefaultConfigFile.Click += CreateDefaultConfigFile_Click;
			// 
			// V3KeyManager
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(775, 450);
			Controls.Add(CreateDefaultConfigFile);
			Controls.Add(OpenBackgroundImageButton);
			Controls.Add(WindowSizeLabel);
			Controls.Add(WindowLocationLabel);
			Controls.Add(FXAALabel);
			Controls.Add(MouseSpeedLabel);
			Controls.Add(RenderResolutionLabel);
			Controls.Add(WindowModeLabel);
			Controls.Add(InputTypeLabel);
			Controls.Add(VersionLabel);
			Controls.Add(BackgroundImage);
			Controls.Add(LaunchGameButton);
			Controls.Add(ResetChangesButton);
			Controls.Add(EditKeyboardSettingsButton);
			Controls.Add(EditEffectSettingsButton);
			Controls.Add(EditMouseSettingsButton);
			Controls.Add(EditPadSettingsButton);
			Controls.Add(EditWindowSettingsButton);
			Controls.Add(EditVersionButton);
			Controls.Add(SaveConfigButton);
			Controls.Add(OpenConfigButton);
			Name = "V3KeyManager";
			Text = "V3KeyManager";
			((System.ComponentModel.ISupportInitialize)BackgroundImage).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button OpenConfigButton;
		private Button SaveConfigButton;
		private Button EditVersionButton;
		private Button EditWindowSettingsButton;
		private Button EditPadSettingsButton;
		private Button EditMouseSettingsButton;
		private Button EditEffectSettingsButton;
		private Button EditKeyboardSettingsButton;
		private Button ResetChangesButton;
		private Button LaunchGameButton;
		private PictureBox BackgroundImage;
		private Label VersionLabel;
		private Label InputTypeLabel;
		private Label WindowModeLabel;
		private Label RenderResolutionLabel;
		private Label MouseSpeedLabel;
		private Label FXAALabel;
		private Label WindowLocationLabel;
		private Label WindowSizeLabel;
		private Button OpenBackgroundImageButton;
		private Button CreateDefaultConfigFile;
	}
}