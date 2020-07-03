using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
	public static class ExtensionsTask
	{
		public static double Median(this IEnumerable<double> items)
		{
			var itemsList = items.ToList();
			if (itemsList.Count == 0)
				throw new InvalidOperationException();

			itemsList.Sort();
			var halfCount = itemsList.Count / 2;

			if (itemsList.Count % 2 == 0)
				return (itemsList[halfCount] + itemsList[halfCount - 1]) / 2;
			return itemsList[halfCount];
		}

		public static IEnumerable<Tuple<T, T>> Bigrams<T>(this IEnumerable<T> items)
		{
			var item1 = default(T);
			var isFirst = true;
			foreach (var item2 in items)
			{
				if(isFirst)
				{
					item1 = item2;
					isFirst = false;
					continue;
				}
				yield return Tuple.Create(item1, item2);
				item1 = item2;
			}
		}
	}
}