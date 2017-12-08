using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2017.classes
{
    public class DaySeven
    {
        public static string GetResult(out long timeElapsed)
        {
            var testData = Helpers.getDataFromFile("dayseven-test.txt");
            Debug.Assert(GetNameOfBottomProgram(testData) == "tknk");

            var result = "";
            var data = Helpers.getDataFromFile("dayseven.txt");
            var stopWatch = Stopwatch.StartNew();
            result = GetNameOfBottomProgram(data);
            timeElapsed = stopWatch.ElapsedMilliseconds;

            return result;
        }

        public static int GetResultTwo(out long timeElapsed)
        {
            var result = 0;
            var data = Helpers.getDataFromFile("dayseven.txt");

            Debug.Assert(GetNameOfBottomProgram(data) == "dtacyn");

            var stopWatch = Stopwatch.StartNew();
            result = getMissingWeight(data);
            timeElapsed = stopWatch.ElapsedMilliseconds;

            return result;
        }

        private static string GetNameOfBottomProgram(string data)
        {
            var programs = new List<string>();
            var dataRows = data.Split("\r\n");
            var nameOfBottomProgram = "";

            foreach (var row in dataRows)
            {
                var rowArray = row.Split("->");
                var parent = rowArray[0].Split(' ')[0].Trim();
                programs.Add(parent);

                if (rowArray.Length > 1)
                {
                    var children = rowArray[1].Split(',');
                    foreach (var child in children)
                    {
                        programs.Add(child.Trim());
                    }
                }
            }

            nameOfBottomProgram = programs
            .GroupBy(x => x)
            .Select(x => new
            {
                key = x.Key,
                cnt = x.Count()
            })
            .OrderBy(x => x.cnt)
            .First()
            .key;

            return nameOfBottomProgram;
        }

        private static int getMissingWeight(string data)
        {
            var nameOfBottomProgram = GetNameOfBottomProgram(data);
            var dataRows = data.Split("\r\n");
            var root = dataRows.Single(x => x.StartsWith(nameOfBottomProgram));
            var rootProgram = CastToProgram(root);

            rootProgram.Children = GetChildren(rootProgram, dataRows);

            var result = GetAdjustedValue(rootProgram);

            return 0;
        }

        private static int GetAdjustedValue(Program program)
        {
            // var sum = program
            //     .Children
            //     .First()
            //     .ChildrensWeight;

            // var num = program
            //     .Children
            //     .Count(x => x.ChildrensWeight == sum);

            // if (num == program.Children.Count)
            // {
            //     return 0;
            // }

            // var child = program
            // .Children
            // .GroupBy(x => x.ChildrensWeight)
            // .Select(x => new
            // {
            //     key = x.Key,
            //     cnt = x.Count()
            // })
            // .OrderBy(x => x.cnt)
            // .Single();

            return 0;

        }

        private static List<Program> GetChildren(Program parent, string[] dataRows)
        {
            var children = new List<Program>();

            foreach (var child in parent.ChildrenNames)
            {
                var current = dataRows.Single(x => x.StartsWith(child));
                var currentProgram = CastToProgram(current);

                currentProgram.Parent = parent;
                currentProgram.Level = parent.Level + 1;
                currentProgram.Children = GetChildren(currentProgram, dataRows);

                currentProgram.ChildrensWeight = currentProgram.Weight;

                currentProgram.ChildrensWeight += currentProgram
                    .Children
                    .Sum(x => x.ChildrensWeight);

                children.Add(currentProgram);
            }

            return children;
        }

        private static int GetWeight(string program)
        {
            var weight = Regex.Match(program, @"\(([^)]*)\)").Groups[1].Value;

            return Convert.ToInt32(weight);
        }

        private static Program CastToProgram(string program)
        {
            var rowArray = program.Split("->");
            var name = rowArray[0].Split(' ')[0].Trim();
            var childrenNames = new List<string>();
            var weight = GetWeight(rowArray[0]);

            if (rowArray.Length > 1)
            {
                var children = rowArray[1].Split(',');
                foreach (var child in children)
                {
                    childrenNames.Add(child.Trim());
                }
            }

            return new Program()
            {
                Name = name,
                ChildrenNames = childrenNames,
                Children = new List<Program>(),
                Weight = weight
            };
        }

        private class Program
        {
            public string Name { get; set; }
            public Program Parent { get; set; }
            public List<Program> Children { get; set; }
            public List<string> ChildrenNames { get; set; }
            public int Weight { get; set; }
            public int Level { get; set; }
            public int ChildrensWeight { get; set; }
        }
    }
}