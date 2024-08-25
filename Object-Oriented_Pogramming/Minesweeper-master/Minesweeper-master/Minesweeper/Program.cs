using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MinesweeperTest")]
namespace Minesweeper
{
    internal class Program
    {
        static void Main()
        {
            //var score = new Score("Player", 555, 10, 64);
            //var context = new MinesweeperContext();
            //context.Scores.Add(score);
            //context.SaveChanges();

            //context.Scores.OrderByDescending(s => s.Points).ToList();
            EnsureDbMigrated();
            while (true)
            {
                Game game = new Game();
                game.PlayGame();
                Console.ReadKey();
                Console.Clear();
            }
        }

        private static void EnsureDbMigrated()
        {
            using var context = new MinesweeperContext();

                context.Database.Migrate();
        }
    }
}
