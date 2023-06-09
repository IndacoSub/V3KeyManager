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
			SuspendLayout();
			// 
			// OpenConfigButton
			// 
			OpenConfigButton.Location = new Point(13, 383);
			OpenConfigButton.Name = "OpenConfigButton";
			OpenConfigButton.Size = new Size(120, 58);
			OpenConfigButton.TabIndex = 0;
			OpenConfigButton.Text = "Open Config";
			OpenConfigButton.UseVisualStyleBackColor = true;
			OpenConfigButton.Click += OpenConfigButton_Click;
			// 
			// SaveConfigButton
			// 
			SaveConfigButton.Location = new Point(139, 383);
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
			// 
			// EditWindowSettingsButton
			// 
			EditWindowSettingsButton.Location = new Point(139, 317);
			EditWindowSettingsButton.Name = "EditWindowSettingsButton";
			EditWindowSettingsButton.Size = new Size(120, 60);
			EditWindowSettingsButton.TabIndex = 3;
			EditWindowSettingsButton.Text = "Edit Window Settings";
			EditWindowSettingsButton.UseVisualStyleBackColor = true;
			// 
			// EditPadSettingsButton
			// 
			EditPadSettingsButton.Location = new Point(265, 317);
			EditPadSettingsButton.Name = "EditPadSettingsButton";
			EditPadSettingsButton.Size = new Size(120, 60);
			EditPadSettingsButton.TabIndex = 4;
			EditPadSettingsButton.Text = "Edit Pad Settings";
			EditPadSettingsButton.UseVisualStyleBackColor = true;
			// 
			// EditMouseSettingsButton
			// 
			EditMouseSettingsButton.Location = new Point(391, 317);
			EditMouseSettingsButton.Name = "EditMouseSettingsButton";
			EditMouseSettingsButton.Size = new Size(120, 60);
			EditMouseSettingsButton.TabIndex = 5;
			EditMouseSettingsButton.Text = "Edit Mouse Settings";
			EditMouseSettingsButton.UseVisualStyleBackColor = true;
			// 
			// EditEffectSettingsButton
			// 
			EditEffectSettingsButton.Location = new Point(517, 317);
			EditEffectSettingsButton.Name = "EditEffectSettingsButton";
			EditEffectSettingsButton.Size = new Size(120, 60);
			EditEffectSettingsButton.TabIndex = 6;
			EditEffectSettingsButton.Text = "Edit Effect Settings";
			EditEffectSettingsButton.UseVisualStyleBackColor = true;
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
			ResetChangesButton.Location = new Point(265, 383);
			ResetChangesButton.Name = "ResetChangesButton";
			ResetChangesButton.Size = new Size(120, 58);
			ResetChangesButton.TabIndex = 8;
			ResetChangesButton.Text = "Reset Changes";
			ResetChangesButton.UseVisualStyleBackColor = true;
			// 
			// LaunchGameButton
			// 
			LaunchGameButton.Location = new Point(391, 383);
			LaunchGameButton.Name = "LaunchGameButton";
			LaunchGameButton.Size = new Size(120, 58);
			LaunchGameButton.TabIndex = 9;
			LaunchGameButton.Text = "Launch Game";
			LaunchGameButton.UseVisualStyleBackColor = true;
			// 
			// V3KeyManager
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(775, 450);
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
			ResumeLayout(false);
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
	}
}