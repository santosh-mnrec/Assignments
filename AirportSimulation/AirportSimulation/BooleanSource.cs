using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AirportSimulation
{
    public class BooleanSource
    {
        private double probability; // The approximate probability of query( ) returning true.

      
        public BooleanSource(double p)
        {
            if ((p < 0) || (1 < p))
                throw new Exception("Illegal p: " + p);
            probability = p;
        }

        /**
        * Get the next value from this <CODE>BooleanSource</CODE>.
        * @param - none
        * @return
        *   The return value is either <CODE>true</CODE> or <CODE>false</CODE>,
        *   with the probability of a <CODE>true</CODE> value being determined
        *   by the argument that was given to the constructor.
        **/

        Random randon = new Random();
        public bool query()
        {
            Thread.Sleep(10);
            return (randon.Next(-1,3)< probability);
            
        }

    }
}
