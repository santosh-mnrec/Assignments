using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSScheduling
{
    public class Job
    {
        
        public int StartTime { get; set; }
        public int EndTime { get; set; }

        public int Id { get; set; }
        public int SubmitTime { get; set; }
        public int CpuTime { get; set; }
        public int CpuTimeLeft { get; set; }
        public int ProcessCompletionTime { get; set; }
        public int ProcessArrivalTime { get; set; }

        public int WaitingTime { get; set; }
        public int TurnAroundTime { get; set; }

        private JobFinishEvent evt;

        public int ArrivalTIme { get; set; }
        public int ProcessId { get; set; }

        public Job(int id, int submitTime, int CPUTime, JobFinishEvent evt)
        {

            this.Id = id;
            this.SubmitTime = submitTime;
            this.CpuTime = CPUTime;
            this.CpuTimeLeft = CPUTime;
            this.evt = evt;
        }

        public Job(int processId, int arrivalTime, int cpuTime)
        {

            this.ProcessId = processId;
            this.ArrivalTIme = arrivalTime;
            this.CpuTime = cpuTime;

        }

        public void start(int sysTime)
        {
            EndTime = sysTime;
        }

        public void tick(int sysTime)
        {
            CpuTimeLeft--;
            if (CpuTimeLeft <= 0)
            {
                EndTime = sysTime;
                evt.onFinish(this);
            }

        }


    }
}