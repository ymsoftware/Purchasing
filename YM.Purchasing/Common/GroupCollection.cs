using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YM.Purchasing.Common
{
    class GroupCollection<TItem,TGroup>
    {
        private List<GroupCollectionGroup<TGroup>> _groups;
        private List<GroupCollectionItem<TItem>> _items;

        public GroupCollection<TItem, TGroup> AddItem(TItem item)
        {
            EnsureItems();

            var add = new GroupCollectionItem<TItem>()
            {
                Item = item,
                Index = _items.Count,
                GroupIndex = -1
            };

            _items.Add(add);

            return this;
        }

        public GroupCollection<TItem, TGroup> AddItem(TItem item, int groupIndex)
        {
            EnsureItems();
            EnsureGroups();

            if (groupIndex < 0 || groupIndex >= _groups.Count)
            {
                throw new ArgumentException(string.Format("invalid index {0}", groupIndex));
            }

            var add = new GroupCollectionItem<TItem>()
            {
                Item = item,
                GroupIndex = groupIndex
            };

            var first = _items.FirstOrDefault(e => e.GroupIndex > groupIndex || e.GroupIndex == -1);
            if (first == null)
            {
                add.Index = _items.Count;
                _items.Add(add);
            }
            else
            {
                add.Index = first.Index;

                foreach (var i in _items.Where(e => e.Index >= add.Index))
                {
                    i.Index++;
                }

                _items.Insert(add.Index, add);
            }                       

            var group = _groups[groupIndex];
            group.ItemCount++; 

            return this;
        }

        public GroupCollection<TItem, TGroup> MoveItem(int itemIndex, int groupIndex)
        {
            EnsureItems();
            EnsureGroups();

            if (itemIndex < 0 || itemIndex >= _items.Count)
            {
                throw new ArgumentException(string.Format("invalid index {0}", itemIndex));
            }

            if (groupIndex < 0 || groupIndex > _groups.Count)
            {
                throw new ArgumentException(string.Format("invalid index {0}", groupIndex));
            }

            var item = _items[itemIndex];
            if (item.GroupIndex != groupIndex)
            {
                RemoveItem(itemIndex);
                return AddItem(item.Item, groupIndex);
            }

            return this;
        }

        public GroupCollection<TItem, TGroup> RemoveItem(int itemIndex)
        {
            EnsureItems();

            if (itemIndex < 0 || itemIndex >= _items.Count)
            {
                throw new ArgumentException(string.Format("invalid index {0}", itemIndex));
            }

            bool reorder = itemIndex < _items.Count - 1;
            int groupIndex = _items[itemIndex].GroupIndex;

            _items.RemoveAt(itemIndex);

            if (reorder)
            {
                foreach (var i in _items.Where(e => e.Index >= itemIndex))
                {
                    i.Index--;
                }
            }

            if (groupIndex >= 0)
            {
                EnsureGroups();

                _groups.First(e => e.Index == itemIndex).ItemCount--;
            }

            return this;
        }

        public GroupCollection<TItem, TGroup> AddGroup(TGroup group)
        {
            EnsureGroups();
            EnsureItems();

            var add = new GroupCollectionGroup<TGroup>()
            {
                Group = group,
                Index = _groups.Count
            };

            _groups.Add(add);

            return this;
        }

        public GroupCollection<TItem, TGroup> RemoveGroup(int groupIndex)
        {
            EnsureGroups();

            if (groupIndex < 0 || groupIndex > _groups.Count)
            {
                throw new ArgumentException(string.Format("invalid index {0}", groupIndex));
            }

            bool reorder = groupIndex < _groups.Count - 1;

            _groups.RemoveAt(groupIndex);

            EnsureItems();

            var move = _items.Where(e => e.GroupIndex == groupIndex).ToArray();
            foreach (var item in move)
            {
                _items.RemoveAt(item.Index);
                item.GroupIndex = -1;
                _items.Add(item);
            }

            return this;
        }

        public TItem[] Items
        {
            get
            {
                EnsureItems();
                return _items.Select(e => e.Item).ToArray();
            }
        }

        public TGroup[] Groups
        {
            get
            {
                EnsureGroups();
                return _groups.Select(e => e.Group).ToArray();
            }
        }

        private void EnsureItems()
        {
            if (_items == null)
            {
                _items = new List<GroupCollectionItem<TItem>>();
            }
        }

        private void EnsureGroups()
        {
            if (_groups == null)
            {
                _groups = new List<GroupCollectionGroup<TGroup>>();
            }
        }
    }

    class GroupCollectionItem<T>
    {
        public T Item { get; set; }
        public int Index { get; set; }
        public int GroupIndex { get; set; }
    }

    class GroupCollectionGroup<T>
    {
        public T Group { get; set; }
        public int Index { get; set; }
        public int ItemCount { get; set; }
    }
}
