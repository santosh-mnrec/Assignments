using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Star_Trek_Battle_2014
{
    class GalaxyClass : StarShip
    {
        private int pTorpedos;//photon torpedos
        const int PHASER_STRENGTH = 1;
        const int TORPEDO_STRENGTH = 2;

        public GalaxyClass(string registry, string name, int initShield) : base(registry, name, initShield)
        {
            //default quantity for torpedos is 10
            //phasers are unlimited
            pTorpedos = 10;            
        }

        public override int Fire()
        {
            // randomly select a weapon and return it fire strength
            Random rand = new Random();
            int weapon = rand.Next(200);//random number between 0 - 200

            if (weapon < 100) //firing phasers (unlimited supply, fire strength = 2
            {
                FiredWeapon = "phaser";
                return PHASER_STRENGTH;
            }
                
            else //firing photon torpedos (supply is 10, fire strength = 2)
            {
                if (pTorpedos == 0)
                {
                    //if no more torpedoes are available, we need to reload back to 10
                    pTorpedos = 10;
                    FiredWeapon = "photon torpedos";
                    return 0;
                }

                //decrement torpedos
                pTorpedos--;
                FiredWeapon = "photon torpedo";
                return TORPEDO_STRENGTH;
            }
        }
    }
}
