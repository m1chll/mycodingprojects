using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class Score
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public  string Player { get; private set; }
        public double Points { get; private set; }
        public double Seconds { get; private set; }
        public int Bombs { get; private set; }
        public int FieldSize { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Score() 
        {

        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public Score(string player, double seconds, int bombs, int fieldSize) 
        {
            Player = player;
            Seconds = Math.Round(seconds, 4);
            Bombs = bombs;
            FieldSize= fieldSize;
            Points = Math.Round(103 - Seconds / Bombs, 4);

            if (Points > 100)
            {
                Points = 100;
            }
        }
    }
}
