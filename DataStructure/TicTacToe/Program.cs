using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            bool gameOver = false;
            string name = "";
            Console.WriteLine("Enter Player 1");
            name = Console.ReadLine();
            TicTacToe t1 = new TicTacToe(1, name);

            Console.WriteLine("Enter Player 2");
            name = Console.ReadLine();
            TicTacToe t2 = new TicTacToe(2, name);
            while (!gameOver)
            {
                TicTacToe.InitBoard();
                while (!t1.PlayGame() && !t2.PlayGame())
                {
                    gameOver = true;
                }
                if (gameOver)
                {
                    Console.WriteLine("Again");
                    if (Console.ReadLine() == "y")
                    {
                        gameOver = false;
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
