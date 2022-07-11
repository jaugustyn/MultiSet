using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using ClassLibraryMultiSet;


namespace MultiSetTests
{
    [TestClass]
    public class MultiSetUnitTests
    {
        # region Constructors
        
        [TestCategory("Constructors")]
        [TestMethod]
        public void Empty_Multiset()
        {
            var ms1 = new MultiSet<int>();
            var arr = Array.Empty<int>();
            var ms2 = new MultiSet<int>(arr);

            Assert.AreEqual(typeof(MultiSet<int>), ms1.GetType());
            Assert.AreEqual(typeof(MultiSet<int>), ms2.GetType());
            Assert.AreEqual(ms1.Count(), 0);
            Assert.AreEqual(ms2.Count(), 0);
        }
        
        [TestCategory("Constructors")]
        [TestMethod]
        public void Valid_Multiset()
        {
            var ms1 = new MultiSet<int> { 1, 2, 3, 4, -5 };
            var ms2 = new MultiSet<int> { 0, 0, 0, 1, 2 };
            var ms3 = new MultiSet<char> { 'a', 'b', 'c', 'd', 'd', 'd' };
            var ms4 = new MultiSet<char> { 'a', 'b', 'c', {'d',5} };

            Assert.AreEqual(typeof(MultiSet<int>), ms1.GetType());
            Assert.AreEqual(typeof(MultiSet<int>), ms2.GetType());
            Assert.AreEqual(typeof(MultiSet<char>), ms3.GetType());
            Assert.AreEqual(typeof(MultiSet<char>), ms4.GetType());
            Assert.AreEqual(5, ms1.Count());
            Assert.AreEqual(5, ms2.Count());
            Assert.AreEqual(6, ms3.Count());
            Assert.AreEqual(8, ms4.Count());
        }
        
        [TestCategory("Constructors")]
        [TestMethod]
        public void Valid_Sequence()
        {
            var arr1 = new []{ 1, 2, 3, 4, -5 };
            var arr2 = new[] { 0, 0, 0, 1, 2 };
            var arr3 = new[] { 'a', 'b', 'c', 'd', 'd', 'd' };

            var ms1 = new MultiSet<int>(arr1);
            var ms2 = new MultiSet<int>(arr2);
            var ms3 = new MultiSet<char>(arr3);

            Assert.AreEqual(typeof(MultiSet<int>), ms1.GetType());
            Assert.AreEqual(typeof(MultiSet<int>), ms2.GetType());
            Assert.AreEqual(typeof(MultiSet<char>), ms3.GetType());
            Assert.AreEqual(5, ms1.Count());
            Assert.AreEqual(5, ms2.Count());
            Assert.AreEqual(6, ms3.Count());
        }
        #endregion

        #region ICollections Multiset

        [TestCategory("ICollections")]
        [TestMethod]
        public void Add_Item()
        {
            var ms1 = new MultiSet<int>();
            ms1.Add(1);
            ms1.Add(1, 1);
            ms1.Add(10, 3);

            Assert.AreEqual("1: 2, 10: 3", ms1.ToString());
            Assert.AreEqual(5, ms1.Count());
        }

        [TestCategory("ICollections")]
        [TestMethod]
        public void Remove_Item()
        {
            var ms1 = new MultiSet<int>(){1, {2, 6}, 1500};
            ms1.Remove(1);
            ms1.Remove(1);
            ms1.Remove(2, 2);

            Assert.AreEqual("2: 4, 1500: 1", ms1.ToString());
            Assert.AreEqual(5, ms1.Count());
        }
        
        [TestCategory("ICollections")]
        [TestMethod]
        public void RemoveAll_Item()
        {
            var ms1 = new MultiSet<int>() { 1, { 2, 6 }, 1500 };
            ms1.RemoveAll(2);

            Assert.AreEqual("1: 1, 1500: 1", ms1.ToString());
            Assert.AreEqual(2, ms1.Count());
        }

        [TestCategory("ICollections")]
        [TestMethod]
        public void Clear_Multiset()
        {
            var ms1 = new MultiSet<int>() { 1, { 2, 6 }, 1500 };
            ms1.Clear();

            Assert.AreEqual(null, ms1.ToString());
            Assert.AreEqual(0, ms1.Count());
        }
        
