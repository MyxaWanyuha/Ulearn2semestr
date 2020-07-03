using System.Collections.Generic;
using System.Drawing;

namespace Rivals
{
	public class RivalsTask
	{
		private static readonly Point[] offsetToDirection = new Point[]
		{
			new Point(0, -1),
			new Point(0, 1),
			new Point(-1, 0),
			new Point(1, 0)
		};

		public static IEnumerable<OwnedLocation> AssignOwners(Map map)
		{
			var queue = new Queue<OwnedLocation>();
			var visitedPoints = new HashSet<Point>();

			for (int i = 0; i < map.Players.Length; i++)
				queue.Enqueue(new OwnedLocation(i, map.Players[i], 0));

			while (queue.Count != 0)
			{
				var path = queue.Dequeue();
				if (IsBadLocation(map, visitedPoints, path)) continue;
				visitedPoints.Add(path.Location);
				yield return path;

				foreach (var e in offsetToDirection)
				{
					var p = new Point(path.Location.X + e.X, path.Location.Y + e.Y);
					if (!visitedPoints.Contains(p))
					{
						var newLocation = new OwnedLocation(path.Owner,	p, path.Distance + 1);
						queue.Enqueue(newLocation);
					}
				}
			}
		}

		private static bool IsBadLocation(Map map, HashSet<Point> visitedPoints, OwnedLocation path)
		{
			return visitedPoints.Contains(path.Location)
				   || !map.InBounds(path.Location)
				   || map.Maze[path.Location.X, path.Location.Y] == MapCell.Wall;
		}
	}
}