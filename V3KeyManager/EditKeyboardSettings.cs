using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace V3KeyManager
{
	public partial class EditKeyboardSettings : Form
	{
		public EditKeyboardSettings()
		{
			InitializeComponent();
		}

		private List<KeyInfo> GetNecessaryKeys()
		{
			List<KeyInfo> all = GetAllKeys();
			List<KeyInfo> ret = new List<KeyInfo>();
			foreach (KeyInfo key in all)
			{
				bool necessary = key.KeyTextbox1.Text != null && key.KeyTextbox1.Text.Length > 0;
				if(!necessary)
				{
					continue;
				}
				ret.Add(new KeyInfo {
					CurrentKeyLabel1 = key.CurrentKeyLabel1, 
					CurrentKeyLabel2 = key.CurrentKeyLabel2,
					CurrentKeyLabel3 = key.CurrentKeyLabel3,
					KeyNameLabel = key.KeyNameLabel,
					KeyTextbox1 = key.KeyTextbox1,
					KeyTextbox2 = key.KeyTextbox2,
					KeyTextbox3 = key.KeyTextbox3,
				});
			}

			return ret;
		}

		private bool AreKeysSet(List<KeyInfo> list, bool try_to_fix)
		{
			bool ret = true;
			List<string> possible_commands = GetPossibleCommands();

			foreach (KeyInfo key in list)
			{
				bool valid = key.KeyTextbox1.Text != null && key.KeyTextbox1.Text.Length > 0;
				if(!valid)
				{
					Debug.WriteLine("Key " + key.KeyNameLabel.Text + " not set!");
					if(try_to_fix)
					{
						Random rand = new Random();
						int index = rand.Next(0, possible_commands.Count - 1);
						key.KeyTextbox1.Text = possible_commands[index];
						valid = key.KeyTextbox1.Text != null && key.KeyTextbox1.Text.Length > 0;
						if(!valid)
						{
							Debug.WriteLine("Could not solve set keys");
							ret = false;
							break;
						} else
						{
							Debug.WriteLine("Set keys solved?");
						}

					} else
					{
						ret = false;
						break;
					}
				}
			}

			return ret;
		}

		private List<KeyInfo> GetAllKeys()
		{
			List<KeyInfo> keys = new List<KeyInfo> 
			{
				PadUpKey,
				PadDownKey,
				PadLeftKey,
				PadRightKey,
				PadR1Key,
				PadR2Key,
				PadR3Key,
				PadL1Key,
				PadL2Key,
				PadL3Key,
				PadCrossKey,
				PadCircleKey,
				PadSquareKey,
				PadTriangleKey,
				PadStartKey,
				PadSelectKey,
				PadAnyKey,
				MoveFrontKey,
				MoveBackKey,
				MoveLeftKey,
				MoveRightKey,
				MoveCameraUpKey,
				MoveCameraDownKey,
				MoveCameraLeftKey,
				MoveCameraRightKey,
				MoveCameraLeftDRKey,
				MoveCameraRightDRKey,
				MoveDashKey,
				MessageSkipKey,
				PushWKey,
				PushSKey,
				PushAKey,
				PushDKey,
				MouseModeKey,
				ReturnButtonKey,
			};

			return keys;
		}

		private void ResetChangesButton_Click(object sender, EventArgs e)
		{

			this.ResetTextboxes();
			this.DialogResult = DialogResult.No;
			this.Close();
		}

		private bool AreControlsInvalid(bool override_duplicates)
		{
			List<string> possible_commands = GetPossibleCommands();
			List<KeyInfo> keys = GetAllKeys();
			bool invalid = false;

			int count = 0;

			foreach (KeyInfo key in keys)
			{
				bool has_duplicates = key.HasDuplicates();
				if (has_duplicates)
				{
					Debug.WriteLine("(" + count.ToString() + ") " + key.KeyNameLabel.Text + " has duplicates: " + key.KeyTextbox1.Text + ", " + key.KeyTextbox2.Text + " and " + key.KeyTextbox3.Text + "!");
					if(override_duplicates)
					{
						Random rand = new Random();
						int index_1 = rand.Next(0, possible_commands.Count - 1);
						if(key.KeyTextbox1.Text != null && key.KeyTextbox1.Text.Length > 0)
						{
							key.KeyTextbox1.Text = possible_commands[index_1];
						}
						int index_2 = rand.Next(0, possible_commands.Count - 1);
						if (key.KeyTextbox2.Text != null && key.KeyTextbox2.Text.Length > 0)
						{
							key.KeyTextbox2.Text = possible_commands[index_2];
						}
						int index_3 = rand.Next(0, possible_commands.Count - 1);
						if (key.KeyTextbox3.Text != null && key.KeyTextbox3.Text.Length > 0)
						{
							key.KeyTextbox3.Text = possible_commands[index_3];
						}
						if (possible_commands[index_1] != possible_commands[index_2] && possible_commands[index_1] != possible_commands[index_3])
						{
							Debug.WriteLine("Duplication solved?");
						} else
						{
							Debug.WriteLine("Could not solve duplication.");
						}
					}
				}
				invalid |= has_duplicates;
				invalid |= key.HasBrackets();
				invalid |= key.HasWeirdSymbols();

				count++;

				if (invalid)
				{
					break;
				}
			}

			return invalid;
		}

		private void SetButton_Click(object sender, EventArgs e)
		{
			bool invalid = AreControlsInvalid(false);

			if (!invalid)
			{
				DialogResult = DialogResult.OK;
				this.Close();
			}
		}

		public void ResetLabels()
		{
			List<KeyInfo> keys = GetAllKeys();
			foreach (KeyInfo key in keys)
			{
				key.CurrentKeyLabel1.Text = string.Empty;
				key.CurrentKeyLabel2.Text = string.Empty;
				key.CurrentKeyLabel3.Text = string.Empty;
			}
		}

		public void ResetTextboxes()
		{
			List<KeyInfo> keys = GetAllKeys();
			foreach (KeyInfo key in keys)
			{
				key.KeyTextbox1.Text = string.Empty;
				key.KeyTextbox2.Text = string.Empty;
				key.KeyTextbox3.Text = string.Empty;
			}
		}

		private void ResetTextboxesButton_Click(object sender, EventArgs e)
		{
			ResetTextboxes();
		}

		private List<string> GetPossibleCommands()
		{
			List<string> ret = new List<string>();

			var terms = ConfigFile.GetNormalControls();
			var weird = ConfigFile.GetWeirdControls();

			foreach( var term in terms )
			{
				ret.Add(term.Key);
			}
			foreach(var weird_s in weird)
			{
				ret.Add(weird_s.Key);
			}

			return ret;
		}

		private void RandomizeControlsButton_Click(object sender, EventArgs e)
		{

			List<KeyInfo> keys = GetAllKeys();
			List<KeyInfo> necessary = GetNecessaryKeys();
			List<string> possible_commands = GetPossibleCommands();
			Random rand	= new Random();

			bool invalid_controls = false;
			bool necessary_keys_not_set = false;
			bool many_different_keys = false;

			int while_c = 0;

			do
			{
				rand = new Random();
				invalid_controls = false;
				necessary_keys_not_set = false;
				many_different_keys = true;
				ResetTextboxes();

				foreach (KeyInfo key in keys)
				{
					int control_numbers = rand.Next(0, 100);
					if (control_numbers <= 30)
					{
						control_numbers = 0;
					}
					else
					{
						if (control_numbers <= 65)
						{
							control_numbers = 1;
						}
						else
						{
							if (control_numbers <= 80)
							{
								control_numbers = 2;
							}
							else
							{
								if (control_numbers <= 99)
								{
									control_numbers = 3;
								}
							}
						}
					}

					for (int j = 0; j < control_numbers; j++)
					{
						int control_index = rand.Next(0, possible_commands.Count - 1);
						switch(j)
						{
							case 0:
								key.KeyTextbox1.Text = possible_commands[control_index];
								break;
							case 1:
								key.KeyTextbox2.Text = possible_commands[control_index];
								break;
							case 2:
								key.KeyTextbox3.Text = possible_commands[control_index];
								break;
							default:
								Debug.WriteLine(while_c.ToString() + ": Unknown control index!");
								break;
						}
					}
				}

				necessary_keys_not_set = !this.AreKeysSet(necessary, true);
				invalid_controls = this.AreControlsInvalid(true);
				int new_possible_count = GetPossibleCommands().Count;
				many_different_keys = new_possible_count > 21;				// Arbitrary number

				if(invalid_controls && necessary_keys_not_set && !many_different_keys)
				{
					//Debug.WriteLine(while_c.ToString() + ": (Randomizer) All invalid!");
				} else
				{
					if (invalid_controls)
					{
						//Debug.WriteLine(while_c.ToString() + ": (Randomizer) Invalid controls!");
					}

					if (necessary_keys_not_set)
					{
						//Debug.WriteLine(while_c.ToString() + ": (Randomizer) Necessary keys not set!");
					}

					if(!many_different_keys)
					{
						Debug.WriteLine(while_c.ToString() + ": (Randomizer) Found limited number of keys (" + new_possible_count.ToString() + ") !");
					}
				}

				while_c++;

			} while (invalid_controls || necessary_keys_not_set || !many_different_keys);

			DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
