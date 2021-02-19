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
                // throw;                     
            }
            double g = 9.5;
            try{
                int h = ToInt32(g);
                WriteLine($"g is {g:N1} and h is {h}");                       
            } catch(OverflowException oe) {
                WriteLine($"number too large");  
                // throw;                     
            }
            g = 10.5;
            try{
                int h = ToInt32(g);
                WriteLine($"g is {g:N1} and h is {h}");                       
            } catch(OverflowException oe) {
                WriteLine($"number too large");  
                // throw;                     
            }            
            double[] doubles = new double[] { 9.49, 9.5, 9.51, 10.49, 10.5, 10.51 };
            foreach (double n in doubles)
            {
              WriteLine($"ToInt({n}) is {ToInt32(n)}");
              
            }            
            foreach (double n in doubles)
            {
                WriteLine(format:
                "Math.Round({0}, 0, MidpointRounding.AwayFromZero) is {1}",
                arg0: n,
                arg1: Math.Round(value: n, digits: 0, mode: MidpointRounding.AwayFromZero));
            }        
            // allocate array of 128 bytes
            byte[] binaryObject = new byte[128];
            // populate array with random bytes
            (new Random()).NextBytes(binaryObject);
            WriteLine("Binary Object as bytes:");
            for(int index = 0; index < binaryObject.Length; index++)
            {
                Write($"{binaryObject[index]:X} ");
            }
            WriteLine();
            // convert to Base64 string and output as text
            string encoded = ToBase64String(binaryObject);
            WriteLine($"Binary Object as Base64: {encoded}");     
            byte[] decoded = FromBase64String(encoded);
            for(int index = 0; index < decoded.Length; index++)
            {
                Write($"{decoded[index]:X} ");
            }

                      
        }
    }
}
