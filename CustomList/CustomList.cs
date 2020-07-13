using System;
using System.Collections;
using System.Collections.Generic;

namespace CustomList
{
    public class CustomList<T> : IList<T>
    {
        /// <summary>
        /// The property return first element of list
        /// </summary>
        public Item<T> Head { get; private set; }

        /// <summary>
        /// The property return number of elements contained in the CustomList
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the IList is read-only.
        /// Make it always false
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Constructor that gets params T as parameter
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when values is null</exception>
        /// <param name="values"></param>
        public CustomList(params T[] values)
        {
            if (values is null)
                throw new ArgumentNullException(nameof(values));

            foreach (var item in values)
            {
                Add(item);
            }
        }

        /// <summary>
        /// Constructor that gets Ienumerable collection as parameter
        /// </summary>
        ///<exception cref="ArgumentNullException">Thrown when values is null</exception>
        /// <param name="values"></param>
        public CustomList(IEnumerable<T> values)
        {
            if (values is null)
                throw new ArgumentNullException(nameof(values));

            foreach (var item in values)
            {
                Add(item);
            }
        }

        /// <summary>
        /// Get or set data at the position.
        /// </summary>
        /// <param name="index">Position</param>
        /// <exception cref="IndexOutOfRangeException">Throw when index is less than 0 or greater than Count - 1</exception>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index > Count-1)
                    throw new IndexOutOfRangeException(nameof(index)) as Exception;

                int count = 0;
                Item<T> current = Head;
                while (current != null && count <= index)
                {
                    if (count == index)
                        return current.Data;
                    else
                        current = current.Next;
                    ++count;
                }
                return default;
            }
            set
            {
                if (index < 0 || index > Count - 1)
                    throw new IndexOutOfRangeException(nameof(index)) as Exception;

                int count = 0;
                Item<T> current = Head;
                while (current != null && count <= index)
                {
                    if (count == index)
                        current.Data = value;
                    else
                        current = current.Next;
                    ++count;
                }
            }
        }

        /// <summary>
        ///  Adds an object to the end of the CustomList.
        /// </summary>
        /// <param name="item">Object that should be added in the CustomList</param>
        /// <exception cref="ArgumentNullException">Throws when you try to add null</exception>
        public void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            Item<T> addItem = new Item<T>(item);
            Item<T> current = Head;

            if (Head == null)
            {
                Head = addItem;
            }
            else
            {
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = addItem;
                current.Next.Data = addItem.Data;
            }
            Count++;
        }

        /// <summary>
        /// Removes all elements from the CustomList
        /// </summary>
        public void Clear()
        {
            Head = null;
            Count = 0;
        }

        /// <summary>
        /// Determines whether an element is in the CustomList
        /// </summary>
        /// <param name="item">Object we check to see if it is on the CustomLIst</param>
        /// <returns>True if the element exists in the CustomList, else false</returns>
        public bool Contains(T item)
        {
            if (Head is null)
                return false;
            if (Head.Data.Equals(item))
                return true;

            Item<T> current = Head;
            while (current.Next != null)
            {
                if (current.Next.Data.Equals(item))
                    return true;
                else
                    current = current.Next;
            }
            return false;
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the CustomList.
        /// </summary>
        /// <param name="item"> The object to remove from the CustomList</param>
        /// <returns>True if item is successfully removed; otherwise, false. This method also returns
        ///     false if item was not found in the CustomList.</returns>
        /// <exception cref="ArgumentNullException">Throws when you try to remove null</exception>
        public bool Remove(T item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            if (Contains(item))
            {
                RemoveAt(IndexOf(item));
                return true;
            }
            return false;
        }

        /// <summary>
        /// Searches for the specified object and returns the zero-based index of the first
        ///     occurrence within the CustomList.
        /// </summary>
        /// <param name="item">The object whose index we want to get </param>
        /// <returns>The zero-based index of the first occurrence of item within the entire CustomList,
        ///    if found; otherwise, -1.</returns>
        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i].Equals(item))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Inserts an element into the CustomList at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="item">The object to insert.</param>
        /// <exception cref="ArgumentOutOfRangeException">Throw when index is less than 0 or greater than Count - 1</exception>
        /// <exception cref="ArgumentNullException">Thrown when item is null</exception>
        public void Insert(int index, T item)
        {
            if (index < 0 || index > Count)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (item is null)
                throw new ArgumentNullException(nameof(index));

            Item<T> current = Head;

            int countIndex = 1;
            do
            {
                if (index == 0)
                {
                    Head.Data = item;
                    Head.Next = current;
                    break;
                }
                else if (index == countIndex)
                {
                    if (current.Next != null)
                    {
                        Item<T> temp = current.Next;
                        current.Next.Data = item;
                        current.Next.Next = temp;
                    }
                    else
                    {
                        Item<T> temp = new Item<T>(item);
                        current.Next = temp;
                    }
                    break;
                }
                else
                {
                    current = current.Next;
                }
                ++countIndex;
            }
            while (countIndex <= Count);

            ++Count;
        }

        /// <summary>
        /// Removes the element at the specified index of the CustomList.
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        /// <exception cref="ArgumentOutOfRangeException">Throw when index is less than 0 or greater than Count - 1</exception>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            T item = this[index];

            Item<T> current = Head;
            do
            {
                if (Head.Data.Equals(item))
                {
                    if (Head.Next is null)
                        Head = null;
                    else
                        Head = Head.Next;
                    break;
                }
                else if (current.Next.Data.Equals(item))
                {
                    if (current.Next.Next != null)
                        current.Next = current.Next.Next;
                    else
                        current.Next = null;
                    break;
                }
                else
                {
                    current = current.Next;
                }
            }
            while (current.Next != null);

            Count--;
        }

        /// <summary>
        /// Copies the entire CustomList to a compatible one-dimensional array, starting at the beginning of the target array.
        /// </summary>
        /// <param name="array">The one-dimensional System.Array that is the destination of the elements copied
        ///     from CustomList</param>
        /// <param name="arrayIndex">The zero-based index in the source System.Array at which
        ///   copying begins.</param>
        ///   <exception cref="ArgumentNullException">Array is null.</exception>
        ///   <exception cref="ArgumentException">The number of elements in the source CustomList is greater
        ///    than the number of elements that the destination array can contain</exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));
            if (array.Length < Count)
                throw new ArgumentException(nameof(array));

            for (int i = arrayIndex, j = 0; i < Count + arrayIndex; i++, j++)
            {
                array[i] = this[j];
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the CustomList.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            Item<T> current = Head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
        }
    }
}