namespace V3KeyManager
{
	partial class EditVersion
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
			InsertVersionTextbox = new TextBox();
			SetButton = new Button();
			NewVersionLabel = new Label();
			CurrentVersionLabel = new Label();
			SuspendLayout();
			// 
			// InsertVersionTextbox
			// 
			InsertVersionTextbox.Location = new Point(107, 35);
			InsertVersionTextbox.Name = "InsertVersionTextbox";
			InsertVersionTextbox.Size = new Size(86, 23);
			InsertVersionTextbox.TabIndex = 0;
			// 
			// SetButton
			// 
			SetButton.Location = new Point(12, 69);
			SetButton.Name = "SetButton";
			SetButton.Size = new Size(181, 23);
			SetButton.TabIndex = 1;
			SetButton.Text = "Set";
			SetButton.UseVisualStyleBackColor = true;
			SetButton.Click += SetVersionButton_Click;
			// 
			// NewVersionLabel
			// 
			NewVersionLabel.AutoSize = true;
			NewVersionLabel.Location = new Point(26, 38);
			NewVersionLabel.Name = "NewVersionLabel";
			NewVersionLabel.Size = new Size(75, 15);
			NewVersionLabel.TabIndex = 2;
			NewVersionLabel.Text = "New Version:";
			// 
			// CurrentVersionLabel
			// 
			CurrentVersionLabel.AutoSize = true;
			CurrentVersionLabel.Location = new Point(12, 9);
			CurrentVersionLabel.Name = "CurrentVersionLabel";
			CurrentVersionLabel.Size = new Size(94, 15);
			CurrentVersionLabel.TabIndex = 3;
			CurrentVersionLabel.Text = "Current Version: ";
			// 
			// EditVersion
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(210, 104);
			Controls.Add(CurrentVersionLabel);
			Controls.Add(NewVersionLabel);
			Controls.Add(SetButton);
			Controls.Add(InsertVersionTextbox);
			Name = "EditVersion";
			Text = "EditVersion";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		public TextBox InsertVersionTextbox;
		public Button SetButton;
		public Label NewVersionLabel;
		public Label CurrentVersionLabel;
	}
}