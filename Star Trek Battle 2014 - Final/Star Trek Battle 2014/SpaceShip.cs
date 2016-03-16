using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Star_Trek_Battle_2014
{    
    abstract class SpaceShip
    {
        public readonly string Name;
        public int ShieldStrength;

        public SpaceShip(string name, int shields)
        {
            Name = name;
            ShieldStrength = shields;
            IsDestroyed = false;
        }

        public bool IsDestroyed { get; set; }

        public void Hit(int hitStrength)
        {
            // decrement ShieldStrength by amount in hitStrength
            ShieldStrength -= hitStrength;

            //if ship was destroyed with this shot, set the statys isDestroyed to true
            if (ShieldStrength <= 0)
            {
                IsDestroyed = true;
            }
        }

        public string FiredWeapon {get; set;}//what weapon was fired
        public abstract int Fire();

        public override string ToString()
        {
            return Name;
        }
    }
}
