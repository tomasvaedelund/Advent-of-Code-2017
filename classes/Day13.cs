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

        private static int GetResult(string data)
        {
            var firewall = InitFirewall(data);

            var result = GetSeverity(firewall);

            return result;
        }

        private static List<List<char>> InitFirewall(string data)
        {
            var layers = data.Split("\r\n");
            var firewall = new List<List<char>>();

            var layerCount = 0;
            foreach (var layer in layers)
            {
                var layerNum = Convert.ToInt32(layer.Split(':')[0]);
                var depth = Convert.ToInt32(layer.Split(':')[1].Trim());

                while (layerCount < layerNum)
                {
                    firewall.Add(InitLayer(0));
                    layerCount++;
                }

                firewall.Add(InitLayer(depth));
                layerCount++;
            }

            return firewall;
        }

        private static List<char> InitLayer(int depth)
        {
            var layer = new List<char>(depth);

            if (depth > 0)
            {
                layer.Add('S');

                for (int i = 1; i < depth; i++)
                {
                    layer.Add('\0');
                }
            }

            return layer;
        }

        private static int GetSeverity(List<List<char>> firewall)
        {
            var severity = 0;

            for (int packetPos = 0; packetPos < firewall.Count; packetPos++)
            {
                if (firewall[packetPos].Any() && (firewall[packetPos][0] == 'S' || firewall[packetPos][0] == 'T'))
                {
                    severity += packetPos * firewall[packetPos].Count;
                }

                firewall = MoveScanner(firewall);
            }

            return severity;
        }

        private static List<List<char>> MoveScanner(List<List<char>> firewall)
        {
            for (int j = 0; j < firewall.Count; j++)
            {
                var layer = firewall[j];

                for (int i = 0; i < layer.Count; i++)
                {
                    // At bottom, turn
                    if (i == layer.Count - 1 && layer[i] == 'S')
                    {
                        layer[i] = '\0';
                        layer[i - 1] = 'T';
                        break;
                    }

                    // At top, turn
                    if (i == 0 && layer[i] == 'T')
                    {
                        layer[i] = '\0';
                        layer[i + 1] = 'S';
                        break;
                    }

                    // Not at top or bottom, just move
                    if (layer[i] == 'S')
                    {
                        layer[i] = '\0';
                        layer[i + 1] = 'S';
                        break;
                    }
                    else if (layer[i] == 'T')
                    {
                        layer[i] = '\0';
                        layer[i - 1] = 'T';
                        break;
                    }
                }
            }

            return firewall;
        }
    }
}