        [TestCategory("ICollections")]
        [TestMethod]
        public void Contains_Item()
        {
            var ms1 = new MultiSet<int>() { 1, { 2, 6 }, 1500 };
            Assert.AreEqual(true, ms1.Contains(1));
            Assert.AreEqual(false, ms1.Contains(0));
        }

        [TestCategory("ICollections")]
        [TestMethod]
        public void CopyTo_Array_From_Multiset()
        {
            var ms1 = new MultiSet<int>() { 1, { 2, 6 }, 1500 };
            int[] arr = new int[100];
            int[] arr2 = new int[100];

            ms1.CopyTo(arr, 0);
            ms1.CopyTo(arr2, 5);
            
            Assert.AreEqual(1, arr[0]);
            Assert.AreEqual(0, arr2[0]);
            Assert.AreEqual(1, arr2[5]);
        }
        
        [TestCategory("ICollections")]
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void CopyTo_Array_From_Multiset_Invalid_Index()
        {
            var ms1 = new MultiSet<int>() { 1, { 2, 6 }, 1500 };
            int[] arr = new int[10];
            int[] arr2 = new int[10];

            ms1.CopyTo(arr, -1);
            ms1.CopyTo(arr2, 15);

            Assert.AreEqual(1, arr[0]);
            Assert.AreEqual(0, arr2[0]);
            Assert.AreEqual(1, arr2[5]);
        }

        #endregion

        #region IEnumerable Mutliset

        [TestCategory("IEnumerable")]
        [TestMethod]
        public void Foreach_Multiset()
        {
            var ms1 = new MultiSet<int>() { 1, { 2, 6 }, 1500 };
            var result = new StringBuilder();

            foreach (var item in ms1)
                result.Append(item + " ");

            result.Length--;
            Assert.AreEqual("1 2 2 2 2 2 2 1500", result.ToString());
        }

        #endregion

        #region IMultiSet Multiset

        [TestCategory("IMultiSet")]
        [TestMethod]
        public void IsEmpty_Multiset()
        {
            var ms1 = new MultiSet<int>();
            var ms2 = new MultiSet<int>(){1};
            Assert.AreEqual(true, ms1.IsEmpty);
            Assert.AreEqual(false, ms2.IsEmpty);
        }


        [TestCategory("IMultiSet")]
        [TestMethod]
        public void ToString_Multiset()
        {
            var ms1 = new MultiSet<int>();
            var ms2 = new MultiSet<int>() { 1 };
            var ms3 = new MultiSet<int>() { {1,6}, 6, -2, 2000 };

            Assert.AreEqual(null, ms1.ToString());
            Assert.AreEqual("1: 1", ms2.ToString());
            Assert.AreEqual("1: 6, 6: 1, -2: 1, 2000: 1", ms3.ToString());
        }
        
        [TestCategory("IMultiSet")]
        [TestMethod]
        public void GetEmpty_Multiset()
        {
            IMultiSet<int> ms1 = MultiSet<int>.Empty;
            Assert.AreEqual(null, ms1.ToString());
            Assert.AreEqual(0, ms1.Count);
        }
        
        [TestCategory("IMultiSet")]
        [TestMethod]
        public void Item_indexer_Multiset()
        {
            var ms1 = new MultiSet<int>() { { 1, 6 }, 6, -2, 2000 };
            
            Assert.AreEqual(0, ms1[0]);
            Assert.AreEqual(1, ms1[6]);
            Assert.AreEqual(6, ms1[1]);
        }

        [TestCategory("IMultiSet")]
        [TestMethod]
        public void Mutltiset_AsDictionary()
        {
            var ms1 = new MultiSet<int>() { { 1, 6 }, 6, -2, 2000 };

            var dict = ms1.AsDictionary();
            Assert.AreEqual(typeof(Dictionary<int, int>), dict.GetType());
            Assert.AreEqual(6, dict[1]);
        }
        
        [TestCategory("IMultiSet")]
        [TestMethod]
        public void Mutltiset_AsSet()
        {
            var ms1 = new MultiSet<int>() { { 1, 6 }, 6, -2, 2000 };

            var set = ms1.AsSet();
            Assert.AreEqual(typeof(HashSet<int>), set.GetType());
            Assert.AreEqual(4, set.Count); // 4, a nie 9 bo Set nie przyjmuje duplikatów
        }

