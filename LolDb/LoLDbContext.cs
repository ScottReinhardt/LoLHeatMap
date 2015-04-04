using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LoLJson;

namespace LolDb
{
    internal class LoLDbContext : DbContext
    {
        public DbSet<Match> Matches { get; set; }
        public DbSet<GameData> GameData { get; set; }
    }

    internal class GameData
    {
        [Key]
        public int Id { get; set; }
        public DateTime StartQueringTime { get; set; }
        public bool Downloaded { get; set; }
    }

    internal class ApiManager
    {
        private static readonly LoLDbContext _Db = new LoLDbContext();
        public bool Querying { get; internal set; }

        internal async static void SaveGameIdsToDownload(List<int> ids)
        {
            try
            {
                var timeToStartQuerying = DateTime.Now.AddHours(1);
                _Db.GameData.AddRange(ids.Select(i => new GameData
                {
                    Id = i,
                    StartQueringTime = timeToStartQuerying,
                }));
                await _Db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        internal static async Task<GameData> GetGameData()
        {
            try
            {
                return await _Db.GameData.SqlQuery("SELECT * FROM dbo.GameDatas WHERE StartQueringTime < GETDATE() ORDER BY NEWID()").FirstAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            
        }

        internal async static void SaveMatch(Match match)
        {
            try
            {
                _Db.Matches.Add(match);
                await _Db.SaveChangesAsync();
            }
            catch (Exception)
            {

            }

        }

        internal IEnumerable<Match> GetMatches()
        {
            return _Db.Matches;
        }
    }

}
