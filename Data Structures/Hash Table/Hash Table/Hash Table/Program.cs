using System;

namespace Hash_Table
{
    class Program
    {
        static void Main(string[] args)
        {
            Hashtable<string, int> hash = new Hashtable<string, int>();

            hash.Add("zachary", 1);
            hash.Add("jeffery", 5);
            hash.Remove("testing");
            
            Console.WriteLine(hash.Get("zachary"));
        }
    }
}
