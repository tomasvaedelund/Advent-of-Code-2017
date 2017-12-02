using System;
using System.Diagnostics;
using System.Linq;

namespace Advent_of_Code_2017.classes
{
    public static class DayTwo
    {
        public static int GetResult(out long timeElapsed)
        {
            var result = 0;

            var data = "5\t1\t9\t5\r\n7\t5\t3\r\n2\t4\t6\t8";
            
            Debug.Assert(SplitAndCalculateTotalChecksum(data) == 18);

            data = Helpers.getDataFromFile("daytwo.txt");
            var stopWatch = Stopwatch.StartNew();
            result = SplitAndCalculateTotalChecksum(data);
            timeElapsed = stopWatch.ElapsedMilliseconds;

            return result;
        }

        public static int SplitAndCalculateTotalChecksum(string data)
        {
            var checksum = 0;
            var lines = data.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                var lineArray = line.Split('\t').Select(n => Convert.ToInt32(n)).ToArray();
                checksum += CalculateChecksum(lineArray);
            }

            return checksum;
        }

        public static int CalculateChecksum(int[] lineArray)
        {
            var min = Int32.MaxValue;
            var max = Int32.MinValue;

            foreach (var number in lineArray)
            {
                min = (number < min) ? number : min;
                max = (number > max) ? number : max;
            }

            return max - min;
        }
    }
}