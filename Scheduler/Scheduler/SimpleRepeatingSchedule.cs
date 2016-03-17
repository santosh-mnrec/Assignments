using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduler
{
    public class SimpleRepeatingSchedule : RepeatingSchedule
    {
        int _daysBetween;

        public int DaysBetween
        {
            get
            {
                return _daysBetween;
            }
            set
            {
                if (value <= 0) throw new ArgumentException(
                    "The days between appointments must be at least one.");

                _daysBetween = value;
            }
        }

        public override bool OccursOnDate(DateTime date)
        {
            if (DateIsInPeriod(date))
            {
                return DateIsValidForSchedule(date);
            }
            return false;
        }

        private bool DateIsValidForSchedule(DateTime date)
        {
            int daysBetweenFirstAndCheckDate
                = (int)date.Subtract(SchedulingRange.Start).TotalDays;
            return daysBetweenFirstAndCheckDate % DaysBetween == 0;
        }
    }
}
