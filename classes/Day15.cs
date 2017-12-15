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

        private static int GetJudgesCount(long genA, long genB, int count)
        {
            var matches = 0;
            var pairs = new List<Tuple<int, int>>();
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
    }
}