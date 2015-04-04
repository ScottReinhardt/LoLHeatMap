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
        public int MatchToDownload { get; set; }
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
                var timeToStartQuerying = DateTime.Now.AddMinutes(5);
                _Db.GameData.AddRange(ids.Select(i => new GameData
                {
                    MatchToDownload = i,
                    StartQueringTime = timeToStartQuerying,
                }));
                await _Db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        internal static async Task<List<Match>> GetAllGameData()
        {
            try
            {
                return await _Db.Matches.Include(m => m.timeline).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            
        }

        internal static async Task<GameData> GetRandomGameData()
        {
            try
            {
                return await _Db.GameData.SqlQuery("SELECT * FROM dbo.GameDatas WHERE StartQueringTime < GETDATE() AND Downloaded = 0 ORDER BY NEWID()").FirstOrDefaultAsync();
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        internal IEnumerable<Match> GetMatches()
        {
            return _Db.Matches;
        }
    }

}
