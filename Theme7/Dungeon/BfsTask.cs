using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Dungeon
{
	public class BfsTask
	{
	    public static IEnumerable<SinglyLinkedList<Point>> FindPaths(Map map, Point start, Point[] chests)
        {
			var queue = new Queue<SinglyLinkedList<Point>>();
			var pointList = new SinglyLinkedList<Point>(start, null);
			var visitedPoints = new HashSet<Point>() { start };
			queue.Enqueue(pointList);
			var route = Walker.PossibleDirections.Select(p => new Point(p));
			while (queue.Count != 0)
			{
				var path = queue.Dequeue();
				if (!map.InBounds(path.Value)
					|| map.Dungeon[path.Value.X, path.Value.Y] == MapCell.Wall) continue;
				if (chests.Contains(path.Value)) yield return path;

				foreach (var e in route)
				{
					var p = new Point(path.Value.X + e.X, path.Value.Y + e.Y);
					if (!visitedPoints.Contains(p))
					{
						queue.Enqueue(new SinglyLinkedList<Point>(p, path));
						visitedPoints.Add(p);
					}
				}
			}
		}
	}
}