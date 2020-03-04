using System.Collections.Generic;

namespace yield
{
	public static class MovingMaxTask
	{
		public static IEnumerable<DataPoint> MovingMax
			(this IEnumerable<DataPoint> data, int windowWidth)
		{
			var maxs = new LinkedList<double>();
			var elems = new Queue<double>();
			foreach (var e in data)
			{
				elems.Enqueue(e.OriginalY);

				while(maxs.Count > 0 && maxs.Last.Value < e.OriginalY)
					maxs.RemoveLast();
				maxs.AddLast(e.OriginalY);

				if (elems.Count > windowWidth && elems.Dequeue() == maxs.First.Value)
					maxs.RemoveFirst();

				e.MaxY = maxs.First.Value;
				yield return e;
			}
		}
	}
}