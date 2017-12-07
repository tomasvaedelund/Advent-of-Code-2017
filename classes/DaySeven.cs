using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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

        List<Program> Programs = new List<Program>();
        private static int getMissingWeight(string data)
        {
            var nameOfBottomProgram = GetNameOfBottomProgram(data);
            var dataRows = data.Split("\r\n");
            var root = dataRows.Single(x => x.StartsWith(nameOfBottomProgram));
            var rootProgram = CastToProgram(root);

            rootProgram.Children = GetChildren(rootProgram, dataRows);

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
                currentProgram.Children = GetChildren(currentProgram, dataRows);

                children.Add(currentProgram);
            }
            return children;
        }

        private static Program CastToProgram(string program)
        {
            var rowArray = program.Split("->");
            var name = rowArray[0].Split(' ')[0].Trim();
            var childrenNames = new List<string>();

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
                Children = new List<Program>()
            };
        }

        private class Program
        {
            public string Name { get; set; }
            public Program Parent { get; set; }
            public List<Program> Children { get; set; }
            public List<string> ChildrenNames { get; set; }
            public int Weight { get; set; }
        }
    }
}