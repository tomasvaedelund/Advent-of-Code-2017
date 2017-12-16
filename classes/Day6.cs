using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Advent_of_Code_2017.classes
{
    public class Day6
    {
        public static int GetResult(out long timeElapsed)
        {
            Debug.Assert(getStepsNeeded("0\t2\t7\t0") == 5);

            var result = 0;
            var data = Helpers.GetDataFromFile("daysix.txt");
            var stopWatch = Stopwatch.StartNew();
            result = getStepsNeeded(data);
            timeElapsed = stopWatch.ElapsedMilliseconds;

            return result;
        }

        public static int GetResultTwo(out long timeElapsed)
        {
            Debug.Assert(getStepsNeeded("0\t2\t7\t0", true) == 4);

            var result = 0;
            var data = Helpers.GetDataFromFile("daysix.txt");
            var stopWatch = Stopwatch.StartNew();
            result = getStepsNeeded(data, true);
            timeElapsed = stopWatch.ElapsedMilliseconds;

            return result;
        }

        private static int getStepsNeeded(string data, bool second = false)
        {
            var steps = 0;
            var array = data.Split('\t').Select(x => Convert.ToInt32(x)).ToArray();
            var arrays = new List<int[]>();

            while (true)
            {
                var posOfMaxElement = getPosOfMaxElement(array);
                var valueOfMaxElement = array[posOfMaxElement];
                array[posOfMaxElement] = 0;

                array = addMaxValueToAllElements(valueOfMaxElement, array, posOfMaxElement);

                if (arrays.Any(x => x.SequenceEqual(array)))
                {
                    if (second && steps++ == 0)
                    {
                        arrays = new List<int[]>();
                    }
                    else
                    {
                        break;
                    }
                }

                var copy = new int[array.Length];
                Array.Copy(array, copy, array.Length);
                arrays.Add(copy);
            }

            return (second) ? arrays.Count : arrays.Count + 1;
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