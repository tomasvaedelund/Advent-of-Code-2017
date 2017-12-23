using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Advent_of_Code_2017.classes
{
    public static class Day23
    {
        public static void GetResult()
        {
            var result = "";
            var data = Helpers.GetDataFromFile("day23.txt");
            var stopWatch = Stopwatch.StartNew();
            result = GetResult(data).ToString();
            Helpers.DisplayDailyResult("23 - 1", result, stopWatch.ElapsedMilliseconds);
        }

        private static (Dictionary<char, long> programs, int pos)[] threads;
        private static List<string[]> commands = new List<string[]>();
        private static int Result = 0;
        private static int GetResult(string data)
        {
            commands = data.Split("\r\n").Select(x => x.Split(' ').ToArray()).ToList();

            // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/anonymous-types
            // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/arrays/implicitly-typed-arrays
            // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/how-to-initialize-a-dictionary-with-a-collection-initializer
            threads = new[]
            {
                (new Dictionary<char, long>()
                {
                    {'a', 0 },
                    {'b', 0 },
                    {'c', 0 },
                    {'d', 0 },
                    {'e', 0 },
                    {'f', 0 },
                    {'g', 0 },
                    {'h', 0 }
                }, 0)
            };

            var line = new string[3];
            var command = "";
            var register = '\0';
            var value = 0L;
            var currThreadId = 0;

            while (!IsDone(0))
            {
                var thread = threads[currThreadId];

                line = commands.ElementAt(thread.pos);
                command = line[0].Trim();
                register = line[1].Trim().ToCharArray()[0];
                value = (line.Length > 2) ? GetTableValue(line[2].Trim(), currThreadId) : 0;

                var newPos = ExecuteCommand(currThreadId, command, register, value);

                threads[currThreadId] = (thread.programs, thread.pos + newPos);
            }

            return Result;
        }

        private static bool IsDone(int currThreadId)
        {
            return threads[currThreadId].pos < 0 || threads[currThreadId].pos >= commands.Count;
        }

        private static int ExecuteCommand(int currThreadId, string command, char register, long value)
        {
            var newValue = 0L;

            switch (command)
            {
                case "set":
                    SetValueInTablet(register, value, currThreadId);

                    return 1;
                case "sub":
                    newValue = checked(GetValue(register, currThreadId) - value);

                    SetValueInTablet(register, newValue, currThreadId);

                    return 1;
                case "mul":
                    newValue = checked(GetValue(register, currThreadId) * value);

                    SetValueInTablet(register, newValue, currThreadId);

                    Result += 1;

                    return 1;
                case "jnz":
                    newValue += checked((GetValue(register, currThreadId) != 0) ? value : 1);

                    return (int)newValue;
                default:
                    throw new ArgumentException("Unknown command");
            }
        }

        private static long GetTableValue(string value, int currThreadId)
        {
            var result = 0;

            if (int.TryParse(value, out result))
            {
                return result;
            }

            return GetValue(value[0], currThreadId);
        }

        private static void SetValueInTablet(char register, long value, int currThreadId)
        {
            var tablet = threads[currThreadId].programs;

            if (tablet.ContainsKey(register))
            {
                tablet[register] = value;
            }
            else
            {
                tablet.Add(register, value);
            }
        }

        private static long GetRegisterValue(char register, int currThreadId)
        {
            var result = 0L;

            var table = threads[currThreadId].programs.TryGetValue(register, out result);

            return result;
        }

        private static long GetValue(char register, int currThreadId)
        {
            var result = 0;

            if (int.TryParse(register.ToString(), out result))
            {
                return result;
            }

            return GetRegisterValue(register, currThreadId);
        }
    }
}