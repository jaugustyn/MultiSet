using System;
using System.Collections.Generic;
using ClassLibraryMultiSet;

namespace MultiSetApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var ms = new MultiSet<char>();
            ms.Add('a', 2);
            ms.Add('a', 2);
            ms.Add('b', 1);
            ms.Add('c', 5);
            ms.Add('d', 13);
            Console.WriteLine(ms);
            
            ms.Remove('d', 10);
            Console.WriteLine(ms);
            
            ms.RemoveAll('c');
            Console.WriteLine(ms);

            Console.WriteLine(ms['v']);
            Console.WriteLine(ms['b']);
            Console.WriteLine(ms['d']);

            char[] array = new char[15];
            ms.CopyTo(array, 3);
            foreach (var c in array)
            {
                Console.Write(c + "|");
            }
            Console.WriteLine();
            
            var dict = ms.AsDictionary();
            Console.WriteLine(dict.GetType());
            
            var set = ms.AsSet();
            Console.WriteLine(set.GetType());
            foreach (var c in set)
            {
                Console.Write(c + "|");
            }
            Console.WriteLine();

            var ms2 = new MultiSet<char>() {{'a',5}, {'v',2}, 'i', 'x', 'b'};
            var ms3 = new MultiSet<char>() { 'v',{ 'a', 3 }, 'i', 'x', 'b', 'd' };
            var list = new List<char>() {'a', 'a', 'a', 'v', 'i', 'x', 'b', 'd', 'a'};
            char[] arr = new[] {'a', 'a', 'a', 'v','v','v', 'i', 'x', 'b', 'd', 'a'};
            
            // Console.WriteLine(ms.ExceptWith(ms2));
            // Console.WriteLine(ms.IntersectWith(ms2));
            // Console.WriteLine(ms.UnionWith(ms2));
            // Console.WriteLine(ms.SymmetricExceptWith(ms2));
            // Console.WriteLine(ms.IsProperSupersetOf(ms2));
            Console.WriteLine(ms2.MultiSetEquals(list));

            var msZListy = new MultiSet<char>(arr);
            foreach (var VARIABLE in msZListy)
            {
                Console.Write(VARIABLE + " ");
            }
            Console.WriteLine();

            var test = new MultiSet<char>(arr, comparer: null);
            
            var t2 = test * ms2;
            Console.WriteLine(t2);
            Console.WriteLine(test);
            Console.WriteLine(ms2);
        }
    }
}
