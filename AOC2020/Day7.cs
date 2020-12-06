using AOC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020
{
    public static class Day7
    {
        public static void exec()
        {
            var groups = File.ReadAllText(@"Data\day7.txt").Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var partOne = 0;


            Console.WriteLine($"partOne: {partOne}");
            if (Console.ReadKey().Key == ConsoleKey.Spacebar)
            {
                Clipboard.SetText(partOne.ToString());
            }
        }
    }
}
