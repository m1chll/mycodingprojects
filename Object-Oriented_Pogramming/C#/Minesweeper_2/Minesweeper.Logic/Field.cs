using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    /// <summary>
    /// Represents a single field in the Minesweeper game.
    /// </summary>
    public class Field
    { 
        /// <summary>
        /// Gets or sets a value indicating whether the field contains a bomb.
        /// </summary>
        public bool IsBomb { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether the field has a flag.
        /// </summary>
        public bool HasFlag { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether the field is visible.
        /// </summary>
        public bool IsVisible { get; set; } = false;

        /// <summary>
        /// Gets or sets the number of bombs around the field.
        /// </summary>
        public int BombsAround { get; set; }


        /// <summary>
        /// Gets the value of the field.
        /// </summary>
        /// <returns>The value of the field, which could be a bomb, number of bombs around, or flag.</returns>
        public string GetValue()
        {
            if (HasFlag)
            {
                return "F";
            }
            else if (IsVisible && !IsBomb)
            {
                return Convert.ToString(BombsAround);
            }
            else
            {
                return "X";
            }
        }
    }
}
