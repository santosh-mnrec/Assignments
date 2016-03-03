using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    public class Program
    {
        static void Main(string[] args)
        {
            BinarySearchTree nums = new BinarySearchTree();
            nums.Insert(23);
            nums.Insert(45);
            nums.Insert(16);
            nums.Insert(37);
            nums.Insert(3);
            nums.Insert(99);
            nums.Insert(22);
            Console.WriteLine("Inorder traversal: ");
            nums.InOrder(nums.root);
        }
    }
}
