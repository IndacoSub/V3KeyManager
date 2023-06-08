namespace V3KeyManager
{
	public partial class Form1 : Form
	{
		ConfigFile CurrentConfig = new ConfigFile();

		public Form1()
		{
			InitializeComponent();
		}

		private void OpenConfigButton_Click(object sender, EventArgs e)
		{

			OpenFileDialog ofd = new OpenFileDialog();
			if (ofd.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			CurrentConfig = new ConfigFile();
			CurrentConfig.Load(ofd.FileName);
		}

		private void SaveConfigButton_Click(object sender, EventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog();
			if(sfd.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			CurrentConfig.Save(sfd.FileName);
		}
	}
}