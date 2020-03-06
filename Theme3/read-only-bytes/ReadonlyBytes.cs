using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace hashes
{
	public class ReadonlyBytes : IEnumerable<byte>
	{
		private byte[] array;

		private int hash = 1;

		private bool hashIsValid;
		
		public int Length { get => array.Length; }

		public ReadonlyBytes(params byte[] arr)
		{
			if (arr == null) throw new ArgumentNullException();
			array = arr;
		}

		public byte this[int index] 
		{ 
			get
			{
				if (index > -1)
					return array[index];
				else
					throw new IndexOutOfRangeException();
			}
		}

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
				//hash = 1;
				//for (int i = 0; i < array.Length; i++)
				//	MakeHash(array[i]);
				//return hash;
				if (hashIsValid) return hash;
				hashIsValid = true;
				//hash = 1;
				for (int i = 0; i < array.Length; i++)
					hash = MakeHash(array[i]); //hash * 1337 ^ array[i];//
				return hash;
			}
		}

		private int MakeHash(byte element)
		{
			var h = 1337 ^ element;

			return hash * h;
		}

		public override string ToString() => string.Format("[{0}]", string.Join(", ", array));
	}
}