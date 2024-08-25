using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper;

namespace Minesweeper.Test
{
    /// <summary>
    /// Provides unit tests for the creation of game boards in Minesweeper.
    /// </summary>

    [TestClass]
    public class BoardCreateTests
    {
        /// <summary>
        /// Tests the creation of a game board for the easy level, ensuring correct size and number of bombs.
        /// </summary>
        [TestMethod]
        public void CreateGameboard_EasyLevel_CreatesCorrectSizeAndBombs()
        {
            GameboardCreator gameboardCreator = new GameboardCreator();

            Gameboard gameboard = gameboardCreator.CreateGameboard("E");

            Assert.AreEqual(8, gameboard.XSize);
            Assert.AreEqual(8, gameboard.YSize);
            Assert.AreEqual(10, gameboard.BombCount);
        }

        /// <summary>
        /// Tests the creation of a game board for the medium level, ensuring correct size and number of bombs.
        /// </summary>
        [TestMethod]
        public void CreateGameboard_MediumLevel_CreatesCorrectSizeAndBombs()
        {
            GameboardCreator gameboardCreator = new GameboardCreator();

            Gameboard gameboard = gameboardCreator.CreateGameboard("M");

            Assert.AreEqual(16, gameboard.XSize);
            Assert.AreEqual(16, gameboard.YSize);
            Assert.AreEqual(40, gameboard.BombCount);
        }

        /// <summary>
        /// Tests the creation of a game board for the hard level, ensuring correct size and number of bombs.
        /// </summary>
        [TestMethod]
        public void CreateGameboard_HardLevel_CreatesCorrectSizeAndBombs()
        {
            GameboardCreator gameboardCreator = new GameboardCreator();

            Gameboard gameboard = gameboardCreator.CreateGameboard("H");

            Assert.AreEqual(30, gameboard.XSize);
            Assert.AreEqual(16, gameboard.YSize);
            Assert.AreEqual(99, gameboard.BombCount);
        }
    }
}
