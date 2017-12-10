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

                // displayResult("DayOne - First", Day1.getResult(out timeElapsed).ToString(), timeElapsed.ToString());
                // displayResult("DayOne - Second", Day1.getResultTwo(out timeElapsed).ToString(), timeElapsed.ToString());

                // displayResult("Daytwo - First", Day2.GetResult(out timeElapsed).ToString(), timeElapsed.ToString());
                // displayResult("Daytwo - Second", Day2.GetResultTwo(out timeElapsed).ToString(), timeElapsed.ToString());

                // displayResult("DayThree - First", Day3.GetResult(out timeElapsed).ToString(), timeElapsed.ToString());
                // displayResult("DayThree - Second", Day3.GetResultTwo(out timeElapsed).ToString(), timeElapsed.ToString());

                // displayResult("DayFour - First", Day4.GetResult(out timeElapsed).ToString(), timeElapsed.ToString());
                // displayResult("DayFour - Second", Day4.GetResultTwo(out timeElapsed).ToString(), timeElapsed.ToString());

                // displayResult("DayFive - First", Day5.GetResult(out timeElapsed).ToString(), timeElapsed.ToString());
                // displayResult("DayFive - Second", Day5.GetResultTwo(out timeElapsed).ToString(), timeElapsed.ToString());

                // displayResult("DaySix - First", Day6.GetResult(out timeElapsed).ToString(), timeElapsed.ToString());
                // displayResult("DaySix - Second", Day6.GetResultTwo(out timeElapsed).ToString(), timeElapsed.ToString());

                // displayResult("DaySeven - First", Day7.GetResult(out timeElapsed).ToString(), timeElapsed.ToString());
                // displayResult("DaySeven - Second", Day7.GetResultTwo(out timeElapsed).ToString(), timeElapsed.ToString());

                // displayResult("DayEight - First", Day8.GetResult(out timeElapsed).ToString(), timeElapsed.ToString());
                // displayResult("DayEight - Second", Day8.GetResultTwo(out timeElapsed).ToString(), timeElapsed.ToString());

                // displayResult("DayNine - First", Day9.GetResult(out timeElapsed).ToString(), timeElapsed.ToString());
                // displayResult("DayNine - Second", Day9.GetResultTwo(out timeElapsed).ToString(), timeElapsed.ToString());

                // displayResult("Day 10 - First", Day10.GetResult(out timeElapsed).ToString(), timeElapsed.ToString());
                // displayResult("Day 10 - Second", Day10.GetResultTwo(out timeElapsed).ToString(), timeElapsed.ToString());
                
                displayResult("Day 11 - First", Day11.GetResult(out timeElapsed).ToString(), timeElapsed.ToString());
                //displayResult("Day 11 - Second", Day11.GetResultTwo(out timeElapsed).ToString(), timeElapsed.ToString());

            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        private static void displayResult(string title, string result, string timeElapsed)
        {
            Console.WriteLine($"{title} result: {result} and it took {timeElapsed}ms");
        }
    }
}
