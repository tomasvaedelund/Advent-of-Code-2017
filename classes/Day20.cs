using System.Diagnostics;

namespace Advent_of_Code_2017.classes
{
    public static class Day20
    {
        public static void GetResult()
        {
            //Debug.Assert(GetLettersFoundInMaze(data) == "ABCDEF");
            
            //var data = Helpers.GetDataFromFile("day20.txt");
            var result = "";
            
            var stopWatch = Stopwatch.StartNew();
            //result = GetLettersFoundInMaze(data);
            stopWatch.Stop();
            Helpers.DisplayDailyResult("20 - 1", result, stopWatch.ElapsedMilliseconds);

            stopWatch = Stopwatch.StartNew();
            //result = GetLettersFoundInMaze(data);
            stopWatch.Stop();
            Helpers.DisplayDailyResult("20 - 2", result, stopWatch.ElapsedMilliseconds);
        }
    }
}