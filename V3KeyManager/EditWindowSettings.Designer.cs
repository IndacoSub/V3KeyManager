namespace V3KeyManager
{
	partial class EditWindowSettings
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
			NewWindowModeComboBox = new ComboBox();
			NewWindowModeLabel = new Label();
			NewWindowLocationLabel = new Label();
			NewWindowLocationX = new TextBox();
			NewWindowLocationY = new TextBox();
			NewWindowSizeLabel = new Label();
			NewWindowSizeX = new TextBox();
			NewWindowSizeY = new TextBox();
			RenderSizeLabel = new Label();
			NewRenderSizeX = new TextBox();
			NewRenderSizeY = new TextBox();
			FullscreenRenderSizeLabel = new Label();
			NewFullscreenRenderSizeX = new TextBox();
			NewFullscreenRenderSizeY = new TextBox();
			SetButton = new Button();
			CurrentWindowModeLabel = new Label();
			CurrentWindowLocationLabel = new Label();
			CurrentWindowSizeLabel = new Label();
			CurrentRenderSizeLabel = new Label();
			CurrentFullscreenRenderSizeLabel = new Label();
			SuspendLayout();
			// 
			// NewWindowModeComboBox
			// 
			NewWindowModeComboBox.FormattingEnabled = true;
			NewWindowModeComboBox.Items.AddRange(new object[] { "Borderless", "Windowed", "Fullscreen" });
			NewWindowModeComboBox.Location = new Point(150, 176);
			NewWindowModeComboBox.Name = "NewWindowModeComboBox";
			NewWindowModeComboBox.Size = new Size(100, 23);
			NewWindowModeComboBox.TabIndex = 0;
			NewWindowModeComboBox.Text = "Borderless";
			// 
			// NewWindowModeLabel
			// 
			NewWindowModeLabel.AutoSize = true;
			NewWindowModeLabel.Location = new Point(23, 179);
			NewWindowModeLabel.Name = "NewWindowModeLabel";
			NewWindowModeLabel.Size = new Size(115, 15);
			NewWindowModeLabel.TabIndex = 1;
			NewWindowModeLabel.Text = "New Window Mode:";
			// 
			// NewWindowLocationLabel
			// 
			NewWindowLocationLabel.AutoSize = true;
			NewWindowLocationLabel.Location = new Point(7, 206);
			NewWindowLocationLabel.Name = "NewWindowLocationLabel";
			NewWindowLocationLabel.Size = new Size(243, 15);
			NewWindowLocationLabel.TabIndex = 2;
			NewWindowLocationLabel.Text = "New Window Location: [                 ,                ]";
			// 
			// NewWindowLocationX
			// 
			NewWindowLocationX.Location = new Point(150, 203);
			NewWindowLocationX.Name = "NewWindowLocationX";
			NewWindowLocationX.Size = new Size(34, 23);
			NewWindowLocationX.TabIndex = 3;
			NewWindowLocationX.Text = "0";
			NewWindowLocationX.TextAlign = HorizontalAlignment.Center;
			// 
			// NewWindowLocationY
			// 
			NewWindowLocationY.Location = new Point(199, 203);
			NewWindowLocationY.Name = "NewWindowLocationY";
			NewWindowLocationY.Size = new Size(34, 23);
			NewWindowLocationY.TabIndex = 4;
			NewWindowLocationY.Text = "0";
			NewWindowLocationY.TextAlign = HorizontalAlignment.Center;
			// 
			// NewWindowSizeLabel
			// 
			NewWindowSizeLabel.AutoSize = true;
			NewWindowSizeLabel.Location = new Point(35, 236);
			NewWindowSizeLabel.Name = "NewWindowSizeLabel";
			NewWindowSizeLabel.Size = new Size(164, 15);
			NewWindowSizeLabel.TabIndex = 5;
			NewWindowSizeLabel.Text = "New Window Size:                  x";
			// 
			// NewWindowSizeX
			// 
			NewWindowSizeX.Location = new Point(150, 232);
			NewWindowSizeX.Name = "NewWindowSizeX";
			NewWindowSizeX.Size = new Size(34, 23);
			NewWindowSizeX.TabIndex = 6;
			NewWindowSizeX.Text = "1920";
			NewWindowSizeX.TextAlign = HorizontalAlignment.Center;
			// 
			// NewWindowSizeY
			// 
			NewWindowSizeY.Location = new Point(199, 232);
			NewWindowSizeY.Name = "NewWindowSizeY";
			NewWindowSizeY.Size = new Size(34, 23);
			NewWindowSizeY.TabIndex = 7;
			NewWindowSizeY.Text = "1080";
			NewWindowSizeY.TextAlign = HorizontalAlignment.Center;
			// 
			// RenderSizeLabel
			// 
			RenderSizeLabel.AutoSize = true;
			RenderSizeLabel.Location = new Point(69, 264);
			RenderSizeLabel.Name = "RenderSizeLabel";
			RenderSizeLabel.Size = new Size(130, 15);
			RenderSizeLabel.TabIndex = 8;
			RenderSizeLabel.Text = "Render Size:                  x";
			// 
			// NewRenderSizeX
			// 
			NewRenderSizeX.Location = new Point(150, 261);
			NewRenderSizeX.Name = "NewRenderSizeX";
			NewRenderSizeX.Size = new Size(34, 23);
			NewRenderSizeX.TabIndex = 9;
			NewRenderSizeX.Text = "1920";
			NewRenderSizeX.TextAlign = HorizontalAlignment.Center;
			// 
			// NewRenderSizeY
			// 
			NewRenderSizeY.Location = new Point(199, 261);
			NewRenderSizeY.Name = "NewRenderSizeY";
			NewRenderSizeY.Size = new Size(34, 23);
			NewRenderSizeY.TabIndex = 10;
			NewRenderSizeY.Text = "1080";
			NewRenderSizeY.TextAlign = HorizontalAlignment.Center;
			// 
			// FullscreenRenderSizeLabel
			// 
			FullscreenRenderSizeLabel.AutoSize = true;
			FullscreenRenderSizeLabel.Location = new Point(13, 293);
			FullscreenRenderSizeLabel.Name = "FullscreenRenderSizeLabel";
			FullscreenRenderSizeLabel.Size = new Size(186, 15);
			FullscreenRenderSizeLabel.TabIndex = 11;
			FullscreenRenderSizeLabel.Text = "Fullscreen Render Size:                  x";
			// 
			// NewFullscreenRenderSizeX
			// 
			NewFullscreenRenderSizeX.Location = new Point(150, 290);
			NewFullscreenRenderSizeX.Name = "NewFullscreenRenderSizeX";
			NewFullscreenRenderSizeX.Size = new Size(34, 23);
			NewFullscreenRenderSizeX.TabIndex = 12;
			NewFullscreenRenderSizeX.Text = "1920";
			NewFullscreenRenderSizeX.TextAlign = HorizontalAlignment.Center;
			// 
			// NewFullscreenRenderSizeY
			// 
			NewFullscreenRenderSizeY.Location = new Point(199, 290);
			NewFullscreenRenderSizeY.Name = "NewFullscreenRenderSizeY";
			NewFullscreenRenderSizeY.Size = new Size(34, 23);
			NewFullscreenRenderSizeY.TabIndex = 13;
			NewFullscreenRenderSizeY.Text = "1080";
			NewFullscreenRenderSizeY.TextAlign = HorizontalAlignment.Center;
			// 
			// SetButton
			// 
			SetButton.Location = new Point(13, 328);
			SetButton.Name = "SetButton";
			SetButton.Size = new Size(260, 25);
			SetButton.TabIndex = 14;
			SetButton.Text = "Set";
			SetButton.UseVisualStyleBackColor = true;
			SetButton.Click += SetButton_Click;
			// 
			// CurrentWindowModeLabel
			// 
			CurrentWindowModeLabel.AutoSize = true;
			CurrentWindowModeLabel.Location = new Point(13, 9);
			CurrentWindowModeLabel.Name = "CurrentWindowModeLabel";
			CurrentWindowModeLabel.Size = new Size(134, 15);
			CurrentWindowModeLabel.TabIndex = 15;
			CurrentWindowModeLabel.Text = "Current Window Mode: ";
			// 
			// CurrentWindowLocationLabel
			// 
			CurrentWindowLocationLabel.AutoSize = true;
			CurrentWindowLocationLabel.Location = new Point(13, 33);
			CurrentWindowLocationLabel.Name = "CurrentWindowLocationLabel";
			CurrentWindowLocationLabel.Size = new Size(149, 15);
			CurrentWindowLocationLabel.TabIndex = 16;
			CurrentWindowLocationLabel.Text = "Current Window Location: ";
			// 
			// CurrentWindowSizeLabel
			// 
			CurrentWindowSizeLabel.AutoSize = true;
			CurrentWindowSizeLabel.Location = new Point(13, 58);
			CurrentWindowSizeLabel.Name = "CurrentWindowSizeLabel";
			CurrentWindowSizeLabel.Size = new Size(123, 15);
			CurrentWindowSizeLabel.TabIndex = 17;
			CurrentWindowSizeLabel.Text = "Current Window Size: ";
			// 
			// CurrentRenderSizeLabel
			// 
			CurrentRenderSizeLabel.AutoSize = true;
			CurrentRenderSizeLabel.Location = new Point(12, 83);
			CurrentRenderSizeLabel.Name = "CurrentRenderSizeLabel";
			CurrentRenderSizeLabel.Size = new Size(116, 15);
			CurrentRenderSizeLabel.TabIndex = 18;
			CurrentRenderSizeLabel.Text = "Current Render Size: ";
			// 
			// CurrentFullscreenRenderSizeLabel
			// 
			CurrentFullscreenRenderSizeLabel.AutoSize = true;
			CurrentFullscreenRenderSizeLabel.Location = new Point(13, 108);
			CurrentFullscreenRenderSizeLabel.Name = "CurrentFullscreenRenderSizeLabel";
			CurrentFullscreenRenderSizeLabel.Size = new Size(169, 15);
			CurrentFullscreenRenderSizeLabel.TabIndex = 19;
			CurrentFullscreenRenderSizeLabel.Text = "Current Fullscreen Render Size:";
			// 
			// EditWindowSettings
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 365);
			Controls.Add(CurrentFullscreenRenderSizeLabel);
			Controls.Add(CurrentRenderSizeLabel);
			Controls.Add(CurrentWindowSizeLabel);
			Controls.Add(CurrentWindowLocationLabel);
			Controls.Add(CurrentWindowModeLabel);
			Controls.Add(SetButton);
			Controls.Add(NewFullscreenRenderSizeY);
			Controls.Add(NewFullscreenRenderSizeX);
			Controls.Add(FullscreenRenderSizeLabel);
			Controls.Add(NewRenderSizeY);
			Controls.Add(NewRenderSizeX);
			Controls.Add(RenderSizeLabel);
			Controls.Add(NewWindowSizeY);
			Controls.Add(NewWindowSizeX);
			Controls.Add(NewWindowSizeLabel);
			Controls.Add(NewWindowLocationY);
			Controls.Add(NewWindowLocationX);
			Controls.Add(NewWindowLocationLabel);
			Controls.Add(NewWindowModeLabel);
			Controls.Add(NewWindowModeComboBox);
			Name = "EditWindowSettings";
			Text = "EditWindowSettings";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		public ComboBox NewWindowModeComboBox;
		public Label NewWindowModeLabel;
		public Label NewWindowLocationLabel;
		public TextBox NewWindowLocationX;
		public TextBox NewWindowLocationY;
		public Label NewWindowSizeLabel;
		public TextBox NewWindowSizeX;
		public TextBox NewWindowSizeY;
		public Label RenderSizeLabel;
		public TextBox NewRenderSizeX;
		public TextBox NewRenderSizeY;
		public Label FullscreenRenderSizeLabel;
		public TextBox NewFullscreenRenderSizeX;
		public TextBox NewFullscreenRenderSizeY;
		public Button SetButton;
		public Label CurrentWindowModeLabel;
		public Label CurrentWindowLocationLabel;
		public Label CurrentWindowSizeLabel;
		public Label CurrentRenderSizeLabel;
		public Label CurrentFullscreenRenderSizeLabel;
	}
}