using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Advent_of_Code_2017.classes
{
    public static class Day14
    {
        public static int GetResult(out long timeElapsed)
        {
            //Debug.Assert(GetResult(data) == 24);

            var result = 0;
            var stopWatch = Stopwatch.StartNew();
            result = GetNumberOfUsedSquares("vbqugkhl");
            timeElapsed = stopWatch.ElapsedMilliseconds;

            return result;
        }

        private static int GetNumberOfUsedSquares(string data)
        {
            var result = 0;
            var disc = new List<string>();

            for (int i = 0; i < 128; i++)
            {
                var rowHex = HashString(256, $"{data}-{i}");
                var rowBin = HexToBinary(rowHex);
                disc.Add(rowBin);
            }

            foreach (var row in disc)
            {
                result += row.Where(x => x == 49).Count();
            }

            return result;
        }

        private static string HexToBinary(string hexstring)
        {
            return String.Join(String.Empty,
                hexstring.Select(
                    c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
                )
            );
        }

        private static string HashString(int size, string stringToHash)
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

        private static int[] GetList(int size)
        {
            var list = new int[size];

            for (int i = 0; i < size; i++)
            {
                list[i] = i;
            }

            return list;
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

                result[i / 16] = hash;
            }

            return result;
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