using System.Drawing;
using System.Linq;
using System;
namespace func_rocket
{
	public class ForcesTask
	{
		public static RocketForce GetThrustForce(double forceValue)
		{
			return r =>
			{
				var vector = new Vector(forceValue, 0);
				var cosDir = Math.Cos(r.Direction);
				var sinDir = Math.Sin(r.Direction);
				var y = cosDir * vector.Y + sinDir * vector.X;
				var x = cosDir * vector.X - sinDir * vector.Y;
				return new Vector(x, y);
			};
		}

		public static RocketForce ConvertGravityToForce(Gravity gravity, Size spaceSize) 
			=> r => gravity(spaceSize, r.Location);

		public static RocketForce Sum(params RocketForce[] forces)
			=> r =>
			{
				var vector = new Vector(0, 0);
				foreach (var e in forces)
					vector += e.Invoke(new Rocket(vector, vector, 0));
				return vector;
			};
	}
}