using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Advent_of_Code_2017.classes
{
    public static class Day21
    {

        private static string[] Matrix = {
            ".#.",
            "..#",
            "###"
        };

        public static void GetResult()
        {
            Debug.Assert(GetLitPixelsAfterNIterations(2) == 12);

            var result = "";

            var stopWatch = Stopwatch.StartNew();
            //result = GetNumberOfBurstsThatCauseInfection(10000, data).ToString();
            stopWatch.Stop();
            Helpers.DisplayDailyResult("22 - 1", result, stopWatch.ElapsedMilliseconds);

        }

        private static IEnumerable<(IEnumerable<string> pattern, IEnumerable<string> rule)> Rules = GetRules();

        private static int GetLitPixelsAfterNIterations(int iterations)
        {
            while (iterations-- > 0)
            {
                
            }

            return string.Join("", Matrix).Count(c => c == '#');
        }

        private static IEnumerable<(IEnumerable<string> pattern, IEnumerable<string> rule)> GetRules()
        {
            var rules = Helpers.GetDataFromFile("day21.txt");

            foreach (var rule in rules.Split("\r\n"))
            {
                var p = rule.Split(" => ")[0].Split('/');
                var r = rule.Split(" => ")[1].Split('/');

                yield return (p, r);
                yield return (p.FlipVertically(), r);
                yield return (p.FlipHorizontally(), r);
                yield return (p.Rotate90(), r);
                yield return (p.Rotate180(), r);
                yield return (p.Rotate270(), r);
            }
        }

        private static IEnumerable<string> FlipHorizontally(this IEnumerable<string> rows)
        {
            return rows.Reverse();
        }

        private static IEnumerable<string> FlipVertically(this IEnumerable<string> rows)
        {
            return rows.Select(x => Reverse(x));
        }

        private static IEnumerable<string> Rotate90(this IEnumerable<string> rows)
        {
            rows = rows.Transpose();
            rows = rows.FlipVertically();

            return rows;
        }

        private static IEnumerable<string> Rotate180(this IEnumerable<string> rows)
        {
            rows = rows.FlipVertically();
            rows = rows.FlipHorizontally();

            return rows;
        }

        private static IEnumerable<string> Rotate270(this IEnumerable<string> rows)
        {
            rows = rows.Rotate180();
            rows = rows.Rotate90();

            return rows;
        }

        private static IEnumerable<string> Transpose(this IEnumerable<string> rows)
        {
            var size = rows.Count();
            var result = rows.Select(x => x.ToCharArray()).ToArray();

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i][j] = rows.ElementAt(j)[i];
                }
            }

            return result.Select(x => string.Join("", x));
        }

        private static string Reverse(string source)
        {
            var charArray = source.ToCharArray();

            Array.Reverse(charArray);
            
            return new string(charArray);
        }
    }
}