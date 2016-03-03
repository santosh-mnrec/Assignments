using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinkedList;
namespace LinkedList.Test
{
    [TestClass]
    public class LinkedListTest
    {
        [TestMethod]
        public void AddFirst_Test()
        {
            LinkedList list = new LinkedList();
            list.AddFirst(2);
            Assert.AreEqual(1, list.Count);

        }
        [TestMethod]
        public void AddLast_Test()
        {
            LinkedList list = new LinkedList();
            list.AddToTail(10);
            list.AddToTail(20);
            Assert.AreEqual(20, list.Tail.Data);

        }
        [TestMethod]

        public void LinkedList_ToString()
        {
            LinkedList list = new LinkedList();
            list.AddToTail(10);
            list.AddToTail(20);
            var expected = "10->20->NULL";
            Assert.AreEqual(expected, list.ToString());

        }

    }
}