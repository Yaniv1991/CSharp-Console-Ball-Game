using System;
using System.Collections.Generic;
using System.Linq;

namespace Ball_Game_Foreach
{
    class Program
    {
        static List<char> targetList = new List<char>();
        static List<char> answerList = new List<char>();

        static string NumberGenerator(byte difficulty)
        {
            var rnd = new Random();
            char[] buffer = new char[difficulty];
            for (int i = 0; i < difficulty; i++)
            {
                buffer[i] = (char)('0' + rnd.Next(0, 10));
            }
            string result = new string(buffer);
            return result;
        }
        static string AcceptAnswer(string s1, byte difficulty)
        {
            while (true)
            {
                while (s1.Length != difficulty)
                {
                    Console.WriteLine($"Please enter a number with {difficulty} digits");
                    s1 = Console.ReadLine();
                }
                if (int.TryParse(s1, out int temp))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter a valid number");
                    s1 = Console.ReadLine();
                }
            }
            return s1;
        }
        static byte AcceptDifficulty()
        {
            byte number = 0;
            while (true)
            {
                if (byte.TryParse(Console.ReadLine(), out number))
                {
                    if(number < 0 || number > 10)
                    {
                        Console.WriteLine("Please enter a number between 1 and 10");
                        continue;
                    }
                    break;
                }
                Console.WriteLine("The number is invalid, Please enter an integer");
            }
            return number;
        }

        static void Main(string[] args)
        {
            int tries = 0;
            Console.WriteLine("Hi! Choose your difficulty level from 1 to 10:");
            byte difficulty = AcceptDifficulty();
            var target = NumberGenerator(difficulty);
            DateTime start = new DateTime(1);
            targetList = new List<char>(target);
            answerList = new List<char>();
            while (true)
            {
                Console.WriteLine("Guess the number");
                string answer = AcceptAnswer(Console.ReadLine(), difficulty);
                tries++;
                if (start.Ticks == 1)
                {
                    start = DateTime.Now;
                }
                if (answer.Equals(target))
                {
                    TimeSpan total = DateTime.Now - start;
                    Console.WriteLine("Done!! You tried {0} times and it took you {1} seconds to complete!", tries, total.TotalSeconds);
                    break;
                } // "Done" method

                foreach (char number in answer)
                {
                    answerList.Add(number);
                }
                if (!CheckForACorrectNumber(answer, target))
                {
                    Console.WriteLine("None of the numbers are correct");
                    answerList.Clear();
                    continue;
                }

                CheckGuessedNumber(answer);
                foreach (char number in target)
                {
                    targetList.Add(number);
                }

            }
            Console.Read();
        }

        private static bool CheckForACorrectNumber(string answer,string target)
        {
            foreach (char number in answer)
            {
                if (target.Contains(number))
                {
                    return true;
                }
            }

                
            return false;
        }

        private static void CheckGuessedNumber(string answer)
        {
            for (int i = answer.Length - 1; i >= 0; i--)
            {
                if (answerList[i] == targetList[i])
                {
                    Console.WriteLine($"The number {answerList[i]} is a hit and is in the right place");
                    answerList.Remove(answerList[i]);
                    targetList.Remove(targetList[i]);
                }
            }
            while (answerList.Count > 0)
            {
                for (int i = answerList.Count - 1; i >= 0; i--)
                {
                    if (targetList.Contains(answerList[i]))
                    {
                        Console.WriteLine($"The number {answerList[i]} is a hit and but not in the right place");
                        targetList.Remove(answerList[i]);
                        answerList.Remove(answerList[i]);
                    }
                }
                answerList.Clear();
                targetList.Clear();
                
            }
        }
    }
 }

