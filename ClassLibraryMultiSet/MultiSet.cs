using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryMultiSet
{
    public class MultiSet<T>: IMultiSet<T>
    {
        private readonly Dictionary<T, int> mset;

        #region Constructors

        public MultiSet()
        {
            mset = new Dictionary<T, int>();
        }
        public MultiSet(IEqualityComparer<T> comparer)
        {
            mset = new Dictionary<T, int>(comparer);
        }
        public MultiSet(IEnumerable<T> sequence): this()
        {
            foreach (var item in sequence.Distinct().ToList())
            {
                mset.Add(item, sequence.Count(x => x.Equals(item)));
            }
        }
        public MultiSet(IEnumerable<T> sequence, IEqualityComparer<T> comparer)
        {
            mset = new Dictionary<T, int>(comparer);
            
            foreach (var item in sequence.Distinct().ToList())
            {
                mset.Add(item, sequence.Count(x => x.Equals(item)));
            }
        }

        #endregion

        #region ICollections
        
        public void Add(T item) => Add(item, 1);
        public bool Remove(T item) => mset.Remove(item);
        public bool IsReadOnly => false;
        public int Count() => mset.Values.Sum();
        public void Clear() => mset.Clear();
        public bool Contains(T item) => mset.ContainsKey(item);
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array.Length < arrayIndex + Count())
                throw new IndexOutOfRangeException("Array is too small");

            int i = 0;
            foreach (var item in this)
                array[arrayIndex + i++] = item;
        }
        int ICollection<T>.Count => Count();

        #endregion

        #region IEnumerable
        
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<T> GetEnumerator() => new MultiSetEnumerator(mset);
        public class MultiSetEnumerator : IEnumerator<T>
        {
            private readonly T[] _data;
            private int _currentIndex = -1;
            public MultiSetEnumerator(Dictionary<T, int> mset)
            {
                _data = new T[mset.Sum(x => x.Value)];
                var index = 0;

                foreach (KeyValuePair<T, int> item in mset)
                {
                    for (int i = 0; i < item.Value; i++)
                    {
                        _data[index] = item.Key;
                        index++;
                    }
                }
            }
            public T Current => _data[_currentIndex];
            object IEnumerator.Current => _data[_currentIndex];
            public void Dispose() { }
            public bool MoveNext()
            {
                if (_currentIndex + 1 != _data.Length)
                {
                    _currentIndex++;
                    return true;
                }
                return false;
            }
            public void Reset() => _currentIndex = -1;

        }
        #endregion

        #region IMultiSet

        public bool IsEmpty => mset.Count == 0;
        public IEqualityComparer<T> Comparer => mset.Comparer;
        public override string ToString()
        {
            var str = new StringBuilder();
            foreach (var (key, value) in mset)
                str.Append($"{key}: {value}, ");

            return str.Length == 0 ? null : str.ToString(0, str.Length - 2);
        }
        public int this[T item]
        {
            get
            {
                if (mset.ContainsKey(item) == false) return 0;
                return mset[item];
            }
        }
        
        public static IMultiSet<T> Empty => new MultiSet<T>();
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
            if (IsReadOnly) throw new NotSupportedException();
            if (mset.ContainsKey(item) == false) return this;

            if (mset[item] <= numberOfItems)
                RemoveAll(item);
            else
                mset[item] -= numberOfItems;

            return this;
        }
        public IMultiSet<T> RemoveAll(T item)
        {
            if (IsReadOnly) throw new NotSupportedException();
            if (mset.ContainsKey(item) == false) return this;
            
            mset.Remove(item);
            return this;
        }

        public IReadOnlyDictionary<T, int> AsDictionary() => mset.ToDictionary(x => x.Key, x => x.Value);
        public IReadOnlySet<T> AsSet() => mset.Keys.ToHashSet();

        public IMultiSet<T> ExceptWith(IEnumerable<T> other)
        {
            if (this.IsReadOnly == true)
                throw new NotSupportedException();
            if (other is null)
                throw new ArgumentNullException();

            foreach (var item in this.Distinct().ToList())
            {
                if (other.Contains(item))
                    this.RemoveAll(item);
            }

            return this;
        }
        public IMultiSet<T> IntersectWith(IEnumerable<T> other)
        {
            if (this.IsReadOnly == true)
                throw new NotSupportedException();
            if (other is null)
                throw new ArgumentNullException();

            foreach (var item in this.Distinct().ToList())
            {
                if (!other.Contains(item))
                    this.RemoveAll(item);
            }

            return this;
        }
        public IMultiSet<T> UnionWith(IEnumerable<T> other)
        {
            if (this.IsReadOnly == true)
                throw new NotSupportedException();
            if (other is null)
                throw new ArgumentNullException();
            
            foreach (var item in other)
            {
                this.Add(item, 1);
            }

            return this;
        }
        public IMultiSet<T> SymmetricExceptWith(IEnumerable<T> other)
        {
            if (this.IsReadOnly == true)
                throw new NotSupportedException();
            if (other is null)
                throw new ArgumentNullException();

            foreach (var item in other.Distinct().ToList())
            {
                if (this.Contains(item))
                    this.RemoveAll(item);
                else
                    this.Add(item, 1);
            }

            return this;
        }

        public bool IsProperSubsetOf(IEnumerable<T> other) => this.IsSubsetOf(other) && this.Distinct().Count() < other.Distinct().Count();
        public bool IsProperSupersetOf(IEnumerable<T> other) => this.IsSupersetOf(other) && this.Distinct().Count() > other.Distinct().Count();
        public bool IsSubsetOf(IEnumerable<T> other)
        {
            if (other is null)
                throw new ArgumentNullException();

            return this.All(item => other.Contains(item) != false);
        }
        public bool IsSupersetOf(IEnumerable<T> other)
        {
            if (other is null)
                throw new ArgumentNullException();

            return other.All(item => this.Contains(item) != false);
        }

        public bool MultiSetEquals(IEnumerable<T> other)
        {
            if (other is null)
                throw new ArgumentNullException();
            if (this.Count() != other.Count()) 
                return false;

            foreach (var item in this.AsSet()) // AsSet żeby nie mieć duplikatów
            {
                if (this.Contains(item) == false)
                    return false;
                
                if (this[item] != other.Count(x => x.Equals(item)))
                    return false;
            }
            return true;
        }
        public bool Overlaps(IEnumerable<T> other)
        {
            if (other is null)
                throw new ArgumentNullException();

            return other.Any(item => this.Contains(item));
        }
        
        public static IMultiSet<T> operator +(MultiSet<T> first, MultiSet<T> second)
        {
            if (first is null || second is null)
                throw new ArgumentNullException();

            var newMs = new MultiSet<T>(first);

            foreach (var item in second)
                newMs.Add(item);

            return newMs;
        }
        public static IMultiSet<T> operator -(MultiSet<T> first, MultiSet<T> second)
        {
            if (first is null || second is null)
                throw new ArgumentNullException();

            var newMs = new MultiSet<T>(first);

            foreach (var item in second)
                newMs.Remove(item, 1);

            return newMs;
        }
        public static IMultiSet<T> operator *(MultiSet<T> first, MultiSet<T> second)
        {
            if (first is null || second is null)
                throw new ArgumentNullException();

            var newMs = new MultiSet<T>(first);

            foreach (var item in second.Distinct().ToList())
                if (newMs.Contains(item))
                {
                    while (newMs[item] > second[item])
                        newMs.Remove(item, 1);
                }

            foreach (var item in newMs.Distinct().ToList())
            {
                if (second.Contains(item) == false)
                    newMs.RemoveAll(item);
            }

            return newMs;
        }

        #endregion

        #region Other

        public class MultiSetComparer : IEqualityComparer<MultiSet<T>>
        {
            public bool Equals(MultiSet<T> x, MultiSet<T> y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.mset == y.mset;
            }

            public int GetHashCode(MultiSet<T> obj) => obj.mset != null ? obj.mset.GetHashCode() : 0;
            
        }

        #endregion
    }
}
