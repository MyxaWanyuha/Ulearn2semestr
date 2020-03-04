using System.Collections.Generic;


namespace TodoApplication
{
    public class ListModel<T>
    {
        enum Action { Added, Removed };

        struct ChangedItem
        {
            public Action Act;
            public T Item;
            public int Index;
            public ChangedItem(Action act1, T item1, int index1)
            {
                Act = act1;
                Item = item1;
                Index = index1;
            }
        }

        private LimitedSizeStack<ChangedItem> changedItems;
        public List<T> Items { get; }
        public int Limit;

        public ListModel(int limit)
        {
            changedItems = new LimitedSizeStack<ChangedItem>(limit);
            Items = new List<T>();
            Limit = limit;
        }

        public void AddItem(T item)
        {
            changedItems.Push(new ChangedItem(Action.Added, item, Items.Count));
            Items.Add(item);
        }

        public void RemoveItem(int index)
        {
            changedItems.Push(new ChangedItem(Action.Removed, Items[index], index));
            Items.RemoveAt(index);
        }

        public bool CanUndo()
        {
            return changedItems.Count != 0;
        }

        public void Undo()
        {
            if (CanUndo())
            {
                var lastIndex = changedItems.Count - 1;
                var e = changedItems.Pop();
                switch (e.Act)
                {
                    case Action.Added:
                        Items.RemoveAt(Items.Count - 1);
                        break;
                    case Action.Removed:
                        Items.Insert(e.Index, e.Item);
                        break;
                }
            }
        }
    }
}