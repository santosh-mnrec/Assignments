using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduler
{
    public abstract class RepeatingSchedule : Schedule
    {
        public Period SchedulingRange { get; set; }

        protected bool DateIsInPeriod(DateTime date)
        {
            return date >= SchedulingRange.Start && date <= SchedulingRange.End;
        }
    }
}
