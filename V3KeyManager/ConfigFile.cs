using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V3KeyManager.Config;

namespace V3KeyManager
{

	internal class ConfigFile
	{
		public VersionSettings Version = new VersionSettings();
		public WindowSettings Window = new WindowSettings();
		public PadSettings Pad = new PadSettings();
		public MouseSettings Mouse = new MouseSettings();
		public EffectSettings Effect = new EffectSettings();
		public KeyboardSettings Keyboard = new KeyboardSettings();

		public string ReadSetting(string? line)
		{

			if(line == null)
			{
				return "NULL";
			}
			int find_equal = line.IndexOf("=");
			if(find_equal == -1)
			{
				return "INVALID";
			}

			line = line.Substring(find_equal + 1);  // Remove everything before, and including, the symbol
			if (line.Length > 1)
			{
				line = line.Substring(1);               // Space

				return line;
			} else
			{
				return string.Empty;
			}
		}

		public string MakeSetting(string setting_name, string setting_value)
		{

			string ret = setting_name + " = " + setting_value;
			return ret;
		}

		public string PadString(string str, int minimum_length, char c)
		{

			while(str.Length < minimum_length)
			{
				str += c;
			}
			return str;
		}

		public string MakeDouble(string str)
		{

			int find_dot = str.IndexOf(".");
			if (find_dot > -1)
			{
				return str;
			}
			str += ".0";
			return str;
		}

		public List<string> StringToList(string? str)
		{

			List<string> ret = new List<string>();
			if (str == null)
			{
				ret.Add("NULL");
				return ret;
			}
			if(str.Length == 0)
			{
				return ret;
			}
			while (str.Contains(","))
			{
				int find_comma = str.IndexOf(",");
				string cut_str = str.Substring(0, find_comma);
				ret.Add(cut_str);
				str = str.Substring(find_comma + 1);
			}
			ret.Add(str);
			return ret;
		}

		public string ConcatenateList(List<string> strings)
		{

			StringBuilder sb = new StringBuilder();
			sb.AppendJoin(',', strings);
			return sb.ToString().Trim();
		}

