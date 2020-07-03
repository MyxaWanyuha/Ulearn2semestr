using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
	public class StatisticsTask
	{
		public static double GetMedianTimePerSlide(List<VisitRecord> visits, SlideType slideType)
		{
			return visits
				.OrderBy(item => item.DateTime)
				.GroupBy(item => item.UserId)
				.SelectMany(group => group.Bigrams().Where(tuple => tuple.Item1.SlideType == slideType))
				.Select(tuple => tuple.Item2.DateTime.Subtract(tuple.Item1.DateTime).TotalMinutes)
				.Where(minutes => minutes >= 1 && minutes <= 120)
				.DefaultIfEmpty(0)
				.Median();
		}
	}
}