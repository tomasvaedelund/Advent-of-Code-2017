using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Advent_of_Code_2017.classes
{
    public static class Day8
    {
        public static string GetResult(out long timeElapsed)
        {
            DecodeInstruction("b inc 5 if a > 1");
            Debug.Assert(Instructions["b"] == 0);

            DecodeInstruction("a inc 1 if b < 5");
            Debug.Assert(Instructions["a"] == 1);

            DecodeInstruction("c dec -10 if a >= 1");
            Debug.Assert(Instructions["c"] == 10);

            DecodeInstruction("c inc -20 if c == 10");
            Debug.Assert(Instructions["c"] == -10);

            // Reset after testst
            Instructions = new Dictionary<string, int>();

            var result = "";
            var data = Helpers.GetDataFromFile("day8.txt");
            var stopWatch = Stopwatch.StartNew();
            result = DecodeAllInstructions(data).ToString();
            timeElapsed = stopWatch.ElapsedMilliseconds;

            return result;
        }

        public static string GetResultTwo(out long timeElapsed)
        {
            //Debug.Assert(GetNameOfBottomProgram(testData) == "tknk");

            var result = "";
            var data = Helpers.GetDataFromFile("dayeight.txt");
            var stopWatch = Stopwatch.StartNew();
            result = DecodeAllInstructionsTwo().ToString();
            timeElapsed = stopWatch.ElapsedMilliseconds;

            return result;
        }

        private static int DecodeAllInstructionsTwo()
        {
            return MaxValue;
        }

        private static Dictionary<string, int> Instructions = new Dictionary<string, int>();
        
        private static int DecodeAllInstructions(string data)
        {
            var result = Int32.MinValue;
            var instructions = data.Split("\r\n");

            foreach (var instruction in instructions)
            {
                DecodeInstruction(instruction);
            }

            foreach (var item in Instructions)
            {
                result = (item.Value > result) ? item.Value : result;
            }

            return result;
        }

        private static int MaxValue = Int32.MinValue;
        private static void DecodeInstruction(string instruction)
        {
            var insArray = instruction.Split(' ');
            var target = insArray[0];
            var command = insArray[1];
            var value = Convert.ToInt32(insArray[2]);
            var source = insArray[4];
            var comparer = insArray[5];
            var condition = Convert.ToInt32(insArray[6]);

            if (!Instructions.ContainsKey(target))
            {
                Instructions.Add(target, 0);
            }

            if (!Instructions.ContainsKey(source))
            {
                Instructions.Add(source, 0);
            }
            
            if (IsInstructionValid(source, comparer, condition))
            {
                switch (command)
                {
                    case "inc":
                        Instructions[target] += value;
                        break;
                    case "dec":
                        Instructions[target] -= value;
                        break;
                    default:
                        throw new Exception("No command to match command");
                }
            }

            var maxValue = Instructions.GetValueOrDefault(target);
            MaxValue = (maxValue > MaxValue) ? maxValue : MaxValue;
        }

        private static bool IsInstructionValid(string source, string comparer, int condition)
        {
            var sourceValue = Instructions.GetValueOrDefault(source);

            switch (comparer)
            {
                case "<":
                    return sourceValue < condition;
                case "<=":
                    return sourceValue <= condition;
                case ">":
                    return sourceValue > condition;
                case ">=":
                    return sourceValue >= condition;
                case "!=":
                    return sourceValue != condition;
                case "==":
                    return sourceValue == condition;
                default:
                    throw new Exception("No comparer to match this comparer");
            }
        }
    }
}