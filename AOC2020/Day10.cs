using AOC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2020
{
    public static class Day10
    {
        public static void Exec()
        {
            var numbersText = File.ReadAllLines(@"Data\day10.txt");
            var numbers = new List<ulong>();
            foreach (var s in numbersText)
                numbers.Add(ulong.Parse(s));
            var one = partOne(numbers);
            var two = partTwo(numbers);

            Console.WriteLine($"partOne: {one} partTwo: {two}");
            if (Console.ReadKey().Key == ConsoleKey.Spacebar)
                Clipboard.SetText(two.ToString());
        }

        public static int partOne(List<ulong> numbers)
        {
            numbers.Sort();
            var count1 = 0;
            var count3 = 1;
            if (numbers[0] == 1)
                ++count1;
            if (numbers[0] == 3)
                ++count3;
            for (int i = 1; i < numbers.Count; ++i)
            {
                if (numbers[i] - numbers[i - 1] == 1)
                    ++count1;
                if (numbers[i] - numbers[i - 1] == 3)
                    ++count3;
            }
            Console.WriteLine($"count1 +count3: {count1 + count3} numbers.Count: {numbers.Count}");
            return count1 * count3;
        }

        public static ulong partTwo(List<ulong> numbers)
        {
            numbers.Add(0);
            numbers.Sort();
            numbers.Add(numbers[numbers.Count - 1] + 3);
            return calc(numbers.ToArray(), 1);
        }

        static ulong calc(ulong[] numbers, int index)
        {
            if (index == numbers.Length - 1)
                return 1;
            if (numbers[index + 1] - numbers[index - 1] <= 3)
            {
                var endExclude = findEndIndex(numbers, index);
                ulong varCount = calcBrute(numbers, index, endExclude);
                return varCount * calc(numbers, endExclude);
            }
            else
                return calc(numbers, index + 1);
        }

        static int findEndIndex(ulong[] numbers, int index)
        {
            if (numbers[index + 1] - numbers[index - 1] > 3)
                return index;
            return findEndIndex(numbers, index + 1);
        }

        static ulong calcBrute(ulong[] numbers, int index, int endIndex)
        {
            if (index >= endIndex)
                return 1;
            if (numbers[index + 1] - numbers[index - 1] <= 3)
                return calcBrute(excludeClone(numbers, index), index, endIndex - 1) + calcBrute(numbers, index + 1, endIndex);
            else
                return calcBrute(numbers, index + 1, endIndex);
        }

        static ulong[] excludeClone(ulong[] numbers, int index)
        {
            var result = new ulong[numbers.Length - 1];
            for (var i = 0; i < result.Length; ++i)
                result[i] = i < index ? numbers[i] : numbers[i + 1];
            return result;
        }
    }
}