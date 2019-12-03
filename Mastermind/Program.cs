using System;
using System.Collections.Generic;

namespace Mastermind
{
    class Program
    {
        static void Main(string[] args)
        {
            MasterMindGame();
            Console.ReadKey();
        }

        private static void MasterMindGame()
        {
            Console.WriteLine("Hello, Welcome to the Master Piece Game");
            Console.WriteLine("The answer is a 4 digit number with each digit between the numbers 1 and 6");
            Console.WriteLine("A minus (-) sign will be printed for every digit that is correct but in the wrong position");
            Console.WriteLine("A plus (+) sign will be printed for every digit that is both correct and in the correct position");
            Console.WriteLine("Nothing will be printed for incorrect digits");
            Console.WriteLine("The player has ten (10) attempts to guess the number correctly");
            var answerDict = new Dictionary<char, int>();
            var isWin = false;

            //generate a 4 digit random answer
            var answer = GenerateRandomAnswer();

            //convert the number into dict
            foreach (var i in answer)
            {
                if (answerDict.ContainsKey(i))
                {
                    answerDict[i] += 1;
                }
                else
                {
                    answerDict[i] = 1;
                }
            }

            //validate the user's input
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine($"Please enter your combination. You left {10 - i} times to guess");
                var userInput = Console.ReadLine();
                if (userInput == null || userInput.Length != 4) continue; //the input's length must be 4
                if (userInput == answer)// if the input matches the answer, jump out of the loop and printer final result
                {
                    isWin = true;
                    break;
                }
                else
                {
                    if (IsAllDigitValid(userInput, new Dictionary<char, int>(answerDict)))
                    {
                        Console.WriteLine("(-)");
                    }
                }
            }
            //print final result
            Console.WriteLine(isWin ? "(+)" : "You are lost!");
        }

        private static bool IsAllDigitValid(string target, Dictionary<char, int> answerDict)
        {
            foreach (var i in target)
            {
                if (!answerDict.ContainsKey(i))
                {
                    return false;
                }
                answerDict[i] -= 1;
                if (answerDict[i] < 0) return false;
            }
            return true;
        }

        private static string GenerateRandomAnswer()
        {
            var num = 0;
            var random = new Random();
            for (var i = 0; i < 4; i++)
            {
                num = num * 10 + random.Next(1, 7);
            }
            return num.ToString();
        }
    }
}
