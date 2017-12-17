using System.Collections.Generic;
using System.Diagnostics;

namespace Advent_of_Code_2017.classes
{
    public static class Day17
    {
        public static void GetResult()
        {
            Debug.Assert(GetValueAfterLastInsert(3) == 638);

            //var data = Helpers.GetDataFromFile("day16.txt");
            var resultFirst = "";
            var resultSecond = "";
            var timeFirst = 0L;
            var timeSecond = 0L;

            var stopWatch = Stopwatch.StartNew();
            resultFirst = GetValueAfterLastInsert(363).ToString();
            timeFirst = stopWatch.ElapsedMilliseconds;

            stopWatch = Stopwatch.StartNew();
            //resultSecond = GetSecondResult();
            timeSecond = stopWatch.ElapsedMilliseconds;

            Helpers.DisplayDailyResult(17, resultFirst, resultSecond, timeFirst, timeSecond);
        }

        private static int GetValueAfterLastInsert(int steps)
        {
            var spinlock = new List<int>(2018) { 0 };
            var current = 0;

            for (int i = 1; i < 2018; i++)
            {
                // Get new position
                current = (steps + current) % spinlock.Count;
                
                // We want the position after
                current += 1;

                if (current >= spinlock.Count)
                {
                    spinlock.Add(i);
                }
                else
                {
                    spinlock.Insert(current, i);
                }
            }

            return spinlock[current + 1];
        }
    }
}