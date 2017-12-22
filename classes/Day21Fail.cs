using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Advent_of_Code_2017.classes
{
    public static class Day21Fail
    {
        private static string StartMatrix = ".#./..#/###";
        private static (string pattern, string rule)[] Rules;

        public static void GetResult()
        {
            GetRules("../.# => ##./#../...\r\n.#./..#/### => #..#/..../..../#..#");
            Debug.Assert(GetNumberOfOnPixelsAfterNIterations(2) == 12);

            //data = Helpers.GetDataFromFile("day20.txt");
            var result = "";

            var stopWatch = Stopwatch.StartNew();
            //result = GetClosestParticleInTheLongRun(data).ToString();
            stopWatch.Stop();
            Helpers.DisplayDailyResult("20 - 1", result, stopWatch.ElapsedMilliseconds);

            stopWatch = Stopwatch.StartNew();
            //result = GetNumerOfUnCollidedParticles(data).ToString();
            stopWatch.Stop();
            Helpers.DisplayDailyResult("20 - 2", result, stopWatch.ElapsedMilliseconds);
        }

        private static void GetRules(string rules)
        {
            Rules = new(string pattern, string rule)[rules.Split("\r\n").Length];

            var index = 0;
            foreach (var row in rules.Split("\r\n"))
            {
                var pattern = row.Split(" => ")[0];
                var rule = row.Split(" => ")[1];

                Rules[index++] = (pattern, rule);
            }
        }

        private static int GetNumberOfOnPixelsAfterNIterations(int iterations)
        {
            var matrix = ConvertToMatrix(StartMatrix);

            while (iterations-- > 0)
            {
                matrix = PerformIteration(matrix);
            }

            var result = ConvertMatrixToString(matrix);

            return result.Where(c => c == '#').Count();
        }

        private static char[,] PerformIteration(char[,] matrix)
        {
            var size = matrix.GetLength(0);

            if (size < 4)
            {
                return ApplyRule(matrix);
            }

            matrix = SplitAndCheckEachSquare(matrix);

            return matrix;
        }

        private static char[,] SplitAndCheckEachSquare(char[,] matrix)
        {
            var size = matrix.GetLength(0);
            var squareSize = (size % 2 == 0) ? 2 : 3;
            var newSize = squareSize * (size / squareSize);
            var newMatrix = new char[newSize, newSize];

            for (int col = 0; col < size / squareSize; col++)
            {
                for (int row = 0; row < size / squareSize; row++)
                {
                    var square = new char[squareSize, squareSize];

                    square[0, 0] = matrix[col * squareSize + 0, row * squareSize + 0];
                    square[0, 1] = matrix[col * squareSize + 0, row * squareSize + 1];

                    square[1, 0] = matrix[col * squareSize + 1, row * squareSize + 0];
                    square[1, 1] = matrix[col * squareSize + 1, row * squareSize + 1];

                    if (squareSize == 3)
                    {
                        square[0, 2] = matrix[col * squareSize + 0, row * squareSize + 2];

                        square[1, 2] = matrix[col * squareSize + 1, row * squareSize + 2];
                        
                        square[2, 0] = matrix[col * squareSize + 2, row * squareSize + 0];
                        square[2, 1] = matrix[col * squareSize + 2, row * squareSize + 1];
                        square[2, 2] = matrix[col * squareSize + 2, row * squareSize + 2];
                    }

                    var ruledSquare = ApplyRule(square);

                    // I Give up :(
                    
                }
            }

            return newMatrix;
        }

        private static char[,] ApplyRule(char[,] matrix)
        {
            foreach (var rule in Rules)
            {
                // 0
                if (IsThisTheRule(rule, matrix))
                {
                    return ConvertToMatrix(rule.rule);
                }

                // 90
                var rotatedMatrix = RotateMatrixCW(matrix);
                if (IsThisTheRule(rule, rotatedMatrix))
                {
                    return ConvertToMatrix(rule.rule);
                }

                // 180
                rotatedMatrix = RotateMatrixCW(rotatedMatrix);
                if (IsThisTheRule(rule, rotatedMatrix))
                {
                    return ConvertToMatrix(rule.rule);
                }

                // 270
                rotatedMatrix = RotateMatrixCW(rotatedMatrix);
                if (IsThisTheRule(rule, rotatedMatrix))
                {
                    return ConvertToMatrix(rule.rule);
                }
            }

            throw new Exception("No rules apply :(");
        }

        private static bool IsThisTheRule((string pattern, string rule) rule, char[,] matrix)
        {
            if (rule.pattern == ConvertMatrixToString(matrix))
            {
                return true;
            }

            var flippedMatrix = FlipMatrix(matrix);
            if (rule.pattern == ConvertMatrixToString(flippedMatrix))
            {
                return true;
            }

            return false;
        }

        private static string ConvertMatrixToString(char[,] matrix)
        {
            var result = string.Empty;

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    result += matrix[row, col];
                }

                if (row < matrix.GetLength(0) - 1)
                {
                    result += "/";
                }
            }

            return result;
        }

        private static char[,] ConvertToMatrix(string data)
        {
            var rows = data.Split("/");
            var matrix = new char[rows.Length, rows[0].Length];

            for (int row = 0; row < rows.Length; row++)
            {
                for (int col = 0; col < rows[row].Length; col++)
                {
                    matrix[row, col] = rows[row][col];
                }
            }

            return matrix;
        }

        private static char[,] RotateMatrixCW(char[,] matrix)
        {
            var newMatrix = new char[matrix.GetLength(0), matrix.GetLength(1)];

            // https://stackoverflow.com/a/35438327
            var size = matrix.GetLength(0);
            var layerCount = size / 2;

            for (int layer = 0; layer < layerCount; layer++)
            {
                var first = layer;
                var last = size - first - 1;

                for (int element = first; element < last; element++)
                {
                    var offset = element - first;

                    var top = matrix[first, element];
                    var right = matrix[element, last];
                    var bottom = matrix[last, last - offset];
                    var left = matrix[last - offset, first];

                    newMatrix[first, element] = left;
                    newMatrix[element, last] = top;
                    newMatrix[last, last - offset] = right;
                    newMatrix[last - offset, first] = bottom;
                }
            }

            // If size is uneven 3x3, 5x5 etc... We need to copy the center point manually
            if ((size % 2) != 0)
            {
                newMatrix[layerCount, layerCount] = matrix[layerCount, layerCount];
            }

            return newMatrix;
        }

        private static char[,] FlipMatrix(char[,] matrix)
        {
            var newMatrix = new char[matrix.GetLength(0), matrix.GetLength(1)];

            var size = matrix.GetLength(0);
            var layerCount = size / 2;

            for (int layer = 0; layer < layerCount; layer++)
            {
                var first = layer;
                var last = size - first - 1;

                for (int row = layer; row < matrix.GetLength(1); row++)
                {
                    var left = matrix[row, first];
                    var right = matrix[row, last];

                    newMatrix[row, first] = right;
                    newMatrix[row, last] = left;
                }
            }

            // If size is uneven 3x3, 5x5 etc... We need to copy the middle column manually
            if ((size % 2) != 0)
            {
                for (int row = 0; row < matrix.GetLength(1); row++)
                {
                    newMatrix[row, layerCount] = matrix[row, layerCount];
                }
            }

            return newMatrix;
        }
    }
}
