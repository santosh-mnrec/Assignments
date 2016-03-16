using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirportSimulation
{
    public enum PlaneAction
    {
        Landing,
        TakingOff,
        None
    }
    public class Plane
    {
        private int waitTime;    // the time the plane has been waiting
        private int takeOffTime; // the amount of time needed for taking off
        private int landingTime; // the amount of time needed for landing

        /* the constructor */
        //Place constructor code here for checkpoint #1.
        public Plane()
        {
            waitTime = 0;
            takeOffTime = 3;
            landingTime = 2;
        }

        // Complete each of the four methods below for checkpoint #1.
        /* get the wait time */
        public int getWaitTime()
        {
            return waitTime;
        }

        /* get the time needed to take off */
        public int getTakeOffTime()
        {
            return takeOffTime;
        }

        /* get the time needed to land */
        public int getLandingTime()
        {
            return landingTime;
        }

        /* Update the amount of time the plane has been waiting.
           Note that in the current simulation, delta is always 1. */
        public void updateWaitTime(int delta)
        {
            // add delta to waitTime for Checkpoint #1
            waitTime += delta;
        }

        public static void main(String[] args)
   {
	   Plane p = new Plane();
	   
	   // Check initialization
	  Console.Write("Current Wait Time: %s. Should be %s\r\n", p.getWaitTime(), "0");
	  Console.Write("Current Takeoff Time: %s. Should be %s\r\n", p.getTakeOffTime(), "3");
	  Console.Write("Current Landing Time: %s. Should be %s\r\n", p.getLandingTime(), "2");
	   
	   // Check updateWaitTime
	   p.updateWaitTime(1);
	   p.updateWaitTime(1);
	   p.updateWaitTime(1);
	   
	  Console.Write("Current Wait Time: %s. Should be %s\r\n", p.getWaitTime(), "3");
	   
   }

    }
}
