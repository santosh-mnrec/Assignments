using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankSimulation
{

    public class Customer
    {
       public int clock, currentTime;
        public Customer()
        { }
        public Customer(int theTime)
        {
            clock = theTime;
        }

        public int getCurrent(int current)
        {
            currentTime = clock - current;
            return currentTime;
        }
    }
}
