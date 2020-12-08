using AOC;
using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2020
{
    public static class Day8
    {
        public static void exec()
        {
            var commands = File.ReadAllLines(@"Data\day8.txt");
            var one = partOne(commands);
            var two = partTwo(commands);

            Console.WriteLine($"partOne: {one} partTwo: {two}");
            if (Console.ReadKey().Key == ConsoleKey.Spacebar)
                Clipboard.SetText(two.ToString());
        }

        public class Command
        {
            public string command;
            public int arg;
            public int count;
        }

        public static int partOne(string[] commands)
        {
            var cs = new List<Command>();
            foreach (var c in commands)
            {
                var sc = c.Split(' ');
                var nC = new Command { command = sc[0], arg = int.Parse(sc[1]), count = 0 };
                cs.Add(nC);
            }
            var accumulator = 0;
            var cAddr = 0;
            accumulator = 0;
            cAddr = 0;
            while (cAddr < cs.Count && cs[cAddr].count == 0)
            {
                ++cs[cAddr].count;
                switch (cs[cAddr].command)
                {
                    case "nop":
                        {
                            ++cAddr;
                            break;
                        }
                    case "jmp":
                        {
                            cAddr += cs[cAddr].arg;
                            break;
                        }
                    case "acc":
                        {
                            accumulator += cs[cAddr].arg;
                            ++cAddr;
                            break;
                        }
                }
            }
            return accumulator;
        }

        public static int partTwo(string[] commands)
        {
            var cs = new List<Command>();
            foreach (var c in commands)
            {
                var sc = c.Split(' ');
                var nC = new Command { command = sc[0], arg = int.Parse(sc[1]), count = 0 };
                cs.Add(nC);
            }
            var lastMod = -1;
            var accumulator = 0;
            var cAddr = 0;
            do
            {
                foreach (var c in cs)
                    c.count = 0;
                if (lastMod >= 0)
                {
                    if (cs[lastMod].command == "jmp")
                        cs[lastMod].command = "nop";
                    else
                        cs[lastMod].command = "jmp";
                }
                for (var cIndex = lastMod + 1; cIndex < cs.Count; ++cIndex)
                {
                    if (cs[cIndex].command == "jmp")
                    {
                        cs[cIndex].command = "nop";
                        lastMod = cIndex;
                        break;
                    }
                    else if (cs[cIndex].command == "nop")
                    {
                        cs[cIndex].command = "jmp";
                        lastMod = cIndex;
                        break;
                    }
                }

                accumulator = 0;
                cAddr = 0;
                while (cAddr < cs.Count && cs[cAddr].count == 0)
                {
                    ++cs[cAddr].count;
                    switch (cs[cAddr].command)
                    {
                        case "nop":
                            {
                                ++cAddr;
                                break;
                            }
                        case "jmp":
                            {
                                cAddr += cs[cAddr].arg;
                                break;
                            }
                        case "acc":
                            {
                                accumulator += cs[cAddr].arg;
                                ++cAddr;
                                break;
                            }
                    }
                }
            }
            while (cAddr < cs.Count);
            return accumulator;
        }
    }
}