using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    public class Node
    {
        public Node Left { get; set; }
        public Node Right { get; set; }
        public int Data { get; set; }
        public Node()
        {

        }
        public Node(int data)
        {
            this.Data = data;
            this.Left = null;
            this.Right = null;
        }

        public void Display()
        {
            Console.WriteLine(Data);
        }
        public override string ToString()
        {
            string nodeString = "[" + this.Data + " ";

            // Leaf node
            if (this.Left == null && this.Right == null)
            {
                nodeString += " (Leaf) ";
            }

            if (this.Left != null)
            {
                nodeString += "Left: " + this.Left.ToString();
            }

            if (this.Right != null)
            {
                nodeString += "Right: " + this.Right.ToString();
            }

            nodeString += "] ";

            return nodeString;
        }
    }

    public class BinarySearchTree
    {

        public Node root;

        public BinarySearchTree()
        {
            root = null;
        }

        public void Insert(int i)
        {
            Node newNode = new Node();
            newNode.Data = i;
            if (root == null)
                root = newNode;
            else
            {
                Node current = root;
                Node parent;
                while (true)
                {
                    parent = current;
                    if (i < current.Data)
                    {
                        current = current.Left;
                        if (current == null)
                        {
                            parent.Left = newNode;
                            break;
                        }

                    }
                    else
                    {
                        current = current.Right;
                        if (current == null)
                        {
                            parent.Right = newNode;
                            break;
                        }
                    }
                }
            }
        }
        public void InOrder(Node theRoot)
        {
            if (!(theRoot == null))
            {
                InOrder(theRoot.Left);
                theRoot.Display();
                InOrder(theRoot.Right);
            }
        }

        public void PreOrder(Node theRoot)
        {
            if (!(theRoot == null))
            {
                theRoot.Display();
                PreOrder(theRoot.Left);
                PreOrder(theRoot.Right);
            }
        }
        public void PostOrder(Node theRoot)
        {
            if (!(theRoot == null))
            {
                PostOrder(theRoot.Left);
                PostOrder(theRoot.Right);
                theRoot.Display();
            }
        }

        public int FindMin()
        {
            Node current = root;
            while (current.Left != null)
            {
                current = current.Left;
            }
            return current.Data;

        }
        public int FindMax()
        {
            Node current = root;
            while (!(current.Right == null))
                current = current.Right;
            return current.Data;
        }
        public Node Find(int key)
        {
            Node current = root;
            while (current.Data != key)
            {
                if (key < current.Data)
                    current = current.Left;
                else
                    current = current.Right;
                if (current == null)
                    return null;
            }
            return current;
        }

        public Node GetSuccessor(Node delNode)
        {
            Node successorParent = delNode;
            Node successor = delNode;
            Node current = delNode.Right;
            while (!(current == null))
            {
                successorParent = current;
                successor = current;
                current = current.Left;
            }
            if (!(successor == delNode.Right))
            {
                successorParent.Left = successor.Right;
                successor.Right = delNode.Right;
            }
            return successor;
        }
        public bool Delete(int key)
        {
            Node current = root;
            Node parent = root;
            bool isLeftChild = true;

            while (current.Data != key)
            {
                parent = current;
                if (key < current.Data)
                {
                    isLeftChild = true;
                    current = current.Right;
                }
                else
                {
                    isLeftChild = false;
                    current = current.Right;
                }
                if (current == null)
                    return false;
            }
            //Delete Leaf node
            if ((current.Left == null) && (current.Right == null))
            {
                if (current == root)
                    root = null;
                else if (isLeftChild)
                    parent.Left = null;
                else
                    parent.Right = null;
            }
            //Deleting a Node With One Child
            else if (current.Right == null)
            {
                if (current == root)
                    root = current.Left;
                else if (isLeftChild)
                    parent.Left = current.Left;
                else
                    parent.Right = current.Right;
            }
            else if (current.Left == null)
            {
                if (current == root)
                    root = current.Right;
                else if (isLeftChild)
                    parent.Left = parent.Right;
                else
                    parent.Right = current.Right;
            }
            //Deleting a Node With Two Children
            else
            {
                Node successor = GetSuccessor(current);
                if (current == root)
                    root = successor;
                else if (isLeftChild)
                    parent.Left = successor;
                else
                    parent.Right = successor;
                successor.Left = current.Left;
            }

            return true;
        }
    }
}