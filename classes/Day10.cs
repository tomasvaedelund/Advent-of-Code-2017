using System;
using System.Diagnostics;
using System.Linq;

namespace Advent_of_Code_2017.classes
{
    public class Day10
    {
        public static string GetResult(out long timeElapsed)
        {
            var data = new int[] { 0, 1, 2, 3, 4 };
            var expected = new int[] { 2, 1, 0, 3, 4 };
            Debug.Assert(GetListWithReversedSublist(data, 0, 3).SequenceEqual(expected));

            data = expected;
            expected = new int[] { 4, 3, 0, 1, 2 };
            Debug.Assert(GetListWithReversedSublist(data, 3, 4).SequenceEqual(expected));

            data = expected;
            expected = new int[] { 4, 3, 0, 1, 2 };
            Debug.Assert(GetListWithReversedSublist(data, 3, 1).SequenceEqual(expected));

            data = expected;
            expected = new int[] { 3, 4, 2, 1, 0 };
            Debug.Assert(GetListWithReversedSublist(data, 1, 5).SequenceEqual(expected));

            var lengths = new int[] { 3, 4, 1, 5 };
            Debug.Assert(GetResult(5, lengths) == 12);

            lengths = new int[] { 3, 4, 1, 5 };
            Debug.Assert(GetResult(10, lengths) == 8);

            lengths = new int[] { 8, 2, 1, 5, 0, 7 };
            Debug.Assert(GetResult(10, lengths) == 6);

            var result = "";
            lengths = new int[] { 129, 154, 49, 198, 200, 133, 97, 254, 41, 6, 2, 1, 255, 0, 191, 108 };
            var stopWatch = Stopwatch.StartNew();
            result = GetResult(256, lengths).ToString();
            timeElapsed = stopWatch.ElapsedMilliseconds;

            return result;
        }

        private static int GetResult(int size, int[] lengths)
        {
            var list = GetList(size);
            var index = 0;

            for (int i = 0; i < lengths.Length; i++)
            {
                var length = lengths[i];

                if (length > 1)
                {
                    list = GetListWithReversedSublist(list, index, length);
                }

                index = (index + length + i) % size;
            }

            return list[0] * list[1];
        }

        private static int[] GetList(int size)
        {
            var list = new int[size];

            for (int i = 0; i < size; i++)
            {
                list[i] = i;
            }

            return list;
        }

        private static int[] GetListWithReversedSublist(int[] list, int index, int length)
        {
            while (true)
            {
                var temp = list[index];
                var newIndex = (index + length - 1) % list.Length;

                list[index] = list[newIndex];
                list[newIndex] = temp;

                index = (index + 1) % list.Length;
                length -= 2;

                if (length > 1)
                {
                    continue;
                }
                else
                {
                    break;
                }
            }

            return list;
        }
    }
}