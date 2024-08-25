using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Minesweeper.Test
{
    /// <summary>
    /// Provides unit tests for determining difficulty level in Minesweeper.
    /// </summary>
    [TestClass]
    public class DifficultyTests
    {
        /// <summary>
        /// Tests getting difficulty level for easy input.
        /// </summary>
        [TestMethod]
        public void GetDifficulty_EasyInput_ReturnsEasy()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                Console.SetIn(new StringReader("E\n"));

                var expectedDifficulty = "E";
                var actualDifficulty = GetDifficulty();

                Assert.AreEqual(expectedDifficulty, actualDifficulty);
            }
        }

        /// <summary>
        /// Tests getting difficulty level for medium input.
        /// </summary>
        [TestMethod]
        public void GetDifficulty_MediumInput_ReturnsMedium()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                Console.SetIn(new StringReader("M\n"));

                var expectedDifficulty = "M";
                var actualDifficulty = GetDifficulty();

                Assert.AreEqual(expectedDifficulty, actualDifficulty);
            }
        }

        /// <summary>
        /// Tests getting difficulty level for hard input.
        /// </summary>
        [TestMethod]
        public void GetDifficulty_HardInput_ReturnsHard()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                Console.SetIn(new StringReader("H\n"));

                var expectedDifficulty = "H";
                var actualDifficulty = GetDifficulty();

                Assert.AreEqual(expectedDifficulty, actualDifficulty);
            }
        }

        /// <summary>
        /// Tests that no difficulty level is returned for invalid input.
        /// </summary>
        [TestMethod]
        public void GetDifficulty_InvalidInput_NothingHappens()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                Console.SetIn(new StringReader("X\n"));

                GetDifficulty();

                Assert.AreEqual("Please enter your difficulty: \r\nE = Easy\r\nM = Medium\r\nH = Hard\r\n", sw.ToString());
            }

        }

        /// <summary>
        /// Gets the difficulty level based on user input.
        /// </summary>
        /// <returns>The difficulty level selected by the user.</returns>
        private string GetDifficulty()
        {
            Console.WriteLine("Please enter your difficulty: ");
            Console.WriteLine("E = Easy");
            Console.WriteLine("M = Medium");
            Console.WriteLine("H = Hard");

            string difficulty = Console.ReadLine()?.ToUpper();

            return difficulty;
        }
    }
}
