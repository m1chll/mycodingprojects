using System;
using System.Collections.Generic;

namespace Minesweeper
{
    public class GameboardCreator
    {
        public Gameboard CreateGameboard(string difficultyString)
        {
            IStrategy difficulty;
            switch (difficultyString.ToUpper())
            {
                case "H":
                    difficulty = new StrategyHard();
                    break;
                case "M":
                    difficulty = new StrategyMedium();
                    break;
                default:
                    difficulty = new StrategyEasy();
                    break;
            }

            Gameboard gameboard = new Gameboard();
            gameboard.CreateFields(difficulty.XSize, difficulty.YSize);

            AddBombs(gameboard, difficulty.NumberOfBombs);

            CalculateBombsAround(gameboard);

            gameboard.BombCount = difficulty.NumberOfBombs;
            return gameboard;
        }

        private void AddBombs(Gameboard gameboard, int numberOfBombs)
        {
            Random random = new Random();
            int bombsPlaced = 0;
            while (bombsPlaced < numberOfBombs)
            {
                int x = random.Next(gameboard.XSize);
                int y = random.Next(gameboard.YSize);

                if (!gameboard.Fields[x][y].IsBomb)
                {
                    gameboard.Fields[x][y].IsBomb = true;
                    bombsPlaced++;
                }
            }
        }

        private void CalculateBombsAround(Gameboard gameboard)
        {
            for (int i = 0; i < gameboard.XSize; i++)
            {
                for (int j = 0; j < gameboard.YSize; j++)
                {
                    if (!gameboard.Fields[i][j].IsBomb)
                    {
                        gameboard.Fields[i][j].BombsAround = CountBombsAround(gameboard, i, j);
                    }
                }
            }
        }

        private int CountBombsAround(Gameboard gameboard, int x, int y)
        {
            int bombsAround = 0;

            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (i >= 0 && i < gameboard.XSize && j >= 0 && j < gameboard.YSize && gameboard.Fields[i][j].IsBomb)
                    {
                        bombsAround++;
                    }
                }
            }
            return bombsAround;
        }

    }
}
