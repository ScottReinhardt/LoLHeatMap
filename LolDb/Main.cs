using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LoLJson;
using Newtonsoft.Json;


namespace LolDb
{
    public partial class Main : Form
    {
        private bool querying = false;
        public Main()
        {
            InitializeComponent();
            JobIdTimer.Stop();
            ServicePointManager.DefaultConnectionLimit = 100;
        }

        private void BtnToggleDownload_Click(object sender, System.EventArgs e)
        {
            if (querying)
            {
                querying = false;
                BtnToggleDownload.Text = "Start Downloading Data";
                JobIdTimer.Stop();
            }
            else
            {
                querying = true;
                JobIdTimer.Start();
                JobIdTimer_Tick(sender, e);
                Run();
                BtnToggleDownload.Text = "Stop Downloading Data";
            }
        }

        private async void Run()
        {
            var sleepTime = TimeSpan.FromSeconds(10);
            while (querying)
            {
                await Task.Factory.StartNew(() =>
                {
                    DbJobs.DownloadGames();
                    Thread.Sleep(sleepTime);
                });
            }
            
        }

        private void JobIdTimer_Tick(object sender, System.EventArgs e)
        {
            Console.WriteLine("Getting match ids.  Next tick at: {0}", DateTime.Now.AddMilliseconds(JobIdTimer.Interval));
            DbJobs.DownloadIds();
        }

        private async void button1_Click(object sender, System.EventArgs e)
        {
            var data = await ApiManager.GetAllGameData();
            var points = new List<Point>();
            foreach (var match in data.Where(m => m.timeline.frames.Any()))
            {
                foreach (var frame in match.timeline.frames)
                {
                    foreach (var ev in frame.events.Where(even => even.position.x != 0 && even.position.y != 0))
                    {
                        points.Add(new Point(ev));
                    }
                }   
            }

            var groupedPoints = Parse.GroupPoints(points, data.Count);
            using (var file = new FileStream("data.lol", FileMode.Create))
            using (var writer = new StreamWriter(file))
            using(var jsonWriter = new JsonTextWriter(writer))
            {
                jsonWriter.Formatting = Formatting.Indented;
                new JsonSerializer().Serialize(jsonWriter, groupedPoints);
            }
            Console.WriteLine("Done serializing");
        }
    }
}
