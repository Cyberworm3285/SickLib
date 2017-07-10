using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using static SickLib.LINQ_Extensions.TypeConverter;

namespace SickLib.Collections.SickLinkedList
{
    public enum Insertion
    {
        InFront,
        Before
    }

    public class SickLinkedList<T> : IList<T>
    {
        public int Count { get; private set; }
        public SickLinkedNode<T> Current { get; private set; }
        public SickLinkedNode<T> Head { get; private set; }
        public SickLinkedNode<T> Tail { get; private set; }

        #region Constrctors

        public SickLinkedList() { }

        public SickLinkedList(IEnumerable<T> en)
        {
            using (var e = en.GetEnumerator())
            {
                e.MoveNext();
                Current = new SickLinkedNode<T>(e.Current);
                Head = Current;
                Count = 1;
                while (e.MoveNext())
                {
                    Current.Next = new SickLinkedNode<T>(
                        item: e.Current,
                        p: Current
                        );
                    Current = Current.Next;
                    Count++;
                }
                Tail = Current;
            }
        }

        #endregion

        #region Methods

        public bool MoveToPosition(int index)
        {
            if (index >= Count || index < 0)
                return false;
            if (index < Count / 2)
            {
                Current = Head;
                for (var i = 0; i < index; i++)
                    Current = Current.Next;
            }
            else
            {
                Current = Tail;
                for (var i = 0; i < Count - index - 1; i++)
                    Current = Current.Prev;
            }

            return true;
        }

        public bool MoveNext()
        {
            if (Current == Tail)
                return false;
            else
                Current = Current.Next;
            return true;
        }

        public bool MovePrev()
        {
            if (Current == Head)
                return false;
            else
                Current = Current.Prev;
            return true;
        }

        public void MoveToHead() => Current = Head;

        public void MoveToTail() => Current = Tail;

        public void InsertAt(int index, T value, Insertion insertion = Insertion.Before)
        {
            if (index < 0 || index > Count)
                throw new IndexOutOfRangeException();
            if (index == Count)
            {
                Add(value);
                return;
            }
            if (index == 0)
            {
                Head = new SickLinkedNode<T>(
                    item: value,
                    n: Head
                    );
                Head.Next.Prev = Head;
            }
            else if (index == Count - 1)
            {
                Tail = new SickLinkedNode<T>(
                    item:  value,
                    p: Tail
                    );
                Tail.Prev.Next = Tail;
            }
            else
            {
                var temp = Current;
                MoveToPosition(index);
                switch (insertion)
                {
                    case Insertion.InFront:
                        Current.Next = new SickLinkedNode<T>(
                            item: value,
                            n: Current.Next,
                            p: Current
                            );
                        Current.Next.Next.Prev = Current.Next;
                        break;
                    case Insertion.Before:
                        Current.Prev = new SickLinkedNode<T>(
                            item: value,
                            n: Current,
                            p: Current.Prev
                            );
                        Current.Prev.Prev.Next = Current.Prev;
                        break;
                }
                Current = temp;
            }

            Count++;
        }

        private void Init(T item)
        {
            Current = new SickLinkedNode<T>(item);
            Head = Current;
            Tail = Current;
            Count = 1;
        }

        public void Sort()
        {
            var temp = this.OrderBy(e => e).ToSickLinkedList();
            Head = temp.Head;
            Tail = temp.Tail;
            Current = Head;
        }

        #endregion

        #region IList<T> implementation

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException();
            if (index == 0)
            {
                Head = Head.Next;
            }
            else if (index == Count - 1)
            {
                Tail = Tail.Prev;
            }
            else
            {
                var temp = Current;
                MoveToPosition(index);
                if (temp == Current)        //falls der momentane Knoten gelöscht wird
                    temp = Current.Next;
                Current.Prev.Next = Current.Next;
                Current.Next.Prev = Current.Prev;
                Current = temp;
            }

            Count--;
        }

        public bool IsReadOnly => false;

        public T this[int index]
        {
            get
            {
                MoveToPosition(index);
                return Current.Item;
            }
            set
            {
                if (index < 0)
                     throw new IndexOutOfRangeException();
                if (index >= Count)
                {
                    if (Count == 0)
                        if (index == 0)
                        {
                            Init(value);
                            return;
                        }
                        else
                        {
                            Init(default(T));
                        }
                    Current = Tail;
                    for (var i = 0; i < index - Count; i++)
                    {
                        Current.Next = new SickLinkedNode<T>(
                            item: default(T),
                            p: Current
                            );
                        Current = Current.Next;
                    }
                    Current.Next = new SickLinkedNode<T>(
                        item: value,
                        p:Current
                        );
                    Current = Current.Next;
                    Tail = Current;
                    Count = index + 1;
                }
                else
                {
                    MoveToPosition(index);
                    Current.Item = value;
                }
            }
        }

        public int IndexOf(T item)
        {
            var index = 0;
            Current = Head;
            for (var i = 0; i < Count; i++)
            {
                if (Current.Item.Equals(item))
                    return index;
                else
                    index++;
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            InsertAt(index, item);
        }

        public void Add(T item)
        {
            if (Count == 0)
                Init(item);
            else
            {
                Tail.Next = new SickLinkedNode<T>(
                    item: item,
                    p: Tail
                    );
                Tail = Tail.Next;
                Count++;
            }
        }

        public void Clear()
        {
            Current = null;
            Head = null;
            Tail = null;
            Count = 0;
        }

        public bool Contains(T item)
        {
            Current = Head;
            for (var i = 0; i < Count; i++)
            {
                if (Current.Item.Equals(item))
                    return true;
                else
                    Current = Current.Next;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Current = Head;
            for (var i = 0; i < Count; i++)
            {
                array[i + arrayIndex] = Current.Item;
                Current = Current.Next;
            }
        }

        public bool Remove(T item)
        {
            Current = Head;
            for (var i = 0; i < Count; i++)
            {
                if (!Current.Item.Equals(item)) continue;
                if (Current == Head)
                {
                    Head = Head.Next;
                }
                else if (Current == Tail)
                {
                    Tail = Tail.Prev;
                }
                else
                {
                    Current.Prev.Next = Current.Next;
                    Current.Next.Prev = Current.Prev;
                    Current = Current.Next;
                }
                Count--;
                return true;
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new SickLinkedListEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new SickLinkedListEnumerator<T>(this);
        }

        #endregion

        #region Operators

        public static SickLinkedList<T> operator +(SickLinkedList<T> a, T b)
        {
            a.Add(b);
            return a;
        }

        public static SickLinkedList<T> operator -(SickLinkedList<T> a, T b)
        {
            a.Remove(b);
            return a;
        }

        #endregion
    }
}
