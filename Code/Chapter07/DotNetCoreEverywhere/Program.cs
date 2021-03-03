using System;
using System.Diagnostics;

namespace DotNetCoreEverywhere
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("I Can run everyWhere!");
            Debug.WriteLine("Debug says, I am watching!");
            Trace.WriteLine("Trace says, I am watching!");
        }
    }
}
