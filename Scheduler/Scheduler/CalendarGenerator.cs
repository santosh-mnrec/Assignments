using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduler
{
    public class CalendarGenerator
    {
        public IEnumerable<Appointment> GenerateCalendar(Period period, IEnumerable<Schedule> schedules)
        {
            var appointments = new List<Appointment>();
            for (DateTime checkDate = period.Start; checkDate <= period.End; checkDate = checkDate.AddDays(1))
            {
                AddAppointmentsForDate(checkDate, schedules, appointments);
            }
            return appointments.OrderBy(a => a.Time);
        }

        private void AddAppointmentsForDate(DateTime checkDate, IEnumerable<Schedule> schedules, List<Appointment> appointments)
        {
            foreach (Schedule schedule in schedules)
            {
                if (schedule.OccursOnDate(checkDate))
                {
                    appointments.Add(GenerateAppointment(checkDate, schedule));
                }
            }
        }

        private Appointment GenerateAppointment(DateTime checkDate, Schedule schedule)
        {
            return new Appointment
            {
                Name = schedule.Name,
                Time = checkDate.Add(schedule.TimeOfDay)
            };
        }
    }
}
