using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Advent_of_Code_2017.classes
{
    public class DaySix
    {
        public static int GetResult(out long timeElapsed)
        {
            Debug.Assert(getStepsNeeded("0\t2\t7\t0") == 5);

            var result = 0;
            var data = Helpers.getDataFromFile("daysix.txt");
            var stopWatch = Stopwatch.StartNew();
            result = getStepsNeeded(data);
            timeElapsed = stopWatch.ElapsedMilliseconds;

            return result;
        }

        public static int GetResultTwo(out long timeElapsed)
        {
            //Debug.Assert(getStepsNeeded("0\r\n3\r\n0\r\n1\r\n-3") == 5);

            var result = 0;
            var data = Helpers.getDataFromFile("dayfive.txt");
            var stopWatch = Stopwatch.StartNew();
            //result = getStepsNeeded(data);
            timeElapsed = stopWatch.ElapsedMilliseconds;

            return result;
        }

        private static int getStepsNeeded(string data)
        {
            var steps = 0;
            var array = data.Split('\t').Select(x => Convert.ToInt32(x)).ToArray();
            var arrays = new List<int[]>();

            while (true)
            {
                steps++;

                var posOfMaxElement = getPosOfMaxElement(array);
                var valueOfMaxElement = array[posOfMaxElement];
                array[posOfMaxElement] = 0;

                array = addMaxValueToAllElements(valueOfMaxElement, array, posOfMaxElement);

                if (arrays.Any(x => x.SequenceEqual(array)))
                {
                    break;
                }

                var copy = new int[array.Length];
                Array.Copy(array, copy, array.Length);
                arrays.Add(copy);
            }

            return steps;
        }

        private static int[] addMaxValueToAllElements(int max, int[] array, int pos)
        {
            for (int i = 1; i <= max; i++)
            {
                pos = (pos == array.Length - 1) ? 0 : pos += 1;
                array[pos]++;
            }

            return array;
        }

        private static int getPosOfMaxElement(int[] data)
        {
            var max = Int32.MinValue;
            var pos = 0;

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] > max)
                {
                    max = data[i];
                    pos = i;
                }
            }

            return pos;
        }

    }
}