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
            Helpers.DisplayDailyResult("23 - 1", result, stopWatch.ElapsedMilliseconds);
        }

        private static (Dictionary<char, long> programs, int pos) Thread;
        private static List<string[]> Commands = new List<string[]>();
        private static long GetResult(string data)
        {
            Commands = data.Split("\r\n").Select(x => x.Split(' ').ToArray()).ToList();

            Thread = (new Dictionary<char, long>()
                {
                    {'a', 1 },
                    {'b', 0 },
                    {'c', 0 },
                    {'d', 0 },
                    {'e', 0 },
                    {'f', 0 },
                    {'g', 0 },
                    {'h', 0 }
                }, 0);

            var line = new string[3];
            var command = "";
            var register = '\0';
            var value = 0L;

            while (!IsDone(0))
            {
                line = Commands.ElementAt(Thread.pos);
                command = line[0].Trim();
                register = line[1].Trim().ToCharArray()[0];
                value = (line.Length > 2) ? GetTableValue(line[2].Trim()) : 0;

                var newPos = ExecuteCommand(command, register, value);

                Thread = (Thread.programs, Thread.pos + newPos);
            }

            return GetRegisterValue('h');
        }

        private static bool IsDone(int currThreadId)
        {
            return Thread.pos < 0 || Thread.pos >= Commands.Count;
        }

        private static int ExecuteCommand(string command, char register, long value)
        {
            var newValue = 0L;

            switch (command)
            {
                case "set":
                    SetValueInTablet(register, value);

                    return 1;
                case "sub":
                    newValue = checked(GetValue(register) - value);

                    SetValueInTablet(register, newValue);

                    return 1;
                case "mul":
                    newValue = checked(GetValue(register) * value);

                    SetValueInTablet(register, newValue);

                    return 1;
                case "jnz":
                    newValue += checked((GetValue(register) != 0) ? value : 1);

                    return (int)newValue;
                default:
                    throw new ArgumentException("Unknown command");
            }
        }

        private static long GetTableValue(string value)
        {
            var result = 0;

            if (int.TryParse(value, out result))
            {
                return result;
            }

            return GetValue(value[0]);
        }

        private static void SetValueInTablet(char register, long value)
        {
            var tablet = Thread.programs;

            if (tablet.ContainsKey(register))
            {
                tablet[register] = value;
            }
            else
            {
                tablet.Add(register, value);
            }
        }

        private static long GetRegisterValue(char register)
        {
            var result = 0L;

            var table = Thread.programs.TryGetValue(register, out result);

            return result;
        }

        private static long GetValue(char register)
        {
            var result = 0;

            if (int.TryParse(register.ToString(), out result))
            {
                return result;
            }

            return GetRegisterValue(register);
        }
    }
}