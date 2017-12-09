using System.Diagnostics;

namespace Advent_of_Code_2017.classes
{
    public class DayNine
    {
        public static string GetResult(out long timeElapsed)
        {
            Debug.Assert(GetScore("{}") == 1);
            Debug.Assert(GetScore("{{{}}}") == 6);
            Debug.Assert(GetScore("{{},{}}") == 5);
            Debug.Assert(GetScore("{{{},{},{{}}}}") == 16);
            Debug.Assert(GetScore("{<a>,<a>,<a>,<a>}") == 1);
            Debug.Assert(GetScore("{{<ab>},{<ab>},{<ab>},{<ab>}}") == 9);
            Debug.Assert(GetScore("{{<!!>},{<!!>},{<!!>},{<!!>}}") == 9);
            Debug.Assert(GetScore("{{<a!>},{<a!>},{<a!>},{<ab>}}") == 3);
            Debug.Assert(GetScore("{{{}},{}}") == 8);
            Debug.Assert(GetScore("{{{}},{},{}}") == 10);
            Debug.Assert(GetScore("{{{!!}},{!},{!!}}") == 10);
            Debug.Assert(GetScore("{{<!>},{<!>},{<!>},{<a>}}") == 3);
            Debug.Assert(GetScore("{{<a>},{<a>},{<a>},{<a>}}") == 9);

            var result = "";
            var data = Helpers.getDataFromFile("daynine.txt");
            var stopWatch = Stopwatch.StartNew();
            result = GetScore(data).ToString();
            timeElapsed = stopWatch.ElapsedMilliseconds;

            return result;
        }

        public static string GetResultTwo(out long timeElapsed)
        {
            //Debug.Assert(GetNameOfBottomProgram(testData) == "tknk");

            var result = "";
            var data = Helpers.getDataFromFile("dayeight.txt");
            var stopWatch = Stopwatch.StartNew();
            //result = DecodeAllInstructionsTwo().ToString();
            timeElapsed = stopWatch.ElapsedMilliseconds;

            return result;
        }

        private static int GetScore(string data)
        {
            var level = 0;
            var score = 0;
            var skip = false;
            var garbage = false;

            for (int i = 0; i < data.Length; i++)
            {
                var c = data[i];

                if (skip)
                {
                    skip = false;
                    continue;
                }

                if (garbage)
                {
                    if (c == '!')
                    {
                        skip = true;
                    }
                    else if (c == '>')
                    {
                        garbage = false;
                    }

                    continue;
                }

                switch (c)
                {
                    case '{':
                        level += 1;
                        break;
                    case '}':
                        score += level;
                        level -= 1;
                        break;
                    case '<':
                        garbage = true;
                        break;
                    default:
                        break;
                }
            }

            return score;
        }
    }
}