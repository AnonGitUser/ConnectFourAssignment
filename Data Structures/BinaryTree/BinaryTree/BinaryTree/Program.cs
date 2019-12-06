using System;
using System.Linq;

namespace BinaryTree
{
    class Program
    {
        static void Main(string[] args)
        {
            const int min = 0;
            const int max = 100;
            var tree = new BinaryTree<int>();
            var rnd = new int[10];

            Node<int> root = null;

            var rndNo = new Random();
            for (var i = 0; i < rnd.Length; i++)
            {
                rnd[i] = rndNo.Next(min, max);
            }

            foreach (var r in rnd)
            {
                tree.InsertNode(r);
            }

            Console.WriteLine("input array: ");
            Console.WriteLine(string.Join(",", rnd));

            var values = tree.PreorderTraverseTree().ToList();
            Console.WriteLine("\nthe trees preorder traversal: ");
            Console.WriteLine(string.Join(",", values));

            var vals = tree.InOrderTraverseTree().ToList();
            Console.WriteLine("\nthe trees inorder traversal: ");
            Console.WriteLine(string.Join(",", vals));

            var rndItem = rnd[rndNo.Next(0, rnd.Length)];
            Console.WriteLine("\n" + rndItem + " will be deleted from tree");
            tree.DeleteNode(rndItem);

            var restTree = tree.PreorderTraverseTree().ToList();
            Console.WriteLine("\ntree after deletion: ");
            Console.WriteLine(string.Join(",", restTree));

            var treeContent = tree.PreorderTraverseTree().ToList();
            var rndItem1 = treeContent[2];
            Console.WriteLine("\ntry to find: " + rndItem1);
            var test = tree.FindNode(rndItem1);
            Console.WriteLine("Found: " + test);

            Console.WriteLine("Balancing tree now");
            var balancedTree = tree.BalancedTree();

            var balancedPreOrder = balancedTree.PreorderTraverseTree().ToList();
            Console.WriteLine("\npreorder traversal of balanced tree: ");
            Console.WriteLine(string.Join(",", balancedPreOrder));
            Console.ReadLine();
        }
    }
}
