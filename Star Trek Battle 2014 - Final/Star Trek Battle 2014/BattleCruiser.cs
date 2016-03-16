using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Star_Trek_Battle_2014
{
    class BattleCruiser : KlingonShip
    {
        private int pTorpedos;//photon torpedos
        const int TORPEDO_STRENGTH = 2;
        const int DISRUPTOR_STRENGTH = 1;

        public BattleCruiser(string name, int initShield) : base(name, initShield)
        {
            //default quantity for torpedos is 10
            //unlimited supply of disruptors
            pTorpedos = 10;
        }

        public override int Fire()
        {
            // randomly select a weapon and return it fire strength
            Random rand = new Random();
            int weapon = rand.Next(200);//random number between 0 - 200

            if (weapon < 100) //firing disruptors (unlimited supply, fire strength = 1
            {
                FiredWeapon = "disruptor";
                return DISRUPTOR_STRENGTH;
            }                
            else //firing photon torpedos (supply is 3, fire strength = 2)
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
