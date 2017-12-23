using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Advent_of_Code_2017.classes
{
    public static class Day23Second
    {
        public static void GetResult()
        {
            var result = "";
            var data = Helpers.GetDataFromFile("day23.txt");
            var stopWatch = Stopwatch.StartNew();
            result = GetResult(data).ToString();
            Helpers.DisplayDailyResult("23 - 2", result, stopWatch.ElapsedMilliseconds);
        }

        private static Dictionary<string, long> Thread;
        private static int Pos;
        private static List<string[]> Commands = new List<string[]>();
        private static long GetResult(string data)
        {
            Commands = data.Split("\r\n").Select(x => x.Split(' ').ToArray()).ToList();

            Thread = new Dictionary<string, long>()
                {
                    {"a", 1 },
                    {"b", 0 },
                    {"c", 0 },
                    {"d", 0 },
                    {"e", 0 },
                    {"f", 0 },
                    {"g", 0 },
                    {"h", 0 }
                };

            Pos = 0;

            while (!IsDone(0))
            {
                var line = Commands.ElementAt(Pos);
                var command = line[0];
                var register = line[1];
                var value = (line.Length > 2) ? line[2] : string.Empty;

                Pos += ExecuteCommand(command, register, value);

                Console.WriteLine(Thread.ToDebugString() + "Pos: " + Pos);
            }

            return GetRegisterValue("h");
        }

        private static bool IsDone(int currThreadId)
        {
            return Pos < 0 || Pos >= Commands.Count;
        }

        private static int ExecuteCommand(string command, string register, string value)
        {
            switch (command)
            {
                case "set":
                    Thread[register] = GetValue(value);
                    return 1;
                case "sub":
                    Thread[register] -= GetValue(value); ;
                    return 1;
                case "mul":
                    Thread[register] *= GetValue(value);
                    return 1;
                case "jnz":
                    return (GetValue(register) != 0) ? (int)GetValue(value) : 1;
                default:
                    throw new ArgumentException("Unknown command");
            }
        }

        private static long GetRegisterValue(string register)
        {
            var result = 0L;

            Thread.TryGetValue(register, out result);

            return result;
        }

        private static long GetValue(string register)
        {
            var result = 0;

            if (int.TryParse(register.ToString(), out result))
            {
                return result;
            }

            return GetRegisterValue(register);
        }

        public static string ToDebugString<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return "{" + string.Join(",", dictionary.Select(kv => kv.Key + "=" + kv.Value).ToArray()) + "}";
        }
    }
}