using System.Windows.Forms;


namespace LolDb
{
    public partial class Main : Form
    {
        private bool querying = false;
        public Main()
        {
            InitializeComponent();
            JobIdTimer.Stop();
        }

        private void BtnToggleDownload_Click(object sender, System.EventArgs e)
        {
            if (querying)
            {
                BtnToggleDownload.Text = "Start Downloading Data";
                JobIdTimer.Stop();
                GameDataTimer.Stop();
            }
            else
            {
                JobIdTimer.Start();
                JobIdTimer_Tick(sender, e);
                GameDataTimer.Start();
                GameDataTimer_Tick(sender, e);
                BtnToggleDownload.Text = "Stop Downloading Data";
            }
        }

        private void JobIdTimer_Tick(object sender, System.EventArgs e)
        {
            DbJobs.DownloadIds();
        }

        private void GameDataTimer_Tick(object sender, System.EventArgs e)
        {
            DbJobs.DownloadGames();
        }
    }
}
