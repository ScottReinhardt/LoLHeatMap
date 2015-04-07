using System;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using LoLJson;
using Newtonsoft.Json;


namespace LolDb
{
    internal static class DbJobs
    {
        private static readonly string _ApiKey = ConfigurationManager.AppSettings["apiKey"];
        private static readonly string _Region = ConfigurationManager.AppSettings["region"];
        private static readonly TimeSpan _TimeToRoundDownTo = TimeSpan.FromMinutes(5);
        private static readonly TimeSpan _TimeToSleepFor = TimeSpan.FromSeconds(2);

        public static async Task DownloadIds()
        {
            var currentTime = new DateTime(DateTime.Now.Ticks / _TimeToRoundDownTo.Ticks * _TimeToRoundDownTo.Ticks);
            TimeSpan t = currentTime - new DateTime(1970, 1, 1);
            var epoc = (int)t.TotalSeconds;
            try
            {
                string response = "";
                await Task.Run(async () =>
                {
                    response = new WebClient().DownloadString(
                        string.Format(
                            "https://na.api.pvp.net/api/lol/{0}/v4.1/game/ids?beginDate={1}&api_key={2}", _Region, epoc,
                            _ApiKey));
                    Console.WriteLine("Got new ids");
                    await Task.Delay(1000);
                });
                
                var games = await JsonConvert.DeserializeObjectAsync<GamseToDownload>(string.Format("{0} \"games\":  {1} {2}", "{", response, "}"));
                if (games != null && games.games != null && games.games.Count > 0)
                {
                    ApiManager.SaveGameIdsToDownload(games.games);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

        }

        public static async Task DownloadGames(CancellationToken token)
        {
            var data = await ApiManager.GetGameDataToDownload();
            if (data == null || data.Count == 0)
            {
                return;
            }
            try
            {
                foreach (var gameToDownload in data)
                {
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }
                    string response = "";
                    
                    await Task.Run(async() =>
                    {
                        response = new WebClient().DownloadString(
                        string.Format(
                            "https://na.api.pvp.net/api/lol/{0}/v2.2/match/{1}?includeTimeline=true&api_key={2}",
                            _Region, gameToDownload.MatchToDownload, _ApiKey));
                        Console.WriteLine("Got game {0}", gameToDownload.MatchToDownload);
                        await Task.Delay(_TimeToSleepFor);
                    });

                    var match = await JsonConvert.DeserializeObjectAsync<Match>(response);
                    if (match == null)
                    {
                        return;
                    }
                    gameToDownload.Downloaded = true;
                    ApiManager.SaveMatch(match);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
