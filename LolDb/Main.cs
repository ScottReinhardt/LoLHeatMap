using System;
using System.Collections.Concurrent;
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
        private readonly CancellationTokenSource _token = new CancellationTokenSource();
        private bool quereying = false;
        private Thread thread;
        public Main()
        {
            InitializeComponent();
            ServicePointManager.DefaultConnectionLimit = 100;
        }

        private void BtnToggleDownload_Click(object sender, System.EventArgs e)
        {
            if (quereying)
            {
                BtnToggleDownload.Text = "Start Downloading Data";
                Cancel();
                quereying = false;
            }
            else
            {
                thread = new Thread(Run);
                thread.Start();
                BtnToggleDownload.Text = "Stop Downloading Data";
                quereying = true;
            }
        }

        private void Cancel()
        {
            _token.Cancel();
        }

        private async void Run()
        {
            while (!_token.Token.IsCancellationRequested)
            {
                await Task.Run(async () =>
                {
                    await Task.Run(async () =>
                    {
                        await DbJobs.DownloadIds();
                        await DbJobs.DownloadGames(_token.Token);
                    }, _token.Token);
                }, _token.Token);
            }

            
        }

        private async void button1_Click(object sender, System.EventArgs e)
        {
            var data = await ApiManager.GetAllGameData();
            var points = new ConcurrentBag<Point>();
            var matches = data.Where(m => m.timeline.frames.Any());
            Parallel.ForEach(matches, match =>
            {
                foreach (var frame in match.timeline.frames)
                {
                    foreach (var ev in frame.events.Where(even => even.position != null && even.position.x != 0 && even.position.y != 0))
                    {
                        points.Add(new Point(ev));
                    }
                }   
            });

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
