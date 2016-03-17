using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduler
{
    public class SingleSchedule : Schedule
    {
        public DateTime Date { get; set; }

        public override bool OccursOnDate(DateTime date)
        {
            return Date.Date == date;
        }
    }
}
