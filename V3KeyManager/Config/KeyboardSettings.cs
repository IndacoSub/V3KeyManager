using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V3KeyManager
{
	internal class KeyboardSettings
	{
		public List<string> PadUp = new List<string> { "key_w", "up_arrow" };
		public List<string> PadDown = new List<string> { "key_s", "down_arrow" };
		public List<string> PadLeft = new List<string> { "key_a", "left_arrow" };
		public List<string> PadRight = new List<string> { "key_d", "right_arrow" };
		public List<string> PadR1 = new List<string> { "key_e", "space" };
		public List<string> PadR2 = new List<string>();
		public List<string> PadR3 = new List<string>();
		public List<string> PadL1 = new List<string> { "key_q" };
		public List<string> PadL2 = new List<string>();
		public List<string> PadL3 = new List<string>();
		public List<string> PadCross = new List<string> { "key_z" };
		public List<string> PadCircle = new List<string> { "escape", "key_r", "key_z" };
		public List<string> PadSquare = new List<string> { "f1" };
		public List<string> PadTriangle = new List<string> { "return" };
		public List<string> PadStart = new List<string> { "f3" };
		public List<string> PadSelect = new List<string> { "f2" };
		public List<string> PadAny = new List<string>();
		public List<string> MoveFront = new List<string> { "key_w" };
		public List<string> MoveBack = new List<string> { "key_s" };
		public List<string> MoveLeft = new List<string> { "key_a" };
		public List<string> MoveRight = new List<string> { "key_d" };
		public List<string> MoveCameraUp = new List<string> { "up_arrow" };
		public List<string> MoveCameraDown = new List<string> { "down_arrow" };
		public List<string> MoveCameraLeft = new List<string> { "left_arrow" };
		public List<string> MoveCameraRight = new List<string> { "right_arrow" };
		public List<string> MoveCameraLeftDR = new List<string> { "left_arrow", "key_q" };
		public List<string> MoveCameraRightDR = new List<string> { "right_arrow", "key_e" };
		public List<string> MoveDash = new List<string> { "l_shift" };
		public List<string> MessageSkip = new List<string> { "l_ctrl", "escape", "key_x" };
		public List<string> PushW = new List<string> { "key_w" };
		public List<string> PushS = new List<string> { "key_s" };
		public List<string> PushA = new List<string> { "key_a" };
		public List<string> PushD = new List<string> { "key_d" };
		public List<string> MouseMode = new List<string> { "slash" };
		public List<string> ReturnButton = new List<string> { "key_z", "return" };
	}
}
