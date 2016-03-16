using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Star_Trek_Battle_2014
{
    class DefiantClass : StarShip
    {
        private int pTorpedos;//photon torpedos
        private int qTorpedos;//quantum torpedos
        const int PHASER_STRENGTH = 1;
        const int pTORPEDO_STRENGTH = 2;
        const int qTORPEDO_STRENGTH = 4;

        public DefiantClass(string registry, string name, int initShield) : base (registry, name, initShield)
        {
            //default quantity for both torpedos is 1
            //phasers are unlimited
            pTorpedos = 1; 
            qTorpedos = 1;
        }

        public override int Fire()
        {
            // randomly select a weapon and return it fire strength
            Random rand = new Random();
            int weapon = rand.Next(300);//random number between 0 - 300

            if (weapon < 100) //firing phasers (unlimited supply, fire strength = 1
            {
                FiredWeapon = "phaser";
                return PHASER_STRENGTH;
            }
                
            else if (weapon < 200) //firing photon torpedos (supply is 1, fire strength = 2)
            {
                if (pTorpedos == 0)
                {
                    //if no more photon torpedos are available, we need to reload back to 1
                    pTorpedos = 1;
                    FiredWeapon = "photon torpedos";
                    return 0;
                }
                //decrement torpedos
                pTorpedos--;
                FiredWeapon = "photon torpedo";
                return pTORPEDO_STRENGTH;
            }
            else //firing quantum torpedos (supply is 1, fire strength = 4)
            {
                if (qTorpedos == 0)
                {
                    //if no more quantum torpedoes are available, we need to reload back to 1
                    qTorpedos = 1;
                    FiredWeapon = "quantum torpedos";
                    return 0;
                }
                //decrement torpedos
                qTorpedos--;
                FiredWeapon = "quantum torpedo";
                return qTORPEDO_STRENGTH;
            }
        }
    }
}
