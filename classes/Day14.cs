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

        public static int GetResultTwo(out long timeElapsed)
        {
            //Debug.Assert(GetResult(data) == 24);

            var result = 0;
            var stopWatch = Stopwatch.StartNew();
            result = GetNumberOfGroups("vbqugkhl");
            timeElapsed = stopWatch.ElapsedMilliseconds;

            return result;
        }

        private static int GetNumberOfUsedSquares(string data)
        {
            var result = 0;
            var disc = GetDisc(data);

            foreach (var row in disc)
            {
                result += row.Where(x => x == 49).Count();
            }

            return result;
        }

        private static int GetNumberOfGroups(string data)
        {
            var result = 0;
            var disc = GetDisc(data);
            var relations = new List<string>();

            for (int row = 0; row < 128; row++)
            {
                for (int col = 0; col < 128; col++)
                {
                    var current = disc[row][col];
                    if (current == 49)
                    {
                        var relation = CheckAdjecentCells(row, col, disc);

                        if (relation.Any())
                        {
                            relations.Add($"{(row * 128) + col} <-> {string.Join(',', relation)}");
                        }
                        else
                        {
                            // Because even singles count
                            result += 1;
                        }
                    }
                }
            }

            result += GetNumberOfGroupsFromRelations(string.Join("\r\n", relations));

            return result;
        }

        private static List<int> CheckAdjecentCells(int row, int col, int[][] disc)
        {
            var current = (row * 128) + col;
            var relation = new List<int>();

            // Check up
            if (row > 0)
            {
                if (disc[row - 1][col] == 49)
                {
                    relation.Add(((row - 1) * 128) + col);
                }
            }

            // Check down
            if (row < 127)
            {
                if (disc[row + 1][col] == 49)
                {
                    relation.Add(((row + 1) * 128) + col);
                }
            }

            // Check left
            if (col > 0)
            {
                if (disc[row][col - 1] == 49)
                {
                    relation.Add((row * 128) + col - 1);
                }
            }

            // Check right
            if (col < 127)
            {
                if (disc[row][col + 1] == 49)
                {
                    relation.Add((row * 128) + col + 1);
                }
            }

            return relation;
        }

        private static int[][] GetDisc(string data)
        {
            var disc = new int[128][];

            for (int i = 0; i < 128; i++)
            {
                var rowHex = HashString(256, $"{data}-{i}");
                var rowBin = HexToBinary(rowHex);
                disc[i] = rowBin.Select(c => Convert.ToInt32(c)).ToArray();
            }

            return disc;
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

        private static int GetNumberOfGroupsFromRelations(string data)
        {
            var array = data.Split("\r\n");
            var programs = new List<int>();

            var numberOfConnectedPrograms = GetNumberOfConnectedPrograms(0, array, programs);
            var numberOfGroups = 1;

            foreach (var row in array)
            {
                var currentId = Convert.ToInt32(row.Split("<->")[0].Trim());
                if (!numberOfConnectedPrograms.Contains(currentId))
                {
                    numberOfConnectedPrograms = GetNumberOfConnectedPrograms(currentId, array, numberOfConnectedPrograms);
                    numberOfGroups++;
                }
            }

            return numberOfGroups;
        }

        private static List<int> GetNumberOfConnectedPrograms(int id, string[] array, List<int> programs)
        {
            if (!programs.Any(x => x == id))
            {
                programs.Add(id);
            }

            var current = array.FirstOrDefault(x => x.StartsWith($"{id} <->"));
            var children = (current.Split("<->").Length > 1) ? current.Split("<->")[1] : "";
            var childrenIds = children.Split(',').Select(x => Convert.ToInt32(x.Trim()));

            foreach (var childId in childrenIds)
            {
                if (programs.Contains(childId))
                {
                    continue;
                }

                programs = GetNumberOfConnectedPrograms(childId, array, programs);
            }

            return programs;
        }
    }
}