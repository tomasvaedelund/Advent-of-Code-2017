using System.Diagnostics;

namespace Advent_of_Code_2017.classes
{
    public static class Day17
    {
        public static void GetResult()
        {
            //Debug.Assert(GetNewOrder("eabdc", "pe/b") == "baedc");

            //var data = Helpers.GetDataFromFile("day16.txt");
            var resultFirst = "";
            var resultSecond = "";
            var timeFirst = 0L;
            var timeSecond = 0L;

            var stopWatch = Stopwatch.StartNew();
            //resultFirst = GetFirstResult();
            timeFirst = stopWatch.ElapsedMilliseconds;

            stopWatch = Stopwatch.StartNew();
            //resultSecond = GetSecondResult();
            timeSecond = stopWatch.ElapsedMilliseconds;

            Helpers.DisplayDailyResult(17, resultFirst, resultSecond, timeFirst, timeSecond);
        }
    }
}