using System;
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
            ms.Add('c');
            ms.Add('d', 13);

            Console.WriteLine(ms);
            ms.Remove('a');
            Console.WriteLine(ms);
            ms.Remove('d', 10);
            Console.WriteLine(ms);
            ms.RemoveAll('c');
            Console.WriteLine(ms);

            Console.WriteLine(ms['v']);
            Console.WriteLine(ms['b']);
        }
    }
}
