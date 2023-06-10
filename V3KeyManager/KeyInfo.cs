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
	public partial class KeyInfo : UserControl
	{
		string KeyName = "";

		public KeyInfo()
		{
			InitializeComponent();
		}

		public void LoadKey(string keyname, List<string> list, bool fill_entries)
		{

			string key_1 = list.ElementAtOrDefault(0) ?? "";
			string key_2 = list.ElementAtOrDefault(1) ?? "";
			string key_3 = list.ElementAtOrDefault(2) ?? "";
			string dec_key_1 = ConfigFile.TranslateNormalControls(key_1, false) ?? "";
			string dec_key_2 = ConfigFile.TranslateNormalControls(key_2, false) ?? "";
			string dec_key_3 = ConfigFile.TranslateNormalControls(key_3, false) ?? "";
			string enc_key_1 = dec_key_1;
			string enc_key_2 = dec_key_2;
			string enc_key_3 = dec_key_3;

			if (enc_key_1.Contains('(') && enc_key_1.Contains(')'))
			{
				int last_bracket = enc_key_1.LastIndexOf(')');
				enc_key_1 = enc_key_1.Substring(0, last_bracket);
				int first_bracket = enc_key_1.LastIndexOf('(');
				enc_key_1 = enc_key_1.Substring(first_bracket + 1);
			}

			if (enc_key_2.Contains('(') && enc_key_2.Contains(')'))
			{
				int last_bracket = enc_key_2.LastIndexOf(')');
				enc_key_2 = enc_key_2.Substring(0, last_bracket);
				int first_bracket = enc_key_2.LastIndexOf('(');
				enc_key_2 = enc_key_2.Substring(first_bracket + 1);
			}

			if (enc_key_3.Contains('(') && enc_key_3.Contains(')'))
			{
				int last_bracket = enc_key_3.LastIndexOf(')');
				enc_key_3 = enc_key_3.Substring(0, last_bracket);
				int first_bracket = enc_key_3.LastIndexOf('(');
				enc_key_3 = enc_key_3.Substring(first_bracket + 1);
			}

			this.KeyName = keyname;
			this.KeyNameLabel.Text = this.KeyName; // with ":" ?
			this.CurrentKeyLabel1.Text = dec_key_1;
			this.CurrentKeyLabel2.Text = dec_key_2;
			this.CurrentKeyLabel3.Text = dec_key_3;
			this.KeyTextbox1.Text = fill_entries ? enc_key_1 : string.Empty;
			this.KeyTextbox2.Text = fill_entries ? enc_key_2 : string.Empty;
			this.KeyTextbox3.Text = fill_entries ? enc_key_3 : string.Empty;
		}

		public bool HasDuplicates()
		{

			bool ret = (this.KeyTextbox1.Text == this.KeyTextbox2.Text && this.KeyTextbox2.Text.Length > 0)
					|| (this.KeyTextbox1.Text == this.KeyTextbox3.Text && this.KeyTextbox1.Text.Length > 0)
					|| (this.KeyTextbox2.Text == this.KeyTextbox3.Text && this.KeyTextbox3.Text.Length > 0);

			return ret;
		}

		public bool HasBrackets()
		{

			bool any_bracket = this.KeyTextbox1.Text.Contains('(') || this.KeyTextbox2.Text.Contains('(') || this.KeyTextbox3.Text.Contains('(')
				|| this.KeyTextbox1.Text.Contains(')') || this.KeyTextbox2.Text.Contains(')') || this.KeyTextbox3.Text.Contains(')');


			return any_bracket;
		}

		public bool HasWeirdSymbols()
		{

			bool contains_equal = this.KeyTextbox1.Text.Contains('=') || this.KeyTextbox2.Text.Contains('=') || this.KeyTextbox3.Text.Contains('=');
			bool contains_comma = this.KeyTextbox1.Text.Contains(',') || this.KeyTextbox2.Text.Contains(',') || this.KeyTextbox3.Text.Contains(',');

			return contains_comma || contains_equal;
		}

		public List<string> ToStringList()
		{
			List<string> ret = new List<string>();

			if (this.KeyTextbox1.Text != null && this.KeyTextbox1.Text.Length > 0)
			{
				string str = this.KeyTextbox1.Text;
				str = ConfigFile.TranslateNormalControls(str, true);
				str = ConfigFile.TranslateSpecialControls(str);
				ret.Add(str);
			} else {
				return ret;
			}

			if (this.KeyTextbox2.Text != null && this.KeyTextbox2.Text.Length > 0)
			{
				string str = this.KeyTextbox2.Text;
				str = ConfigFile.TranslateNormalControls(str, true);
				str = ConfigFile.TranslateSpecialControls(str);
				ret.Add(str);
			}
			else
			{
				return ret;
			}

			if (this.KeyTextbox3.Text != null && this.KeyTextbox3.Text.Length > 0)
			{
				string str = this.KeyTextbox3.Text;
				str = ConfigFile.TranslateNormalControls(str, true);
				str = ConfigFile.TranslateSpecialControls(str);
				ret.Add(str);
			}
			else
			{
				return ret;
			}

			return ret;
		}
	}
}
