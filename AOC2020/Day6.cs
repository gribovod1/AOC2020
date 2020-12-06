using AOC;
using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2020
{
    public static class Day6
    {
        public static void exec()
        {
            var groups = File.ReadAllText(@"Data\day6.txt").Split(new string[] {Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var partOne = 0;

            foreach(var g in groups)
            {
                HashSet<char> s = null;
                var first = true;
                var people = g.Split(new string[] { Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
                foreach (var answers in people)
                {
                    var current = new HashSet<char>();
                    foreach (var c in answers)
                    {
                        if (c >= 'a' && c <= 'z')
                        {
                            current.Add(c);
                        }
                    }
                    if (first)
                    {
                        s = current;
                        first = false;
                    }
                    else
                        s.IntersectWith(current);
                }
                partOne += s.Count;
            }

            Console.WriteLine($"partOne: {partOne}");
            if (Console.ReadKey().Key == ConsoleKey.Spacebar)
            {
                Clipboard.SetText(partOne.ToString());
            }
        }
    }
}
