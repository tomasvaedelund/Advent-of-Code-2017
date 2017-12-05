using System;
using System.Diagnostics;
using System.Linq;

namespace Advent_of_Code_2017.classes
{
    public class DayFive
    {
        public static int GetResult(out long timeElapsed)
        {
            Debug.Assert(getStepsNeeded("0\r\n3\r\n0\r\n1\r\n-3") == 5);

            var result = 0;
            var data = Helpers.getDataFromFile("dayfive.txt");
            var stopWatch = Stopwatch.StartNew();
            result = getStepsNeeded(data);
            timeElapsed = stopWatch.ElapsedMilliseconds;

            return result;
        }

        public static int GetResultTwo(out long timeElapsed)
        {
            Debug.Assert(getStepsNeededTwo("0\r\n3\r\n0\r\n1\r\n-3") == 10);

            var result = 0;
            var data = Helpers.getDataFromFile("dayfive.txt");
            var stopWatch = Stopwatch.StartNew();
            result = getStepsNeededTwo(data);
            timeElapsed = stopWatch.ElapsedMilliseconds;

            return result;
        }

        private static int getStepsNeeded(string data)
        {
            var stepsNeeded = 0;
            var array = data.Split("\r\n").Select(x => Convert.ToInt32(x)).ToArray();
            var position = 0;
            var current = 0;

            while (true)
            {
                stepsNeeded++;

                current = position;
                position += array[position];

                if (position >= array.Length)
                {
                    break;
                }

                array[current]++;
            }

            return stepsNeeded;
        }

        private static int getStepsNeededTwo(string data)
        {
            var stepsNeeded = 0;
            var array = data.Split("\r\n").Select(x => Convert.ToInt32(x)).ToArray();
            var position = 0;
            var current = 0;

            while (true)
            {
                stepsNeeded++;

                current = position;
                position += array[position];

                if (position >= array.Length)
                {
                    break;
                }

                if (array[current] >= 3)
                {
                    array[current]--;
                }
                else
                {
                    array[current]++;
                }
            }

            return stepsNeeded;
        }
    }
}