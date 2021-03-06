﻿using System;
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

        [TestMethod]

        public void InsertAt_Begining()
        {
            LinkedList list = new LinkedList();
            list.AddToTail(10);
            list.AddToTail(20);
            list.InsertAt(30,0);
            var expected = "30->10->20->NULL";
            Assert.AreEqual(expected, list.ToString());

        }

        [TestMethod]

        public void InsertAt_Random()
        {
            LinkedList list = new LinkedList();
            list.AddToTail(10);
            list.AddToTail(20);
            list.AddToTail(40);
            list.AddFirst(100);
            list.InsertAt(30,2 );
            var expected = "100->10->30->20->40->NULL";
            Assert.AreEqual(expected, list.ToString());

        }

    }
}