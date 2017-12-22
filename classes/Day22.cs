using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Advent_of_Code_2017.classes
{
    public static class Day22
    {
        public static void GetResult()
        {
            var data = "..#\r\n#..\r\n...";
            Debug.Assert(GetNumberOfBurstsThatCauseInfection(7, data) == 5);
            Debug.Assert(GetNumberOfBurstsThatCauseInfection(70, data) == 41);
            Debug.Assert(GetNumberOfBurstsThatCauseInfection(10000, data) == 5587);

            data = Helpers.GetDataFromFile("day22.txt");
            var result = "";

            var stopWatch = Stopwatch.StartNew();
            result = GetNumberOfBurstsThatCauseInfection(10000, data).ToString();
            stopWatch.Stop();
            Helpers.DisplayDailyResult("22 - 1", result, stopWatch.ElapsedMilliseconds);

            data = "..#\r\n#..\r\n...";
            Debug.Assert(GetNumberOfBurstsThatCauseInfection(100, data, true) == 26);
            Debug.Assert(GetNumberOfBurstsThatCauseInfection(10000000, data, true) == 2511944);

            data = Helpers.GetDataFromFile("day22.txt");

            stopWatch = Stopwatch.StartNew();
            result = GetNumberOfBurstsThatCauseInfection(10000000, data, true).ToString();
            stopWatch.Stop();
            Helpers.DisplayDailyResult("22 - 2", result, stopWatch.ElapsedMilliseconds);
        }

        private static (int x, int y, int direction, int causedInfections) Carrier;
        private static IEnumerable<string> Grid;

        private static int GetNumberOfBurstsThatCauseInfection(int bursts, string data, bool second = false)
        {
            Grid = ToGrid(data);
            var startPos = Grid.GetStartPos();

            Carrier = (startPos.x, startPos.y, 0, 0);

            while (bursts-- > 0)
            {
                PerformBurst(second);
            }

            return Carrier.causedInfections;
        }

        private static void PerformBurst(bool second)
        {
            if (second)
            {
                UpdateAndTurnSecond();
            }
            else
            {
                UpdateAndTurn();
            }

            if (Carrier.direction < 0)
            {
                Carrier.direction += 360;
            }

            switch (Carrier.direction % 360)
            {
                case 0:
                    Carrier.y -= 1;
                    break;
                case 90:
                    Carrier.x += 1;
                    break;
                case 180:
                    Carrier.y += 1;
                    break;
                case 270:
                    Carrier.x -= 1;
                    break;
                default:
                    throw new Exception("Unknown direction");
            }

            if (Grid.IsOutOfBounds())
            {
                Grid = Grid.Expand();
            }
        }

        private static void UpdateAndTurn()
        {
            if (Grid.IsCurrentPosInfected())
            {
                Carrier.direction += 90;
                Grid = Grid.CleanCurrentPos();
            }
            else
            {
                Carrier.direction -= 90;
                Grid = Grid.InfectCurrentPos();
            }
        }

        private static void UpdateAndTurnSecond()
        {
            var currPos = Grid.GetValueOfCurrentPos();

            switch (currPos)
            {
                case '.':
                    Grid = Grid.UpdateCurrentPos('W');
                    Carrier.direction -= 90;
                    break;
                case '#':
                    Grid = Grid.UpdateCurrentPos('F');
                    Carrier.direction += 90;
                    break;
                case 'W':
                    Grid = Grid.InfectCurrentPos();
                    // Don't turn
                    break;
                case 'F':
                    Grid = Grid.CleanCurrentPos();
                    Carrier.direction += 180;
                    break;
                default:
                    throw new Exception($"Unknown value: {currPos}");
            }
        }

        private static bool IsOutOfBounds(this IEnumerable<string> grid)
        {
            return Carrier.y < 0 || Carrier.y >= grid.Count() || Carrier.x < 0 || Carrier.x >= grid.Count();
        }

        private static IEnumerable<string> ToGrid(string data)
        {
            return data.Split("\r\n").ToList();
        }

        private static (int x, int y) GetStartPos(this IEnumerable<string> grid)
        {
            return (grid.Count() / 2, grid.Count() / 2);
        }

        private static char GetValueOfCurrentPos(this IEnumerable<string> grid)
        {
            return grid.ElementAt(Carrier.y)[Carrier.x];
        }

        private static IEnumerable<string> UpdateCurrentPos(this IEnumerable<string> grid, char value)
        {
            var list = grid.ToList();
            var row = list[Carrier.y].ToCharArray();

            row[Carrier.x] = value;

            list[Carrier.y] = new string(row);

            return list;
        }

        private static IEnumerable<string> InfectCurrentPos(this IEnumerable<string> grid)
        {
            Carrier.causedInfections += 1;

            return grid.UpdateCurrentPos('#');
        }

        private static IEnumerable<string> CleanCurrentPos(this IEnumerable<string> grid)
        {
            return grid.UpdateCurrentPos('.');
        }

        private static bool IsCurrentPosInfected(this IEnumerable<string> grid)
        {
            return grid.GetValueOfCurrentPos() == '#';
        }

        private static IEnumerable<string> Expand(this IEnumerable<string> grid)
        {
            var list = grid.ToList();

            for (int i = 0; i < grid.Count(); i++)
            {
                list[i] = $".{list[i]}.";
            }

            list.Insert(0, new string('.', list.Count + 2));
            list.Add(new string('.', list.Count + 1));

            // Since we add a row and a column before current position, current position must be increased
            Carrier.y += 1;
            Carrier.x += 1;

            return list;
        }
    }
}