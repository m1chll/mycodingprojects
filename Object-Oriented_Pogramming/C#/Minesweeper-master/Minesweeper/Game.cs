using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    internal class Game
    {


        public int GameboardSize { get; set; }
        private string Username { get; set; }   
        public void PlayGame()
        {
            Gameboard gameboard = new Gameboard();
            UI.Instance.PrintStartScreen();
            Username = UI.Instance.GetUsername();
            int gameboardSize = UI.Instance.GetGameboardSize();
            gameboard.CreateGameboard(gameboardSize);
            Thread.Sleep(100);
            Console.Clear();
            var content = gameboard.GetFieldRepresentation();
            UI.Instance.PrintGame(content, gameboardSize, gameboard.BombCount, gameboard.FlagCount);

            bool gameOver = false;

            while (gameOver == false) 
            {
                FieldInput updateField = UI.Instance.GetFieldUpate();
                gameboard.UpdateField(updateField);
                content = gameboard.GetFieldRepresentation();
                Console.Clear();
                UI.Instance.PrintGame(content, gameboardSize, gameboard.BombCount, gameboard.FlagCount);
                gameOver = IsGameOver(gameboard);
            }


        }

        bool IsGameOver(Gameboard gameBoard)
        {
            int markedBomb = default;
            foreach(var field in gameBoard)
            {
                if (field.IsVisible == true && field.IsBomb == true)
                {
                    UI.Instance.GameLost();
                    return true;
                }
                else if (field.IsBomb && field.HasFlag)
                {
                    markedBomb++;
                }
            }

            if (markedBomb == gameBoard.BombCount)
            {
                Score score = new Score(Username, UI.Instance.GetTime(), gameBoard.BombCount, gameBoard.GameboardSize);
                Highscore.AddScoreToDatabase(score);
                UI.Instance.GameWon(score);
                return true;
            }

            return false;
        }
    }
}


