using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Advent_of_Code_2017.classes
{
    public static class Day25
    {
        public static void GetResult()
        {
            //var data = Helpers.GetDataFromFile("day25.txt");
            var result = "";
            var stopWatch = Stopwatch.StartNew();
            result = GetDiagnosticChecksum().ToString();
            Helpers.DisplayDailyResult("25 - 1", result, stopWatch.ElapsedMilliseconds);

            // stopWatch = Stopwatch.StartNew();
            // result = GetDiagnosticChecksum().ToString();
            // Helpers.DisplayDailyResult("25 - 2", result, stopWatch.ElapsedMilliseconds);
        }

        private static int GetDiagnosticChecksum()
        {
            var size = 15000; // Just guessing size here... (Did not work with 10 000!)
            var tape = Enumerable.Repeat(0, size).ToList(); // Create list with zeroes 
            var pos = size / 2; // Start in middle
            var state = 'A';

            for (int i = 0; i < 12425180; i++)
            {
                switch (state)
                {
                    case 'A':
                        if (tape[pos] == 0)
                        {
                            tape[pos] = 1;
                            pos += 1;
                            state = 'B';
                            break;
                        }
                        tape[pos] = 0;
                        pos += 1;
                        state = 'F';
                        break;  
                    case 'B':
                        if (tape[pos] == 0)
                        {
                            tape[pos] = 0;
                            pos -= 1;
                            state = 'B';
                            break;
                        }
                        tape[pos] = 1;
                        pos -= 1;
                        state = 'C';
                        break;  
                    case 'C':
                        if (tape[pos] == 0)
                        {
                            tape[pos] = 1;
                            pos -= 1;
                            state = 'D';
                            break;
                        }
                        tape[pos] = 0;
                        pos += 1;
                        state = 'C';
                        break;  
                    case 'D':
                        if (tape[pos] == 0)
                        {
                            tape[pos] = 1;
                            pos -= 1;
                            state = 'E';
                            break;
                        }
                        tape[pos] = 1;
                        pos += 1;
                        state = 'A';
                        break;  
                    case 'E':
                        if (tape[pos] == 0)
                        {
                            tape[pos] = 1;
                            pos -= 1;
                            state = 'F';
                            break;
                        }
                        tape[pos] = 0;
                        pos -= 1;
                        state = 'D';
                        break;  
                    case 'F':
                        if (tape[pos] == 0)
                        {
                            tape[pos] = 1;
                            pos += 1;
                            state = 'A';
                            break;
                        }
                        tape[pos] = 0;
                        pos -= 1;
                        state = 'E';
                        break;  
                    default:
                        throw new ArgumentException($"Unknown state: {state}");
                }
            }

            return tape.Count(c => c == 1);
        }
    }
}