using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
	public class ParsingTask
	{
		public static IDictionary<int, SlideRecord> ParseSlideRecords(IEnumerable<string> lines)
		{
			return lines
				.Select(file => file.Split(';'))
				.Where(line => line.Length == 3 && GetSlideType(line[1]) != -1)
				.Select(sr => MakeSlideRecord(sr))
				.Where(slide => slide != null)
				.ToDictionary(sid => sid.SlideId, slide => slide);
		}

		private static int GetSlideType(string str)
		{
			switch (str)
			{
				case "quiz":
					return (int)SlideType.Quiz;
				case "theory":
					return (int)SlideType.Theory;
				case "exercise":
					return (int)SlideType.Exercise;
				default:
					return -1;
			} 
		}

		private static SlideRecord MakeSlideRecord(string[] line)
		{
			int id;
			var isNum = int.TryParse(line[0], out id);
			if (isNum)
				return new SlideRecord(id, (SlideType)GetSlideType(line[1]), line[2]);
			return null;
		}

		public static IEnumerable<VisitRecord> ParseVisitRecords(
			IEnumerable<string> lines, IDictionary<int, SlideRecord> slides)
		{
			return lines
				.Where(str => str != "UserId;SlideId;Date;Time")
				.Select(str => ParseLine(str, slides));
		}

		private static VisitRecord ParseLine(string line, IDictionary<int, SlideRecord> slides)
		{
			var data = line.Split(';');
			var userId = 0;
			var slideId = 0;
			var date = default(DateTime);
			if (data.Length == 4)
			{
				var isValidUserId = int.TryParse(data[0], out userId);
				var isValidSlideID = int.TryParse(data[1], out slideId);
				var isValidDate = DateTime.TryParse(data[2], out date);
				var isValidTime = DateTime.TryParse(data[3], out date);
				if (isValidUserId && isValidSlideID
					&& isValidDate && isValidTime
					&& slides.ContainsKey(slideId))
					return new VisitRecord(userId, slideId,
						DateTime.Parse(data[2] + ' ' + data[3]),
						slides[slideId].SlideType);
			}
			throw new FormatException("Wrong line [" + line + "]");
		}
	}
}