		public void Load(string file)
		{

			using(StreamReader reader = new StreamReader(file))
			{
				CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

				Version = new VersionSettings();
				Window = new WindowSettings();
				Pad = new PadSettings();
				Mouse = new MouseSettings();
				Effect = new EffectSettings();
				Keyboard = new KeyboardSettings();

				reader.ReadLine();															// [version]
				Version.GameVersion = ReadSetting(reader.ReadLine());						// version_num
				reader.ReadLine();															//

				reader.ReadLine();															// [window]
				Window.WindowMode = ReadSetting(reader.ReadLine());							// window_mode
				Window.WindowTopX = int.Parse(ReadSetting(reader.ReadLine()));				// window_top_x				
				Window.WindowTopY = int.Parse(ReadSetting(reader.ReadLine()));				// window_top_y
				Window.WindowSizeX = int.Parse(ReadSetting(reader.ReadLine()));				// window_size_x
				Window.WindowSizeY = int.Parse(ReadSetting(reader.ReadLine()));				// window_size_y
				Window.RenderSizeX = int.Parse(ReadSetting(reader.ReadLine()));				// render_size_x
				Window.RenderSizeY = int.Parse(ReadSetting(reader.ReadLine()));				// render_size_y
				Window.FullscreenRenderSizeX = int.Parse(ReadSetting(reader.ReadLine()));	// fullscreen_render_size_x
				Window.FullscreenRenderSizeY = int.Parse(ReadSetting(reader.ReadLine()));   // fullscreen_render_size_y
				reader.ReadLine();                                                          //

				reader.ReadLine();															// [pad]
				Pad.PadHelpType = ReadSetting(reader.ReadLine());                           // pad_help_type
				reader.ReadLine();                                                          //
				reader.ReadLine();                                                          // [mouse]
				Mouse.MouseSpeed = double.Parse(ReadSetting(reader.ReadLine()));				// mouse_speed
				reader.ReadLine();                                                          //

				reader.ReadLine();															// [effect]
				Effect.FXAA = ReadSetting(reader.ReadLine());                               // fxaa
				reader.ReadLine();                                                          //

				reader.ReadLine();                                                          // [keyboard]
				Keyboard.PadUp = StringToList(ReadSetting(reader.ReadLine()));				// pad_up
				Keyboard.PadDown = StringToList(ReadSetting(reader.ReadLine()));			// pad_down
				Keyboard.PadLeft = StringToList(ReadSetting(reader.ReadLine()));			// pad_left
				Keyboard.PadRight = StringToList(ReadSetting(reader.ReadLine()));			// pad_right
				Keyboard.PadR1 = StringToList(ReadSetting(reader.ReadLine()));				// pad_r1
				Keyboard.PadR2 = StringToList(ReadSetting(reader.ReadLine()));				// pad_r2
				Keyboard.PadR3 = StringToList(ReadSetting(reader.ReadLine()));				// pad_r3
				Keyboard.PadL1 = StringToList(ReadSetting(reader.ReadLine()));				// pad_l1
				Keyboard.PadL2 = StringToList(ReadSetting(reader.ReadLine()));				// pad_l2
				Keyboard.PadL3 = StringToList(ReadSetting(reader.ReadLine()));				// pad_l3
				Keyboard.PadCross = StringToList(ReadSetting(reader.ReadLine()));			// pad_cross
				Keyboard.PadCircle = StringToList(ReadSetting(reader.ReadLine()));			// pad_circle
				Keyboard.PadSquare = StringToList(ReadSetting(reader.ReadLine()));			// pad_square
				Keyboard.PadTriangle = StringToList(ReadSetting(reader.ReadLine()));		// pad_triangle
				Keyboard.PadStart = StringToList(ReadSetting(reader.ReadLine()));			// pad_start
				Keyboard.PadSelect = StringToList(ReadSetting(reader.ReadLine()));			// pad_select
				Keyboard.PadAny = StringToList(ReadSetting(reader.ReadLine()));				// pad_any
				Keyboard.MoveFront = StringToList(ReadSetting(reader.ReadLine()));			// move_front
				Keyboard.MoveBack = StringToList(ReadSetting(reader.ReadLine()));			// move_back
				Keyboard.MoveLeft = StringToList(ReadSetting(reader.ReadLine()));			// move_left
				Keyboard.MoveRight = StringToList(ReadSetting(reader.ReadLine()));			// move_right
				Keyboard.MoveCameraUp = StringToList(ReadSetting(reader.ReadLine()));		// move_camera_up
				Keyboard.MoveCameraDown = StringToList(ReadSetting(reader.ReadLine()));		// move_camera_down
				Keyboard.MoveCameraLeft = StringToList(ReadSetting(reader.ReadLine()));		// move_camera_left
				Keyboard.MoveCameraRight = StringToList(ReadSetting(reader.ReadLine()));	// move_camera_right
				Keyboard.MoveCameraLeftDR = StringToList(ReadSetting(reader.ReadLine()));	// move_camera_left_dr
				Keyboard.MoveCameraRightDR = StringToList(ReadSetting(reader.ReadLine()));	// move_camera_right_dr
				Keyboard.MoveDash = StringToList(ReadSetting(reader.ReadLine()));			// move_dash
				Keyboard.MessageSkip = StringToList(ReadSetting(reader.ReadLine()));		// message_skip
				Keyboard.PushW = StringToList(ReadSetting(reader.ReadLine()));				// push_w
				Keyboard.PushS = StringToList(ReadSetting(reader.ReadLine()));				// push_s
				Keyboard.PushA = StringToList(ReadSetting(reader.ReadLine()));				// push_a
				Keyboard.PushD = StringToList(ReadSetting(reader.ReadLine()));				// push_d
				Keyboard.MouseMode = StringToList(ReadSetting(reader.ReadLine()));			// mouse_mode
				Keyboard.ReturnButton = StringToList(ReadSetting(reader.ReadLine()));       // return_button
				reader.ReadLine();															//

			}	
		}

