using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace V3KeyManager
{
	public partial class EditMouseSettings : Form
	{
		public EditMouseSettings()
		{
			InitializeComponent();
		}

		private void SetButton_Click(object sender, EventArgs e)
		{
			const string accepted = "0123456789";
			bool invalid_text = !NewMouseSpeedTextbox.Text.Replace('.', '0').All(x => accepted.Contains(x)) ||
								NewMouseSpeedTextbox.Text.Length != 8 ||
								!NewMouseSpeedTextbox.Text.Contains(".") ||
								NewMouseSpeedTextbox.Text.StartsWith(".") ||
								NewMouseSpeedTextbox.Text.EndsWith(".") ||
								NewMouseSpeedTextbox.Text.IndexOf(".") != NewMouseSpeedTextbox.Text.LastIndexOf(".");
			if (!invalid_text)
			{
				this.DialogResult = DialogResult.OK;
				Close();
			}
		}
	}
}
