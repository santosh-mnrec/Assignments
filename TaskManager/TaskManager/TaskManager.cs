using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    public static class TaskManager
    {
        private static List<Scheduler> _schedulers;

        /// <summary>
        /// Gets a list of currently running schedulers.
        /// </summary>
        public static IEnumerable<Scheduler> RunningSchedulers
        {
            get { return _schedulers.Where(scheduler => scheduler.IsRunning()); }
        }

        /// <summary>
        /// Gets all scheduleres.
        /// </summary>
        public static IEnumerable<Scheduler> AllSchedulers(string name)
        {
            return _schedulers;
        }

        /// <summary>
        /// Gets a scheduler by it's name.
        /// </summary>
        public static Scheduler GetScheduler(string name)
        {
            return _schedulers.Find(scheduler => scheduler.Name == name);
        }

        /// <summary>
        /// Attempts to remove a scheduler from the list.
        /// </summary>
        public static bool RemoveScheduler(Scheduler scheduler)
        {
            return _schedulers.Remove(scheduler);
        }

        /// <summary>
        /// Creates a new scheduler and adds it the the list.
        /// </summary>
        public static Scheduler CreateScheduler(int priority, string name = null)
        {
            if (_schedulers == null)
                _schedulers = new List<Scheduler>();

            var scheduler = new Scheduler(priority, name);
            _schedulers.Add(scheduler);
            return scheduler;
        }
    }
}
