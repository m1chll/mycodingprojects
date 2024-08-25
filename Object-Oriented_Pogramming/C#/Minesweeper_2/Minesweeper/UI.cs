using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks.Sources;
using System.Runtime.CompilerServices;
using System.Drawing;
using System.Media;
using System.Reflection;
using NAudio.Wave;

namespace Minesweeper
{
    public class UI
    {
        /// <summary>
        /// Stopwatch used to track elapsed time during the game.
        /// </summary>
        ConsoleColor CurrentColor;
        public Stopwatch stopwatch = new Stopwatch();

        public void PrintStartScreen()
        {
            string input;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("         ______   __  __  ____    ____    __      __  ____    ____    ____    ____    ____       ");
            Console.WriteLine(" /'\\_/`\\/\\__  _\\ /\\ \\ /\\ \\ /\\  _`\\ /\\  _`\\ /\\ \\  __/\\ \\ /\\  _`\\ /\\  _`\\ /\\  _`\\ /\\  _`\\     ");
            Console.WriteLine("/\\      \\/_/\\ \\/ \\ \\ `\\\\ \\ \\ \\L\\_\\ \\,\\L\\_\\ \\ \\/\\ \\ \\ \\ \\ \\L\\_\\ \\ \\L\\_\\ \\ \\L\\ \\ \\ \\L\\_\\ \\ \\L\\ \\   ");
            Console.WriteLine("\\ \\ \\__\\ \\ \\ \\ \\  \\ \\ , ` \\ \\  _\\L\\/_\\__ \\\\ \\ \\ \\ \\ \\ \\ \\  _\\L\\ \\  _\\L\\ \\ ,__/\\ \\  _\\L\\ \\ ,  /   ");
            Console.WriteLine(" \\ \\ \\_/\\ \\ \\_\\ \\__\\ \\ \\`\\ \\ \\ \\L\\ \\/\\ \\L\\ \\ \\ \\_/ \\_\\ \\ \\ \\L\\ \\ \\ \\L\\ \\ \\ \\/  \\ \\ \\L\\ \\ \\ \\\\ \\  ");
            Console.WriteLine("  \\ \\_\\\\ \\_\\/\\_____\\\\ \\_\\ \\_\\ \\____/\\ `\\____\\ `\\___x___/\\ \\____/\\ \\____/\\ \\_\\   \\ \\____/\\ \\_\\ \\_\\");
            Console.WriteLine("   \\/_/ \\/_/\\/_____/ \\/_/\\/_/\\/___/  \\/_____/'\\/__/\\__/  \\/___/  \\/___/  \\/_/    \\/___/  \\/_/\\/_/");
            Console.WriteLine("");
            Console.WriteLine("2");

            Console.ResetColor();
            Console.WriteLine("");
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("Instructions:");
            Console.WriteLine("   1. You can decide what size you want your gameboard to be but it must be a square. Typing a single number is enough.");
            Console.WriteLine("   2. After, you'll be presented with a grid of covered cells.");
            Console.WriteLine("   3. Enter coordinates (row, column) to reveal a cell or flag it.");
            Console.WriteLine("   4. After the coordinates of the field, type:");
            Console.WriteLine("     '- R' to Reveal.'");
            Console.WriteLine("     '- F' to set a Flag.'");
            Console.WriteLine("     '- RF' to set Remove a Flag.'");
            Console.WriteLine("     '- Example: \"a4r\"'");
            Console.WriteLine("   5. Numbers indicate how many adjacent cells contain mines.");
            Console.WriteLine("   6. Be careful! If you reveal a mine, the game ends.");
            Console.WriteLine("   7. You win by setting a flag on all covered mines. ;)");
            Console.WriteLine("   8. As soon as you enter the size you want your gameboard to be, a stopwatch starts. So think fast!");
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("Legend:");
            Console.WriteLine("   - '■' represents a covered cell.");
            Console.WriteLine("   - 'M' is a mine.");
            Console.WriteLine("   - 'F' is a flag marking a potential mine.");
            Console.WriteLine("   - Numbers indicate how many mines are adjacent to that cell.");
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("Hotkeys:");
            Console.WriteLine("   - 'U' Undo the last move.");
            Console.WriteLine("   - 'P' Make a break.");
            Console.WriteLine("   - 'C' Continue after making a break.");
            Console.WriteLine("   - Numbers indicate how many mines are adjacent to that cell.");
            Console.WriteLine("-----------------------------------------------------------------------");


            while (true)
            {
                Console.WriteLine("Press 1 to start");
                input = Console.ReadLine();
                if (input == "1")
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Asks the user for the game difficulty (Hard, Medium, Easy).
        /// </summary>
        /// <returns>The selected difficulty level.</returns>
        public string GetDifficulty()
        {
            string difficulty = "";
            Regex validInput = new Regex("^[EMH]$"); // Regex für E, M, oder H

            while (!validInput.IsMatch(difficulty))
            {
                Console.WriteLine("Please enter your difficulty: ");
                Console.WriteLine("E = Easy");
                Console.WriteLine("M = Medium");
                Console.WriteLine("H = Hard");
                difficulty = Console.ReadLine().ToUpper(); // Großbuchstaben, um auf Groß-/Kleinschreibung nicht zu achten

                if (!validInput.IsMatch(difficulty))
                {
                    Console.WriteLine("Invalid input. Please enter E, M, or H.");
                }
            }
            return difficulty;
        }


        /// <summary>
        /// Asks the user for field coordinates and action (Reveal, Flag, Remove Flag).
        /// </summary>
        /// <returns>The input field coordinates and action.</returns>
        public FieldInput GetFieldUpdate()
        {
            bool inputCorrect = false;
            string fieldInputString;

            do
            {
                Console.WriteLine("Enter coordinates (row, column) and action (R for Reveal, F for Flag, RF for Remove Flag), e.g., 'A4R':");
                fieldInputString = Console.ReadLine();
                fieldInputString = fieldInputString.ToUpper();
                inputCorrect = CheckFieldInput(fieldInputString);
            } while (!inputCorrect);

            if(fieldInputString == "U" || fieldInputString == "u")
            {
                FieldInput inputUndo = new FieldInput(0,0,FieldInput.UserAction.Undo);
                return inputUndo;   
            }
            else if(fieldInputString == "P" || fieldInputString == "P")
            {
                FieldInput inputPause = new FieldInput(0, 0, FieldInput.UserAction.Pause);
                return(inputPause);
            }
            else
            {
                int fieldInputLength = fieldInputString.Length;

                Match xMatch = Regex.Match(fieldInputString, @"[A-Z]");
                char yChar = Convert.ToChar(xMatch.Value);
                int yCoordinate = (int)yChar - 65;


                Match yMatch = Regex.Match(fieldInputString, @"\d{1,2}");
                int xCoordinate = Convert.ToInt32(yMatch.Value) - 1;

                Match actionMatch = Regex.Match(fieldInputString, @"(RF|R|F)$");
                string action = actionMatch.Value;

                var enumValue = action switch
                {
                    "R" => FieldInput.UserAction.Reveal,
                    "F" => FieldInput.UserAction.Flag,
                    "RF" => FieldInput.UserAction.RemoveFlag,
                    _ => FieldInput.UserAction.Reveal,
                };

                FieldInput fieldInput = new FieldInput(xCoordinate, yCoordinate, enumValue);

                return fieldInput;
            }
        }



        /// <summary>
        /// Checks if the entered field input is valid.
        /// </summary>
        /// <param name="input">The input string to be checked.</param>
        /// <returns>True if the input is valid, otherwise false.</returns>
        public bool CheckFieldInput(string input)
        {
            Regex rg = new Regex(@"([A-Z](2[0-6]|1[0-9]|[1-9])(RF|R|F))|((2[0-6]|1[0-9]|[1-9])[A-Z](RF|R|F))");
            if (Regex.IsMatch(input, "[UuPp]"))
            {
                return true;
            }
            else if (input.Length < 3 || input.Length > 5 | rg.IsMatch(input) != true)
            {
                return false;
            }
            return true;
        }

        public void MakePause()
        {
            string input;
            do
            {
                stopwatch.Stop();
                Console.WriteLine("Press C to continue!");
                input = Console.ReadLine();
            } while (input != "C" && input != "c");
            stopwatch.Start();
        }


        /// <summary>
        /// Prints the current game board.
        /// </summary>
        /// <param name="gameBoard">The game board to be printed.</param>
        public void PrintGame(List<List<string>> gameBoard, int bombs, int flags)
        {
            Console.WriteLine();
            PrintGameInformations(bombs, flags);
            PrintGameBoard(gameBoard);
        }

        /// <summary>
        /// Prints the number of bombs and flags.
        /// </summary>
        /// <param name="bombs">The number of bombs.</param>
        /// <param name="flags">The number of flags.</param>
        private void PrintGameInformations(int bombs, int flags)
        {

            if(!stopwatch.IsRunning)
            {
                stopwatch.Start();
            }
            TimeSpan elapsedTime = stopwatch.Elapsed;
            Console.WriteLine($"Bombs: {bombs} Flags: {flags} Time: {elapsedTime.Hours:00}:{elapsedTime.Minutes:00}:{elapsedTime.Seconds:00}");

        }

        /// <summary>
        /// Prints the game board.
        /// </summary>
        /// <param name="gameBoard">The game board to be printed.</param>
        /// <summary>
        /// Prints the game board.
        /// </summary>
        /// <param name="gameBoard">The game board to be printed.</param>
        public void PrintGameBoard(List<List<string>> gameBoard)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
            Console.Write("   ");

            for (int i = 0; i < gameBoard.Count; i++)
            {
                if (i % 2 == 1)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (i % 2 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                CurrentColor = Console.ForegroundColor;

                Console.Write(Convert.ToChar(i + 65) + " ");
            }

            Console.WriteLine("");
            int t = 0;
            int z = 0;
            foreach (var list in gameBoard)
            {
                t++;

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(String.Format("{0:00}", t) + " ");

                z = 0;
                foreach (var element in list)
                {
                    z++;

                    if (z % 2 == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (z % 2 == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }

                    switch (element)
                    {
                        case "P":
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write(element + " ");
                            break;

                        case "M":
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(element + " ");
                            break;

                        case "X":
                            Console.Write("■" + " ");
                            break;

                        case "0":
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(element + " ");
                            break;

                        case "1":
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write(element + " ");
                            break;

                        case "2":
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write(element + " ");
                            break;

                        case "3":
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(element + " ");
                            break;

                        case "4":
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write(element + " ");
                            break;

                        case "5":
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            Console.Write(element + " ");
                            break;

                        case "6":
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(element + " ");
                            break;

                        case "7":
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write(element + " ");
                            break;

                        case "8":
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write(element + " ");
                            break;

                        default:
                            Console.Write(element + " ");
                            break;
                    }
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }
        public void PrintGameWon()
        {
            Console.Clear();
            Thread.Sleep(300);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("██╗   ██╗ ██████╗ ██╗   ██╗    ██╗    ██╗ ██████╗ ███╗   ██╗██╗");
            Console.WriteLine("╚██╗ ██╔╝██╔═══██╗██║   ██║    ██║    ██║██╔═══██╗████╗  ██║██║");
            Console.WriteLine(" ╚████╔╝ ██║   ██║██║   ██║    ██║ █╗ ██║██║   ██║██╔██╗ ██║██║");
            Console.WriteLine("  ╚██╔╝  ██║   ██║██║   ██║    ██║███╗██║██║   ██║██║╚██╗██║╚═╝");
            Console.WriteLine("   ██║   ╚██████╔╝╚██████╔╝    ╚███╔███╔╝╚██████╔╝██║ ╚████║██╗");
            Console.WriteLine("   ╚═╝    ╚═════╝  ╚═════╝      ╚══╝╚══╝  ╚═════╝ ╚═╝  ╚═══╝╚═╝");
            Console.WriteLine();
            Console.ResetColor();
            stopwatch.Reset();
        }

        public void PrintGameLost()
        {
            Console.Clear();
            Thread.Sleep(300);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(" ██████╗  █████╗ ███╗   ███╗███████╗     ██████╗ ██╗   ██╗███████╗██████╗ ██╗");
            Console.WriteLine("██╔════╝ ██╔══██╗████╗ ████║██╔════╝    ██╔═══██╗██║   ██║██╔════╝██╔══██╗██║");
            Console.WriteLine("██║  ███╗███████║██╔████╔██║█████╗      ██║   ██║██║   ██║█████╗  ██████╔╝██║");
            Console.WriteLine("██║   ██║██╔══██║██║╚██╔╝██║██╔══╝      ██║   ██║╚██╗ ██╔╝██╔══╝  ██╔══██╗╚═╝");
            Console.WriteLine("╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗    ╚██████╔╝ ╚████╔╝ ███████╗██║  ██║██╗");
            Console.WriteLine(" ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝     ╚═════╝   ╚═══╝  ╚══════╝╚═╝  ╚═╝╚═╝");
            Console.WriteLine();
            Console.ResetColor();
            stopwatch.Reset();
        }
        public static void PlaySound(string fileName)
        {
            var assembly = typeof(UI).Assembly;

            var resStream = assembly.GetManifestResourceStream($"Minesweeper.Sounds.{fileName}");

            if (resStream is null)
            {
                return;
            }
            var pathOfAssembly = typeof(Program).Assembly.Location;
            SoundPlayer musicPlayer = new SoundPlayer(resStream);
            musicPlayer.Play();
        }
    }
}