        [TestCategory("IMultiSet")]
        [TestMethod]
        public void ExceptWith_Multiset()
        {
            var ms1 = new MultiSet<int>() { { 1, 6 }, 6, -2, 2000 };
            var ms2 = new MultiSet<int>() { { 1, 6 }, 6, -2, 2000 };
            var ms3 = new MultiSet<int>() { { 1, 6 }, 6, -2, 2000 };
            var ms4 = new MultiSet<int>() { { 1, 4 }, 6, -6, 1166 };
            
            Assert.AreEqual(null, ms1.ExceptWith(ms2).ToString());
            Assert.AreEqual("-2: 1, 2000: 1", ms3.ExceptWith(ms4).ToString());
        }
        
        [TestCategory("IMultiSet")]
        [TestMethod]
        public void IntersectWith_Multiset()
        {
            var ms1 = new MultiSet<int>() { { 1, 6 }, 6, -2, 2000 };
            var ms2 = new MultiSet<int>() { { 1, 6 }, 6, -2, 2000 };
            var ms3 = new MultiSet<int>() { { 1, 6 }, 6, -2, 2000 };
            var ms4 = new MultiSet<int>() { { 1, 4 }, 6, -6, 1166 };

            Assert.AreEqual("1: 6, 6: 1, -2: 1, 2000: 1", ms1.IntersectWith(ms2).ToString());
            Assert.AreEqual("1: 6, 6: 1", ms3.IntersectWith(ms4).ToString());
        }

        [TestCategory("IMultiSet")]
        [TestMethod]
        public void UnionWith_Multiset()
        {
            var ms1 = new MultiSet<int>() { { 1, 6 }, 6, -2, 2000 };
            var ms2 = new MultiSet<int>() { { 1, 6 }, 6, -2, 2000 };
            var ms3 = new MultiSet<int>() { { 1, 6 }, 6, -2, 2000 };
            var ms4 = new MultiSet<int>() { { 1, 4 }, 6, -6, 1166 };

            Assert.AreEqual("1: 12, 6: 2, -2: 2, 2000: 2", ms1.UnionWith(ms2).ToString());
            Assert.AreEqual("1: 10, 6: 2, -2: 1, 2000: 1, -6: 1, 1166: 1", ms3.UnionWith(ms4).ToString());
        }

        [TestCategory("IMultiSet")]
        [TestMethod]
        public void SymmetricExceptWith_Multiset()
        {
            var ms1 = new MultiSet<int>() { { 1, 6 }, 6, -2, 2000 };
            var ms2 = new MultiSet<int>() { { 1, 6 }, 6, -2, 2000 };
            var ms3 = new MultiSet<int>() { { 1, 6 }, 6, -2, 2000 };
            var ms4 = new MultiSet<int>() { { 1, 4 }, 6, -6, 1166 };

            Assert.AreEqual(null, ms1.SymmetricExceptWith(ms2).ToString());
            Assert.AreEqual("1166: 1, -6: 1, -2: 1, 2000: 1", ms3.SymmetricExceptWith(ms4).ToString());
        }

        [TestCategory("IMultiSet")]
        [TestMethod]
        public void IsSubsetOf_Multiset()
        {
            var ms1 = new MultiSet<int>() { { 1, 6 }, 6, -2, 2000 };
            var ms2 = new MultiSet<int>() { { 1, 6 }, 6, -2, 2000 };
            var ms3 = new MultiSet<int>() { { 1, 6 }, 6, -2, 2000 };
            var ms4 = new MultiSet<int>() { { 1, 4 }, 6, -6, 1166 };
            var ms5 = new MultiSet<int>() { 1 };

            Assert.AreEqual(true, ms1.IsSubsetOf(ms2));
            Assert.AreEqual(false, ms3.IsSubsetOf(ms4));
            Assert.AreEqual(true, ms5.IsSubsetOf(ms1));
            Assert.AreEqual(false, ms1.IsSubsetOf(ms5));
        }

