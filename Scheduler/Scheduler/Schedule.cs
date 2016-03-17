using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduler
{
    public abstract class Schedule
    {
        public TimeSpan TimeOfDay { get; set; }

        public string Name { get; set; }

        public abstract bool OccursOnDate(DateTime date);
    }
}
