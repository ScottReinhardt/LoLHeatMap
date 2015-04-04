using System;
using System.Configuration;
using System.Net;
using LoLJson;
using Newtonsoft.Json;


namespace LolDb
{
    internal static class DbJobs
    {
        private static readonly WebClient _Client = new WebClient();
        private static readonly string _ApiKey = ConfigurationManager.AppSettings["apiKey"];
        private static readonly string _Region = ConfigurationManager.AppSettings["region"];
        private static readonly TimeSpan _TimeToRoundDownTo = TimeSpan.FromMinutes(5);

        public static async void DownloadIds()
        {
            var currentTime = new DateTime(DateTime.Now.Ticks / _TimeToRoundDownTo.Ticks * _TimeToRoundDownTo.Ticks);
            TimeSpan t = currentTime - new DateTime(1970, 1, 1);
            var epoc = (int)t.TotalSeconds;
            try
            {
                var response = await _Client.DownloadStringTaskAsync(
                    string.Format(
                        "https://na.api.pvp.net/api/lol/{0}/v4.1/game/ids?beginDate={1}&api_key={2}", _Region, epoc,
                        _ApiKey));


                var games = JsonConvert.DeserializeObject<GamseToDownload>(string.Format("{0} \"games\":  {1} {2}", "{", response, "}"));
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

        public static async void DownloadGames()
        {
            var data = await ApiManager.GetGameData();
            if (data == null)
            {
                return;
            }
            try
            {
                var response = await _Client.DownloadStringTaskAsync(
                    string.Format(
                        "https://na.api.pvp.net/api/lol/{0}/v4.1/game/ids?beginDate={1}&api_key={2}", _Region, data.Id, _ApiKey));


                var match = JsonConvert.DeserializeObject<Match>(response);
                if (match != null)
                {
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
