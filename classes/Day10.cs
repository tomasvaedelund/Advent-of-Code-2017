using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

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

        public static string GetResultTwo(out long timeElapsed)
        {
            Debug.Assert(DenseHash(new int[] { 65, 27, 9, 1, 4, 3, 40, 50, 91, 7, 6, 0, 2, 5, 68, 22 }).SequenceEqual(new int[] {64, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}));

            Debug.Assert(GetResultTwo(256, "") == "a2582a3a0e66e6e86e3812dcb672a272");
            Debug.Assert(GetResultTwo(256, "AoC 2017") == "33efeb34ea91902bb2f59c9920caa6cd");
            Debug.Assert(GetResultTwo(256, "1,2,3") == "3efbe78a8d82f29979031a4aa0b16a9d");
            Debug.Assert(GetResultTwo(256, "1,2,4") == "63960835bcdc130f0b66d7ff4f6a5a8e");

            var result = "";
            var stopWatch = Stopwatch.StartNew();
            result = GetResultTwo(256, "129,154,49,198,200,133,97,254,41,6,2,1,255,0,191,108");
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

        private static string GetResultTwo(int size, string stringToHash)
        {
            var list = GetList(size);
            stringToHash = stringToHash.Trim();

            var bytes = Encoding.ASCII.GetBytes(stringToHash).Select(x => Convert.ToInt32(x)).ToList();
            bytes.Add(17);
            bytes.Add(31);
            bytes.Add(73);
            bytes.Add(47);
            bytes.Add(23);

            var bytesArray = bytes.ToArray();

            var index = 0;
            var skip = 0;
            for (int j = 0; j < 64; j++)
            {
                for (int i = 0; i < bytesArray.Length; i++)
                {
                    var length = bytesArray[i];

                    if (length > 1)
                    {
                        list = GetListWithReversedSublist(list, index, length);
                    }

                    index = (index + length + skip) % size;
                    skip++;
                }
            }

            var denseHash = DenseHash(list);
            var result = new string[16];

            for (int i = 0; i < denseHash.Length; i++)
            {
                result[i] = denseHash[i].ToString("X2");
            }

            return string.Join("", result).ToLower();
        }

        private static int[] DenseHash(int[] byteArray)
        {
            var result = new int[16];
            var size = byteArray.Length;

            var hash = 0;
            for (int i = 0; i < size; i += 16)
            {
                hash = byteArray[i];
                for (int j = i + 1; j < i + 16; j++)
                {
                    hash ^= byteArray[j];
                }

                result[i/16] = hash;
            }

            return result;
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