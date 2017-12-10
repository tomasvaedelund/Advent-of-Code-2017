using System.Diagnostics;

namespace Advent_of_Code_2017.classes
{
    public class Day11
    {
        public static string GetResult(out long timeElapsed)
        {
            //Debug.Assert(GetScore("{{<a>},{<a>},{<a>},{<a>}}") == 9);

            var result = "";
            //var data = Helpers.getDataFromFile("daynine.txt");
            var stopWatch = Stopwatch.StartNew();
            //result = GetScore(data).ToString();
            timeElapsed = stopWatch.ElapsedMilliseconds;

            return result;
        }
    }
}