namespace V3KeyManager
{
	partial class EditMouseSettings
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
			CurrentMouseSpeedLabel = new Label();
			NewMouseSpeedLabel = new Label();
			NewMouseSpeedTextbox = new TextBox();
			SetButton = new Button();
			SuspendLayout();
			// 
			// CurrentMouseSpeedLabel
			// 
			CurrentMouseSpeedLabel.AutoSize = true;
			CurrentMouseSpeedLabel.Location = new Point(12, 9);
			CurrentMouseSpeedLabel.Name = "CurrentMouseSpeedLabel";
			CurrentMouseSpeedLabel.Size = new Size(127, 15);
			CurrentMouseSpeedLabel.TabIndex = 0;
			CurrentMouseSpeedLabel.Text = "Current Mouse Speed: ";
			// 
			// NewMouseSpeedLabel
			// 
			NewMouseSpeedLabel.AutoSize = true;
			NewMouseSpeedLabel.Location = new Point(12, 36);
			NewMouseSpeedLabel.Name = "NewMouseSpeedLabel";
			NewMouseSpeedLabel.Size = new Size(108, 15);
			NewMouseSpeedLabel.TabIndex = 1;
			NewMouseSpeedLabel.Text = "New Mouse Speed:";
			// 
			// NewMouseSpeedTextbox
			// 
			NewMouseSpeedTextbox.Location = new Point(126, 33);
			NewMouseSpeedTextbox.Name = "NewMouseSpeedTextbox";
			NewMouseSpeedTextbox.Size = new Size(100, 23);
			NewMouseSpeedTextbox.TabIndex = 2;
			// 
			// SetButton
			// 
			SetButton.Location = new Point(12, 71);
			SetButton.Name = "SetButton";
			SetButton.Size = new Size(214, 23);
			SetButton.TabIndex = 3;
			SetButton.Text = "Set";
			SetButton.UseVisualStyleBackColor = true;
			SetButton.Click += SetButton_Click;
			// 
			// EditMouseSettings
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(241, 112);
			Controls.Add(SetButton);
			Controls.Add(NewMouseSpeedTextbox);
			Controls.Add(NewMouseSpeedLabel);
			Controls.Add(CurrentMouseSpeedLabel);
			Name = "EditMouseSettings";
			Text = "EditMouseSettings";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		public Label CurrentMouseSpeedLabel;
		public Label NewMouseSpeedLabel;
		public TextBox NewMouseSpeedTextbox;
		public Button SetButton;
	}
}