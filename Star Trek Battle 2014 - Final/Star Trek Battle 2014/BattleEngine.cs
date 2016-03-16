using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Star_Trek_Battle_2014
{
    class BattleEngine
    {
        const int SHIELD = 15;
        static void Main(string[] args)
        {
            // Battle logic goes here
            // 1.  Create an array of 6 SpaceShips
            //I decided to use List instead as it makes much easier to remove ships once they are destroyed
            SpaceShip [] ships = new SpaceShip[6];
            //List <SpaceShip> destroyedShips = new List<SpaceShip>();//holds objects of destroyed ships
            bool gameOver = false;

            // 2.  Load this array with 6 ships:
            //     a. 3 StarShips (either 1 GalaxyClass and 2 DefiantClass OR 2 GalaxyClass and 1 DefiantClass)
            //     b. 3 KlingonShips (either 1 BattleCruiser and 2 BirdOfPrey OR 2 BattleCruiser and 1 BirdOfPrey)
            ships[0] = new BirdOfPrey("Vor'Nak", SHIELD);
            ships[1] = new BattleCruiser("K'Hecth", SHIELD);
            ships[2] = new BattleCruiser("Toh'Koht", SHIELD);
            ships[3] = new DefiantClass("NCC-75633", "Defiant", SHIELD);            
            ships[4] = new GalaxyClass("NCC-1701-D", "Enterprise", SHIELD);            
            ships[5] = new GalaxyClass("NCC-26517-A", "Excalibur", SHIELD);

            Console.WriteLine("Let the battle begin!\n");

            while (!gameOver)
            {
            // 3.  Select 1 ship at random from the array to fire
                //there are 6 ships, so we randomly select number 0-5 
                //that corresponds with the index of a ship in the array
                Random rand = new Random();
                SpaceShip firingShip;
                SpaceShip hitShip;
                int shipIndex = 0; //used to indetify whether USS or KES ship fired

                //we need to loop before assigning the random number
                //otherwise the random generator would not work properly
                for (int i = 0; i < 150000; i++)
                {
                    shipIndex = rand.Next(0, 6);
                }

                firingShip = ships[shipIndex];

            // 4.  Select 1 ship at random from the other side to be hit (that has not already been destroyed)
           
                //depending on what team was firing, we will hit the ship on the opposing team
                //index 0-2 means, USS was firing, so KES was hit
                //index 3-5 means, KES was firing, so USS was hit
                if (shipIndex < 3)
                    hitShip = ships[rand.Next(3, 6)];
                else
                    hitShip = ships[rand.Next(0, 3)];

                //check if valid firing ship and valid hit ship were selected
                //(ship that was already destroyed cannot shoot or be shot at)
                //if this shot destroys the ship, set the status to DESTROYED
                if (!firingShip.IsDestroyed && !hitShip.IsDestroyed)
                {                    
                    int fireStrength = firingShip.Fire();
                    hitShip.Hit(fireStrength);
                    DisplayBattle(firingShip, hitShip, fireStrength);
                    DisplayStatus(ships);
                }

                gameOver = isGameOver(ships);                
                
            // 5.  Continue step 4 until all ships from one side has been destroyed
            }

            Console.ReadKey();
        }

        public static void DisplayBattle(SpaceShip firingShip, SpaceShip hitShip, int fireStrength)
        {
            //if firingStrength was 0 it means no shot was fired as the ship had to reload
            if (fireStrength == 0)
                Console.WriteLine("\n{0} had to reload {1}\n", firingShip.Name, firingShip.FiredWeapon);
            else
            {
                Console.WriteLine("\n{0} fires: {1}", firingShip, firingShip.FiredWeapon);
                Console.WriteLine("{0}: is HIT. Shield strength is {1}\n", hitShip.Name, hitShip.ShieldStrength);
            }
        }

        public static void DisplayStatus(SpaceShip [] ships)
        {
            //display all ships and their status.
            //display destroyed ships at the end
            for (int i = 0; i < 6; i++)
            {
                if (!ships[i].IsDestroyed)
                    Console.WriteLine("{0} shield strength is {1}", ships[i].Name, ships[i].ShieldStrength);
            }

            for (int i = 0; i < 6; i++)
            {
                if (ships[i].IsDestroyed)
                    Console.WriteLine("{0} has been DESTROYED!", ships[i].Name);
            }
                
        }

        public static bool isGameOver(SpaceShip[] ships)
        {
            if (ships[0].IsDestroyed && ships[1].IsDestroyed && ships[2].IsDestroyed)
            {
                Console.WriteLine("Victory for USS");
                return true;
            }
                
            if (ships[3].IsDestroyed && ships[4].IsDestroyed && ships[5].IsDestroyed)
            {
                Console.WriteLine("Victory for KES");
                return true;
            }                

            return false;
        }
    }
}
