using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Advent_of_Code_2017.classes
{
    public class DayFour
    {
        public static int GetResult(out long timeElapsed)
        {
            Debug.Assert(isPassphraseValid("aa bb cc dd ee") == true);
            Debug.Assert(isPassphraseValid("aa bb cc dd aa") == false);
            Debug.Assert(isPassphraseValid("aa bb cc dd aaa") == true);

            var result = 0;
            var data = Helpers.getDataFromFile("dayfour.txt");
            var stopWatch = Stopwatch.StartNew();
            result = calulateValidPassphrases(data);
            timeElapsed = stopWatch.ElapsedMilliseconds;

            return result;
        }

        private static int calulateValidPassphrases(string data)
        {
            var passphrases = data.Split("\r\n");

            var numValidPassphrases = passphrases.Where(x => isPassphraseValid(x)).Count();

            return numValidPassphrases;
        }

        private static bool isPassphraseValid(string passphrase)
        {
            var grouped = getPassphraseArray(passphrase)
            .GroupBy(x => x)
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