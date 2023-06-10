namespace V3KeyManager
{
	partial class EditPadSettings
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
			CurrentInputTypeLabel = new Label();
			NewInputTypeLabel = new Label();
			InputTypeComboBox = new ComboBox();
			SetButton = new Button();
			SuspendLayout();
			// 
			// CurrentInputTypeLabel
			// 
			CurrentInputTypeLabel.AutoSize = true;
			CurrentInputTypeLabel.Location = new Point(12, 20);
			CurrentInputTypeLabel.Name = "CurrentInputTypeLabel";
			CurrentInputTypeLabel.Size = new Size(108, 15);
			CurrentInputTypeLabel.TabIndex = 0;
			CurrentInputTypeLabel.Text = "Current Input Type:";
			// 
			// NewInputTypeLabel
			// 
			NewInputTypeLabel.AutoSize = true;
			NewInputTypeLabel.Location = new Point(14, 41);
			NewInputTypeLabel.Name = "NewInputTypeLabel";
			NewInputTypeLabel.Size = new Size(92, 15);
			NewInputTypeLabel.TabIndex = 1;
			NewInputTypeLabel.Text = "New Input Type:";
			// 
			// InputTypeComboBox
			// 
			InputTypeComboBox.FormattingEnabled = true;
			InputTypeComboBox.Items.AddRange(new object[] { "Controller", "Keyboard and Mouse (QWERTY)", "Keyboard and Mouse (AZERTY)" });
			InputTypeComboBox.Location = new Point(108, 38);
			InputTypeComboBox.Name = "InputTypeComboBox";
			InputTypeComboBox.Size = new Size(204, 23);
			InputTypeComboBox.TabIndex = 2;
			// 
			// SetButton
			// 
			SetButton.Location = new Point(12, 67);
			SetButton.Name = "SetButton";
			SetButton.Size = new Size(300, 23);
			SetButton.TabIndex = 3;
			SetButton.Text = "Set";
			SetButton.UseVisualStyleBackColor = true;
			SetButton.Click += SetButton_Click;
			// 
			// EditPadSettings
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(324, 98);
			Controls.Add(SetButton);
			Controls.Add(InputTypeComboBox);
			Controls.Add(NewInputTypeLabel);
			Controls.Add(CurrentInputTypeLabel);
			Name = "EditPadSettings";
			Text = "EditPadSettings";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		public Label CurrentInputTypeLabel;
		public Label NewInputTypeLabel;
		public ComboBox InputTypeComboBox;
		public Button SetButton;
	}
}