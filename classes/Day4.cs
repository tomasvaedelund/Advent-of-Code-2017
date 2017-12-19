using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2017.classes
{
    public class Day4
    {
        public static int GetResult(out long timeElapsed)
        {
            Debug.Assert(isPassphraseValid("aa bb cc dd ee") == true);
            Debug.Assert(isPassphraseValid("aa bb cc dd aa") == false);
            Debug.Assert(isPassphraseValid("aa bb cc dd aaa") == true);

            var result = 0;
            var data = Helpers.GetDataFromFile("day4.txt");
            var stopWatch = Stopwatch.StartNew();
            result = calulateValidPassphrases(data);
            timeElapsed = stopWatch.ElapsedMilliseconds;

            return result;
        }

        public static int GetResultTwo(out long timeElapsed)
        {
            Debug.Assert(isPassphraseValidTwo("abcde fghij") == true);
            Debug.Assert(isPassphraseValidTwo("abcde xyz ecdab") == false);
            Debug.Assert(isPassphraseValidTwo("a ab abc abd abf abj") == true);
            Debug.Assert(isPassphraseValidTwo("iiii oiii ooii oooi oooo") == true);
            Debug.Assert(isPassphraseValidTwo("oiii ioii iioi iiio") == false);

            var result = 0;
            var data = Helpers.GetDataFromFile("day4.txt");
            var stopWatch = Stopwatch.StartNew();
            result = calulateValidPassphrases(data, true);
            timeElapsed = stopWatch.ElapsedMilliseconds;

            return result;
        }

        private static int calulateValidPassphrases(string data, bool isTwo = false)
        {
            var passphrases = data.Split("\r\n");

            var numValidPassphrases = (isTwo) ? passphrases.Where(x => isPassphraseValidTwo(x)).Count() : passphrases.Where(x => isPassphraseValid(x)).Count();

            return numValidPassphrases;
        }

        private static bool isPassphraseValid(string passphrase)
        {
            var phrases = getPassphraseArray(passphrase);
            var grouped = phrases
            .GroupBy(x => x)
            .Select(x => new
            {
                word = x.Key,
                cnt = x.Count()
            });

            return !grouped.Any(x => x.cnt > 1);
        }

        private static bool isPassphraseValidTwo(string passphrase)
        {
            var phrases = getPassphraseArray(passphrase);
            var grouped = phrases
            .GroupBy(x => string.Join("", x.OrderBy(y => y)))
            .Select(x => new
            {
                word = x.Key,
                cnt = x.Count()
            });

            return !grouped.Any(x => x.cnt > 1);
        }

        private static string[] getPassphraseArray(string passphrase)
        {
            return passphrase.Split(' ');
        }
    }
}