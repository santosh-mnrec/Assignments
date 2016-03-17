using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scheduler;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var single1 = new SingleSchedule
            {
                Name = "Meet Bob for Pint",
                TimeOfDay = new TimeSpan(19, 30, 0),
                Date = new DateTime(2012, 5, 8)
            };

            var single2 = new SingleSchedule
            {
                Name = "Confirm Meeting",
                TimeOfDay = new TimeSpan(9, 30, 0),
                Date = new DateTime(2012, 5, 12)
            };

            var simple = new SimpleRepeatingSchedule
            {
                Name = "Sprint Planning Meeting",
                TimeOfDay = new TimeSpan(10, 0, 0),
                SchedulingRange = new Period(new DateTime(2012, 1, 2), new DateTime(2012, 12, 31)),
                DaysBetween = 7
            };

            var weekly = new WeeklySchedule
            {
                Name = "Check Backup Reliability",
                TimeOfDay = new TimeSpan(8, 0, 0),
                SchedulingRange = new Period(new DateTime(2012, 5, 28), new DateTime(2012, 6, 8))
            };
            weekly.SetDays(new DayOfWeek[] { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday });

            var monthly = new MonthlySchedule
            {
                Name = "Check Wages",
                TimeOfDay = new TimeSpan(18, 0, 0),
                DayOfMonth = 31,
                SchedulingRange = new Period(new DateTime(2012, 1, 2), new DateTime(2100, 1, 1))
            };

            var schedules = new List<Schedule> { single1, single2, simple, weekly, monthly };

            var generator = new CalendarGenerator();
            var period = new Period(new DateTime(2012, 5, 1), new DateTime(2012, 6, 30));
            var appointments = generator.GenerateCalendar(period, schedules);

            foreach (var appointment in appointments)
            {
                Console.WriteLine(
                    "{0} | {1}", appointment.Time.ToString("yyyy-MM-dd HH:mm"), appointment.Name);
            }
        }
    }
}
