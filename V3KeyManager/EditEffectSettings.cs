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
	public partial class EditEffectSettings : Form
	{
		public EditEffectSettings()
		{
			InitializeComponent();
		}

		private void SetButton_Click(object sender, EventArgs e)
		{
			// Literally nothing to check

			this.DialogResult = DialogResult.OK;
			Close();
		}
	}
}
