namespace V3KeyManager
{
	partial class EditEffectSettings
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			CurrentFXAALabel = new Label();
			NewFXAALabel = new Label();
			ActivateFXAACheckbox = new CheckBox();
			SetButton = new Button();
			SuspendLayout();
			// 
			// CurrentFXAALabel
			// 
			CurrentFXAALabel.AutoSize = true;
			CurrentFXAALabel.Location = new Point(12, 9);
			CurrentFXAALabel.Name = "CurrentFXAALabel";
			CurrentFXAALabel.Size = new Size(82, 15);
			CurrentFXAALabel.TabIndex = 0;
			CurrentFXAALabel.Text = "Current FXAA:";
			// 
			// NewFXAALabel
			// 
			NewFXAALabel.AutoSize = true;
			NewFXAALabel.Location = new Point(12, 36);
			NewFXAALabel.Name = "NewFXAALabel";
			NewFXAALabel.Size = new Size(66, 15);
			NewFXAALabel.TabIndex = 1;
			NewFXAALabel.Text = "New FXAA:";
			// 
			// ActivateFXAACheckbox
			// 
			ActivateFXAACheckbox.AutoSize = true;
			ActivateFXAACheckbox.Location = new Point(84, 35);
			ActivateFXAACheckbox.Name = "ActivateFXAACheckbox";
			ActivateFXAACheckbox.Size = new Size(59, 19);
			ActivateFXAACheckbox.TabIndex = 2;
			ActivateFXAACheckbox.Text = "Active";
			ActivateFXAACheckbox.UseVisualStyleBackColor = true;
			// 
			// SetButton
			// 
			SetButton.Location = new Point(12, 60);
			SetButton.Name = "SetButton";
			SetButton.Size = new Size(210, 23);
			SetButton.TabIndex = 3;
			SetButton.Text = "Set";
			SetButton.UseVisualStyleBackColor = true;
			SetButton.Click += SetButton_Click;
			// 
			// EditEffectSettings
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(234, 90);
			Controls.Add(SetButton);
			Controls.Add(ActivateFXAACheckbox);
			Controls.Add(NewFXAALabel);
			Controls.Add(CurrentFXAALabel);
			Name = "EditEffectSettings";
			Text = "EditEffectSettings";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		public Label CurrentFXAALabel;
		public Label NewFXAALabel;
		public CheckBox ActivateFXAACheckbox;
		public Button SetButton;
	}
}