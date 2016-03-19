using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSScheduling
{
   public class FirstComeFirstServed : AllocationStrategy {
    
    int temp;
    int proceessArrivalTime;
    int waitingTime;
    double avgWaitingTime;
    double avgTurnAroundTime;
    
    public FirstComeFirstServed(List<Job> jobs) :base(jobs) {
      //  super(jobs);
    }

    public override void run()
    {
        
    }
    
    public void run(List<Job> jobList) {
        int count = 0;
       Console.WriteLine("============================================ ");
       Console.WriteLine("Process ID | Turnaround time | Waiting time ");
       Console.WriteLine("============================================ ");
        foreach(Job job in jobList){
            if(count==0){
                job.ProcessArrivalTime = job.ArrivalTIme;
                job.ProcessCompletionTime = job.ArrivalTIme + job.CpuTime;
                }else{
                    job.ProcessArrivalTime = temp - job.ArrivalTIme;
                    job.ProcessCompletionTime = temp + job.CpuTime;
            }
            
            temp = job.ProcessCompletionTime;
            job.TurnAroundTime = temp - job.ArrivalTIme;
            job.WaitingTime = job.TurnAroundTime - job.CpuTime;
            count++;
            
            avgWaitingTime =  avgWaitingTime+job.WaitingTime;
            avgTurnAroundTime = avgTurnAroundTime+job.TurnAroundTime;
           Console.WriteLine("   "+job.ProcessId+"  | "+"   "+job.TurnAroundTime+"  | "+"   "+job.WaitingTime+" ");
           Console.WriteLine("----------------------------------------");
        }
       Console.WriteLine("===============================================");
       Console.WriteLine("Avg waiting time:"+avgWaitingTime/jobList.Count());
       Console.WriteLine("===============================================");
       Console.WriteLine("Avg turn around time:"+avgTurnAroundTime/jobList.Count());
       Console.WriteLine("===============================================");
        
    }
    
}
}
