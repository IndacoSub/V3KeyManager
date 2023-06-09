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
			SetVersionButton = new Button();
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
			// SetVersionButton
			// 
			SetVersionButton.Location = new Point(199, 35);
			SetVersionButton.Name = "SetVersionButton";
			SetVersionButton.Size = new Size(75, 23);
			SetVersionButton.TabIndex = 1;
			SetVersionButton.Text = "Set";
			SetVersionButton.UseVisualStyleBackColor = true;
			SetVersionButton.Click += SetVersionButton_Click;
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
			ClientSize = new Size(281, 67);
			Controls.Add(CurrentVersionLabel);
			Controls.Add(NewVersionLabel);
			Controls.Add(SetVersionButton);
			Controls.Add(InsertVersionTextbox);
			Name = "EditVersion";
			Text = "EditVersion";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		public TextBox InsertVersionTextbox;
		public Button SetVersionButton;
		public Label NewVersionLabel;
		public Label CurrentVersionLabel;
	}
}