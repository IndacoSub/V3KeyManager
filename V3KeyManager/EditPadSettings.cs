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
	public partial class EditPadSettings : Form
	{
		public EditPadSettings()
		{
			InitializeComponent();
		}

		private void SetButton_Click(object sender, EventArgs e)
		{
			bool combobox_valid = InputTypeComboBox.Items.Contains(InputTypeComboBox.Text);
			if (combobox_valid)
			{
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
		}
	}
}
