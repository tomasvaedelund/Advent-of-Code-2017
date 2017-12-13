using System;
using Advent_of_Code_2017.classes;

namespace AoC2017
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                var timeElapsed = 0L;

                // displayResult("Day1 - First", Day1.getResult(out timeElapsed).ToString(), timeElapsed.ToString());
                // displayResult("Day1 - Second", Day1.getResultTwo(out timeElapsed).ToString(), timeElapsed.ToString());

                // displayResult("Day2 - First", Day2.GetResult(out timeElapsed).ToString(), timeElapsed.ToString());
                // displayResult("Day2 - Second", Day2.GetResultTwo(out timeElapsed).ToString(), timeElapsed.ToString());

                // displayResult("Day3 - First", Day3.GetResult(out timeElapsed).ToString(), timeElapsed.ToString());
                // displayResult("Day3 - Second", Day3.GetResultTwo(out timeElapsed).ToString(), timeElapsed.ToString());

                // displayResult("Day4 - First", Day4.GetResult(out timeElapsed).ToString(), timeElapsed.ToString());
                // displayResult("Day4 - Second", Day4.GetResultTwo(out timeElapsed).ToString(), timeElapsed.ToString());

                // displayResult("Day5 - First", Day5.GetResult(out timeElapsed).ToString(), timeElapsed.ToString());
                // displayResult("Day5 - Second", Day5.GetResultTwo(out timeElapsed).ToString(), timeElapsed.ToString());

                // displayResult("Day6 - First", Day6.GetResult(out timeElapsed).ToString(), timeElapsed.ToString());
                // displayResult("Day6 - Second", Day6.GetResultTwo(out timeElapsed).ToString(), timeElapsed.ToString());

                // displayResult("Day7 - First", Day7.GetResult(out timeElapsed).ToString(), timeElapsed.ToString());
                // displayResult("Day7 - Second", Day7.GetResultTwo(out timeElapsed).ToString(), timeElapsed.ToString());

                // displayResult("Day8 - First", Day8.GetResult(out timeElapsed).ToString(), timeElapsed.ToString());
                // displayResult("Day8 - Second", Day8.GetResultTwo(out timeElapsed).ToString(), timeElapsed.ToString());

                // displayResult("Day9 - First", Day9.GetResult(out timeElapsed).ToString(), timeElapsed.ToString());
                // displayResult("Day9 - Second", Day9.GetResultTwo(out timeElapsed).ToString(), timeElapsed.ToString());

                // displayResult("Day 10 - First", Day10.GetResult(out timeElapsed).ToString(), timeElapsed.ToString());
                // displayResult("Day 10 - Second", Day10.GetResultTwo(out timeElapsed).ToString(), timeElapsed.ToString());
                
                // displayResult("Day 11 - First", Day11.GetResult(out timeElapsed).ToString(), timeElapsed.ToString());
                // displayResult("Day 11 - Second", Day11.GetResultTwo(out timeElapsed).ToString(), timeElapsed.ToString());

                // displayResult("Day 12 - First", Day12.GetResult(out timeElapsed).ToString(), timeElapsed.ToString());
                // displayResult("Day 12 - Second", Day12.GetResultTwo(out timeElapsed).ToString(), timeElapsed.ToString());

                displayResult("Day 13 - First", Day13.GetResult(out timeElapsed).ToString(), timeElapsed.ToString());
                // displayResult("Day 13 - Second", Day13.GetResultTwo(out timeElapsed).ToString(), timeElapsed.ToString());

            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        private static void displayResult(string title, string result, string timeElapsed)
        {
            Console.WriteLine($"{title} result: {result} and it took {timeElapsed}ms");
        }
    }
}
