using System;
using static System.Console;
using static System.Convert;

namespace CastingConverting
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 10;
            double b = a; // an int can be safely cast into a double
            WriteLine(b);
            double c = 9.8;
            int d = (int)c; // compiler gives an error for this line
            WriteLine(d); 
            long e = 10;
            int f = (int)e;
            WriteLine($"e is {e:N0} and f is {f:N0}");
            e = long.MaxValue;
            f = (int)e;
            WriteLine($"e is {e:N0} and f is {f:N0}");                       
            e = 5_000_000_000;
            try{
                f = ToInt32(e);
                WriteLine($"e is {e:N0} and f is {f:N0}");                       
            } catch(OverflowException oe) {
                WriteLine($"number too large");  
                throw;                     
            }
        }
    }
}
