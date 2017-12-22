using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent_of_Code_2017.classes
{
    public static class Day21
    {
        public static void GetResult()
        {
            var test = ".#./.##/##.";

            var array = test.Split('/');

            Print(array);
            Print(array.FlipHorizontally());
            Print(array.FlipVertically());
            Print(array.Transpose());
            Print(array.Rotate90());
        }

        static void Print(IEnumerable<string> value)
        {
            Console.WriteLine(string.Join("/", value));
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