        [TestCategory("IMultiSet")]
        [TestMethod]
        public void IsProperSubsetOf_Multiset()
        {
            var ms1 = new MultiSet<int>() { { 1, 6 }, 6, -2, 2000 };
            var ms2 = new MultiSet<int>() { { 1, 6 }, 6, -2, 2000 };
            var ms3 = new MultiSet<int>() { { 1, 7 }, 6, -2, 2000, 3 };
            var ms4 = new MultiSet<int>() { { 1, 4 }, 6, -6, 1166 };
            var ms5 = new MultiSet<int>() { 1 };

            Assert.AreEqual(false, ms1.IsProperSubsetOf(ms2)); // ms1 nie jest podzbiorem w³aœciwym ms2 ze wzglêdu na tak¹ sam¹ liczebnoœæ elementów
            Assert.AreEqual(true, ms1.IsProperSubsetOf(ms3));
            Assert.AreEqual(false, ms3.IsProperSubsetOf(ms4));
            Assert.AreEqual(true, ms5.IsProperSubsetOf(ms1));
            Assert.AreEqual(false, ms1.IsProperSubsetOf(ms5));
        }

        [TestCategory("IMultiSet")]
        [TestMethod]
        public void IsSupersetOf_Multiset()
        {
            var ms1 = new MultiSet<int>() { { 1, 6 }, 6, -2, 2000 };
            var ms2 = new MultiSet<int>() { { 1, 6 }, 6, -2, 2000 };
            var ms3 = new MultiSet<int>() { { 1, 6 }, 6, -2, 2000 };
            var ms4 = new MultiSet<int>() { { 1, 4 }, 6, -6, 1166 };
            var ms5 = new MultiSet<int>() { 1 };

            Assert.AreEqual(true, ms1.IsSupersetOf(ms2));
            Assert.AreEqual(false, ms3.IsSupersetOf(ms4));
            Assert.AreEqual(false, ms5.IsSupersetOf(ms1));
            Assert.AreEqual(true, ms1.IsSupersetOf(ms5));
        }

        [TestCategory("IMultiSet")]
        [TestMethod]
        public void IsProperSupersetOf_Multiset()
        {
            var ms1 = new MultiSet<int>() { { 1, 6 }, 6, -2, 2000 };
            var ms2 = new MultiSet<int>() { { 1, 6 }, 6, -2, 2000 };
            var ms3 = new MultiSet<int>() { { 1, 7 }, 6, -2, 2000, 3 };
            var ms4 = new MultiSet<int>() { { 1, 4 }, 6, -6, 1166 };
            var ms5 = new MultiSet<int>() { 1 };

            Assert.AreEqual(false, ms1.IsProperSupersetOf(ms2)); // ms1 nie jest nadzbiorem w³aœciwym ms2 ze wzglêdu na tak¹ sam¹ liczebnoœæ elementów
            Assert.AreEqual(false, ms1.IsProperSupersetOf(ms3));
            Assert.AreEqual(false, ms3.IsProperSupersetOf(ms4));
            Assert.AreEqual(false, ms5.IsProperSupersetOf(ms1));
            Assert.AreEqual(true, ms1.IsProperSupersetOf(ms5));
        }
        #endregion

        #region Operators of Multiset

        [TestCategory("IMultiSet")]
        [TestMethod]
        public void Plus_Operator_Multiset()
        {
            var ms1 = new MultiSet<int>() {{1, 6}, 6, -2, 2000};
            var ms2 = new MultiSet<int>() {{1, 6}, 6, -2, 2000};
            var ms3 = new MultiSet<int>() {{1, 4}, 6, -6, 1166};
            var ms4 = new MultiSet<int>() {1};

            Assert.AreEqual("1: 12, 6: 2, -2: 2, 2000: 2", (ms1 + ms2).ToString());
            Assert.AreEqual("1: 5, 6: 1, -6: 1, 1166: 1", (ms3 + ms4).ToString());
            Assert.AreEqual("1: 10, 6: 2, -2: 1, 2000: 1, -6: 1, 1166: 1", (ms1 + ms3).ToString());
            Assert.AreEqual("1: 2", (ms4 + ms4).ToString());
        }

