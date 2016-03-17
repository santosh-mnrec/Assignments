using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduler
{
    public class WeeklySchedule : RepeatingSchedule
    {
        List<DayOfWeek> _days;

        public void SetDays(IEnumerable<DayOfWeek> days)
        {
            _days = days.Distinct().ToList();
        }

        public override bool OccursOnDate(DateTime date)
        {
            return DateIsInPeriod(date) && _days.Contains(date.DayOfWeek);
        }
    }
}
