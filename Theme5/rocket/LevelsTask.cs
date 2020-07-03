using System;
using System.Collections.Generic;

namespace func_rocket
{
	public class LevelsTask
	{
		static readonly Physics standardPhysics = new Physics();
		static Rocket rocket = new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI);
		static Vector target = new Vector(600, 200);

		public static IEnumerable<Level> CreateLevels()
		{
			yield return CreateLevel("Zero", (size, v) => Vector.Zero, rocket, target);
			yield return CreateLevel("Heavy", (size, v) => new Vector(0, 0.9), rocket, target);
			yield return CreateLevel("Up", (size, v) => new Vector(0, -300 / (300 + (size.Height - v.Y))), 
				rocket, new Vector(700, 500));
			yield return CreateLevel("WhiteHole", (size, v) => WhiteHoleForce(v, target), rocket, target);
			yield return CreateLevel("BlackHole", (size, v) => BlackHoleForce(v, target, rocket.Location), rocket, target);
			yield return CreateLevel("BlackAndWhite", (size, v) => BlackAndWhite(v, target, rocket.Location), rocket, target);
		}

		public static Level CreateLevel(
			string name, Gravity gravity, Rocket rocket = null, Vector target = null)
		{
			return new Level(name, rocket, target, gravity, standardPhysics);
		}

		public static Vector WhiteHoleForce(Vector v, Vector target)
		{
			var d = (v - target).Length;
			return (v - target).Normalize() * 140 * d / (d * d + 1);
		}

		public static Vector BlackHoleForce(Vector v, Vector target, Vector rocketLocation) 
		{
			var bLoc = (target + rocketLocation) / 2;
			var d = (bLoc - v).Length;
			return (bLoc - v).Normalize() * 300 * d / (d * d + 1);
		}

		public static Vector BlackAndWhite(Vector v, Vector target, Vector rocketLocation)
		{
			var wForce = WhiteHoleForce(v, target);
			var bForce = BlackHoleForce(v, target, rocketLocation);
			return (wForce + bForce) / 2;
		}
	}
}