using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper;

namespace Minesweeper.Test
{
    /// <summary>
    /// Provides unit tests for input actions on the game board in Minesweeper.
    /// </summary>
    [TestClass]
    public class InputGameboardTests
    {
        /// <summary>
        /// Tests revealing a field on the game board.
        /// </summary>
        [TestMethod]
        public void TestRevealField()
        {
            var gameboard = new Gameboard();
            gameboard.CreateFields(8, 8);
            gameboard.Fields[0][0].IsBomb = false;

            var status = gameboard.UpdateFields(new FieldInput(0, 0, FieldInput.UserAction.Reveal));

            if (status == GameStatus.Lost)
            {
                Console.WriteLine("Game Lost");
            }
            else
            {
                Console.WriteLine("Game Ongoing");
            }
        }

        /// <summary>
        /// Tests placing a flag on the game board.
        /// </summary>
        [TestMethod]
        public void TestPlaceFlag()
        {
            var gameboard = new Gameboard();
            gameboard.CreateFields(8, 8);

            gameboard.UpdateFields(new FieldInput(0, 0, FieldInput.UserAction.Flag));

            if (gameboard.Fields[0][0].HasFlag)
            {
                Console.WriteLine("Flag placed successfully");
            }
            else
            {
                Console.WriteLine("Failed to place flag");
            }
        }

        /// <summary>
        /// Tests removing a flag from the game board.
        /// </summary>
        [TestMethod]
        public void TestRemoveFlag()
        {
            var gameboard = new Gameboard();
            gameboard.CreateFields(8, 8);
            gameboard.Fields[0][0].HasFlag = true;

            gameboard.UpdateFields(new FieldInput(0, 0, FieldInput.UserAction.RemoveFlag));
            if (!gameboard.Fields[0][0].HasFlag)
            {
                Console.WriteLine("Flag removed successfully");
            }
            else
            {
                Console.WriteLine("Failed to remove flag");
            }
        }
    }
}