		public void Save(string file)
		{

			if(File.Exists(file))
			{
				File.Delete(file);
			}

			using(StreamWriter sr = new StreamWriter(file))
			{
				sr.WriteLine("[version]");
				sr.WriteLine(MakeSetting("version_num", Version.GameVersion));
				sr.WriteLine("");

				sr.WriteLine("[window]");
				sr.WriteLine(MakeSetting("window_mode", Window.WindowMode));
				sr.WriteLine(MakeSetting("window_top_x", Window.WindowTopX.ToString()));
				sr.WriteLine(MakeSetting("window_top_y", Window.WindowTopY.ToString()));
				sr.WriteLine(MakeSetting("window_size_x", Window.WindowSizeX.ToString()));
				sr.WriteLine(MakeSetting("window_size_y", Window.WindowSizeY.ToString()));
				sr.WriteLine(MakeSetting("render_size_x", Window.RenderSizeX.ToString()));
				sr.WriteLine(MakeSetting("render_size_y", Window.RenderSizeY.ToString()));
				sr.WriteLine(MakeSetting("fullscreen_render_size_x", Window.FullscreenRenderSizeX.ToString()));
				sr.WriteLine(MakeSetting("fullscreen_render_size_y", Window.FullscreenRenderSizeY.ToString()));
				sr.WriteLine("");

				sr.WriteLine("[pad]");
				sr.WriteLine(MakeSetting("pad_help_type", Pad.PadHelpType));
				sr.WriteLine("");

				sr.WriteLine("[mouse]");
				sr.WriteLine(MakeSetting("mouse_speed", PadString(MakeDouble(Mouse.MouseSpeed.ToString()), 8, '0')));
				sr.WriteLine("");

				sr.WriteLine("[effect]");
				sr.WriteLine(MakeSetting("fxaa", Effect.FXAA));
				sr.WriteLine("");

				sr.WriteLine("[keyboard]");
				sr.WriteLine(MakeSetting("pad_up", ConcatenateList(Keyboard.PadUp)));
				sr.WriteLine(MakeSetting("pad_down", ConcatenateList(Keyboard.PadDown)));
				sr.WriteLine(MakeSetting("pad_left", ConcatenateList(Keyboard.PadLeft)));
				sr.WriteLine(MakeSetting("pad_right", ConcatenateList(Keyboard.PadRight)));
				sr.WriteLine(MakeSetting("pad_r1", ConcatenateList(Keyboard.PadR1)));
				sr.WriteLine(MakeSetting("pad_r2", ConcatenateList(Keyboard.PadR2)));
				sr.WriteLine(MakeSetting("pad_r3", ConcatenateList(Keyboard.PadR3)));
				sr.WriteLine(MakeSetting("pad_l1", ConcatenateList(Keyboard.PadL1)));
				sr.WriteLine(MakeSetting("pad_l2", ConcatenateList(Keyboard.PadL2)));
				sr.WriteLine(MakeSetting("pad_l3", ConcatenateList(Keyboard.PadL3)));
				sr.WriteLine(MakeSetting("pad_cross", ConcatenateList(Keyboard.PadCross)));
				sr.WriteLine(MakeSetting("pad_circle", ConcatenateList(Keyboard.PadCircle)));
				sr.WriteLine(MakeSetting("pad_square", ConcatenateList(Keyboard.PadSquare)));
				sr.WriteLine(MakeSetting("pad_triangle", ConcatenateList(Keyboard.PadTriangle)));
				sr.WriteLine(MakeSetting("pad_start", ConcatenateList(Keyboard.PadStart)));
				sr.WriteLine(MakeSetting("pad_select", ConcatenateList(Keyboard.PadSelect)));
				sr.WriteLine(MakeSetting("pad_any", ConcatenateList(Keyboard.PadAny)));
				sr.WriteLine(MakeSetting("move_front", ConcatenateList(Keyboard.MoveFront)));
				sr.WriteLine(MakeSetting("move_back", ConcatenateList(Keyboard.MoveBack)));
				sr.WriteLine(MakeSetting("move_left", ConcatenateList(Keyboard.MoveLeft)));
				sr.WriteLine(MakeSetting("move_right", ConcatenateList(Keyboard.MoveRight)));
				sr.WriteLine(MakeSetting("move_camera_up", ConcatenateList(Keyboard.MoveCameraUp)));
				sr.WriteLine(MakeSetting("move_camera_down", ConcatenateList(Keyboard.MoveCameraDown)));
				sr.WriteLine(MakeSetting("move_camera_left", ConcatenateList(Keyboard.MoveCameraLeft)));
				sr.WriteLine(MakeSetting("move_camera_right", ConcatenateList(Keyboard.MoveCameraRight)));
				sr.WriteLine(MakeSetting("move_camera_left_dr", ConcatenateList(Keyboard.MoveCameraLeftDR)));
				sr.WriteLine(MakeSetting("move_camera_right_dr", ConcatenateList(Keyboard.MoveCameraRightDR)));
				sr.WriteLine(MakeSetting("move_dash", ConcatenateList(Keyboard.MoveDash)));
				sr.WriteLine(MakeSetting("message_skip", ConcatenateList(Keyboard.MessageSkip)));
				sr.WriteLine(MakeSetting("push_w", ConcatenateList(Keyboard.PushW)));
				sr.WriteLine(MakeSetting("push_s", ConcatenateList(Keyboard.PushS)));
				sr.WriteLine(MakeSetting("push_a", ConcatenateList(Keyboard.PushA)));
				sr.WriteLine(MakeSetting("push_d", ConcatenateList(Keyboard.PushD)));
				sr.WriteLine(MakeSetting("mouse_mode", ConcatenateList(Keyboard.MouseMode)));
				sr.WriteLine(MakeSetting("return_button", ConcatenateList(Keyboard.ReturnButton)));
				sr.WriteLine("");
			}
		}

		// "Human Readable"
		static public string GetHR(string str, bool reverse)
		{

			Dictionary<string, string> terms = new Dictionary<string, string>()
			{
				{ "off", "Off" },
				{ "on", "On" },
				{ "pad", "Controller" },
				{ "keyboard_fr", "Keyboard and Mouse (AZERTY)" },
				{ "keyboard", "Keyboard and Mouse (QWERTY)" },
				{ "windowed", "Windowed" },
				{ "borderless", "Borderless" },
				{ "fullscreen", "Fullscreen" },
			};

			if (!reverse)
			{
				string ret = terms.FirstOrDefault(x => x.Key == str).Value;
				return ret;
			} else
			{
				string ret = terms.FirstOrDefault(x => x.Value == str).Key;
				return ret;
			}
		}
	}
}
