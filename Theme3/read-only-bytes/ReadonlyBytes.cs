using System;
using System.Collections;
using System.Collections.Generic;

namespace hashes
{
	public class ReadonlyBytes : IEnumerable<byte>
	{
		private byte[] array;

		private int hash = 1;
		
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
				if (index > -1 && index < array.Length)
					return array[index];
				else
					throw new IndexOutOfRangeException();
			}
		}

        public IEnumerator<byte> GetEnumerator() => ((IEnumerable<byte>)array).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<byte>)array).GetEnumerator();

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
                if (hash != 1) return hash;

				for (int i = 0; i < array.Length; i++)
					hash = MakeHash(array[i]);
                return hash;
			}
		}

		private int MakeHash(byte element)
        {
            unchecked
            {
                return hash * 1337 ^ element;
            }
		}

		public override string ToString() => string.Format("[{0}]", string.Join(", ", array));
	}
}