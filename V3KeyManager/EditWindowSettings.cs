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
	public partial class EditWindowSettings : Form
	{
		public EditWindowSettings()
		{
			InitializeComponent();
		}

		private void SetButton_Click(object sender, EventArgs e)
		{
			string accepted = "0123456789-";
			bool combobox_valid = NewWindowModeComboBox.Items.Contains(NewWindowModeComboBox.Text);
			bool windowloc_valid = NewWindowLocationX.Text.All(x => accepted.Contains(x)) && NewWindowLocationY.Text.All(x => accepted.Contains(x))
									&& (NewWindowLocationX.Text.Contains('-') ? NewWindowLocationX.Text.Length <= 5 : NewWindowLocationX.Text.Length <= 4)
									&& (NewWindowLocationY.Text.Contains('-') ? NewWindowLocationY.Text.Length <= 5 : NewWindowLocationY.Text.Length <= 4);
			bool windowsize_valid = NewWindowSizeX.Text.All(x => accepted.Contains(x)) && NewWindowSizeY.Text.All(x => accepted.Contains(x))
									&& !NewWindowSizeX.Text.Contains('-') && !NewWindowSizeY.Text.Contains("-")
									&& NewWindowSizeX.Text.Length <= 4 && NewWindowSizeY.Text.Length <= 4;
			bool rendersize_valid = NewRenderSizeX.Text.All(x => accepted.Contains(x)) && NewRenderSizeY.Text.All(x => accepted.Contains(x))
									&& !NewRenderSizeX.Text.Contains('-') && !NewRenderSizeY.Text.Contains("-")
									&& NewRenderSizeX.Text.Length <= 4 && NewRenderSizeY.Text.Length <= 4;
			bool fsrendersize_valid = NewFullscreenRenderSizeX.Text.All(x => accepted.Contains(x)) && NewFullscreenRenderSizeY.Text.All(x => accepted.Contains(x))
									&& !NewFullscreenRenderSizeX.Text.Contains('-') && !NewFullscreenRenderSizeY.Text.Contains("-")
									&& NewFullscreenRenderSizeX.Text.Length <= 4 && NewFullscreenRenderSizeY.Text.Length <= 4;

			if (!combobox_valid || !windowloc_valid || !windowsize_valid || !rendersize_valid || !fsrendersize_valid)
			{
				return;
			}

			this.DialogResult = DialogResult.OK;
			Close();
		}
	}
}
