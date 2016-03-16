using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Star_Trek_Battle_2014
{
    abstract class StarShip : SpaceShip
    {
        //readonly, set via constructor
        public readonly string Registry;
        const string PREFIX = "USS ";
        public StarShip(string registry, string name, int initShields) : base(PREFIX + name, initShields)
        {
            Registry = registry;
        }

        public abstract override int Fire();

        public override string ToString()
        {
            return Registry + " " + base.ToString();
        }
    }
}
