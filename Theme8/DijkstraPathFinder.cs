using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Greedy.Architecture;
using System.Drawing;

namespace Greedy
{
	class DijkstraData
	{
		public Point Previous { get; set; }
		public double Price { get; set; }
	}

	public class DijkstraPathFinder
    {
        public IEnumerable<PathWithCost> GetPathsByDijkstra(State state, Point start,
            IEnumerable<Point> targets)
        {
            var chests = new HashSet<Point>(state.Chests);
			var track = new Dictionary<Point, DijkstraData>();
			track[start] = new DijkstraData { Price = 0, Previous = Point.Empty };
			var visited = new List<Point>();

			foreach (var e in targets)
			{
				var bestPrice = double.PositiveInfinity;
				var toOpen = new Point();
				toOpen = Point.Empty;

				foreach (var t in track)
				{
					if (!visited.Contains(t.Key) && t.Value.Price < bestPrice)
					{
						bestPrice = t.Value.Price;
						toOpen = t.Key;
					}
				}

				if (toOpen == Point.Empty) break;
				yield return new PathWithCost();

				if (toOpen == e)
				{
					foreach (var t in )
					{
						var currentPrice = track[toOpen].Price + weights[t];
						var nextNode = t.OtherNode(toOpen);
						if (!track.ContainsKey(nextNode) || track[nextNode].Price > currentPrice)
						{
							track[nextNode] = new DijkstraData { Previous = toOpen, Price = currentPrice };
						}
					}

					notVisited.Remove(toOpen);
				}
			}
		}
    }
}
/*
class DijkstraData
{
	public Node Previous { get; set; }
	public double Price { get; set; }
}

public class Program
{
	public static List<Node> Dijkstra(Graph graph, Dictionary<Edge, double> weights, Node start, Node end)
	{
		var notVisited = graph.Nodes.ToList();
		var track = new Dictionary<Node, DijkstraData>();
		track[start] = new DijkstraData { Price = 0, Previous = null };

		while (true)
		{
			Node toOpen = null;
			var bestPrice = double.PositiveInfinity;
			foreach (var e in notVisited)
			{
				if (track.ContainsKey(e) && track[e].Price < bestPrice)
				{
					bestPrice = track[e].Price;
					toOpen = e;
				}
			}

			if (toOpen == null) return null;
			if (toOpen == end) break;

			foreach (var e in toOpen.IncidentEdges.Where(z => z.From == toOpen))
			{
				var currentPrice = track[toOpen].Price + weights[e];
				var nextNode = e.OtherNode(toOpen);
				if (!track.ContainsKey(nextNode) || track[nextNode].Price > currentPrice)
				{
					track[nextNode] = new DijkstraData { Previous = toOpen, Price = currentPrice };
				}
			}

			notVisited.Remove(toOpen);
		}

		var result = new List<Node>();
		while (end != null)
		{
			result.Add(end);
			end = track[end].Previous;
		}
		result.Reverse();
		return result;
	}

	[Test]
	public static void Main()
	{
		var graph = new Graph(4);
		var weights = new Dictionary<Edge, double>();
		weights[graph.Connect(0, 1)] = 1;
		weights[graph.Connect(0, 2)] = 2;
		weights[graph.Connect(0, 3)] = 6;
		weights[graph.Connect(1, 3)] = 4;
		weights[graph.Connect(2, 3)] = 2;

		var path = Dijkstra(graph, weights, graph[0], graph[3]).Select(n => n.NodeNumber);
		CollectionAssert.AreEqual(new[] { 0, 2, 3 }, path);
	}
}*/