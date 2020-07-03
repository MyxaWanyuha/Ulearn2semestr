using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace rocket_bot
{
    public class Channel<T> where T : class
    {
        private List<T> list = new List<T>();

        public T this[int index]
        {
            get
            {
                lock (list)
                {
                    if (index >= 0 && index < list.Count)
                        return list[index];
                    return null;
                }
            }
            set
            {
                lock (list)
                {
                    if (index >= 0 && index < list.Count)
                    {
                        list[index] = value;
                        list.RemoveRange(index + 1, list.Count - index - 1);
                    }
                    else if (index == list.Count)
                        list.Add(value);
                }
            }
        }

        public T LastItem()
        {
            lock (list)
            {
                if (list.Count > 0)
                    return list[list.Count - 1];
                return null;
            }
        }

        public void AppendIfLastItemIsUnchanged(T item, T knownLastItem)
        {
            lock (list)
            {
                if ((knownLastItem == null && list.Count == 0)
                    || list[list.Count - 1] == knownLastItem)
                    list.Add(item);
            }
        }

        public int Count
        {
            get
            {
                lock (list)
                {
                    return list.Count;
                }
            }
        }
    }
}