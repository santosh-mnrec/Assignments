using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;



namespace TaskManager
{
    public class Scheduler : Queue<Job>
    {
        public string Name { get; private set; }
        public int Priority { get; set; }
        private Timer _triggerTimer;
        public int Interval { get; private set; }
        public EventHandler<Job> Trigger;

        public Scheduler(int priority, string name = null)
        {
            Priority = priority;
            Name = name;
            Interval = 20;
        }

        private void TriggerTimerCallBack(object state)
        {
            foreach (Job job in this.Where(job => DateTime.Now >= job.Execution && !job.Triggered))
            {
                job.Triggered = true;
                if (job.Repeating)
                    job.StartRepeating();
                Trigger(this, job);
            }
        }

        /// <summary>
        /// Sets the trigger timer interval.
        /// </summary>
        public void SetRefreshInterval(int interval)
        {
            Interval = interval;
        }

        /// <summary>
        /// Gets all the triggered jobs.
        /// </summary>
        public IEnumerable<Job> GetTriggeredJobs()
        {
            return this.Where(job => job.Triggered);
        }

        /// <summary>
        /// Gets all the repeat enabled jobs.
        /// </summary>
        public IEnumerable<Job> GetRepeatingJobs()
        {
            return this.Where(job => job.Repeating);
        }

        /// <summary>
        /// Returns whether the schedular is running.
        /// </summary>
        public bool IsRunning()
        {
            return _triggerTimer != null;
        }

        /// <summary>
        /// Stops the scheduler.
        /// </summary>
        public void Stop()
        {
            if (null == _triggerTimer) return;
            _triggerTimer.Dispose();
            _triggerTimer = null;
        }

        /// <summary>
        /// Starts the scheduler.
        /// </summary>
        public void Start()
        {
            if (_triggerTimer != null) return;
            _triggerTimer = new Timer(TriggerTimerCallBack, null, 0, Interval);
        }
    }
}
