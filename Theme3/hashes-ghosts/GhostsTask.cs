using System;
using System.Text;

namespace hashes
{
	public class GhostsTask :
		IFactory<Document>, IFactory<Vector>, IFactory<Segment>, IFactory<Cat>, IFactory<Robot>,
		IMagic
	{
		private static Vector vector = new Vector(14, 88);
		private Segment segment = new Segment(vector, new Vector(322, 228));
		private Cat cat = new Cat("Soviet Union", "country", new DateTime(1922, 12, 30));
		private Robot robot = new Robot("Autobot", 322);
		private static byte[] bytes = { 54, 27, 14, 88 };

		public void DoMagic()
		{
			var rand = new Random();
			var randNum = rand.Next();
			bytes[randNum % 4] = (byte)randNum;
			//vector = vector.Add(new Vector(randNum, randNum));
			vector.Add(new Vector(2, 2));
			//segment.Start.Add(new Vector(randNum, randNum));
			cat.Rename(randNum.ToString());
			Robot.BatteryCapacity -= randNum;
		}

		public Document Create() => new Document("Doc", Encoding.ASCII, bytes);

		Vector IFactory<Vector>.Create() => vector;

		Segment IFactory<Segment>.Create() => segment;

		Cat IFactory<Cat>.Create() => cat;

		Robot IFactory<Robot>.Create() => robot;
	}
}