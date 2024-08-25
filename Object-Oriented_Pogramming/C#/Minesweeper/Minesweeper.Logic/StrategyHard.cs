using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class StrategyHard : IStrategy
    {
        public int XSize { get; } = 30;
        public int YSize { get; } = 16;
        public int NumberOfBombs { get; } = 99;
    }
}
