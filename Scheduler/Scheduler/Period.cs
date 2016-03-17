using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduler
{
    public class Period
    {
        public DateTime Start { get; private set; }

        public DateTime End { get; private set; }

        public Period(DateTime start, DateTime end)
        {
            Start = start.Date;
            End = end.Date;

            if (Start > End)
            {
                throw new ArgumentException("The start date may not be after the end date.");
            }
        }
    }
}
