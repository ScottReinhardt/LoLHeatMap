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
        public bool Querying { get; internal set; }

        internal async static void SaveGameIdsToDownload(List<int> ids)
        {
            try
            {
                using (var db = new LoLDbContext())
                {
                    var timeToStartQuerying = DateTime.Now.AddMinutes(5);
                    db.GameData.AddRange(ids.Select(i => new GameData
                    {
                        MatchToDownload = i,
                        StartQueringTime = timeToStartQuerying,
                    }));
                    await db.SaveChangesAsync();    
                }
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
                using (var db = new LoLDbContext())
                {
                    return await db.Matches.Include(
                        m => m.timeline.frames.Select(
                            y => y.events.Select(z => z.position))).ToListAsync();    
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            
        }

        internal static async Task<List<GameData>> GetGameDataToDownload()
        {
            try
            {
                var time = DateTime.Now;
                using (var db = new LoLDbContext())
                {
                    return await db.GameData.Where(g => g.Downloaded == false && g.StartQueringTime < time).ToListAsync();
                }
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
                using (var db = new LoLDbContext())
                {
                    db.Matches.Add(match);
                    await db.SaveChangesAsync();
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        internal IEnumerable<Match> GetMatches()
        {
            using (var db = new LoLDbContext())
            {
                return db.Matches;
            }
        }
    }

}
