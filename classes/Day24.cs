using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Advent_of_Code_2017.classes
{
    public static class Day24
    {
        public static void GetResult()
        {
            var testData = Helpers.GetDataFromFile("day24test.txt");
            var testResult = GetStrengthOfStrongestBridge(testData);
            Debug.Assert(testResult == 31);

            var data = Helpers.GetDataFromFile("day24.txt");
            var result = "";
            var stopWatch = Stopwatch.StartNew();
            result = GetStrengthOfStrongestBridge(data).ToString();
            Helpers.DisplayDailyResult("24 - 1", result, stopWatch.ElapsedMilliseconds);

            stopWatch = Stopwatch.StartNew();
            result = GetLongestAndStrongest(data).ToString();
            Helpers.DisplayDailyResult("24 - 2", result, stopWatch.ElapsedMilliseconds);
        }

        private static int GetStrengthOfStrongestBridge(string data)
        {
            var dataRows = data.Split("\r\n");
            var components = new List<Component>(dataRows.Select(c => new Component(c)));

            var maxStrength = components
                .Where(x => x.CanConnect(0))
                .Select(x => x.GetBridgeStrenght(components.Clone()))
                .Max();

            return maxStrength;
        }

        private static int GetLongestAndStrongest(string data)
        {
            var dataRows = data.Split("\r\n");
            var components = new List<Component>(dataRows.Select(c => new Component(c)));

            var maxStrength = components
                .Where(x => x.CanConnect(0))
                .Select(x => x.GetBridgeLengthStrenght(components.Clone()))
                .OrderBy(x => x.length)
                .ThenBy(x => x.strength)
                .LastOrDefault()
                .strength;

            return maxStrength;
        }


        public static List<T> Clone<T>(this IEnumerable<T> source)
        {
            return new List<T>(source);
        }
    }

    class Component
    {
        public int A { get; set; }
        public int B { get; set; }

        public Component(string ports)
        {
            A = Convert.ToInt32(ports.Split('/').Min());
            B = Convert.ToInt32(ports.Split('/').Max());
        }

        public bool CanConnect(int value)
        {
            return value == A || value == B;
        }

        public int Strength() => A + B;

        // Recursive Select
        public int GetBridgeStrenght(List<Component> available, int value = 0, int strength = 0)
        {
            available.Remove(this);
            strength += this.Strength();
            var next = (A == value) ? B : A;
            return available
                .Where(i => i.CanConnect(next))
                .Select(i => i.GetBridgeStrenght(available.Clone(), next, strength))
                .DefaultIfEmpty(strength)
                .Max();
        }

        public (int strength, int length) GetBridgeLengthStrenght(List<Component> available, int value = 0, int strength = 0, int length = 0)
        {
            available.Remove(this);
            strength += this.Strength();
            length += 1;
            var next = (A == value) ? B : A;
            return available
                .Where(i => i.CanConnect(next))
                .Select(i => i.GetBridgeLengthStrenght(available.Clone(), next, strength, length))
                .DefaultIfEmpty((strength, length))
                .OrderBy(x => x.length)
                .ThenBy(x => x.strength)
                .LastOrDefault();
        }
    }
}