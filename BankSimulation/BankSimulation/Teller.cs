using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BankSimulation
{
    public class Queue<T>
    {
        private T[] array;

        public Queue()
        {
            array = new T[16];
        }

        public void Enqueue(T item)
        {
            Thread.Sleep(1000);
            if (Count == array.Length / 2)
                Grow();

            array[Count++] = item;
        }

        public void Grow()
        {
            var temp = array;
            array = new T[array.Length * 2];
            Array.Copy(temp, startingPos, array, 0, Count);
            startingPos = 0;
        }

        public void Shrink()
        {
            var temp = array;
            array = new T[array.Length / 2];
            Array.Copy(temp, startingPos, array, 0, Count);
            startingPos = 0;
        }

        public T Dequeue()
        {
            if (Count == array.Length / 4)
                Shrink();

            var item = array[startingPos];
            array[startingPos++] = default(T);
            Count--;
            return item;
        }

        int startingPos = 0;

        public int Count { get; set; }
    }
    public class Teller
    {

        Customer c = new Customer();

        Queue<Customer> q;


        public int customers { get; set; }
        public int RunTime { get; set; }
        public int CustomerArrival { get; set; }
        public int TransactionTime { get; set; }

        public Teller(int run, int maxNewCustomer, int maxHandle)
        {
            this.RunTime = run;
            this.CustomerArrival = maxNewCustomer;
            this.TransactionTime = maxHandle;
            q = new Queue<Customer>();


        }//end constructor


        Random rand = new Random();
        public void Run()
        {



            int newArrival = rand.Next(CustomerArrival);


            CustomerArrival = newArrival;
            //  q.empty();
            int time;
            Customer next = new Customer();
            for (time = 0; time < RunTime; time++)
            {
                if (q.Count == 0)
                {
                    if (c.currentTime >= CustomerArrival)
                    {
                        q.Enqueue(next);
                        customers++;

                    }
                    else if (CustomerArrival > TransactionTime)
                    {

                        q.Dequeue();
                        customers++;
                        int nextArrival = rand.Next(CustomerArrival);
                        CustomerArrival = newArrival;
                        nextArrival = c.clock + nextArrival;

                    }//end if
                }//end if
            }//end for
        }//end Run
        public void Report()
        {
            int totalTime = c.clock;
            Console.WriteLine(totalTime);
            int Average = totalTime / customers;
            Console.WriteLine(Average);

            int numberServed = customers;
            Console.WriteLine(numberServed);

            if (q.Count == 0)
            {
                Console.WriteLine(q.ToString());
                Console.WriteLine(c.clock);
            }//end if
        }//end report

        public static void Main(String[] args)
        {
            if (args.Length == 0) { args = new string[] { "3", "43", "6" }; }
            IEnumerator<int> scanner = (from arg in args select int.Parse(arg)).GetEnumerator();
            while (scanner.MoveNext())
            {
                Console.WriteLine("how many seconds to run: ");
                int newRunTime = scanner.Current;
                Console.WriteLine("max time between customers: ");
                int newCustomerArrival = scanner.Current;
                Console.WriteLine("max processing time: ");
                int newTransTime = scanner.Current;
                Teller t = new Teller(newRunTime, newCustomerArrival, newTransTime);
                t.Run();
                t.Report();
            }







        }//end main
    }


}
