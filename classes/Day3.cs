using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Advent_of_Code_2017.classes
{
    public class Day3
    {
        public static void GetResult()
        {
            Debug.Assert(calculateDistanceToOrigo(1) == 0);
            Debug.Assert(calculateDistanceToOrigo(12) == 3);
            Debug.Assert(calculateDistanceToOrigo(23) == 2);
            //Debug.Assert(calculateDistanceToOrigo(1024) == 31);

            var result = "";
            var stopWatch = Stopwatch.StartNew();
            result = calculateDistanceToOrigo(312051).ToString();
            stopWatch.Stop();
            Helpers.DisplayDailyResult("3 - 1", result, stopWatch.ElapsedMilliseconds);

            Debug.Assert(getFirstValueThatIsLargerThanInput(1) == 2);
            Debug.Assert(getFirstValueThatIsLargerThanInput(9) == 10);
            Debug.Assert(getFirstValueThatIsLargerThanInput(147) == 304);
            Debug.Assert(getFirstValueThatIsLargerThanInput(747) == 806);

            stopWatch = Stopwatch.StartNew();
            result = getFirstValueThatIsLargerThanInput(312051).ToString();
            stopWatch.Stop();
            Helpers.DisplayDailyResult("3 - 2", result, stopWatch.ElapsedMilliseconds);
        }

        private static int calculateDistanceToOrigo(int position)
        {
            var matrix = getMatrix(position);
            var final = matrix.ElementAt(position - 1);
            var result = (0 - final.Item1) + (0 - final.Item2);

            return Math.Abs(result);
        }

        private static int getFirstValueThatIsLargerThanInput(int max)
        {
            var matrix = getMatrixTwo(max);
            var final = matrix.Last().Item3;

            return final;
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
                    result.Add(new Tuple<int, int>(x, y));
                    x = x + d;
                }

                while (2 * y * d < m)
                {
                    result.Add(new Tuple<int, int>(x, y));
                    y = y + d;
                }

                d = -1 * d;
                m = m + 1;
            }

            return result;
        }

        private static IEnumerable<Tuple<int, int, int>> getMatrixTwo(int max)
        {
            var result = new List<Tuple<int, int, int>>();

            var x = 0;
            var y = 0;
            var d = 1;
            var m = 1;
            var value = 0;

            while (value <= max)
            {

                while (2 * x * d < m && value <= max)
                {
                    value = getAdjacentSumForPos(result, x, y);
                    result.Add(new Tuple<int, int, int>(x, y, value));
                    x = x + d;
                }

                while (2 * y * d < m && value <= max)
                {
                    value = getAdjacentSumForPos(result, x, y);
                    result.Add(new Tuple<int, int, int>(x, y, value));
                    y = y + d;
                }

                d = -1 * d;
                m = m + 1;
            }

            return result;
        }

        private static int getAdjacentSumForPos(IEnumerable<Tuple<int, int, int>> matrix, int x, int y)
        {
            var sum = 0;

            sum += getValueFromPos(matrix, x + 1, y);
            sum += getValueFromPos(matrix, x + 1, y + 1);
            sum += getValueFromPos(matrix, x, y + 1);
            sum += getValueFromPos(matrix, x - 1, y + 1);
            sum += getValueFromPos(matrix, x - 1, y);
            sum += getValueFromPos(matrix, x - 1, y - 1);
            sum += getValueFromPos(matrix, x, y - 1);
            sum += getValueFromPos(matrix, x + 1, y - 1);

            return (sum == 0) ? 1 : sum;
        }

        private static int getValueFromPos(IEnumerable<Tuple<int, int, int>> matrix, int x, int y)
        {
            var item = matrix.FirstOrDefault(pos => pos.Item1 == x && pos.Item2 == y);

            return (item == null) ? 0 : item.Item3;
        }
    }
}