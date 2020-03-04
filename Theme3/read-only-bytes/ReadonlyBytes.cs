using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace hashes
{
	public class ReadonlyBytes : IEnumerable<byte>
	{
		private byte[] array;

		private bool hashIsValid;

		private int hash = 1;

		public int Length { get => array.Length; }

		public ReadonlyBytes(params byte[] arr)
		{
			if (arr == null) throw new ArgumentNullException();
			array = arr;
		}

		public byte this[int index] { get => array[index]; }

		public IEnumerator<byte> GetEnumerator()
		{
			return ((IEnumerable<byte>)array).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<byte>)array).GetEnumerator();
		}

		public override bool Equals(object obj)
		{
			var castObj = obj as ReadonlyBytes;
			if (castObj == null 
				|| castObj.Length != array.Length 
				|| obj.GetType() != this.GetType())
				return false;

			for (int i = 0; i < array.Length; i++)
				if (castObj[i] != array[i])
					return false;
			return true;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				if (hashIsValid) return hash;
				hashIsValid = true;

				for (int i = 0; i < array.Length; i++)
					hash = hash * 1337 ^ array[i];
				return hash;
			}
		}

		public override string ToString()
		{
			if (array.Length == 0) return "[]";

			var sb = new StringBuilder("[");
			for (int i = 0; i < array.Length; i++)
			{
				sb.Append(array[i].ToString());
				if(i != array.Length - 1)
					sb.Append(", ");
				else
					sb.Append("]");
			}
			return sb.ToString();
		}
	}
}