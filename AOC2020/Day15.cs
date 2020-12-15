using AOC;
using System;
using System.Collections.Generic;

namespace AOC2020
{
    public class Day15
    {
        public void Exec()
        {
            var numbers = new int[] { 7, 14, 0, 17, 11, 1, 2 };

            var one = partOne(numbers);
            var two = partTwo(numbers);
            Console.WriteLine($"partOne: {one} partTwo: {two}");
            if (Console.ReadKey().Key == ConsoleKey.Spacebar)
                Clipboard.SetText(two.ToString());
        }

        public int partOne(int[] numbers)
        {
            return play(numbers, 2020);
        }

        public int partTwo(int[] numbers)
        {
            return play(numbers, 30000000);
        }

        int play(int[] numbers, int stepCount)
        {
            var step = 0;
            var d = new Dictionary<int, List<int>>();
            for (step = 0; step < numbers.Length; ++step)
            {
                d.Add(numbers[step], new List<int>());
                d[numbers[step]].Add(step + 1);
            }
            if (!d.ContainsKey(0))
                d.Add(0, new List<int>());
            var lastNumber = numbers[numbers.Length - 1];
            for (step = numbers.Length + 1; step <= stepCount; ++step)
            {
                lastNumber = getAge(d, lastNumber);
                addStep(d, lastNumber, step);
            }
            return lastNumber;
        }

        int getAge(Dictionary<int, List<int>> storage, int number)
        {
            if (!storage.ContainsKey(number) || storage[number].Count < 2) return 0;
            return storage[number][1] - storage[number][0];
        }

        void addStep(Dictionary<int, List<int>> storage, int number, int step)
        {
            if (!storage.ContainsKey(number))
                storage.Add(number, new List<int>());
            storage[number].Add(step);
            if (storage[number].Count > 2)
                storage[number].RemoveAt(0);
        }
    }
}