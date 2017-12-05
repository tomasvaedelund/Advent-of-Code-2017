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

                displayResult("DayOne - First", DayOne.getResult(out timeElapsed).ToString(), timeElapsed.ToString());
                displayResult("DayOne - Second", DayOne.getResultTwo(out timeElapsed).ToString(), timeElapsed.ToString());

                displayResult("Daytwo - First", DayTwo.GetResult(out timeElapsed).ToString(), timeElapsed.ToString());
                displayResult("Daytwo - Second", DayTwo.GetResultTwo(out timeElapsed).ToString(), timeElapsed.ToString());

                displayResult("DayThree - First", DayThree.GetResult(out timeElapsed).ToString(), timeElapsed.ToString());
                displayResult("DayThree - Second", DayThree.GetResultTwo(out timeElapsed).ToString(), timeElapsed.ToString());

                displayResult("DayFour - First", DayFour.GetResult(out timeElapsed).ToString(), timeElapsed.ToString());
                displayResult("DayFour - Second", DayFour.GetResultTwo(out timeElapsed).ToString(), timeElapsed.ToString());

                displayResult("DayFive - First", DayFive.GetResult(out timeElapsed).ToString(), timeElapsed.ToString());
                displayResult("DayFive - Second", DayFive.GetResultTwo(out timeElapsed).ToString(), timeElapsed.ToString());

            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        private static void displayResult(string title, string result, string timeElapsed)
        {
            Console.WriteLine($"{title} result: {result} and it took {timeElapsed}ms");
        }
    }
}
