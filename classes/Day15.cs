using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Advent_of_Code_2017.classes
{
    public static class Day15
    {
        public static int GetResult(out long timeElapsed)
        {
            Debug.Assert(GetJudgesCount(65, 8921, 5) == 1);

            var result = 0;
            var stopWatch = Stopwatch.StartNew();
            result = GetJudgesCount(703, 516, 40000000);
            timeElapsed = stopWatch.ElapsedMilliseconds;

            return result;
        }

        public static int GetResultTwo(out long timeElapsed)
        {
            Debug.Assert(GetJudgesCountTwo(65, 8921, 5000000) == 309);

            var result = 0;
            var stopWatch = Stopwatch.StartNew();
            result = GetJudgesCountTwo(703, 516, 5000000);
            timeElapsed = stopWatch.ElapsedMilliseconds;

            return result;
        }

        private static int GetJudgesCount(long genA, long genB, int count)
        {
            var matches = 0;
            var previous = GetNextPair(Tuple.Create(genA, genB));

            for (int i = 0; i < count; i++)
            {
                var next = GetNextPair(previous);

                if ((next.Item1 & 0x000ffff) == (next.Item2 & 0x000ffff))
                {
                    matches += 1;
                }

                previous = next;
            }

            return matches;
        }

        private static Tuple<long, long> GetNextPair(Tuple<long, long> previous)
        {
            var genA = (previous.Item1 * 16807) % 2147483647;
            var genB = (previous.Item2 * 48271) % 2147483647;

            var next = Tuple.Create(genA, genB);

            return next;
        }

        private static int GetJudgesCountTwo(long genA, long genB, int count)
        {
            var matches = 0;
            var previous = GetNextPairTwo(Tuple.Create(genA, genB));

            for (int i = 0; i < count; i++)
            {
                var next = GetNextPairTwo(previous);

                if ((next.Item1 & 0x000ffff) == (next.Item2 & 0x000ffff))
                {
                    matches += 1;
                }

                previous = next;
            }

            return matches;
        }

        private static Tuple<long, long> GetNextPairTwo(Tuple<long, long> previous)
        {
            var genA = GetNextValue(previous.Item1, 16807, 4);
            var genB = GetNextValue(previous.Item2, 48271, 8);

            var next = Tuple.Create(genA, genB);

            return next;
        }

        private static long GetNextValue(long previous, int multiplier, int checker)
        {
            var next = (previous * multiplier) % 2147483647;

            while ((next % checker) != 0)
            {
                next = (next * multiplier) % 2147483647;
            }

            return next;
        }
    }
}