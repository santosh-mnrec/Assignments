using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{


    public class AirportSimulation
    {

        public static int SIMULATION_TIME = 100;  // the total number of minutes for the simulation

        private Queue<Plane> takeOffQ;        // the queue of planes waiting to takeoff
        private Queue<Plane> landingQ;        // the queue of planes waiting to land
        private LinkedList<Plane> allPlanes;  // all the active planes in the simulation

        private BooleanSource wantsToTakeOff; // a bool generator to help create new planes
        private BooleanSource wantsToLand;    // a bool generator to help create new planes

        private Runway runway; // the runway object

        // The following variables are used to collect stats on the simulation
        int totalTakeOffWait;  // total time planes have waited to takeoff
        int totalLandingWait;  // total time planes have waited to land

        int totalPlanesLanded; // total number of planes that have landed
        int totalPlanesTakenOff;  // total number of planes that have taken off

        int avgLandingQueueSize;
        int avgTakeOffQueueSize;
        int avgAllPlanesQueueSize;
        int avgCounter;
        int avgBusyCounter;


        // the constructor
        // Place the constructor here for Checkpoint #3.
        public AirportSimulation()
        {
            wantsToLand = new BooleanSource(0.25);
            wantsToTakeOff = new BooleanSource(0.1);

            runway = new Runway();

            takeOffQ = new Queue<Plane>();
            landingQ = new Queue<Plane>();
            allPlanes = new LinkedList<Plane>();

            totalTakeOffWait = 0;
            totalLandingWait = 0;

            totalPlanesLanded = 0;
            totalPlanesTakenOff = 0;

            avgLandingQueueSize = 0;
            avgTakeOffQueueSize = 0;
            avgAllPlanesQueueSize = 0;
            avgCounter = 0;
            avgBusyCounter = 0;
        }

        /* This is the driver method for the simulation.  It runs through the simulation
            updating the planes, runway and summary statistics 
         */
        public void run()
        {

            for (int t = 0; t <= SIMULATION_TIME; t++)
            {
                // Check if a new plane wants to takeoff
                if (wantsToTakeOff.query())
                {
                    Plane p = new Plane();
                    takeOffQ.Enqueue(p);
                    allPlanes.AddFirst(p);
                }

                // Check if a new plane wants to land
                // Place code here for checkpoint #3, to see if any planes
                // want to land.  Hint: This code closely parallels checking
                // if any planes want to take off.
                if (wantsToLand.query())
                {
                    Plane p = new Plane();
                    landingQ.Enqueue(p);
                    allPlanes.AddFirst(p);
                }
                else

                    // Process plane on the runway
                    if (runway.isClear())
                    {
                        // Place code here for Checkpoint #3.
                        // First check if any planes want to land, since landing
                        // takes priority over taking off.  If there is a plane that
                        // wants to land, 
                        // 1. remove it from the landingQ and assign it
                        //    to the runway, with the action of "Landing".
                        // 2. remove it from allPlanes.
                        // 3. Add its waitTime to totalLandingWait
                        // 4. Add 1 to totalPlanesLanded.

                        // else if there is a plane waiting to take off:
                        // - take action appropriate to taking off.  This is
                        //   completely parallel to landing.

                        // If planes want to land
                        if (landingQ.Count>0 && landingQ.Peek() != null)
                        {
                            // remove from the landingQ and assign it
                            Plane landingPlane = landingQ.Dequeue();
                            runway.assignPlane(landingPlane, PlaneAction.Landing);

                            // remove from the allPlanes
                            allPlanes.Remove(landingPlane);

                            // Add it's waitTime to the totalLandingWait
                            totalLandingWait += landingPlane.getLandingTime();

                            //Add 1 to totalPlanesLanded
                            totalPlanesLanded++;
                        }
                        else if (takeOffQ.Count>0 && takeOffQ.Peek() != null)
                        {
                            // remove from the landingQ and assign it
                            Plane takeoffPlane = takeOffQ.Dequeue();
                            runway.assignPlane(takeoffPlane, PlaneAction.TakingOff);

                            // remove from the allPlanes
                            allPlanes.Remove(takeoffPlane);

                            // Add it's waitTime to the totalTakeOffWait
                            totalTakeOffWait += takeoffPlane.getTakeOffTime();

                            //Add 1 to planes taken off
                            totalPlanesTakenOff++;
                        }
                    }
                    else
                    {
                        runway.update();
                    }

                // Add to the wait time of all remaining planes
           var it = allPlanes.GetEnumerator();
                while (it.MoveNext())
                {
                    Plane p = it.Current;
                    p.updateWaitTime(1);
                    avgBusyCounter++;
                }

                avgLandingQueueSize += landingQ.Count;
                avgTakeOffQueueSize += takeOffQ.Count;
                avgAllPlanesQueueSize += allPlanes.Count;
                avgCounter++;
            }
        }

        /* This method prints a brief report summarizing statistics about the simulations */
        public void report()
        {
            Console.WriteLine(totalPlanesLanded + " planes have landed.");
            Console.WriteLine(totalPlanesTakenOff + " planes have taken off.");
            Console.WriteLine(landingQ.Count + " planes still waiting to land.");
            Console.WriteLine(takeOffQ.Count + " planes still waiting to takeoff.");

            Console.WriteLine(1.0 * totalTakeOffWait / SIMULATION_TIME + " average take off wait time.");
            Console.WriteLine(1.0 * totalLandingWait / SIMULATION_TIME + " average landing wait time.");

            Console.WriteLine("Average size of the landingQ: {0}\r\n", avgLandingQueueSize / avgCounter);
            Console.WriteLine("Average size of the takeOffQ: {0}\r\n", avgTakeOffQueueSize / avgCounter);
            Console.WriteLine("Average size of the allPlanes: {0}\r\n", avgAllPlanesQueueSize / avgCounter);
            Console.WriteLine("Percent Busy: %s\r\n", 1.0 - ((double)totalPlanesLanded + (double)totalPlanesTakenOff) / (double)avgCounter);

        }


        public static void Main(String[] args)
        {
            AirportSimulation sim = new AirportSimulation();
            sim.run();
            sim.report();
        }
    }
}
