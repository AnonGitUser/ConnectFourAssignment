using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinaryTree
{
    public class Node<T>
    {
        public T Value { get; set; }
        //lesser value than this node gets put into left node
        public Node<T> LeftNode;
        //higher value than this node gets put into right node
        public Node<T> RightNode;

        //creates new nodes
        public Node(T value)
        {
            Value = value;
        }
    }

    public class BinaryTree<T> where T : IComparable<T>
    {
        private Node<T> globalRoot;

        public void InsertNode(T value)
        {
            if (globalRoot == null)
            {
                globalRoot = new Node<T>(value);
            }
            else
            {
                InsertNode(globalRoot, value);
            }
        }

        private static Node<T> InsertNode(Node<T> root, T value)
        {
            if (root == null)
            {
                root = new Node<T>(value);
            }
            else
            {
                if (value.CompareTo(root.Value) <= 0)
                {
                    root.LeftNode = InsertNode(root.LeftNode, value);
                }
                else if (value.CompareTo(root.Value) > 0)
                {
                    root.RightNode = InsertNode(root.RightNode, value);
                }
            }
            return root;
        }

        
        //Visits the root, then the left, then the right
        public IEnumerable<T> PreorderTraverseTree()
        {
            if (globalRoot == null) yield break;

            foreach (var node in PreorderTraverseTree(globalRoot))
                yield return node;
        }

        private static IEnumerable<T> PreorderTraverseTree(Node<T> root)
        {
            if (root == null) yield break;
            yield return root.Value;
            foreach (var v in PreorderTraverseTree(root.LeftNode))
            {
                yield return v;
            }

            foreach (var v in PreorderTraverseTree(root.RightNode))
            {
                yield return v;
            }
        }

        //in order transverse of the tree
        public IEnumerable<T> InOrderTraverseTree()
        {
            if (globalRoot == null) yield break;

            foreach (var node in InOrderTraverseTree(globalRoot))
                yield return node;
        }

        private static IEnumerable<T> InOrderTraverseTree(Node<T> root)
        {
            if (root == null) yield break;
            foreach (var v in InOrderTraverseTree(root.LeftNode))
            {
                yield return v;
            }
            yield return root.Value;
            foreach (var v in InOrderTraverseTree(root.RightNode))
            {
                yield return v;
            }
        }

        //deletes a node from the tree
        /// <param name="value">Value of the node which is to be deleted.</param>
        public void DeleteNode(T value)
        {
            if (globalRoot == null) return;
            DeleteNode(ref globalRoot, value);
        }

        private void DeleteNode(ref Node<T> root, T value)
        {
            if (root == null) return;
            if (root.Value.Equals(value))
                root = Delete(ref root);
            else if (value.CompareTo(root.Value) <= 0)
                DeleteNode(ref root.LeftNode, value);
            else if (value.CompareTo(root.Value) >= 0)
            {
                DeleteNode(ref root.RightNode, value);
            }
        }

        private static void Replace(ref Node<T> root, ref T newValue)
        {
            if (root == null) return;
            if (root.LeftNode == null)
            {
                newValue = root.Value;
                root = root.RightNode;
            }
            else
            {
                Replace(ref root.LeftNode, ref newValue);
            }
        }

        //transverse the tree to find the node with the value chosen
        /// <param name="value">The value to search for</param>
        public T FindNode(T value)
        {
            var res = FindNode(globalRoot, value);

            return res.Value;
        }

        private static Node<T> FindNode(Node<T> root, T value)
        {
            Node<T> res = null;
            if (root.LeftNode != null)
                res = FindNode(root.LeftNode, value);

            if (value.CompareTo(root.Value) == 0)
                return root;

            if (res == null && root.RightNode != null)
                res = FindNode(root.RightNode, value);

            return res;
        }
       
        public Node<T> Delete(ref Node<T> root)
        {
            var tempValue = default(T);

            if (globalRoot == root && root.LeftNode == null && root.RightNode == null)
            {
                //Deletion of root element is allowed  - to for forbid it, return root
                return null;
            }
            if (root.LeftNode == null && root.RightNode == null)
            {
                root = null;
            }
            else if (root.LeftNode == null)
            {
                root = root.RightNode;
            }
            else if (root.RightNode == null)
            {
                root = root.LeftNode;
            }
            else
            {
                Replace(ref root, ref tempValue);
                root.Value = tempValue;
            }
            return root;
        }

        public BinaryTree<T> BalancedTree()
        {
            var balanced = new BinaryTree<T>();
            var inorder = InOrderTraverseTree().ToArray();

            balanced.globalRoot = BalanceTree(inorder, 0, inorder.Length - 1);

            return balanced;
        }

        private static Node<T> BalanceTree(IReadOnlyList<T> inorder, int startIndex, int endIndex)
        {
            if (startIndex > endIndex) return null;

            var middIndex = (startIndex + endIndex) / 2;

            var root = new Node<T>(inorder[middIndex]);

            root.LeftNode = BalanceTree(inorder, startIndex, middIndex - 1);
            root.RightNode = BalanceTree(inorder, middIndex + 1, endIndex);

            return root;
        }
    }
}