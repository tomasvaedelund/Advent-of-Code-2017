using System;
using System.Diagnostics;

namespace Advent_of_Code_2017.classes
{
    public static class Day19
    {
        public static void GetResult()
        {
            var data = Helpers.GetDataFromFile("day19-test.txt");
            Debug.Assert(GetLettersFoundInMaze(data) == "ABCDEF");
            Debug.Assert(StepsTaken == 38);

            var result = "";

            data = Helpers.GetDataFromFile("day19.txt");
            FoundLetters = "";
            StepsTaken = 0;

            var stopWatch = Stopwatch.StartNew();
            result = GetLettersFoundInMaze(data);
            Helpers.DisplayDailyResult("19 - 1", result, stopWatch.ElapsedMilliseconds);

            stopWatch = Stopwatch.StartNew();
            Helpers.DisplayDailyResult("19 - 2", StepsTaken.ToString(), stopWatch.ElapsedMilliseconds);
        }

        private static char[,] Diagram;
        private static string FoundLetters = "";
        private static int StepsTaken = 0;

        private static char[,] GenerateDiagram(string data)
        {
            var lines = data.Split("\r\n");
            var columns = lines[0].Length;

            var diagram = new char[lines.Length, columns];

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    diagram[y, x] = lines[y][x];
                }
            }

            return diagram;
        }

        private static int FindStartPosition()
        {
            for (int x = 0; x < Diagram.GetLength(1); x++)
            {
                if (Diagram[0, x] != ' ')
                {
                    return x;
                }
            }

            throw new Exception("No startpoint was found");
        }

        public static string GetLettersFoundInMaze(string data)
        {
            Diagram = GenerateDiagram(data);
            var startX = FindStartPosition();

            Move(startX);

            return FoundLetters;
        }

        private static void Move(int x)
        {
            Move(0, x, 180);
        }

        private static void Move(int y, int x, int direction)
        {
            var currentChar = Diagram[y, x];

            while (currentChar != '+')
            {
                if (currentChar != '-' && currentChar != '|' && currentChar != ' ')
                {
                    FoundLetters += currentChar;
                }

                switch (direction)
                {
                    // North
                    case 0:
                        y -= 1;
                        break;
                    // East
                    case 90:
                        x += 1;
                        break;
                    // South
                    case 180:
                        y += 1;
                        break;
                    // West
                    case 270:
                        x -= 1;
                        break;
                    default:
                        throw new Exception("Unknown direction");
                }

                if (IsOutOfBounds(y, x) || currentChar == ' ')
                {
                    return;
                }

                currentChar = Diagram[y, x];
                StepsTaken++;
            }

            var newDirection = GetNewDirection(y, x, direction);

            y = (newDirection == 0) ? y - 1 : y;
            y = (newDirection == 180) ? y + 1 : y;
            x = (newDirection == 90) ? x + 1 : x;
            x = (newDirection == 270) ? x - 1 : x;

            StepsTaken++;
            Move(y, x, newDirection);
        }

        private static bool IsOutOfBounds(int y, int x)
        {
            if (y < 0 || y >= Diagram.GetLength(0))
            {
                return true;
            }

            if (x < 0 || x >= Diagram.GetLength(1))
            {
                return true;
            }

            return false;
        }

        private static int GetNewDirection(int y, int x, int direction)
        {
            switch (direction)
            {
                case 0:
                case 180:
                    if (Diagram[y, x - 1] == '|' || Diagram[y, x - 1] == ' ' || Diagram[y, x - 1] == '+')
                    {
                        return 90;
                    }
                    return 270;
                case 90:
                case 270:
                    if (Diagram[y - 1, x] == '-' || Diagram[y - 1, x] == ' ' || Diagram[y - 1, x] == '+')
                    {
                        return 180;
                    }
                    return 0;
                default:
                    throw new Exception("Unknown direction");
            }
        }
    }
}