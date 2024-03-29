﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loops
{
    /* Denne klasse implementerer LykkeSpil som er et terningspil */
    class LykkeSpil
    {
        bool isRunning;
        public static Random rand;
        List<Player> players;

        public const int winningScore = 100;

        private int _currentPlayerIndex;
        public int CurrentPlayerIndex
        {
            get { return _currentPlayerIndex; }
            set
            {
                if (value >= players.Count)
                {
                    _currentPlayerIndex = 0;
                }
                else if (value < 0)
                {
                    _currentPlayerIndex = 0;
                }
                else
                {
                    _currentPlayerIndex = value;
                }
            }
        }

        /* Method to get current player - test A og B*/
        public Player CurrentPlayer
        {
            get
            {
                return players[CurrentPlayerIndex];
            }
        }

        /* static factory method */
        public static void Start()
        {
            LykkeSpil game = new LykkeSpil();
            
            game.Init();
            game.Run();

            Console.CursorVisible = true;
        }

        /* This initialization method is called from the Start factory method */
        void Init()
        {
            isRunning = true;
            rand = new Random();
            players = new List<Player>();
            CurrentPlayerIndex = 0;

            Console.WriteLine("Enter at least two players then an empty name to start.");

            bool isGettingUsers = true;
            while (isGettingUsers)
            {
                Console.WriteLine("Please enter a name:");
                string input = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(input))
                {
                    if (players.Count >= 2)
                    {
                        isGettingUsers = false;
                    }
                    else
                    {
                        Console.WriteLine("You need at least two players to start.");
                    }
                }
                else
                {
                    if (players.Where(p => p.Name == input)?.Count() > 0)
                    {
                        Console.WriteLine("Players cannot have the same name.");
                    }
                    else
                    {
                        players.Add(new Player(input));
                    }
                }
            }

            Console.CursorVisible = false;
        }

        void Quit()
        {
            isRunning = false;
        }

        /* Run method - this method makes things happen */
        void Run()
        {
            CurrentPlayer.StartTurn();

            Draw();

            while (isRunning)
            {
                ConsoleKeyInfo input = Console.ReadKey();
                
                if (CurrentPlayer.isDone)
                {
                    if (CurrentPlayer.Score >= winningScore)
                    {
                        isRunning = false;
                    }
                    else
                    {
                        CurrentPlayerIndex++;
                        CurrentPlayer.StartTurn();
                    }
                }
                else
                {
                    CurrentPlayer.TakeTurn(input);
                }

                Draw();
            }
        }

        void Draw()
        {
            Console.Clear();

            Console.CursorLeft = 0;
            Console.CursorTop = 0;

            Console.WriteLine("Lykkespil");
            Console.WriteLine("Players:");
            
            List<Player> scoreTable = GetSortedList(players);

            foreach (Player player in scoreTable)
            {
                Console.WriteLine("{0}: {1} points", player.Name, player.Score);
            }
            Console.WriteLine();

            if (isRunning)
            {
                CurrentPlayer.Draw();
            }
            else
            {
                Console.WriteLine("{0} has won the game!", scoreTable.First().Name);
            }
        }

        List<Player> GetSortedList(List<Player> list)
        {
            List<Player> scoreTable = new List<Player>();

            foreach (Player player in list)
            {
                bool sorted = false;
                for (int i = 0; i < scoreTable.Count; i++)
                {
                    if (player.Score > scoreTable[i].Score)
                    {
                        scoreTable.Insert(i, player);
                        sorted = true;
                        break;
                    }
                }

                if (!sorted)
                {
                    scoreTable.Add(player);
                }
            }

            return scoreTable;
        }

        public static int RollDice()
        {
            return rand.Next(1, 7);
        }

        public class Player
        {
            public string Name { get; set; }
            public int Score { get; set; }

            public bool isDone;

            List<int> rolls;

            public Player(string name)
            {
                Name = name;
                Score = 0;
                rolls = new List<int>();
                isDone = true;
            }

            public void StartTurn()
            {
                isDone = false;
                rolls.Clear();
            }

            public void TakeTurn(ConsoleKeyInfo input)
            {
                switch (input.Key)
                {
                    case ConsoleKey.R:
                        int roll = RollDice();
                        rolls.Add(roll);
                        if (roll == 1)
                        {
                            isDone = true;
                        }
                        break;
                    case ConsoleKey.S:
                        Score += rolls.Sum();
                        isDone = true;
                        break;
                    default:
                        break;
                }
            }

            public void Draw()
            {
                Console.WriteLine("{0} turn", Name + (Name.ToLower().EndsWith("s") ? "'" : "'s"));
                
                string line = "";

                for (int i = 0; i < rolls.Count; i++)
                {
                    if (i != 0)
                    {
                        line += ",";
                    }
                    
                    line += rolls[i].ToString();
                }
                Console.WriteLine("Rolls: {0}", line);
                Console.WriteLine("Total: {0}", rolls.Sum());

                if (!isDone)
                {
                    Console.WriteLine("Actions");
                    Console.WriteLine("  'R'oll");
                    Console.WriteLine("  'S'top");
                }
                else
                {
                    Console.WriteLine("{0} turn is over.", Name + (Name.EndsWith("s") ? "'" : "s"));
                    Console.WriteLine("Press any key to continue.");
                }
            }
        }
    }
}
