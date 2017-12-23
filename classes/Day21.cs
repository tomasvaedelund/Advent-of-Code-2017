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

        private static string TestRules = 
            "../.# => ##./#../...\r\n.#./..#/### => #..#/..../..../#..#";

        public static void GetResult()
        {
            Rules = GetRules(TestRules);
            Debug.Assert(GetLitPixelsAfterNIterations(2) == 12);

            var result = "";

            var stopWatch = Stopwatch.StartNew();
            var data = Helpers.GetDataFromFile("day21.txt");
            Rules = GetRules(data);
            result = GetLitPixelsAfterNIterations(5).ToString();
            stopWatch.Stop();
            Helpers.DisplayDailyResult("21 - 1", result, stopWatch.ElapsedMilliseconds);

            stopWatch = Stopwatch.StartNew();
            result = GetLitPixelsAfterNIterations(18).ToString();
            stopWatch.Stop();
            Helpers.DisplayDailyResult("21 - 2", result, stopWatch.ElapsedMilliseconds);
        }

        private static IEnumerable<(string pattern, IEnumerable<string> rule)> Rules;

        private static int GetLitPixelsAfterNIterations(int iterations)
        {
            var result = Matrix.ToList();

            while (iterations-- > 0)
            {
                var size = result.Count();
                var steps = (size % 2 == 0) ? 2 : 3;
                var tempResult = new List<string>();

                for (int i = 0; i < size; i += steps)
                {
                    // https://msdn.microsoft.com/library/bb348899(v=vs.110).aspx
                    var temp = Enumerable.Repeat(string.Empty, steps + 1);

                    for (int j = 0; j < size; j += steps)
                    {
                        var square = result.Skip(i).Take(steps).Select(x => x.Substring(j, steps));
                        var rule = Rules.GetRule(square.ToKey()).ToList();

                        temp = temp.Zip(rule, (a, b) => $"{a}{b}").ToList();
                    }

                    tempResult.AddRange(temp);
                } 

                result = tempResult;
            }

            return string.Join("", result).Count(c => c == '#');
        }

        private static IEnumerable<string> GetRule(this IEnumerable<(string pattern, IEnumerable<string> rule)> rules, string key)
        {
            return rules.First(x => x.pattern == key).rule;
        }

        private static IEnumerable<(string pattern, IEnumerable<string> rule)> GetRules(string rules)
        {
            foreach (var rule in rules.Split("\r\n"))
            {
                var p = rule.Split(" => ")[0].Split('/');
                var r = rule.Split(" => ")[1].Split('/');

                yield return (p.ToKey(), r);
                yield return (p.Rotate90().ToKey(), r);
                yield return (p.Rotate180().ToKey(), r);
                yield return (p.Rotate270().ToKey(), r);
                yield return (p.FlipHorizontally().ToKey(), r);
                yield return (p.FlipHorizontally().Rotate90().ToKey(), r);
                yield return (p.FlipHorizontally().Rotate180().ToKey(), r);
                yield return (p.FlipHorizontally().Rotate270().ToKey(), r);
            }
        }

        private static string ToKey(this IEnumerable<string> rows)
        {
            return string.Join("/", rows);
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