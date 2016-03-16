using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Star_Trek_Battle_2014
{
    abstract class KlingonShip : SpaceShip
    {
        const string PREFIX = "KES ";
        public KlingonShip(string name, int initShield) : base(PREFIX + name, initShield)
        {
        }

        public abstract override int Fire();
    }
}
