using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSScheduling
{
    class Program
    {
        static void Main(string[] args)
        {
            String filename;
            String allocationStrategy;
            int quantum = 20;

            /*filename = args[0];
            allocationStrategy = args[1];*/

            filename = "testing.txt";
            allocationStrategy = "FCFS";
            List<Job> jobList = new List<Job>();
            jobList.Add(new Job(1, 0, 2, new callback()));
            jobList.Add(new Job(2, 1, 3, new callback()));
            FirstComeFirstServed fcfs = new FirstComeFirstServed(jobList);
               fcfs.run(jobList);
        }
    }

    public class callback : JobFinishEvent
    {
        public void onFinish(Job j)
        {
            Console.WriteLine(j);
        }
    }

}
