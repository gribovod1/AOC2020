using AOC;
using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2020
{
    public class Day21
    {
        Dictionary<int, string> Rules = new Dictionary<int, string>();

        public void Exec()
        {
            var productsText = File.ReadAllLines(@"Data\day21.txt");
            var iaPairs = new Dictionary<KeyValuePair<string, string>,int>();
            for (var productIndex = 0; productIndex< productsText.Length;++productIndex)
            {
                var sp = productsText[productIndex].Split(new string[] { " (contains" },StringSplitOptions.RemoveEmptyEntries);
                var ingridientList = sp[0].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                var allergenList = sp[1].Split(new string[] { " ",")" }, StringSplitOptions.RemoveEmptyEntries);
                var hs = new HashSet<KeyValuePair<string, string>>();
                foreach (var ingridient in ingridientList)
                    foreach (var allergen in allergenList)
                    {
                        var pair = new KeyValuePair<string, string>(ingridient, allergen);
                        if (iaPairs.ContainsKey(pair))
                            iaPairs[pair] = iaPairs[pair] + 1;
                        else
                            iaPairs.Add(pair, 1);
                    }
            }



            var one = PartOne();
            var two = PartTwo();
            Console.WriteLine($"partOne: {one} partTwo: {two}");
            if (Console.ReadKey().Key == ConsoleKey.Spacebar)
                Clipboard.SetText(one.ToString());
        }

        ulong PartOne()
        {
            ulong result = 0;

            return result;
        }

        ulong PartTwo()
        {
            ulong result = 0;
            return result;
        }
    }
}