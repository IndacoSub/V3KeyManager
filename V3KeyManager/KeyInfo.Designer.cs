using System.Windows.Forms;

namespace V3KeyManager
{
	partial class KeyInfo
	{
		/// <summary> 
		/// Variabile di progettazione necessaria.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Pulire le risorse in uso.
		/// </summary>
		/// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Codice generato da Progettazione componenti

		/// <summary> 
		/// Metodo necessario per il supporto della finestra di progettazione. Non modificare 
		/// il contenuto del metodo con l'editor di codice.
		/// </summary>
		private void InitializeComponent()
		{
			KeyNameLabel = new Label();
			KeyTextbox1 = new TextBox();
			KeyTextbox2 = new TextBox();
			KeyTextbox3 = new TextBox();
			CurrentKeyLabel1 = new Label();
			CurrentKeyLabel2 = new Label();
			CurrentKeyLabel3 = new Label();
			SuspendLayout();
			// 
			// KeyNameLabel
			// 
			KeyNameLabel.AutoSize = true;
			KeyNameLabel.Location = new Point(0, 21);
			KeyNameLabel.Name = "KeyNameLabel";
			KeyNameLabel.Size = new Size(29, 15);
			KeyNameLabel.TabIndex = 0;
			KeyNameLabel.Text = "Key:";
			KeyNameLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// KeyTextbox1
			// 
			KeyTextbox1.Location = new Point(96, 18);
			KeyTextbox1.Name = "KeyTextbox1";
			KeyTextbox1.Size = new Size(78, 23);
			KeyTextbox1.TabIndex = 1;
			KeyTextbox1.Text = "Left Control";
			KeyTextbox1.TextAlign = HorizontalAlignment.Center;
			// 
			// KeyTextbox2
			// 
			KeyTextbox2.Location = new Point(180, 18);
			KeyTextbox2.Name = "KeyTextbox2";
			KeyTextbox2.Size = new Size(78, 23);
			KeyTextbox2.TabIndex = 2;
			KeyTextbox2.Text = "Left Control";
			KeyTextbox2.TextAlign = HorizontalAlignment.Center;
			// 
			// KeyTextbox3
			// 
			KeyTextbox3.Location = new Point(264, 18);
			KeyTextbox3.Name = "KeyTextbox3";
			KeyTextbox3.Size = new Size(78, 23);
			KeyTextbox3.TabIndex = 3;
			KeyTextbox3.Text = "Left Control";
			KeyTextbox3.TextAlign = HorizontalAlignment.Center;
			// 
			// CurrentKeyLabel1
			// 
			CurrentKeyLabel1.AutoSize = true;
			CurrentKeyLabel1.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
			CurrentKeyLabel1.Location = new Point(92, 0);
			CurrentKeyLabel1.Name = "CurrentKeyLabel1";
			CurrentKeyLabel1.Size = new Size(13, 13);
			CurrentKeyLabel1.TabIndex = 4;
			CurrentKeyLabel1.Text = "1";
			CurrentKeyLabel1.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// CurrentKeyLabel2
			// 
			CurrentKeyLabel2.AutoSize = true;
			CurrentKeyLabel2.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
			CurrentKeyLabel2.Location = new Point(177, 0);
			CurrentKeyLabel2.Name = "CurrentKeyLabel2";
			CurrentKeyLabel2.Size = new Size(13, 13);
			CurrentKeyLabel2.TabIndex = 5;
			CurrentKeyLabel2.Text = "2";
			CurrentKeyLabel2.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// CurrentKeyLabel3
			// 
			CurrentKeyLabel3.AutoSize = true;
			CurrentKeyLabel3.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
			CurrentKeyLabel3.Location = new Point(260, 0);
			CurrentKeyLabel3.Name = "CurrentKeyLabel3";
			CurrentKeyLabel3.Size = new Size(13, 13);
			CurrentKeyLabel3.TabIndex = 6;
			CurrentKeyLabel3.Text = "3";
			CurrentKeyLabel3.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// KeyInfo
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(CurrentKeyLabel3);
			Controls.Add(CurrentKeyLabel2);
			Controls.Add(CurrentKeyLabel1);
			Controls.Add(KeyTextbox3);
			Controls.Add(KeyTextbox2);
			Controls.Add(KeyTextbox1);
			Controls.Add(KeyNameLabel);
			Name = "KeyInfo";
			Size = new Size(342, 47);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		public Label KeyNameLabel;
		public TextBox KeyTextbox1;
		public TextBox KeyTextbox2;
		public TextBox KeyTextbox3;
		public Label CurrentKeyLabel1;
		public Label CurrentKeyLabel2;
		public Label CurrentKeyLabel3;
	}
}
