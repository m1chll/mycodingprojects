using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class StrategyMedium : IStrategy
    {
        public int XSize { get; } = 16;
        public int YSize { get; } = 16;
        public int NumberOfBombs { get; } = 40;
    }
}
