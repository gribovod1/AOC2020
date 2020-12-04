using AOC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllText("data.txt");
            var countOne = 0;
            var countTwo = 0;
            var ps = lines.Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);



            Clipboard.SetText(countOne.ToString());
            Console.WriteLine($"countOne: {countOne} countTwo: {countTwo}");
            Console.ReadKey();
        }
    }
}
