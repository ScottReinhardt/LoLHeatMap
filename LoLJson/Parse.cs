using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace LoLJson
{
    public class Parse
    {
        public static IEnumerable<Point> GetPoints(string fileName)
        {
            var points = GetAllPoints(fileName);

            return GroupPoints(points, points.Count());
        }

        public static IEnumerable<Point> GetAllPoints(string fileName)
        {
            var points = new List<Point>();
            var data = File.ReadAllLines(fileName);

            points.AddRange(data.Select(l =>
            {
                var split = l.Split(' ').Select(int.Parse).ToArray();
                return new Point(split[0], split[1], split[2]);
            }));


            return points;
        }

        private static IEnumerable<Point> GroupPoints(IEnumerable<Point> ungroupedPoints, int matches)
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
        public Point(Position p, IEnumerable<int?> playerInts)
        {
            X = p.x;
            Y = p.y;
            if (playerInts == null)
            {
                PlayerCount = 1;
            }
            else
            {
                PlayerCount = 1 + playerInts.Count();
            }
        }

        public Point(int x, int y, int count)
        {
            X = x;
            Y = y;
            PlayerCount = count;
        }

        public int X;
        public int Y;
        public double PlayerCount;
    }
}
