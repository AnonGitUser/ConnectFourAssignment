using System;

namespace Linked_List
{ 
        class Program
        {
            static void Main(string[] args)
            {
                LinkedList<String> list = new LinkedList<string>();
                list.Add("C");
                list.Add("B");
                list.Add("A");
                list.Add("E");
                list.Add("D");
                list.Add("F");
                list.Add("G");
                list.Add("Z");
                list.Display();

                list.Remove("D");
                list.Remove("G");
                list.Display();

                list.Sort();
                list.Display();
            }

        }
}


