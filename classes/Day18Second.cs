using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Advent_of_Code_2017.classes
{
    public static class Day18Second
    {
        public static void GetResult()
        {
            var result = "";

            //var data = "snd 1\r\nsnd 2\r\nsnd p\r\nrcv a\r\nrcv b\r\nrcv c\r\nrcv d";

            var data = Helpers.GetDataFromFile("day18.txt");
            var stopWatch = Stopwatch.StartNew();
            result = GetResult(data).ToString();
            Helpers.DisplayDailyResult("18 - 2", result, stopWatch.ElapsedMilliseconds);
        }

        private static (Dictionary<char, long> programs, Queue<long> queue, int pos)[] threads;
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
                (new Dictionary<char, long>() { { 'p', 0 } }, new Queue<long>(), 0),
                (new Dictionary<char, long>() { { 'p', 1 } }, new Queue<long>(), 0),
            };

            var line = new string[3];
            var command = "";
            var register = '\0';
            var value = 0L;
            var shouldBreak = false;
            var currThreadId = 0;

            while (IsNotDone(0) || IsNotDone(1))
            {
                var thread = threads[currThreadId];

                line = commands.ElementAt(thread.pos);
                command = line[0].Trim();
                register = line[1].Trim().ToCharArray()[0];
                value = (line.Length > 2) ? GetTableValue(line[2].Trim(), currThreadId) : 0;

                var newPos = ExecuteCommand(currThreadId, command, register, value);

                threads[currThreadId] = (thread.programs, thread.queue, thread.pos + newPos);

                if (newPos == 0)
                {
                    if (shouldBreak)
                    {
                        break;
                    }

                    currThreadId = (1 - currThreadId);
                
                    shouldBreak = true;
                }
                else
                {
                    shouldBreak = false;
                }
            }

            return Result;
        }

        private static bool IsNotDone(int currThreadId)
        {
            return threads[currThreadId].pos >= 0 && threads[currThreadId].pos < commands.Count;
        }

        private static int ExecuteCommand(int currThreadId, string command, char register, long value)
        {
            var newValue = 0L;

            switch (command)
            {
                case "snd":
                    var valueToSend = GetValue(register, currThreadId);
                
                    threads[1 - currThreadId].queue.Enqueue(valueToSend);
                
                    Result += (currThreadId == 0) ? 0 : 1;
                
                    return 1;
                case "set":
                    SetValueInTablet(register, value, currThreadId);
                
                    return 1;
                case "add":
                    newValue = checked(GetValue(register, currThreadId) + value);
                
                    SetValueInTablet(register, newValue, currThreadId);
                
                    return 1;
                case "mul":
                    newValue = checked(GetValue(register, currThreadId) * value);
                
                    SetValueInTablet(register, newValue, currThreadId);
                
                    return 1;
                case "mod":
                    newValue = GetValue(register, currThreadId) % value;
                
                    SetValueInTablet(register, newValue, currThreadId);
                
                    return 1;
                case "rcv":
                    if (!threads[currThreadId].queue.Any())
                    {
                        return 0;
                    }
                
                    SetValueInTablet(register, threads[currThreadId].queue.Dequeue(), currThreadId);
                
                    return 1;
                case "jgz":
                    newValue += checked((GetValue(register, currThreadId) > 0) ? value : 1);
                
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
