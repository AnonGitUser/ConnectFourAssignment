using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    class LinkedList<T> where T : IComparable<T>
    {
        private Node head;
        private int length = 0;

        private class Node
        {
            public Node(T data) { this.data = data; }
            public T data;
            public Node next;
        }

        public int Length { get { return length; } }

        public void Add(T data)
        {
            Node node = new Node(data);
            node.next = head;
            head = node;
            length++;
        }

        public T Get(int index)
        {
            int counter = 0;
            Node curr = head;
            while (curr != null)
            {
                if (index == counter) { return curr.data; }
                curr = curr.next;
                counter++;
            }
            return default(T);
        }

        public bool Contains(T data)
        {
            Node curr = head;
            while (curr != null)
            {
                if (curr.data.Equals(data)) { return true; }
                curr = curr.next;
            }
            return false;
        }

        public void Clear()
        {
            head = null;
        }

        public void Remove(T data)
        {
            Node prev = head;
            Node curr = head;
            while (curr != null)
            {
                if (curr.data.Equals(data))
                {
                    length--;
                    prev.next = curr.next;
                    if (curr == head)
                    {
                        head = head.next;
                    }
                }
                prev = curr;
                curr = curr.next;
            }
        }

        public void RemoveAt(int index)
        {
            int counter = 0;
            Node prev = head;
            Node curr = head;
            while (curr != null)
            {
                if (counter == index)
                {
                    length--;
                    prev.next = curr.next;
                    if (curr == head)
                    {
                        head = head.next;
                    }
                }
                prev = curr;
                curr = curr.next;
            }
        }

        public override string ToString()
        {
            String output = "";
            Node temp = head;
            while (temp != null)
            {
                output += temp.data.ToString() + " ";
                temp = temp.next;
            }
            return output;
        }
    }
}