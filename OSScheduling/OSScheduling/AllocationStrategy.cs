using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSScheduling
{
    public abstract class AllocationStrategy
    {
        protected List<Job> Jobs;
       // protected ArrayList<Job> Queue;

        public AllocationStrategy(List<Job> jobs)
        {
            
            Jobs = jobs;
        }

        public abstract void run();
        // update current job by 1 tick
        // check if the job queue might need to be changed.
        // check for jobs to add to the queue
    }
}
