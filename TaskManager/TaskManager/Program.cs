using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager;

namespace TaskManager
{
    class Program
    {
        static void Main(string[] args)
        {


            var meetings = TaskManager.CreateScheduler(0, "Meeting Scheduler");
            var jobs = TaskManager.CreateScheduler(1, "Work Scheduler");


            meetings.Trigger += (sender, job) => Console.WriteLine(job.Name);
            jobs.Trigger += (sender, job) => Console.WriteLine(job.Name);

            
            //One time meeting to be triggered after 15 seconds.
            meetings.Enqueue(new Job(DateTime.Now.AddSeconds(1), "Meet Barack Obama"));
           
            ////Repeated meeting to be triggered after 5 days and repeated every 5 days.
            meetings.Enqueue(new Job(DateTime.Now.AddSeconds(5), "Visit Your Dad").TriggerEvery(5).Seconds());

            ////Repeated job to be triggered after 5 minutes and repeated every 2 hours.
            //jobs.Add(new Job(Job, DateTime.Now.AddMinutes(5), "Make a cheese sandwich").TriggerEvery(2).Hours());


            meetings.Start();
            
            jobs.Start();
            Console.ReadLine();

        }
    }
}
