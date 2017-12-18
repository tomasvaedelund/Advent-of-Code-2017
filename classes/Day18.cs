using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Advent_of_Code_2017.classes
{
    public static class Day18
    {
        public static void GetResult()
        {
            var data = Helpers.GetDataFromFile("day18-test.txt");
            Debug.Assert(GetValueOfRecoveredFrequency(data) == 4);

            var result = "";

            data = Helpers.GetDataFromFile("day18.txt");
            lastPlayedSoundSaved = 0;
            var stopWatch = Stopwatch.StartNew();
            result = GetValueOfRecoveredFrequency(data).ToString();
            Helpers.DisplayDailyResult("18 - 1", result, stopWatch.ElapsedMilliseconds);

            stopWatch = Stopwatch.StartNew();
            //result = GetValueAfterLastInsertTwo(363);
            Helpers.DisplayDailyResult("18 - 2", result, stopWatch.ElapsedMilliseconds);
        }

        private static long GetValueOfRecoveredFrequency(string data)
        {
            var commands = data.Split("\r\n").Select(x => x.Split(' '));
            var tablet = new Dictionary<char, long>();

            for (int i = 0; i < commands.Count(); i++)
            {
                var line = commands.ElementAt(i);
                var command = line[0].Trim();
                var register = line[1].Trim().ToCharArray()[0];
                var value = (line.Length > 2) ? GetValueOfCommand(line[2].Trim(), tablet) : 0;

                tablet = ExecuteCommand(command, register, value, tablet, i, out i);

                if (lastPlayedSoundSaved > 0)
                {
                    break;
                }
            }

            return lastPlayedSoundSaved;
        }

        private static long lastPlayedSound = 0;
        private static long lastPlayedSoundSaved = 0;
        private static Dictionary<char, long> ExecuteCommand(string command, char register, long value, Dictionary<char, long> tablet, int i, out int newPos)
        {
            var newValue = 0L;
            newPos = i;

            switch (command)
            {
                case "snd":
                    lastPlayedSound = GetTabletValue(tablet, register);
                    break;
                case "set":
                    tablet = SetValueInTablet(register, value, tablet);
                    break;
                case "add":
                    newValue = GetTabletValue(tablet, register) + value;
                    tablet = SetValueInTablet(register, newValue, tablet);
                    break;
                case "mul":
                    newValue = checked(GetTabletValue(tablet, register) * value);
                    tablet = SetValueInTablet(register, newValue, tablet);
                    break;
                case "mod":
                    newValue = GetTabletValue(tablet, register) % value;
                    tablet = SetValueInTablet(register, newValue, tablet);
                    break;
                case "rcv":
                    lastPlayedSoundSaved = (GetTabletValue(tablet, register) == 0) ? 0 : lastPlayedSound;
                    break;
                case "jgz":
                    // Value - 1 since we do a i++ in the loop
                    newPos += (int)checked((GetTabletValue(tablet, register) == 0) ? 0 : value - 1);
                    break;
                default:
                    throw new ArgumentException("Unknown command");
            }

            return tablet;
        }

        private static Dictionary<char, long> SetValueInTablet(char register, long value, Dictionary<char, long> tablet)
        {
            if (tablet.ContainsKey(register))
            {
                tablet[register] = value;
            }
            else
            {
                tablet.Add(register, value);
            }

            return tablet;
        }

        private static long GetValueOfCommand(string value, Dictionary<char, long> tablet)
        {
            var result = 0L;
            if (long.TryParse(value, out result))
            {
                return result;
            }

            if (tablet.TryGetValue(value.ToCharArray()[0], out result))
            {
                return result;
            }

            throw new ArgumentException("Cuuld not find a value");
        }

        private static long GetTabletValue(Dictionary<char, long> tablet, char register)
        {
            if (tablet.ContainsKey(register))
            {
                return tablet[register];
            }

            return 0L;
        }
    }
}