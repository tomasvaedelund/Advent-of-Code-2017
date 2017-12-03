using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Advent_of_Code_2017.classes
{
    public class DayThree
    {
        public static int GetResult(out long timeElapsed)
        {
            var result = 0;

            Debug.Assert(calculateDistanceToOrigo(1) == 0);
            Debug.Assert(calculateDistanceToOrigo(12) == 3);
            Debug.Assert(calculateDistanceToOrigo(23) == 2);
            //Debug.Assert(calculateDistanceToOrigo(1024) == 31);

            var stopWatch = Stopwatch.StartNew();
            result = calculateDistanceToOrigo(312051);
            timeElapsed = stopWatch.ElapsedMilliseconds;

            return result;
        }

        private static int calculateDistanceToOrigo(int position)
        {
            var matrix = getMatrix(position);
            var final = matrix.ElementAt(position - 1);
            var result = (0 - final.Item1) + (0 - final.Item2);

            return Math.Abs(result);
        }

        private static IEnumerable<Tuple<int, int>> getMatrix(int length)
        {
            var result = new List<Tuple<int, int>>();

            var x = 0;
            var y = 0;
            var d = 1;
            var m = 1;

            while (result.Count < length)
            {
                while (2 * x * d < m)
                {
                    result.Add(new Tuple <int, int>(x, y));
                    x = x + d;
                }

                while (2 * y * d < m)
                {
                    result.Add(new Tuple <int, int>(x, y));
                    y = y + d;
                }

                d = -1 * d;
                m = m + 1;
            }

            return result;
        }
    }
}