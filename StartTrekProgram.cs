using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Loops
{
    class StartTrekProgram
    {
        static string[] vowels = new string[]
            {
                "a",
                "e",
                "i",
                "o",
                "u",
                "y"
            };

        static string[] consonants = new string[]
            {
                "r",
                "t",
                "p",
                "d",
                "f",
                "j",
                "k",
                "l",
                "v",
                "b",
                "n",
                "m"
            };

        static string[,][] nameStartAndEnd = new string[,][]
        {
            {   // Male
                new string[] { "S","Sp","Sk","St","T" }, // Starts
                new string[] { "q","p","k","ck","l" } // Endings
            },
            {   // Female
                new string[] { "T'P","T'K","T'Q" }, // Starts
                new string[] { "r","j","'p","k","l" } // Endings
            }
        };
        
        public static string maleNameFilter = "((" + string.Join("|", nameStartAndEnd[0, 0]) + ")(" + string.Join("|", vowels) + ")((" + string.Join("|", consonants) + ")(" + string.Join("|", vowels) + "))?(" + string.Join("|", nameStartAndEnd[0, 1]) + "))";
        public static string femaleNameFilter = "((" + string.Join("|", nameStartAndEnd[1, 0]) + ")(" + string.Join("|", vowels) + ")(" + string.Join("|", nameStartAndEnd[1, 1]) + "))";

        public static void Run()
        {
            Console.WriteLine("Star Trek Names");

            Console.WriteLine("Commands:");
            Console.WriteLine("'gen [number] [seed]' : Generates N number of valid names. Seed is optional. - Example 'gen 5 HelloWorld'");
            Console.WriteLine("'check [name] [name]' : Checks validity of a name. Can take multiple names.  - Example 'check Spock'");

            Console.Write(">");

            string input = Console.ReadLine();
            input = input.Trim();

            if (!string.IsNullOrEmpty(input))
            {
                if (input.StartsWith("gen ") || input.StartsWith("check "))
                {
                    string[] args = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    if (args.Length >= 2)
                    {
                        switch (args[0])
                        {
                            case "gen":
                                int number = 0;
                                if (int.TryParse(args[1], out number))
                                {
                                    if (number > 0)
                                    {
                                        if (args.Length > 2)
                                        {
                                            GenerateNames(number, args[2]);
                                        }
                                        else
                                        {
                                            GenerateNames(number);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Must generate at least 1 name.");
                                    }
                                }
                                break;
                            case "check":
                                Console.WriteLine();
                                for (int i = 1; i < args.Length; i++)
                                {
                                    bool isValid = IsNameValid(args[i]);
                                    string gender = isValid ? (args[i].StartsWith("T'") ? "female " : "male ") : "";
                                    Console.WriteLine("{0} is {1}a valid {2}name.", args[i], isValid ? "" : "not ", gender);
                                }
                                break;
                            default:
                                Console.WriteLine("Invalid command.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid argument count.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid command.");
                }
            }
            else
            {
                Console.WriteLine("No command.");
            }

            Console.WriteLine();
            Console.WriteLine("Done.");
        }

        static void GenerateNames(int count, string seed = "")
        {
            Random rand;
            if (string.IsNullOrEmpty(seed))
            {
                rand = new Random();
            }
            else //hbvfdhfkj
            {
                rand = new Random(seed.GetHashCode());
            }

            List<string> names = new List<string>();

            Console.WriteLine();

            while (names.Count() < count)
            {
                string name = "";

                int gender = rand.Next(0, 100) % 2 == 0 ? 0 : 1;

                name += nameStartAndEnd[gender, 0][rand.Next(0, nameStartAndEnd[gender, 0].Length)];
                name += vowels[rand.Next(0, vowels.Length)];

                if (gender == 0 && rand.Next(0, 100) % 2 == 0)
                {
                    name += consonants[rand.Next(0, consonants.Length)];
                    name += vowels[rand.Next(0, vowels.Length)];
                }
                
                name += nameStartAndEnd[gender, 1][rand.Next(0, nameStartAndEnd[gender, 1].Length)];

                if (!names.Contains(name))
                {
                    names.Add(name);
                }
            }

            foreach (string name in names)
            {
                Console.WriteLine("{0, -8} {1}", name, name.StartsWith("T'") ? "female" : "male");
            }
        }

        static bool IsNameValid(string name)
        {
            Regex regex;
            if (name.StartsWith("T'"))
            {
                regex = new Regex(femaleNameFilter);
                return regex.IsMatch(name);
            }
            else
            {
                regex = new Regex(maleNameFilter);
                return regex.IsMatch(name);
            }
        }
    }
}
