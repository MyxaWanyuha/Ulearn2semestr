using System;
using System.Collections.Generic;
namespace TodoApplication
{
    public class LimitedSizeStack<T>
    {
        private int limit;
        LinkedList<T> stack = new LinkedList<T>();

        public LimitedSizeStack(int limit)
        {
            if (limit > 0)
            {
                this.limit = limit;
            }
            else throw new Exception();
        }

        public void Push(T item)
        {
            if (stack.Count >= limit) stack.RemoveFirst();
            stack.AddLast(item);
        }

        public T Pop()
        {
            var last = stack.Last.Value;
            stack.RemoveLast();
            return last;
        }

        public int Count { get { return stack.Count; } }
    }
}
