using Minesweeper;
using System.Runtime.InteropServices;
using System.Text;

namespace Minesweeper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Game Game = new Game();
                Game.PlayGame();

                Console.WriteLine("Press 1 to play again!");
                string input = Console.ReadLine();
                Thread.Sleep(1000);
                Console.Clear();

                if (input != "1")
                {
                    break;
                }
            }
        }
    }
}