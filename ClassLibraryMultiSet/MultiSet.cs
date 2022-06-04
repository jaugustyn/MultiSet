using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryMultiSet
{
    public class MultiSet<T>: IMultiSet<T>
    {
        // wewnetrzna reprezentacja danych
        private Dictionary<T, int> mset = new Dictionary<T, int>();

        #region Constructors
        public MultiSet() { }

        #endregion

        #region ICollections
        public void Add(T item) => Add(item, 1);
        public bool Remove(T item) => mset.Remove(item);
        public bool IsReadOnly => false;
        public int Count() => mset.Values.Sum();
        public void Clear() => mset.Clear();
        public bool Contains(T item) => mset.ContainsKey(item);
        public void CopyTo(T[] array, int arrayIndex) // TO DO, wymaga indexowania (raczej do zmiany)
        {
        /*    for (int i = 0; i < Count(); i++)
            {
                array[arrayIndex + i] = mset[i];
            }
        */
        }

        #endregion

        #region IMultiSet

        public bool IsEmpty => mset.Count == 0;
        public override string ToString()
        {
            var str = new StringBuilder();
            foreach (var (key, value) in mset)
                str.Append($"{key}: {value}, ");

            return str.ToString(0, str.Length - 2);
        }
        public IMultiSet<T> Add(T item, int numberOfItems = 1)
        {
            if (mset.ContainsKey(item))
                mset[item] += numberOfItems;
            else
                mset.Add(item, numberOfItems);

            return this;
        }
        public IMultiSet<T> Remove(T item, int numberOfItems = 1)
        {
            if (mset.ContainsKey(item) == false) return this;

            if (mset[item] < numberOfItems)
                Remove(item);
            else
                mset[item] -= numberOfItems;

            return this;
        }
        public IMultiSet<T> RemoveAll(T item)
        {
            if (mset.ContainsKey(item) == false) return this;
            Remove(item);
            return this;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        int ICollection<T>.Count => Count();
        public int this[T item] // todo
        {
            get
            {
                if (mset.ContainsKey(item) == false) return 0;
                return mset[item];
            }
        }

        #endregion

        //////////// Todo ////////////////
        public IEqualityComparer<T> Comparer => throw new NotImplementedException();

        public IReadOnlyDictionary<T, int> AsDictionary()
        {
            throw new NotImplementedException();
        }

        public IReadOnlySet<T> AsSet()
        {
            throw new NotImplementedException();
        }


        public IMultiSet<T> ExceptWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public IMultiSet<T> IntersectWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool MultiSetEquals(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public IMultiSet<T> SymmetricExceptWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

        public IMultiSet<T> UnionWith(IEnumerable<T> other)
        {
            throw new NotImplementedException();
        }

    }
}
