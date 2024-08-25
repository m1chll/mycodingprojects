using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper;

namespace Minesweeper.Test
{
    /// <summary>
    /// Provides unit tests for checking game results in Minesweeper.
    /// </summary>
    [TestClass]
    public class CheckGameResulttests
    {
        /// <summary>
        /// Tests that revealing a bomb ends the game.
        /// </summary>
        [TestMethod]
        public void RevealingBombEndsGame()
        {
            Game game = new Game();
            Gameboard gameboard = new Gameboard();
            FieldInput bombFieldInput = new FieldInput(0, 0, FieldInput.UserAction.Reveal);
            gameboard.CreateFields(3, 3);
            gameboard.Fields[0][0].IsBomb = true;

            GameStatus status = gameboard.UpdateFields(bombFieldInput);

            Assert.AreEqual(GameStatus.Lost, status);
        }

        /// <summary>
        /// Tests that revealing all non-bomb fields wins the game.
        /// </summary>
        [TestMethod]
        public void RevealingAllNonBombFieldsWinsGame()
        {

            Game game = new Game();
            Gameboard gameboard = new Gameboard();
            gameboard.CreateFields(3, 3);
            gameboard.Fields[0][0].IsBomb = false;
            gameboard.Fields[0][1].IsBomb = false;
            gameboard.Fields[0][2].IsBomb = false;
            gameboard.Fields[1][0].IsBomb = false;
            gameboard.Fields[1][1].IsBomb = false;
            gameboard.Fields[1][2].IsBomb = true;
            gameboard.Fields[2][0].IsBomb = true;
            gameboard.Fields[2][1].IsBomb = false;
            gameboard.Fields[2][2].IsBomb = false;

            gameboard.UpdateFields(new FieldInput(0, 0, FieldInput.UserAction.Reveal));
            gameboard.UpdateFields(new FieldInput(0, 1, FieldInput.UserAction.Reveal));
            gameboard.UpdateFields(new FieldInput(0, 2, FieldInput.UserAction.Reveal));
            gameboard.UpdateFields(new FieldInput(1, 0, FieldInput.UserAction.Reveal));
            gameboard.UpdateFields(new FieldInput(1, 1, FieldInput.UserAction.Reveal));
            gameboard.UpdateFields(new FieldInput(1, 2, FieldInput.UserAction.Flag));
            gameboard.UpdateFields(new FieldInput(2, 0, FieldInput.UserAction.Flag));
            gameboard.UpdateFields(new FieldInput(2, 1, FieldInput.UserAction.Reveal));
            gameboard.UpdateFields(new FieldInput(2, 2, FieldInput.UserAction.Reveal));

            Assert.AreNotEqual(GameStatus.Won, game.GameStatus);
        }
    }
}
