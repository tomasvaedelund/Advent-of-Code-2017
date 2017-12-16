using System;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2017.classes
{
    public static class Helpers
    {
        public static int[] StringToIntArray(string data)
        {
            return data.Select(c => (int)(c - '0')).ToArray();
        }

        public static string GetDataFromFile(string fileName)
        {
            var contents = "";
            // var path = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            // var directory = System.IO.Path.GetDirectoryName(path);
            var directory = System.IO.Directory.GetCurrentDirectory();
            var fullPathToFile = $"{directory}\\data\\{fileName}";

            if (!File.Exists(fullPathToFile))
            {
                throw new FileNotFoundException($"Cannot find {fileName}");
            }

            contents = File.ReadAllText(fullPathToFile);

            return contents;
        }

        public static void DisplayDailyResult(int day, string first, string second, long timeFirst, long timeSecond)
        {
            Console.WriteLine($"Day {day} - first result: {first}, in {timeFirst}ms");
            Console.WriteLine($"Day {day} - second result: {second}, in {timeSecond}ms");
        }
    }
}