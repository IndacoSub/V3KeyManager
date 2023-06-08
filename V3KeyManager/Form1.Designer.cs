namespace V3KeyManager
{
	partial class Form1
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
			SuspendLayout();
			// 
			// OpenConfigButton
			// 
			OpenConfigButton.Location = new Point(23, 370);
			OpenConfigButton.Name = "OpenConfigButton";
			OpenConfigButton.Size = new Size(132, 58);
			OpenConfigButton.TabIndex = 0;
			OpenConfigButton.Text = "Open Config";
			OpenConfigButton.UseVisualStyleBackColor = true;
			OpenConfigButton.Click += OpenConfigButton_Click;
			// 
			// SaveConfigButton
			// 
			SaveConfigButton.Location = new Point(181, 372);
			SaveConfigButton.Name = "SaveConfigButton";
			SaveConfigButton.Size = new Size(145, 56);
			SaveConfigButton.TabIndex = 1;
			SaveConfigButton.Text = "Save Config As...";
			SaveConfigButton.UseVisualStyleBackColor = true;
			SaveConfigButton.Click += SaveConfigButton_Click;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(SaveConfigButton);
			Controls.Add(OpenConfigButton);
			Name = "Form1";
			Text = "Form1";
			ResumeLayout(false);
		}

		#endregion

		private Button OpenConfigButton;
		private Button SaveConfigButton;
	}
}