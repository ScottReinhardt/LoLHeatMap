using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LoLJson
{
    public class Parse
    {
        public static async Task<IEnumerable<Point>> GetPoints(string fileName)
        {
            return await GetAllPoints(fileName);
        }

        public async static Task<IEnumerable<Point>> GetAllPoints(string fileName)
        {
            var points = await JsonConvert.DeserializeObjectAsync<List<Point>>(File.ReadAllText(fileName));

            return points;
        }

        public static IEnumerable<Point> GroupPoints(IEnumerable<Point> ungroupedPoints, int matches)
        {
            var points = new List<Point>();
            foreach (var pointGroup in ungroupedPoints.GroupBy(p => new { p.X, p.Y }))
            {
                var pointTotal = pointGroup.Sum(p => p.PlayerCount);
                var point = pointGroup.First();
                point.PlayerCount = pointTotal / matches;
                points.Add(point);
            }
            return points;
        }

    }

    public class Point
    {
        public Point(Event e)
        {
            X = e.position.x;
            Y = e.position.y;
            if (e.assistingParticipantIds == null)
            {
                PlayerCount = 1;
            }
            else
            {
                PlayerCount = 1 + e.assistingPlayerCount;
            }
        }

        public Point(int x, int y, int count)
        {
            X = x;
            Y = y;
            PlayerCount = count;
        }

        public Point()
        {
            
        }

        public int X;
        public int Y;
        public double PlayerCount;
    }
}
