using System;
using System.Diagnostics;
using System.Linq;

namespace Advent_of_Code_2017.classes
{
    public static class Day16
    {
        public static string GetResult(out long timeElapsed)
        {
            Debug.Assert(GetNewOrder("abcde", "s3") == "cdeab");

            Debug.Assert(GetNewOrder("abcde", "s1") == "eabcd");
            Debug.Assert(GetNewOrder("eabcd", "x3/4") == "eabdc");
            Debug.Assert(GetNewOrder("eabdc", "pe/b") == "baedc");

            var result = "";
            var data = Helpers.getDataFromFile("day16.txt");
            var stopWatch = Stopwatch.StartNew();
            result = GetNewOrderFromASetOfMoves("abcdefghijklmnop", data);
            timeElapsed = stopWatch.ElapsedMilliseconds;

            return result;
        }

        public static string GetResultTwo(out long timeElapsed)
        {
            Debug.Assert(GetNewOrder("baedc", "s1") == "cbaed");
            Debug.Assert(GetNewOrder("cbaed", "x3/4") == "cbade");
            Debug.Assert(GetNewOrder("cbade", "pe/b") == "ceadb");

            var result = "";
            var data = Helpers.getDataFromFile("day16.txt");
            var stopWatch = Stopwatch.StartNew();
            //result = GetNewOrderFromASetOfMoves("namdgkbhifpceloj", data, 1000000000);
            // Pattern repeats every 60 iterations, 1000000000 % 60 = 40, and we dont't want the last one so 39...
            result = GetNewOrderFromASetOfMoves("namdgkbhifpceloj", data, 39);
            timeElapsed = stopWatch.ElapsedMilliseconds;

            return result;
        }

        private static string GetNewOrderFromASetOfMoves(string currentOrder, string data, int repeats = 1)
        {
            var dancemoves = data.Split(',');
            var newOrder = currentOrder;

            for (int i = 0; i < repeats; i++)
            {
                foreach (var dancemove in dancemoves)
                {
                    newOrder = GetNewOrder(newOrder, dancemove);
                }

                if (newOrder == currentOrder)
                {
                    Console.WriteLine(i);
                }

            }

            return newOrder;
        }

        private static string GetNewOrder(string currentOrder, string dancemove)
        {
            var newOrder = "";

            // Spin
            if (dancemove.StartsWith('s'))
            {
                var spinValue = Convert.ToInt32(dancemove.Substring(1));
                return newOrder = GetNewOrderAfterSpin(currentOrder, spinValue);
            }

            // Exchange
            if (dancemove.StartsWith('x'))
            {
                var xPos = dancemove.Substring(1);
                return newOrder = GetNewOrderAfterExchange(currentOrder, xPos);
            }

            // Partner
            if (dancemove.StartsWith('p'))
            {
                var pPos = dancemove.Substring(1);
                return newOrder = GetNewOrderAfterPartnerSwitch(currentOrder, pPos);
            }

            throw new ArgumentException("Unknown move");
        }

        private static string GetNewOrderAfterSpin(string currentOrder, int spinValue)
        {
            var position = currentOrder.Length - spinValue;

            var newOrder = "";
            newOrder += currentOrder.Substring(position);
            newOrder += currentOrder.Substring(0, position);

            return newOrder;
        }

        private static string GetNewOrderAfterExchange(string currentOrder, string pos)
        {
            var posOne = Convert.ToInt32(pos.Split('/')[0]);
            var posTwo = Convert.ToInt32(pos.Split('/')[1]);

            var newOrder = currentOrder.ToCharArray();

            var temp = newOrder[posOne];
            newOrder[posOne] = newOrder[posTwo];
            newOrder[posTwo] = temp;

            return string.Join("", newOrder);
        }

        private static string GetNewOrderAfterPartnerSwitch(string currentOrder, string pos)
        {
            var posOne = currentOrder.IndexOf(pos.Split('/')[0]);
            var posTwo = currentOrder.IndexOf(pos.Split('/')[1]);

            var newOrder = currentOrder.ToCharArray();

            var temp = newOrder[posOne];
            newOrder[posOne] = newOrder[posTwo];
            newOrder[posTwo] = temp;

            return string.Join("", newOrder);
        }
    }
}