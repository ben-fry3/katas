using System;
using System.Diagnostics;

namespace Mines
{
    public class Program
    {
        static void Main(string[] args)
        {
            var layoutArray = new string[]
            {
                "....**.*.*",
                "*.........",
                "........*.",
                ".*.....*.*",
                "*.**......",
                "..*.......",
                "........*.",
                "..**......"
            };

            var layoutString = "....**.*.**.................*..*.....*.**.**........*...............*...**......";

            var mineField = new MineField(8, layoutString);

            var stopwatch = Stopwatch.StartNew();
            var hints = mineField.GenerateHintGrid();
            stopwatch.Stop();

            foreach(var hint in hints)
            {
                Console.WriteLine(hint);
            }

            Console.WriteLine("\nGenerated in {0}ms", stopwatch.ElapsedMilliseconds);

            Console.ReadLine();
        }
    }
}
