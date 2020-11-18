﻿using System;
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

    class Program
    {
        public static Dictionary<string, Delegate> functions = new Dictionary<string, Delegate>();

        static bool isRunning = true; // TEST232

        static void Main(string[] args)
        {
            Init();

            ShowFunctions();

            while (isRunning)
            {
                Console.WriteLine();
                Console.Write("Enter function name:");

                string input = Console.ReadLine().Trim().ToLower();

                if (functions.ContainsKey(input))
                {
                    functions[input].DynamicInvoke();
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
            foreach (KeyValuePair<string, Delegate> del in functions)
            {
                Console.WriteLine(del.Key);
            }
        }

        static void ExitProgram()
        {
            isRunning = false;
        }

        static void Init()
        {
            functions.Add("exit", new Action(() => ExitProgram()));
            functions.Add("x", new Action(() => ExitProgram()));
            functions.Add("help", new Action(() => ShowFunctions()));
            functions.Add("?", new Action(() => ShowFunctions()));
            functions.Add("printnumbers", new Action(() => FuncPrintUpTo()));
            functions.Add("sum", new Action(() => FuncSumUpTo()));
            functions.Add("fibonacci", new Action(() => FuncFibonacci()));
            functions.Add("factorial", new Action(() => FuncFactorial()));
            functions.Add("isprime", new Action(() => FuncIsPrime()));
            functions.Add("printprimes", new Action(() => FuncPrimesUpTo()));
            functions.Add("listdir", new Action(() => FuncListFilesInDir()));
            functions.Add("lykkespil", new Action(() => LykkeSpil.Start()));
            functions.Add("studentgrading", new Action(() => FuncStudentGrading()));
            functions.Add("startreknames", new Action(() => StartTrekProgram.Run()));
        }

        static void FuncPrintUpTo()
        {
            Console.WriteLine("All natural numbers up to n");
            Console.WriteLine("---");
            Console.WriteLine("Example: n as 5");
            Console.WriteLine("Input: 5");
            Console.WriteLine("Output: 1, 2, 3, 4, 5");
            Console.WriteLine("---");
            Console.Write("Enter any natural number above zero:");

            string input = Console.ReadLine();

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
            Console.WriteLine("Sum of natural numbers up to n");
            Console.Write("Enter any natural number above zero:");

            string input = Console.ReadLine();

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
            Console.WriteLine("Fibonacci sequence");
            Console.Write("Enter any natural number above zero:");

            string input = Console.ReadLine();


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
            Console.WriteLine("n factorial");
            Console.Write("Enter any natural non-negative number:");


            string input = Console.ReadLine();

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
            Console.WriteLine("Is n prime");
            Console.Write("Enter any natural number above zero:");

            string input = Console.ReadLine();

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
            Console.WriteLine("Primes up to n");
            Console.Write("Enter any natural number above zero:");

            string input = Console.ReadLine();

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
            Console.WriteLine("Directory listing");
            Console.Write("Enter a directory path:");

            string input = Console.ReadLine();

            if (Directory.Exists(input.Trim()))
            {
                ListDirContents(input.Trim(), 1);
            }

            Console.WriteLine();
            Console.WriteLine("Done.");
        }

        static void ListDirContents(string path, int indent)
        {
            int indentWidth = 2;

            if (Directory.Exists(path))
            {
                try
                {
                    string[] files = Directory.GetFiles(path);
                    string[] folders = Directory.GetDirectories(path);


                    foreach (string file in files)
                    {
                        Console.WriteLine("".PadLeft(indentWidth * indent) + file.Substring(file.LastIndexOf('\\') + 1));
                    }

                    foreach (string folder in folders)
                    {
                        Console.WriteLine("".PadLeft(indentWidth * indent) + folder.Substring(folder.LastIndexOf('\\') + 1));
                        ListDirContents(folder, indent + 1);
                    }
                }
                catch
                {
                    Console.WriteLine($"Error unable to list: {path}");
                }
            }
        }

        static void FuncStudentGrading()
        {
            Console.WriteLine("Student grading");
            Console.Write("How many students:");

            string input = Console.ReadLine();

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
                    if (number % pr == 0)
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

