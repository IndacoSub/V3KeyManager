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
	public partial class EditVersion : Form
	{
		public EditVersion()
		{
			InitializeComponent();
		}

		private void SetVersionButton_Click(object sender, EventArgs e)
		{
			const string accepted = "0123456789";
			bool invalid_text = !InsertVersionTextbox.Text.Replace('.', '0').All(x => accepted.Contains(x)) ||
								InsertVersionTextbox.Text.Length > 4 ||
								!InsertVersionTextbox.Text.Contains(".") ||
								InsertVersionTextbox.Text.StartsWith(".") ||
								InsertVersionTextbox.Text.EndsWith(".") ||
								InsertVersionTextbox.Text.IndexOf(".") != InsertVersionTextbox.Text.LastIndexOf(".");
			if (!invalid_text)
			{
				this.DialogResult = DialogResult.OK;
				Close();
			}
		}
	}
}
