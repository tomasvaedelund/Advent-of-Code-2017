using System;
using System.Diagnostics;

namespace Advent_of_Code_2017.classes
{
    public class Day11
    {
        public static int GetResult(out long timeElapsed)
        {
            Debug.Assert(GetNumberOfSteps("ne,ne,ne") == 3);
            Debug.Assert(GetNumberOfSteps("ne,ne,sw,sw") == 0);
            Debug.Assert(GetNumberOfSteps("ne,ne,s,s") == 2);
            Debug.Assert(GetNumberOfSteps("se,sw,se,sw,sw") == 3);

            var result = 0;
            var data = Helpers.GetDataFromFile("day11.txt");
            var stopWatch = Stopwatch.StartNew();
            result = GetNumberOfSteps(data);
            timeElapsed = stopWatch.ElapsedMilliseconds;

            return result;
        }

        public static int GetResultTwo(out long timeElapsed)
        {
            var result = 0;
            var stopWatch = Stopwatch.StartNew();
            result = MaxDistance;
            timeElapsed = stopWatch.ElapsedMilliseconds;

            return result;
        }

        private static int GetNumberOfSteps(string data)
        {
            var array = data.Split(',');

            var endPoint = GetEndPoint(array);

            var numberOfSteps = GetNumberOfSteps(endPoint);

            return numberOfSteps;
        }

        private static int GetNumberOfSteps(Tuple<int, int, int> point)
        {
            var numberOfSteps = Math.Abs(0 - point.Item1) + Math.Abs(0 - point.Item2) + Math.Abs(0 - point.Item3);

            return numberOfSteps / 2;
        }

        private static int MaxDistance = Int32.MinValue;
        private static Tuple<int, int, int> GetEndPoint(string[] array)
        {
            var currentPos = Tuple.Create(0, 0, 0);
            var distance = 0;

            foreach (var move in array)
            {
                currentPos = GetNextPoint(currentPos, move);
                distance = GetNumberOfSteps(currentPos);
                MaxDistance = (distance > MaxDistance) ? distance : MaxDistance;
            }

            return currentPos;
        }

        private static Tuple<int, int, int> GetNextPoint(Tuple<int, int, int> point, string movement)
        {
            switch (movement)
            {
                case "n":
                    return Tuple.Create(point.Item1 + 1, point.Item2, point.Item3 - 1);
                case "ne":
                    return Tuple.Create(point.Item1, point.Item2 + 1, point.Item3 - 1);
                case "nw":
                    return Tuple.Create(point.Item1 + 1, point.Item2 - 1, point.Item3);
                case "s":
                    return Tuple.Create(point.Item1 - 1, point.Item2, point.Item3 + 1);
                case "se":
                    return Tuple.Create(point.Item1 - 1, point.Item2 + 1, point.Item3);
                case "sw":
                    return Tuple.Create(point.Item1, point.Item2 - 1, point.Item3 + 1);
                default:
                    throw new ArgumentException($"Unkonwn movement: {movement}");
            }
        }
    }
}