using System.Collections.Generic;

namespace yield
{
	public static class MovingAverageTask
	{
		public static IEnumerable<DataPoint> MovingAverage
			(this IEnumerable<DataPoint> data, int windowWidth)
		{
			var sum = 0.0;
			var queue = new Queue<double>();
			foreach(var e in data)
			{
				var res = new DataPoint { X = e.X, OriginalY = e.OriginalY };

				if(queue.Count == windowWidth)
					sum -= queue.Dequeue();

				queue.Enqueue(e.OriginalY);
				sum += e.OriginalY;
				res.AvgSmoothedY = sum / queue.Count; 
				yield return res;
			}
		}
	}
}
/*
		public double Measure()
		{
			var value = sensor.Measure();

			queue.Enqueue(value);
			sum += value;

			if (queue.Count > bufferLength)
				sum -= queue.Dequeue();

			return sum / queue.Count;
		}
*/