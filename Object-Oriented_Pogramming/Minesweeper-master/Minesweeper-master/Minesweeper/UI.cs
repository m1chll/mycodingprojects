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

namespace Minesweeper;

internal class UI
{

    private UI() { }

    private static UI? _instance;
    public static UI Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UI();
            }
            return _instance;
        }
    }



    private int GameboardSize { get; set; }
    private string _fieldInput;
    public Stopwatch stopwatch = new Stopwatch();
    CancellationTokenSource source = new CancellationTokenSource();
    ConsoleColor currentLineColor;
    private bool _isStarted = false;
    private string UserName { get; set; }

    public void PrintStartScreen()
    {
        string input;

        Console.ForegroundColor = ConsoleColor.Cyan;
        WriteLine("    __  ________   ______________       __________________  __________");
        WriteLine("   /  |/  /  _/ | / / ____/ ___| |     / / ____/ ____/ __ \\/ ____/ __ \\");
        WriteLine("  / /|_/ // //  |/ / __/  \\__ \\| | /| / / __/ / __/ / /_/ / __/ / /_/ /");
        WriteLine(" / /  / _/ // /|  / /___ ___/ /| |/ |/ / /___/ /___/ ____/ /___/ _, _/ ");
        WriteLine("/_/  /_/___/_/ |_/_____//____/ |__/|__/_____/_____/_/   /_____/_/ |_|  ");
        Console.ResetColor();
        WriteLine("");

        WriteLine("-----------------------------------------------------------------------");
        WriteLine("Instructions:");
        WriteLine("   1. You can decide what size you want your gameboard to be but it must be a square. Typing a single number is enough.");
        WriteLine("   2. After, you'll be presented with a grid of covered cells.");
        WriteLine("   3. Enter coordinates (row, column) to reveal a cell or flag it.");
        WriteLine("   4. After the coordinates of the field, type:");
        WriteLine("     '- R' to Reveal.'");
        WriteLine("     '- F' to set a Flag.'");
        WriteLine("     '- RF' to set Remove a Flag.'");
        WriteLine("     '- Example: \"a4r\"'");
        WriteLine("   5. Numbers indicate how many adjacent cells contain mines.");
        WriteLine("   6. Be careful! If you reveal a mine, the game ends.");
        WriteLine("   7. You win by setting a flag on all covered mines. ;)");
        WriteLine("   8. As soon as you enter the size you want your gameboard to be, a stopwatch starts. So think fast!");
        WriteLine("-----------------------------------------------------------------------");
        WriteLine("Legend:");
        WriteLine("   - '■' represents a covered cell.");
        WriteLine("   - 'M' is a mine.");
        WriteLine("   - 'P' is a flag marking a potential mine.");
        WriteLine("   - Numbers indicate how many mines are adjacent to that cell.");
        WriteLine("-----------------------------------------------------------------------");
        while (true)
        {
            WriteLine("Press 1 to see the Highscores");
            WriteLine("Press 2 to play MINESWEEPER");
            input = Console.ReadLine();
            if (input == "1")
            {
                Console.Clear();
                PrintHighscores();
                WriteLine("");
                WriteLine("");
            }
            if (input == "2") 
            {
                break;
            }
        _isStarted = false;
        }
        Console.Clear();
    }

    private void PrintHighscores()
    {
        List<Score> topScores = Highscore.GetTopScores();
        List<List<string>> highestScores = Highscore.ConvertHighscorestoList(topScores);

        foreach (var scoreInfo in highestScores)
        {
            foreach (var element in scoreInfo) 
            { 
                Write(element + ", ");
            }
            WriteLine("");
        }
    }


    public string GetUsername()
    {
        string pattern = @"^.{1,30}$";
        string userName;
        do
        {
            Write("Please enter your name: ");
            userName = Console.ReadLine();
        }
        while (!Regex.IsMatch(userName, pattern));

        return userName;        
    }


    public int GetGameboardSize()
    {
        int gameboardSize;
        while (true)
        {
            WriteLine("What size should your gameboard be? (enter a number between 8 and 26) ");
            if (int.TryParse(Console.ReadLine(), out gameboardSize))
            {
                if (gameboardSize >= 8 && gameboardSize <= 26)
                {
                    break;
                }
                else
                {
                    WriteLine("Please enter a number in the range of 8 and 26.");
                }
            }
            else
            {
                WriteLine("Please enter a valid number in the range of 8 and 26.");
            }
        }

        GameboardSize = gameboardSize;
        Console.Clear();
        stopwatch.Reset();
        stopwatch.Start();
        return GameboardSize;
    }

    public FieldInput GetFieldUpate()
    {

        bool inputCheck = false;
        int xCoordinate;
        int yCoordinate;
        string actionS;
        FieldInput.Action action;


        do
        {

            Write("Which field do you want to change?: ");

            _fieldInput = Console.ReadLine();
            _fieldInput = _fieldInput.ToUpper();
            inputCheck = CheckFieldInput(_fieldInput);
        } while (inputCheck == false);
        int fieldInputLength = _fieldInput.Length;

        Match yMatch = Regex.Match(_fieldInput, @"[A-Z]");
        char yChar = Convert.ToChar(yMatch.Value);
        yCoordinate = (int)yChar - 65;


        Match xMatch = Regex.Match(_fieldInput, @"\d{1,2}");
        xCoordinate = (Convert.ToInt32(xMatch.Value) - 1);

        Match actionMatch = Regex.Match(_fieldInput, @"(RF|R|F)$");
        actionS = actionMatch.Value;

        var enumValue = actionS switch
        {
            "R" => FieldInput.Action.Reveal,
            "F" => FieldInput.Action.Flag,
            "RF" => FieldInput.Action.RemoveFlag,
            _ => FieldInput.Action.Reveal,
        };

        FieldInput fieldInput = new FieldInput(yCoordinate, xCoordinate, enumValue);
        return fieldInput;
    }

    public bool CheckFieldInput(string input)
    {
        var x = (char)('A' + GameboardSize);
        Regex rg = new Regex(@"([A-Z](2[0-6]|1[0-9]|[1-9])(RF|R|F))|((2[0-6]|1[0-9]|[1-9])[A-Z](RF|R|F))");

        if (input.Length < 3 || input.Length > 5 | rg.IsMatch(input) != true)
        {
            return false;
        }
        return true;
    }

    public void PrintGame(List<List<string>> gameboard, int gameboardSize, int bombs, int flags)
    {
        PrintGameInformations(bombs, flags);
        PrintGameboard(gameboard, gameboardSize);
    }

    private void PrintGameInformations(int bombs, int flags)
    {
        //Console.Clear();
        Write($"Bombs: {bombs} | Your set flags: {flags} | Seconds: ");
        var hpos = Console.CursorLeft;
        var vpos = Console.CursorTop;
        if (!_isStarted)
        {
            _isStarted = true;
            UI.Instance.PrintTimer(hpos, vpos);
        }


        //source.Cancel();
    }

    public void PrintTimer(int hPos, int vPos)
    {
        source = new CancellationTokenSource();
        new Thread(() =>
        {
            while (!source.Token.IsCancellationRequested)
            {
                lock (_padLock)
                {
                    var currentLPos = Console.CursorLeft;
                    var currentVPos = Console.CursorTop;
                    Console.CursorLeft = hPos;
                    Console.CursorTop = vPos;
                    WriteLine(stopwatch.Elapsed.TotalSeconds.ToString("F2"));
                    Console.CursorLeft = currentLPos;
                    Console.CursorTop = currentVPos;
                }
                Thread.Sleep(100);
            }
        }).Start();
    }

    private readonly static object _padLock = new object();

    public void WriteLine(string text)
    {
        lock (_padLock)
        {
            Console.WriteLine(text);
        }
    }


    public void Write(string text)
    {
        lock (_padLock)
        {
            Console.Write(text);
        }
    }

    private void PrintGameboard(List<List<string>> gameboard, int gameboardSize)
    {
        Console.ForegroundColor = ConsoleColor.White;
        WriteLine("");
        Write("   ");

        for (int i = 0; i < gameboardSize; i++)
        {

            if (i % 2 == 1)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (i % 2 == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
            currentLineColor = Console.ForegroundColor;

            Write(Convert.ToChar(i + 65) + " ");
        }

        WriteLine("");
        int t = 0;
        int z = 0;
        foreach (var list in gameboard)
        {
            t++;

            Console.ForegroundColor = ConsoleColor.White;
            Write(String.Format("{0:00}", t) + " ");

            z = 0;
            foreach (var element in list)
            {
                Console.ForegroundColor = currentLineColor;
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
                        Write(element + " ");
                        Console.ForegroundColor = currentLineColor;
                        break;

                    case "M":
                        Console.ForegroundColor = ConsoleColor.Red;
                        Write(element + " ");
                        Console.ForegroundColor = currentLineColor;
                        break;

                    case "X":
                        Write("■" + " ");
                        break;

                    case "0":
                        Console.ForegroundColor = ConsoleColor.Green;
                        Write(element + " ");
                        Console.ForegroundColor = currentLineColor;
                        break;

                    case "1":
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Write(element + " ");
                        Console.ForegroundColor = currentLineColor;
                        break;

                    case "2":
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Write(element + " ");
                        Console.ForegroundColor = currentLineColor;
                        break;

                    case "3":
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Write(element + " ");
                        Console.ForegroundColor = currentLineColor;
                        break;

                    case "4":
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Write(element + " ");
                        Console.ForegroundColor = currentLineColor;
                        break;

                    case "5":
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Write(element + " ");
                        Console.ForegroundColor = currentLineColor;
                        break;

                    case "6":
                        Console.ForegroundColor = ConsoleColor.Red;
                        Write(element + " ");
                        Console.ForegroundColor = currentLineColor;
                        break;

                    case "7":
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Write(element + " ");
                        Console.ForegroundColor = currentLineColor;
                        break;

                    case "8":
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Write(element + " ");
                        Console.ForegroundColor = currentLineColor;
                        break;

                    default:
                        Write(element + " ");
                        break;
                }
            }
            Console.WriteLine();
        }
        Console.ResetColor();
    }

    public void GameWon(Score score)
    {
        stopwatch.Stop();
        PlaySound("winsound.wav");
        Thread.Sleep(300);
        source.Cancel();
        WriteLine("");
        Console.ForegroundColor = ConsoleColor.Green;
        WriteLine("██╗   ██╗ ██████╗ ██╗   ██╗    ██╗    ██╗ ██████╗ ███╗   ██╗██╗");
        WriteLine("╚██╗ ██╔╝██╔═══██╗██║   ██║    ██║    ██║██╔═══██╗████╗  ██║██║");
        WriteLine(" ╚████╔╝ ██║   ██║██║   ██║    ██║ █╗ ██║██║   ██║██╔██╗ ██║██║");
        WriteLine("  ╚██╔╝  ██║   ██║██║   ██║    ██║███╗██║██║   ██║██║╚██╗██║╚═╝");
        WriteLine("   ██║   ╚██████╔╝╚██████╔╝    ╚███╔███╔╝╚██████╔╝██║ ╚████║██╗");
        WriteLine("   ╚═╝    ╚═════╝  ╚═════╝      ╚══╝╚══╝  ╚═════╝ ╚═╝  ╚═══╝╚═╝");
        WriteLine("");
        WriteLine(Convert.ToString(score.Points));
        WriteLine("");
        Console.ResetColor();
        WriteLine("Press Enter to play again!");
    }

    public void GameLost()
    {
        stopwatch.Stop();
        PlaySound("bombsound.wav");
        Thread.Sleep(300);
        source.Cancel();
        WriteLine("");
        Console.ForegroundColor = ConsoleColor.DarkRed;
        WriteLine(" ██████╗  █████╗ ███╗   ███╗███████╗     ██████╗ ██╗   ██╗███████╗██████╗ ██╗");
        WriteLine("██╔════╝ ██╔══██╗████╗ ████║██╔════╝    ██╔═══██╗██║   ██║██╔════╝██╔══██╗██║");
        WriteLine("██║  ███╗███████║██╔████╔██║█████╗      ██║   ██║██║   ██║█████╗  ██████╔╝██║");
        WriteLine("██║   ██║██╔══██║██║╚██╔╝██║██╔══╝      ██║   ██║╚██╗ ██╔╝██╔══╝  ██╔══██╗╚═╝");
        WriteLine("╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗    ╚██████╔╝ ╚████╔╝ ███████╗██║  ██║██╗");
        WriteLine(" ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝     ╚═════╝   ╚═══╝  ╚══════╝╚═╝  ╚═╝╚═╝");
        Console.ResetColor();
        WriteLine("");
        WriteLine("Press Enter to play again!");
    }

    public static void PlaySound(string fileName)
    {
        var assembly = typeof(UI).Assembly;
        
        var resStream = assembly.GetManifestResourceStream($"Minesweeper.Resources.{fileName}");

        if(resStream is null)
        {
            return;
        }
        var pathOfAssembly = typeof(Program).Assembly.Location;
        SoundPlayer musicPlayer = new SoundPlayer(resStream);
        musicPlayer.Play();
    }

    public double GetTime()
    {
        return Convert.ToDouble(stopwatch.Elapsed.TotalSeconds);
    }
}

