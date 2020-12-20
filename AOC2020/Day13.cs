using AOC;
using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2020
{
    public class Day13
    {
        public void Exec()
        {
            var numbersText = File.ReadAllLines(@"Data\day13.txt");
            var time = ulong.Parse(numbersText[0]);
            var bus = new List<ulong>();
            foreach(var s in numbersText[1].Split(','))
                if (ulong.TryParse(s, out ulong o))
                    bus.Add(o);
            var one = partOne(time,bus.ToArray());

            bus = new List<ulong>();
            foreach (var s in numbersText[1].Split(','))
                if (ulong.TryParse(s, out ulong o))
                    bus.Add(o);
                else bus.Add(0);
            var two = partTwo(bus.ToArray());

            Console.WriteLine($"partOne: {one} partTwo: {two}");
            if (Console.ReadKey().Key == ConsoleKey.Spacebar)
                Clipboard.SetText(two.ToString());
        }

        public ulong partOne(ulong time, ulong[] buss)
        {
            ulong result = 0;
            var currTime = time;
            while (true)
            {
                foreach(var b in buss)
                {
                    if (currTime % b == 0)
                    {
                        result = b * (currTime - time);
                        return result;
                    }
                }
                currTime++;
            }
        }

        public ulong partTwo(ulong[] buss)
        {
            var d = new List<KeyValuePair<ulong, ulong>>();
            KeyValuePair<ulong, ulong> pMax = new KeyValuePair<ulong, ulong>(0,0);
            for (ulong i = 0; i < (ulong)buss.Length; ++i)
                if (buss[i] != 0)
                {
                    d.Add(new KeyValuePair<ulong, ulong>(i, buss[i]));
                    if (buss[i] > pMax.Value)
                        pMax = d[d.Count - 1];
                }
            ulong currTime = pMax.Value - pMax.Key; 
            while (true)
            {
                currTime += pMax.Value;
                var countTry = 0;
                foreach (var p in d)
                {
                    if ((currTime + p.Key) % p.Value == 0)
                        countTry++;
                    else break;
                }
                if (countTry == d.Count)
                {
                    return currTime;
                }
            }
        }
    }
}