        [TestCategory("IMultiSet")]
        [TestMethod]
        public void Minus_Operator_Multiset()
        {
            var ms1 = new MultiSet<int>() { { 1, 6 }, 6, -2, 2000 };
            var ms2 = new MultiSet<int>() { { 1, 6 }, 6, -2, 2000 };
            var ms3 = new MultiSet<int>() { { 1, 4 }, 6, -6, 1166 };
            var ms4 = new MultiSet<int>() { 1 };

            Assert.AreEqual(null, (ms1 - ms2).ToString());
            Assert.AreEqual("1: 3, 6: 1, -6: 1, 1166: 1", (ms3 - ms4).ToString());
            Assert.AreEqual("1: 2, -2: 1, 2000: 1", (ms1 - ms3).ToString());
            Assert.AreEqual(null, (ms4 - ms4).ToString());
        }
        
        [TestCategory("IMultiSet")]
        [TestMethod]
        public void Multiplication_Operator_Multiset()
        {
            var ms1 = new MultiSet<int>() { { 1, 6 }, 6, -2, 2000 };
            var ms2 = new MultiSet<int>() { { 1, 6 }, 6, -2, 2000 };
            var ms3 = new MultiSet<int>() { { 1, 4 }, 6, -6, 1166 };
            var ms4 = new MultiSet<int>() { 1 };

            Assert.AreEqual("1: 6, 6: 1, -2: 1, 2000: 1", (ms1 * ms2).ToString());
            Assert.AreEqual("1: 1", (ms3 * ms4).ToString());
            Assert.AreEqual("1: 4, 6: 1", (ms1 * ms3).ToString());
            Assert.AreEqual("1: 1", (ms4 * ms4).ToString());
        }

        [TestCategory("IMultiSet")]
        [TestMethod]
        public void MutltiSetEquals()
        {
            var ms1 = new MultiSet<int>() { { 1, 6 }, 6, -2, 2000 };
            var ms2 = new MultiSet<int>() { { 1, 6 }, 6, -2, 2000 };
            var ms3 = new MultiSet<int>() { { 1, 4 }, 6, -6, 1166 };
            var ms4 = new MultiSet<int>() { 1 };
            var ms5 = new MultiSet<int>();

            Assert.AreEqual(true, ms1.MultiSetEquals(ms2));
            Assert.AreEqual(false, ms1.MultiSetEquals(ms3));
            Assert.AreEqual(false, ms3.MultiSetEquals(ms4));
            Assert.AreEqual(true, ms4.MultiSetEquals(ms4));
            Assert.AreEqual(true, ms5.MultiSetEquals(ms5));
            Assert.AreEqual(false, ms1.MultiSetEquals(ms5));
        }

        [TestCategory("IMultiSet")]
        [TestMethod]
        public void Overlaps_Multiset()
        {
            var ms1 = new MultiSet<int>() { { 1, 6 }, 6, -2, 2000 };
            var ms2 = new MultiSet<int>() { { 1, 6 }, 6, -2, 2000 };
            var ms3 = new MultiSet<int>() { { 1, 4 }, 6, -6, 1166 };
            var ms4 = new MultiSet<int>() { 1 };
            var ms5 = new MultiSet<int>();

            Assert.AreEqual(true, ms1.Overlaps(ms2));
            Assert.AreEqual(true, ms1.Overlaps(ms3));
            Assert.AreEqual(true, ms3.Overlaps(ms4));
            Assert.AreEqual(true, ms4.Overlaps(ms4));
            Assert.AreEqual(false, ms5.Overlaps(ms5));
            Assert.AreEqual(false, ms1.Overlaps(ms5));
        }

        [TestCategory("IMultiSet")]
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Other_Multiset_ArgNullException()
        {
            var ms1 = new MultiSet<int>() { { 1, 6 }, 6, -2, 2000 };
            var ms2 = new MultiSet<int>() { 1 };
            var ms3 = new MultiSet<int>();

            ms1.ExceptWith(null);
            ms2.UnionWith(null);
            ms3.SymmetricExceptWith(null);
            ms1.IntersectWith(null);
            ms2.IsSubsetOf(null);
            ms3.IsProperSubsetOf(null);
            ms1.IsSupersetOf(null);
            ms2.IsProperSupersetOf(null);
            ms3.Overlaps(null);
            ms1.MultiSetEquals(null);
            ms2.MultiSetEquals(null);
            ms3.MultiSetEquals(null);
        }
        #endregion
    }
}
