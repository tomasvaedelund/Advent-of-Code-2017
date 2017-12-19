using System;
using System.Diagnostics;
using System.Linq;

namespace Advent_of_Code_2017.classes
{
    public static class Day2
    {
        public static void GetResult()
        {
            var testData = "5\t1\t9\t5\r\n7\t5\t3\r\n2\t4\t6\t8";
            Debug.Assert(SplitAndCalculateTotalChecksum(testData) == 18);

            var data = Helpers.GetDataFromFile("day2.txt");
            var result = "";
            var stopWatch = Stopwatch.StartNew();
            result = SplitAndCalculateTotalChecksum(data).ToString();
            stopWatch.Stop();
            Helpers.DisplayDailyResult("2 - 1", result, stopWatch.ElapsedMilliseconds);

            testData = "5\t9\t2\t8\r\n9\t4\t7\t3\r\n3\t8\t6\t5";
            Debug.Assert(SplitAndCalculateTotalChecksum(testData, true) == 9);

            stopWatch = Stopwatch.StartNew();
            result = SplitAndCalculateTotalChecksum(data, true).ToString();
            stopWatch.Stop();
            Helpers.DisplayDailyResult("2 - 2", result, stopWatch.ElapsedMilliseconds);
        }

        public static int SplitAndCalculateTotalChecksum(string data, bool secondStar = false)
        {
            var checksum = 0;
            var lines = data.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                var lineArray = line.Split('\t').Select(n => Convert.ToInt32(n)).ToArray();

                if (secondStar)
                {
                    checksum += CalculateChecksumTwo(lineArray);
                }
                else
                {
                    checksum += CalculateChecksum(lineArray);
                }
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

        public static int CalculateChecksumTwo(int[] lineArray)
        {
            var min = Int32.MaxValue;
            var max = Int32.MinValue;

            for (int i = 0; i < lineArray.Length; i++)
            {
                for (int j = i + 1; j < lineArray.Length; j++)
                {
                    max = lineArray[j];
                    min = lineArray[i];

                    if (lineArray[i] > lineArray[j])
                    {
                        max = lineArray[i];
                        min = lineArray[j];
                    }

                    if (max % min == 0)
                    {
                        return max / min;
                    }
                }
            }

            throw new Exception("No numbers match the rules");
        }
    }
}