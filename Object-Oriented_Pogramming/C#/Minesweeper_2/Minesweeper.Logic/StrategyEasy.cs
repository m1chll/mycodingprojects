using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class StrategyEasy : IStrategy
    {
        public int XSize { get; } = 8;
        public int YSize { get; } = 8;
        public int NumberOfBombs { get; } = 10;
    }
}
