using System;
using System.IO;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            var ss = File.ReadAllText(@"D:\data.txt").Split(Environment.NewLine);
            var nums = new int[ss.Length];
            for (var i = 0; i < ss.Length; i++)
            {
                nums[i] = int.Parse(ss[i]);
                for (var j = 0; j < i; j++)
                {
                    for (var k = 0; k < j; k++)
                    {
                        if (nums[j] + nums[i] + nums[k] == 2020)
                        {
                            Console.WriteLine(nums[j] * nums[i] * nums[k]);
                            Console.ReadKey();
                            return;
                        }
                    }
                }
            }
            
        }
    }
}
