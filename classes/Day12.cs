using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Advent_of_Code_2017.classes
{
    public class Day12
    {
        public static int GetResult(out long timeElapsed)
        {
            var data = Helpers.GetDataFromFile("day12-test.txt");
            Debug.Assert(GetResult(data) == 6);

            var result = 0;
            data = Helpers.GetDataFromFile("day12.txt");
            var stopWatch = Stopwatch.StartNew();
            result = GetResult(data);
            timeElapsed = stopWatch.ElapsedMilliseconds;

            return result;
        }

        public static int GetResultTwo(out long timeElapsed)
        {
            var result = 0;
            var data = Helpers.GetDataFromFile("day12.txt");
            var stopWatch = Stopwatch.StartNew();
            result = GetResultTwo(data);
            timeElapsed = stopWatch.ElapsedMilliseconds;

            return result;
        }

        private static int GetResult(string data)
        {
            var array = data.Split("\r\n");
            var programs = new List<int>();

            var numberOfConnectedPrograms = GetNumberOfConnectedPrograms(0, array, programs);

            return numberOfConnectedPrograms.Count;
        }

        private static int GetResultTwo(string data)
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

            var current = array.Single(x => x.StartsWith($"{id} <->"));
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