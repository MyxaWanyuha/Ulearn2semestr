using System.Collections.Generic;

namespace yield
{
	public static class ExpSmoothingTask
	{
		public static IEnumerable<DataPoint> SmoothExponentialy
			(this IEnumerable<DataPoint> data, double alpha)
		{
			var lastY = double.NaN;
			foreach (var e in data)
			{
				var res = new DataPoint { X = e.X, OriginalY = e.OriginalY };
				if (double.IsNaN(lastY))
				{
					lastY = e.OriginalY;
					res.ExpSmoothedY = e.OriginalY;
					yield return res;
				}
				else
				{
					res.ExpSmoothedY = lastY + alpha * (e.OriginalY - lastY);
					lastY = res.ExpSmoothedY;
					yield return res;
				}
			}
		}
	}
}