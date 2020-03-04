using System;
using System.Collections.Generic;

namespace Clones
{
	public class SimplyList<T>
	{
		public class ListItem
		{
			public T Value;
			public ListItem Next;
			public ListItem(T value, ListItem next)
			{
				Value = value;
				Next = next;
			}
		}

		public ListItem Last;
		public SimplyList() { }
		public SimplyList(SimplyList<T> copy)
		{
			Last = copy.Last;
		}

		public void Push(T item)
		{
			Last = new ListItem(item, Last);
		}

		public T Pop()
		{
			var res = Last.Value;
			Last = Last.Next;
			return res;
		}
	}

	public class CloneVersionSystem : ICloneVersionSystem
	{
		private List<Clone> clones;
		private Clone validClone;

		public CloneVersionSystem()
		{
			clones = new List<Clone>();
			clones.Add(new Clone());
		}


		private class Clone
		{
			public SimplyList<string> Programs;
			public SimplyList<string> DeletedPrograms;
			public Clone()
			{
				Programs = new SimplyList<string>();
				DeletedPrograms = new SimplyList<string>();
			}

			public Clone(Clone parentClone)
			{
				Programs = new SimplyList<string>(parentClone.Programs);
				DeletedPrograms = new SimplyList<string>(parentClone.DeletedPrograms);
			}
		}

		public string Execute(string query)
		{
			var command = query.Split(' ');
			var number = Convert.ToInt32(command[1]) - 1;
			validClone = clones[number];

			switch (command[0])
			{
				case "learn":
					learn(command[2]);
					break;
				case "check":
					return check();
				case "clone":
					clones.Add(new Clone(validClone));
					break;
				case "relearn":
					relearn();
					break;
				case "rollback":
					rollback();
					break;
			}
			return null;
		}

		private void learn(string program)
		{
			validClone.Programs.Push(program);
			validClone.DeletedPrograms.Last = null;
		}

		private void rollback()
		{
			if (validClone.Programs.Last != null)
				validClone.DeletedPrograms.Push(validClone.Programs.Pop());
		}

		private void relearn()
		{
			if (validClone.DeletedPrograms.Last != null)
				validClone.Programs.Push(validClone.DeletedPrograms.Pop());
		}
		private string check()
		{
			if (validClone.Programs.Last != null)
				return validClone.Programs.Last.Value;
			return "basic";
		}
	}
}