using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.Console;
namespace LinqInParallel
{
    class Program
    {
        static void Main(string[] args)
        {
            var watch = new Stopwatch();
            Write("Press ENTER to start: ");
            ReadLine();
            watch.Start();
            IEnumerable<int> numbers = Enumerable.Range(1, 2_000_000_000);
            var squares = from number in numbers
                          where number % 2 == 1
                          select number;
            // var squares = numbers.Select(number => number * number).ToArray();
            watch.Stop();
            WriteLine("{0:#,##0} elapsed milliseconds.",
            arg0: watch.ElapsedMilliseconds);
        }
    }
}
