using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Loops
{
    /* This is the main program */

    //This is the Correction Branch

    struct DescAndFunction
    {
        public string desc;
        public Delegate func;
    }

    class Program
    {
        public static Dictionary<string, DescAndFunction> functions = new Dictionary<string, DescAndFunction>();

        static bool isRunning = true; // TEST232
        static string modifier = "";

        static void Main(string[] args)
        {
            Init();

            ShowFunctions();

            while (isRunning)
            {
                modifier = "";
                Console.WriteLine();
                Console.Write("Enter function name:");


                string[] input = Console.ReadLine().Trim().ToLower().Split(' ');
                string cmd = input[0];
                if (input.Length > 1)
                {
                    List<string> mods = input.ToList();
                    mods.RemoveAt(0);
                    modifier = string.Join(" ",mods);
                }

                if (functions.ContainsKey(cmd))
                {
                    functions[cmd].func.DynamicInvoke();
                }
                else
                {
                    Console.WriteLine("Input not recognized.");
                }
            }

            Console.WriteLine("Closing.");

            Thread.Sleep(500);
        }

        static void ShowFunctions()
        {
            Console.WriteLine("Available functions:");
            foreach (KeyValuePair<string, DescAndFunction> del in functions)
            {
                Console.WriteLine($"{del.Key} -- {del.Value.desc}");
            }
        }

        static void ExitProgram()
        {
            isRunning = false;
        }

        static void Init()
        {
            functions.Add("exit", new DescAndFunction { desc = "exits the program", func = new Action(() => ExitProgram()) });
            functions.Add("x", new DescAndFunction { desc = "exits the program", func = new Action(() => ExitProgram()) });
            functions.Add("help", new DescAndFunction { desc = "shows function list", func = new Action(() => ShowFunctions()) });
            functions.Add("?", new DescAndFunction { desc = "shows function list", func = new Action(() => ShowFunctions()) });
            functions.Add("printnumbers", new DescAndFunction { desc = "print numbers up to a given number", func = new Action(() => FuncPrintUpTo()) });
            functions.Add("sum", new DescAndFunction { desc = "sums all numbers leading up to a given number", func = new Action(() => FuncSumUpTo()) });
            functions.Add("fibonacci", new DescAndFunction { desc = "prints the fibonacci sequence for a given length", func = new Action(() => FuncFibonacci()) });
            functions.Add("factorial", new DescAndFunction { desc = "calculates the factorial of a given number", func = new Action(() => FuncFactorial()) });
            functions.Add("isprime", new DescAndFunction { desc = "checks if a given number is a prime number", func = new Action(() => FuncIsPrime()) });
            functions.Add("printprimes", new DescAndFunction { desc = "prints n prime numbers", func = new Action(() => FuncPrimesUpTo()) });
            functions.Add("listdir", new DescAndFunction { desc = "lists directive", func = new Action(() => FuncListFilesInDir()) });
            functions.Add("lykkespil", new DescAndFunction { desc = "runs lykkespil", func = new Action(() => LykkeSpil.Start()) });
            functions.Add("studentgrading", new DescAndFunction { desc = "grades students", func = new Action(() => FuncStudentGrading()) });
            functions.Add("startreknames", new DescAndFunction { desc = "generate StarTrek names and check if specific names are valid", func = new Action(() => StartTrekProgram.Run()) });
        }

        static void FuncPrintUpTo()
        {
            string input = modifier;
            if (modifier == "")
            {
                Console.WriteLine("All natural numbers up to n");
                Console.WriteLine("---");
                Console.WriteLine("Example: n as 5");
                Console.WriteLine("Input: 5");
                Console.WriteLine("Output: 1, 2, 3, 4, 5");
                Console.WriteLine("---");
                Console.Write("Enter any natural number above zero:");

                input = Console.ReadLine();
            }

            int number = 0;

            if (!string.IsNullOrEmpty(input) && int.TryParse(input.Trim(), out number))
            {
                if (number > 0)
                {
                    Console.WriteLine("Printing numbers from 0 to {0}", number);
                    for (int i = 0; i < number; i++)
                    {
                        Console.WriteLine((i + 1).ToString());
                    }
                }
                else
                {
                    Console.WriteLine("Number is less than 1.");
                }
            }
            else
            {
                Console.WriteLine("Number not recognized.");
            }

            Console.WriteLine("Done.");
        }

        static void FuncSumUpTo()
        {
            string input = modifier;
            if (modifier == "")
            {
                Console.WriteLine("Sum of natural numbers up to n");
                Console.Write("Enter any natural number above zero:");
                input = Console.ReadLine();

            }


            int number = 0;

            if (!string.IsNullOrEmpty(input) && int.TryParse(input.Trim(), out number))
            {
                if (number > 0)
                {
                    int result = 0;
                    if (IsEven(number))
                    {
                        result = number * (number / 2) + (number / 2);
                    }
                    else
                    {
                        result = ((number / 2) * number) + number;
                    }
                    Console.WriteLine("Sum of numbers up to {0}: {1}", number, result);
                }
                else
                {
                    Console.WriteLine("Number is less than 1.");
                }
            }
            else
            {
                Console.WriteLine("Number not recognized.");
            }

            Console.WriteLine("Done.");
        }

        static void FuncFibonacci()
        {
            string input = modifier;
            if (modifier == "")
            {
                Console.WriteLine("Fibonacci sequence");
                Console.Write("Enter any natural number above zero:");

                input = Console.ReadLine();

            }



            if (!string.IsNullOrEmpty(input) && int.TryParse(input.Trim(), out int number))
            {
                if (number > 0)
                {
                    int currNumber = 0;
                    int lastNumber = 1;
                    for (int i = 0; i < number; i++)
                    {
                        int temp = currNumber;
                        currNumber += lastNumber;
                        lastNumber = temp;

                        Console.WriteLine(currNumber);
                    }
                }
                else
                {
                    Console.WriteLine("Number is less than 1.");
                }
            }
            else
            {
                Console.WriteLine("Number not recognized.");
            }

            Console.WriteLine("Done.");
        }

        static void FuncFactorial()
        {
            string input = modifier;
            if (modifier == "")
            {
                Console.WriteLine("n factorial");
                Console.Write("Enter any natural non-negative number:");

                input = Console.ReadLine();
            }





            int number = 0;

            if (!string.IsNullOrEmpty(input) && int.TryParse(input.Trim(), out number))
            {
                string result = GetFactorial(number);
                Console.WriteLine("{0} factorial is {1}", number, result);
                Console.WriteLine($"Length = {result.Length}");
            }
            else
            {
                Console.WriteLine("Number not recognized.");
            }

            Console.WriteLine("Done.");
        }
        static string GetFactorial(int number)
        {


            string result = "";
            List<string> resultList = new List<string>();
            resultList.Add("1");

            for (int i = 1; i <= number; i++)
            {

                List<string> temp = new List<string>();
                List<string> multiplied = new List<string>();

                foreach (string s in resultList)
                {
                    string mult = MultiplyString(s, i.ToString());
                    multiplied.Add(mult);
                    temp.Add(mult);
                }
                bool Stopper = true;


                while (Stopper)
                {
                    int addToIndex = 0;
                    Stopper = false;
                    List<string> tempCopy = new List<string>();
                    foreach (string s in temp)
                    {
                        tempCopy.Add(s);
                    }
                    for (int i1 = 0; i1 < tempCopy.Count; i1++)
                    {
                        if (tempCopy[i1].Length > 3)
                        {
                            Stopper = true;
                            string[] split = SplitInto3s(tempCopy[i1]);
                            int startIndex = i1 + addToIndex - (split.Length - 1);
                            while (startIndex < 0)
                            {
                                temp.Insert(0, "");
                                startIndex++;
                                addToIndex++;
                            }
                            for (int i3 = 0; i3 < split.Count(); i3++)
                            {
                                int tI = i1 + addToIndex - split.Count() + i3 + 1;
                                if (i3 == split.Count() - 1)
                                {
                                    temp[tI] = split[i3];
                                }
                                else
                                {
                                    temp[tI] = AddNumberToString(temp[tI], split[i3]);
                                }
                            }

                        }
                    }

                }
                resultList = temp;
            }
            result = string.Join("", resultList);
            while (result[0] == '0')
            {
                result = result.Substring(1);
            }

            return result;
        }
        static string AddNumberToString(string s1, string s2)
        {
            try
            {
                if (s1 == "")
                {
                    s1 = "0";
                }
                if (s2 == "")
                {
                    s2 = "0";
                }
                string returnValue = (Convert.ToInt32(s1) + Convert.ToInt32(s2)).ToString();
                while (returnValue.Length < 3)
                {
                    returnValue = "0" + returnValue;
                }
                return returnValue;
            }
            catch
            {
                return "";
            }
        }
        static string MultiplyString(string s1, string s2)
        {
            try
            {
                string s = (Convert.ToInt32(s1) * Convert.ToInt32(s2)).ToString();
                while (s.Length < 3)
                {
                    s = "0" + s;
                }
                return s;
            }
            catch
            {
                return "";
            }
        }
        static string[] SplitInto3s(string input)
        {
            int arLength = (int)Math.Floor((double)input.Length / 3 - .1);
            arLength += 1;
            string[] returnValue = new string[arLength];
            int lI = returnValue.Length - 1;
            for (int i = input.Length - 1; i >= 0; i--)
            {
                if (returnValue[lI] == null)
                {
                    returnValue[lI] = "";
                }
                if (returnValue[lI].Length == 3)
                {
                    lI--;
                }
                returnValue[lI] = input[i] + returnValue[lI];
            }
            return returnValue;
        }


        static void FuncIsPrime()
        {
            string input = modifier;
            if (modifier == "")
            {
                Console.WriteLine("Is n prime");
                Console.Write("Enter any natural number above zero:");

                input = Console.ReadLine();
            }




            int number = 0;

            if (!string.IsNullOrEmpty(input) && int.TryParse(input.Trim(), out number))
            {
                if (number > 0)
                {
                    if (IsPrime(number))
                    {
                        Console.WriteLine("{0} is a prime number.", number);
                    }
                    else
                    {
                        Console.WriteLine("{0} is not a prime number.", number);
                    }
                }
                else
                {
                    Console.WriteLine("Number is less than 1.");
                }
            }
            else
            {
                Console.WriteLine("Number not recognized.");
            }

            Console.WriteLine("Done.");
        }

        static void FuncPrimesUpTo()
        {
            string input = modifier;
            if (modifier == "")
            {
                Console.WriteLine("Primes up to n");
                Console.Write("Enter any natural number above zero:");

                input = Console.ReadLine();
            }




            int number = 0;

            if (!string.IsNullOrEmpty(input) && int.TryParse(input.Trim(), out number))
            {
                if (number > 0)
                {
                    int primesFound = 0;

                    int currNum = 1;
                    while (primesFound < number)
                    {
                        if (IsPrime(currNum))
                        {
                            primesFound++;
                            Console.WriteLine(currNum);
                        }
                        currNum++;
                    }
                }
                else
                {
                    Console.WriteLine("Number is less than 1.");
                }
            }
            else
            {
                Console.WriteLine("Number not recognized.");
            }

            Console.WriteLine("Done.");
        }

        static void FuncListFilesInDir()
        {
            string input = modifier.Split(' ').First().Trim();
            string filter = modifier.Replace("full","").Replace(input, "").Trim();
            bool fullPath = false;
            if (modifier == "")
            {
                Console.WriteLine("Directory listing");
                Console.WriteLine("type 'full' at the end of the command to get the full path");
                Console.Write("Enter a directory path:");

                input = Console.ReadLine();
            }
            

            if (modifier.Contains("full"))
            {
                fullPath = true;
                
            }
            if (Directory.Exists(input.Trim()))
            {
                ListDirContents(input.Trim(), 1, fullPath, filter);
            }

            Console.WriteLine();
            Console.WriteLine("Done.");
        }

        static void ListDirContents(string path, int indent, bool fullPath, string filter)
        {
            int indentWidth = 2;
            string[] filters = filter.Trim().Split(' ');

            if (Directory.Exists(path))
            {
                try
                {
                    if(filter == "")
                    {
                        string[] files = Directory.GetFiles(path);
                        string[] folders = Directory.GetDirectories(path);

                        if(fullPath)
                        {
                            foreach(string file in files)
                            {
                                Console.WriteLine(file);

                            }
                            foreach(string folder in folders)
                            {
                                Console.WriteLine(folder);
                                ListDirContents(folder, indent + 1, fullPath, filter);
                            }
                        }
                        else
                        {
                            foreach(string file in files)
                            {
                                Console.WriteLine("".PadLeft(indentWidth * indent) + file.Substring(file.LastIndexOf('\\') + 1));

                            }
                            foreach(string folder in folders)
                            {
                                Console.WriteLine("".PadLeft(indentWidth * indent) + folder.Substring(folder.LastIndexOf('\\') + 1));
                                ListDirContents(folder, indent + 1, fullPath, filter);
                            }
                        }
                    }
                    else
                    {
                        foreach(string filter_ in filters)
                        {
                            string[] files = Directory.GetFiles(path);
                            string[] folders = Directory.GetDirectories(path);

                            if(fullPath)
                            {
                                foreach(string file in files)
                                {
                                    if(file.Contains(filter_))
                                    {
                                        Console.WriteLine(file);   
                                    }

                                }
                                foreach(string folder in folders)
                                {
                                    if(ContainsFileWithFilter(folder, filter_))
                                    {
                                        Console.WriteLine(folder);
                                    }
                                    

                                    ListDirContents(folder, indent + 1, fullPath, filter_);
                                }
                            }
                            else
                            {
                                foreach(string file in files)
                                {
                                    if(filter.Contains(filter_))
                                    {
                                        
                                        Console.WriteLine("".PadLeft(indentWidth * indent) + file.Substring(file.LastIndexOf('\\') + 1));
                                        
                                    }

                                }
                                foreach(string folder in folders)
                                {
                                    if(ContainsFileWithFilter(folder, filter_))
                                    {
                                        Console.WriteLine("".PadLeft(indentWidth * indent) + folder.Substring(folder.LastIndexOf('\\') + 1));
                                    }
                                    
                                    ListDirContents(folder, indent + 1, fullPath, filter_);
                                }
                            }
                        }
                    }
                    
                    

                }
                catch
                {
                    Console.WriteLine($"Error unable to list: {path}");
                }
            }
        }
        static bool ContainsFileWithFilter(string path, string filter)
        {
            if(Directory.Exists(path))
            {
                try
                {
                    string[] files = Directory.GetFiles(path);
                    foreach(string file in files)
                    {
                        if(file.Contains(filter))
                        {
                            return true;
                        }
                    }
                }
                catch
                {
                    Console.WriteLine($"Error unable to list: {path}");
                }
            }
            return false;
        }

        static void FuncStudentGrading()
        {
            string input = modifier;
            if (modifier == "")
            {
                Console.WriteLine("Student grading");
                Console.Write("How many students:");

                input = Console.ReadLine();
            }




            int numberOfStudents = 0;

            if (!string.IsNullOrEmpty(input) && int.TryParse(input.Trim(), out numberOfStudents))
            {
                if (numberOfStudents >= 0)
                {
                    List<int> grades = new List<int>();

                    while (grades.Count() < numberOfStudents)
                    {
                        Console.Write("Enter grade for student #{0}: ", (grades.Count() + 1));

                        input = Console.ReadLine();

                        int grade = 0;

                        if (!string.IsNullOrEmpty(input) && int.TryParse(input.Trim(), out grade))
                        {
                            switch (grade)
                            {
                                case 0:
                                case 2:
                                case 4:
                                case 7:
                                case 10:
                                case 12:
                                    grades.Add(grade);
                                    break;
                                default:
                                    Console.WriteLine("Not a valid grade.");
                                    break;
                            }
                        }
                    }

                    Console.WriteLine();
                    Console.WriteLine("Average grade is {0}", (grades.Sum() / ((double)grades.Count())).ToString("0.##"));
                }
                else
                {
                    Console.WriteLine("Number is less than zero.");
                }
            }
            else
            {
                Console.WriteLine("Number not recognized.");
            }

            Console.WriteLine("Done.");
        }

        static bool IsPrime(int number)
        {
            if (number == 1)
            {
                return false;
            }
            if (number == 2)
            {
                return true;
            }
            int[] primeNumbers = { 3, 5, 7, 11, 13, 17, 19, 23 };

            if (!IsEven(number))
            {
                foreach (int pr in primeNumbers)
                {
                    if (number % pr == 0 && number != pr)
                    {
                        return false;
                    }
                }
                int maxValue = number / primeNumbers.Last();
                for (int i = 23; i < maxValue; i += 2)
                {
                    if (number % i == 0)
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
            return true;

        }

        static bool IsEven(int number)
        {
            return number % 2 == 0;
        }
    }
}

