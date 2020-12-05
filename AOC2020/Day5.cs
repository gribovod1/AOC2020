using AOC;
using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2020
{
    public static class Day5
    {
        public static void exec()
        {
            var lines = File.ReadAllLines("data_day5.txt");
            var countOne = 0;
            var ids = new HashSet<int>();
            for(var i=8; i < 128 * 8 - 8;++i)
                ids.Add(i);
            foreach (var s in lines)
            {
                int col = getValue(s.Substring(0, 7), 0, 127);
                int row = getValue(s.Substring(7, 3), 0, 7);
                int curr = col * 8 + row;
                if (countOne < curr)
                    countOne = curr;
                ids.Remove(curr);
            }
            Console.WriteLine($"countOne: {countOne}");
            foreach(var id in ids)
            {
                Console.WriteLine($"id: {id}");
            }
            if (Console.ReadKey().Key == ConsoleKey.Spacebar)
            {
                Clipboard.SetText(countOne.ToString());
            }
        }

        public static int getValue(string path, int start, int end)
        {
            if (path[0] == 'F' || path[0] == 'L')
            {
                if (path.Length == 1)
                    return start;
                return getValue(path.Substring(1, path.Length - 1), start, start + (end - start) / 2);
            }
            else
            {
                if (path.Length == 1)
                    return end;
                return getValue(path.Substring(1, path.Length - 1), start + (end - start) / 2 + 1, end);
            }
        }
    }
}