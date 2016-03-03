using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    public class LinkedList
    {
        private Node _head;
        private int _count = 0;
        public void AddFirst(int data)
        {
            Node newNode = new Node(data);
            //List is empty
            if (_head == null)
                _head = newNode;
            else
            {
                newNode.Next = _head;
                _head = newNode;

            }
           
        }
        public Node Head
        {

            get
            {
                return _head;
            }
        }
        public Node Tail
        {

            get
            {
                Node current = _head;
                while (current.Next!=null)
                {
                    current = current.Next;
                }
                return current;

            }
        }
        public int Count
        {
            get
            {
                Node current = _head;
                while (current!=null)
                {
                    _count++;
                    current = current.Next;
                }
                return _count;


            }

            
        }

        public void AddToTail(int data)
        {
            Node newNode = new Node(data);
            if (_head == null)
                _head = newNode;

            else
            {
                Node current = _head;
                while (current.Next!=null)
                {
                    current = current.Next;
                }
                current.Next = newNode;

            }
        }

        public override string ToString()
        {
            Node current = _head;
            StringBuilder builder = new StringBuilder();
            while (current!=null)
            {
                builder.Append(current.Data + "->");
                current = current.Next;
            }
            builder.Append("NULL");
            return builder.ToString();
        }
    }
}
