using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Advent_of_Code_2017.classes
{
    public static class Day17
    {
        public static void GetResult()
        {
            Debug.Assert(GetValueAfterLastInsert(3) == "638");

            var result = "";

            var stopWatch = Stopwatch.StartNew();
            result = GetValueAfterLastInsert(363);
            Helpers.DisplayDailyResult("17 - 1", result, stopWatch.ElapsedMilliseconds);

            stopWatch = Stopwatch.StartNew();
            result = GetValueAfterLastInsertTwo(363);
            Helpers.DisplayDailyResult("17 - 2", result, stopWatch.ElapsedMilliseconds);
        }

        private static string GetValueAfterLastInsert(int steps)
        {
            var spinlock = new List<int>(2018) { 0 };
            var current = 0;

            for (int i = 1; i < 2018; i++)
            {
                // Get new position
                current = (steps + current) % i;

                // We want the position after
                current += 1;

                if (current >= i)
                {
                    spinlock.Add(i);
                }
                else
                {
                    spinlock.Insert(current, i);
                }
            }

            return spinlock[current + 1].ToString();
        }

        private static string GetValueAfterLastInsertTwo(int steps)
        {
            var spinlock = new List<int>(50000000) { 0 };
            var current = 0;
            var result = 0;

            for (int i = 1; i < 50000000; i++)
            {
                // Get new position
                current = (steps + current) % i;
                
                // We want the position after
                current += 1;

                if (current == 1)
                {
                    result = i;
                }
            }

            return result.ToString();
        }
    }
}