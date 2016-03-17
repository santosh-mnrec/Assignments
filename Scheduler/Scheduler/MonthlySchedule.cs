using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduler
{
    public class MonthlySchedule : RepeatingSchedule
    {
        public int DayOfMonth { get; set; }

        public override bool OccursOnDate(DateTime date)
        {
            return DateIsInPeriod(date) & IsOnCorrectDate(date);
        }

        private bool IsOnCorrectDate(DateTime date)
        {
            if (date.Day == DayOfMonth)
                return true;
            else if (date.Day == DateTime.DaysInMonth(date.Year, date.Month)
                              && DayOfMonth > date.Day)
                return true;
            else
                return false;
        }
    }
}
