using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    /// <summary>
    /// Represents a strategy interface for configuring game parameters in Minesweeper.
    /// </summary>
    public interface IStrategy
    {
        /// <summary>
        /// Gets the size of the gameboard along the X-axis.
        /// </summary>
        int XSize { get; }

        /// <summary>
        /// Gets the size of the gameboard along the Y-axis.
        /// </summary>
        int YSize { get; }

        /// <summary>
        /// Gets the number of bombs to be placed on the gameboard.
        /// </summary>
        int NumberOfBombs { get; }
    }
}
