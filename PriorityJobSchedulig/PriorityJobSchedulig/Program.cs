using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PriorityJobSchedulig
{
    public class ListComp : IComparer<Job>
    {
        public int Compare(Job o1, Job o2)
        {
            return (o1.Arrival_time > o2.Arrival_time ? 1 : (o1.Arrival_time == o2.Arrival_time ? 0 : -1));
        }
    }

    class PQComp : IComparer<Job>
    {


        public int Compare(Job o1, Job o2)
        {
            return (o1.Priority > o2.Priority ? -1 : (o1.Priority == o2.Priority ? 0 : 1));
        }
    }

    public class Job
    {
        private Thread t;

        public Thread T
        {
            get { return t; }
            set { t = value; }
        }
        private int index;

        public int Index
        {
            get { return index; }
            set { index = value; }
        }
        private int burst_time;

        public int Burst_time
        {
            get { return burst_time; }
            set { burst_time = value; }
        }
        private int arrival_time;

        public int Arrival_time
        {
            get { return arrival_time; }
            set { arrival_time = value; }
        }
        private int priority;

        public int Priority
        {
            get { return priority; }
            set { priority = value; }
        }
        private int cur;

        public int Cur
        {
            get { return cur; }
            set { cur = value; }
        }
        public Job()
        {

        }
        public Job(int i, int at, int bt, int p)
        {
            this.index = i;
            this.Burst_time = bt;
            this.Arrival_time = at;
            this.Priority = p;

        }
        public void Start()
        {
            t = new Thread(new ThreadStart(Run));
            Console.WriteLine("Index={0}", this.Index);
            t.Start();

        }
        public void Run()
        {
            try
            {
                for (cur = 0; cur < burst_time; cur++)
                {

                    Thread.Sleep(500);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("process" + t + " interrupted");

            }
            
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int[] wait = new int[100];

            List<Job> th = new List<Job>();

            Queue<Job> PQ = new Queue<Job>();
            Console.WriteLine("Enter Number of Process");
            var N = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("Enter Burst TIme");
                var b = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Arr");
                var a = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("P");
                var p = Convert.ToInt32(Console.ReadLine());

                th.Add(new Job { Index = i, Arrival_time = a, Burst_time = b, Priority = p });
            }

            th.Sort(new ListComp());

            PQ.Enqueue(th[0]);
            int j = 1, et = 0;
            while (PQ.Count != 0)
            {
                var t = PQ.Dequeue();
                t.Start();

                wait[t.Index] = et - t.Arrival_time;
                while (t.T.IsAlive)
                {
                    if (j < th.Count() && (th[j].Arrival_time < t.Cur + et))
                        PQ.Enqueue(th[j++]);
                }

                if (PQ.Count() == 0 && j < th.Count())
                    PQ.Enqueue(th[j]);
                et += t.Burst_time;

            }

            Console.WriteLine("Total time elapsed: " + et);

            int sum = 0;
            for (int i = 0; i < th.Count(); i++)
                
                sum += wait[i];
            Console.WriteLine("average waiting time: " + (double)sum / th.Count());

        }
    }
}
