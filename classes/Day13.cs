using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Advent_of_Code_2017.classes
{
    public class Day13
    {
        public static int GetResult(out long timeElapsed)
        {
            var data = Helpers.getDataFromFile("day13-test.txt");
            Debug.Assert(GetResult(data) == 24);

            var result = 0;
            data = Helpers.getDataFromFile("day13.txt");
            var stopWatch = Stopwatch.StartNew();
            result = GetResult(data);
            timeElapsed = stopWatch.ElapsedMilliseconds;

            return result;
        }

        public static int GetResultTwo(out long timeElapsed)
        {
            var data = Helpers.getDataFromFile("day13-test.txt");
            Debug.Assert(GetResultTwo(data) == 10);

            var result = 0;
            data = Helpers.getDataFromFile("day13.txt");
            var stopWatch = Stopwatch.StartNew();
            result = GetResultTwo(data);
            timeElapsed = stopWatch.ElapsedMilliseconds;

            return result;
        }

        private static int GetResult(string data)
        {
            var firewall = InitFirewall(data);

            var result = GetSeverity(firewall);

            return result;
        }

        private static int GetResultTwo(string data)
        {
            var result = GetOptimalDelay(data);

            return result;
        }

        private static List<int> InitFirewall(string data)
        {
            var layers = data.Split("\r\n");
            var firewall = new List<int>();

            var layerCount = 0;
            foreach (var layer in layers)
            {
                var depth = Convert.ToInt32(layer.Split(':')[0]);
                var range = Convert.ToInt32(layer.Split(':')[1].Trim());

                while (layerCount < depth)
                {
                    firewall.Add(0);
                    layerCount++;
                }

                firewall.Add(range);
                layerCount++;
            }

            return firewall;
        }

        private static int GetSeverity(List<int> firewall, bool second = false)
        {
            var severity = 0;

            for (int i = 0; i < firewall.Count; i++)
            {
                if (firewall[i] > 0 && i % (firewall[i] * 2 - 2) == 0)
                {
                    severity += i * firewall[i];
                }
            }

            return severity;
        }

        private static int GetOptimalDelay(string data)
        {
            var delay = 0;
            var firewall = InitFirewall(data);

            while (true)
            {
                var caught = false;

                for (int i = 0; i < firewall.Count; i++)
                {
                    if (firewall[i] > 0 && (i + delay) % (firewall[i] * 2 - 2) == 0)
                    {
                        caught = true;
                        break;
                    }
                }

                if (!caught)
                {
                    break;
                }

                delay++;
            }

            return delay;
        }

    }
}