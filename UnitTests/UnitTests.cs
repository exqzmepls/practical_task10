using System;
using System.IO;
using practical_task10;
using ListLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestDesigner()
        {
            MyLinkedList l = new MyLinkedList();
            bool result = l.Last == l.First && l.First == null && l.Count == 0;
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestDesignerWithSize()
        {
            MyLinkedList l = new MyLinkedList(8);
            MyLinkedListNode n = l.First;
            bool result = l.Count == 8;
            while (n != null)
            {
                if (n.A != default && n.I != default)
                {
                    result = false;
                    break;
                }
                n = n.Next;
            }
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestDesignerSizeDelowZero()
        {
            bool result = true;
            try
            {
                MyLinkedList l = new MyLinkedList(-4);
            }
            catch
            {
                result = false;
            }
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void TestGetValues()
        {
            int[] pows = { 1, 2, 3, 2, 3 };
            int[] coefs = { 2, 4, 6, 4, 6 };
            bool result = Program.GetValues("list.txt", out MyLinkedList l);
            MyLinkedListNode n = l.First;
            int i = 0;
            while (n != null)
            {
                if (pows[i] != n.I && coefs[i] != n.A)
                {
                    result = false;
                    break;
                }
                i++;
                n = n.Next;
            }
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestGetValuesNullCoeffs()
        {
            bool result = Program.GetValues("listWithErrors.txt", out MyLinkedList l);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void TestGetValuesFalse()
        {
            bool result = Program.GetValues("emptyList.txt", out MyLinkedList l);
            result = result && l.IsEmpty;
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestRemoveTrue()
        {
            bool result = Program.GetValues("list.txt", out MyLinkedList l);
            result = result && l.Remove(2, 4);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestRemoveFalse()
        {
            bool result = Program.GetValues("list.txt", out MyLinkedList l);
            result = result && l.Remove(1, 4);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void TestRemoveNull()
        {
            bool result = Program.GetValues("list.txt", out MyLinkedList l);
            MyLinkedListNode n = null;
            try
            {
                l.Remove(n);
            }
            catch
            {
                result = false;
            }
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void TestRemoveNotExisting()
        {
            bool result = Program.GetValues("list.txt", out MyLinkedList l1);
            Program.GetValues("list.txt", out MyLinkedList l2);
            try
            {
                l1.Remove(l2.First);
            }
            catch
            {
                result = false;
            }
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void TestRemoveFirst()
        {
            bool result = Program.GetValues("list.txt", out MyLinkedList l);
            l.RemoveFirst();
            result = result && l.First.I != 1;
            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void TestRemoveLast()
        {
            bool result = Program.GetValues("list.txt", out MyLinkedList l);
            l.RemoveLast();
            result = result && l.Last.I != 3;
            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void TestRemoveNode()
        {
            bool result = Program.GetValues("list.txt", out MyLinkedList l);
            l.Remove(l.First.Next);
            result = result && l.First.Next.I != 2;
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestContainsTrue()
        {
            bool result = Program.GetValues("list.txt", out MyLinkedList l);
            result = result && l.Contains(1, 2);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestContainsFalse()
        {
            bool result = Program.GetValues("list.txt", out MyLinkedList l);
            result = result && l.Contains(6, -5);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void TestFindLast()
        {
            Program.GetValues("list.txt", out MyLinkedList l);
            MyLinkedListNode f = l.FindLast(2, 4);
            Assert.AreEqual(l.Last.Previous, f);
        }
        [TestMethod]
        public void TestFindLastNull()
        {
            Program.GetValues("list.txt", out MyLinkedList l);
            MyLinkedListNode f = l.FindLast(5, -7);
            Assert.AreEqual(null, f);
        }
        [TestMethod]
        public void TestFind()
        {
            Program.GetValues("list.txt", out MyLinkedList l);
            MyLinkedListNode f = l.Find(3, 6);
            Assert.AreEqual(l.First.Next.Next, f);
        }
        [TestMethod]
        public void TestFindNull()
        {
            Program.GetValues("list.txt", out MyLinkedList l);
            MyLinkedListNode f = l.Find(3, 9);
            Assert.AreEqual(null, f);
        }
        [TestMethod]
        public void TestAddAfterNull()
        {
            bool result = Program.GetValues("list.txt", out MyLinkedList l);
            try
            {
                l.AddAfter(null, 1, 9);
            }
            catch
            {
                result = false;
            }

            Assert.AreEqual(false, result);
        }
        [TestMethod]
        public void TestAddAfterExisting()
        {
            bool result = Program.GetValues("list.txt", out MyLinkedList l);
            try
            {
                l.AddAfter(l.First, l.Last);
            }
            catch
            {
                result = false;
            }

            Assert.AreEqual(false, result);
        }
        [TestMethod]
        public void TestAddAfterDoesnotBelong()
        {
            bool result = Program.GetValues("list.txt", out MyLinkedList l);
            MyLinkedListNode n = new MyLinkedListNode();
            try
            {
                l.AddAfter(n, 6, -2);
            }
            catch
            {
                result = false;
            }

            Assert.AreEqual(false, result);
        }
        [TestMethod]
        public void TestAddBeforeNull()
        {
            bool result = Program.GetValues("list.txt", out MyLinkedList l);
            try
            {
                l.AddBefore(null, 1, 7);
            }
            catch
            {
                result = false;
            }

            Assert.AreEqual(false, result);
        }
        [TestMethod]
        public void TestAddBeforeExisting()
        {
            bool result = Program.GetValues("list.txt", out MyLinkedList l);
            try
            {
                l.AddBefore(l.First, l.Last);
            }
            catch
            {
                result = false;
            }

            Assert.AreEqual(false, result);
        }
        [TestMethod]
        public void TestAddBeforeDoesnotBelong()
        {
            bool result = Program.GetValues("list.txt", out MyLinkedList l);
            MyLinkedListNode n = new MyLinkedListNode();
            try
            {
                l.AddBefore(n, 2, 4);
            }
            catch
            {
                result = false;
            }

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void TestAddNullToEmpty()
        {
            MyLinkedList l = new MyLinkedList(0);
            MyLinkedListNode n = null;
            bool result = true;
            try
            {
                l.AddLast(n);
            }
            catch
            {
                result = false;
            }

            Assert.AreEqual(false, result);
        }
        [TestMethod]
        public void TestAddExistingToEmpty()
        {
            MyLinkedList l1 = new MyLinkedList(0);
            bool result = Program.GetValues("list.txt", out MyLinkedList l2);
            try
            {
                l1.AddLast(l2.Last);
            }
            catch
            {
                result = false;
            }

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void TestAddBeforeFirst()
        {
            bool result = Program.GetValues("list.txt", out MyLinkedList l);
            l.AddBefore(l.First, 0, 7);
            result = result && l.First.I == 0;

            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void TestAddBefore()
        {
            bool result = Program.GetValues("list.txt", out MyLinkedList l);
            l.AddBefore(l.First.Next, 1, -11);
            result = result && l.First.Next.A == -11;
            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void TestAddAfter()
        {
            bool result = Program.GetValues("list.txt", out MyLinkedList l);
            l.AddAfter(l.First, 2, -9);
            result = result && l.First.Next.A == -9;
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestAddFirst()
        {
            bool result = Program.GetValues("list.txt", out MyLinkedList l);
            l.AddFirst(0, -9);
            result = result && l.First.I == 0;
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestAddFirstToEmpty()
        {
            MyLinkedList l = new MyLinkedList();
            l.AddFirst(0, -9);
            bool result = l.First.I == 0 && l.Count == 1;
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestCalculateEmpty()
        {
            MyLinkedList l = new MyLinkedList();
            bool result = true;
            try
            {
                l.Calculate(8);
            }
            catch
            {
                result = false;
            }
            
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void TestPrint()
        {
            StreamWriter os = new StreamWriter("output.txt", false);
            Console.SetOut(os);

            bool result = Program.GetValues("list.txt", out MyLinkedList l);
            l.Print("; ");

            os.Close();

            
            using (StreamReader sr = new StreamReader("output.txt"))
            {
                result = sr.ReadLine() == "1 2; 2 4; 3 6; 2 4; 3 6";
            }

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestPrintEmpty()
        {
            StreamWriter os = new StreamWriter("output.txt", false);
            Console.SetOut(os);

            MyLinkedList l = new MyLinkedList();
            l.Print();

            os.Close();

            bool result;
            using (StreamReader sr = new StreamReader("output.txt"))
            {
                result = sr.ReadLine() == "This list is empty.";
            }

            Assert.AreEqual(true, result);
        }


        [TestMethod]
        public void TestDoubleInput()
        {
            Console.SetIn(new StreamReader("doubleInput.txt"));
            double result = 0.04;

            double input = Program.DoubleInput(lBound: 0, info: "some info");

            Assert.AreEqual(result, input);
        }

        [TestMethod]
        public void TestGetArgs()
        {
            int[] args = { 2, -1, 34 };
            int[] values = { 132, -6, 480964 };
            StreamWriter os = new StreamWriter("output.txt", false);
            StreamReader argStream = new StreamReader("args.txt");
            Console.SetOut(os);
            Console.SetIn(argStream);

            bool result = Program.GetValues("list.txt", out MyLinkedList l);
            Program.GetArgs(l);

            os.Close();

            using (StreamReader sr = new StreamReader("output.txt"))
            {
                sr.ReadLine();
                sr.ReadLine();
                for (int i= 0; i<args.Length; i++)
                {
                    if (sr.ReadLine() != $"P({args[i]}) = {values[i]}")
                    {
                        result = false;
                        break;
                    }
                }
            }

            Assert.AreEqual(true, result);
        }
    }
}
