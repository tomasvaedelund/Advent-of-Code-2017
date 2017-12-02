using System;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2017.classes
{
    public static class Helpers
    {
        public static int[] stringToIntArray(string data)
        {
            return data.Select(c => (int)(c - '0')).ToArray();
        }

        public static string getDataFromFile(string fileName)
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
    }
}