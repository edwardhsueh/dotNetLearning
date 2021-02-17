using System;
using static System.Console;
namespace Operators
{
    class Program
    {
        static void Main(string[] args)
        {
            int e = 11;
            int f = 3;
            WriteLine($"e is {e}, f is {f}");
            WriteLine($"e + f = {e + f}");
            WriteLine($"e - f = {e - f}");
            WriteLine($"e * f = {e * f}");
            WriteLine($"e / f = {e / f}");
            WriteLine($"e % f = {e % f}");            
            double g = 11.0;
            WriteLine($"g is {g:N}, f is {f}");
            WriteLine($"g / f = {g / f}");
            WriteLine($"f / g = {f / g}");
            bool a = true;
            bool b = false;
            WriteLine($"   a  |   b ");
            WriteLine($"{a,5} | {b,-5}");
            WriteLine($"AND | a | b ");
            WriteLine($"a | {a & a,-5} | {a & b,-5} ");
            WriteLine($"b | {b & a,-5} | {b & b,-5} ");
            WriteLine();
            WriteLine($"OR | a | b ");
            WriteLine($"a | {a | a,-5} | {a | b,-5} ");
            WriteLine($"b | {b | a,-5} | {b | b,-5} ");
            WriteLine();
            WriteLine($"XOR | a | b ");
            WriteLine($"a | {a ^ a,-5} | {a ^ b,-5} ");
            WriteLine($"b | {b ^ a,-5} | {b ^ b,-5} ");
            int age = 47;
            // How many operators in the following statement?
            char firstDigit = age.ToString()[0];
            WriteLine(firstDigit);
            WriteLine(nameof(age));
            WriteLine(sizeof(int));
            // add and remove the "" to change the behavior
            object o = 3;
            int j = 4;
            if (o is int i)
            {
                WriteLine($"{i} x {j} = {i * j}");
            }
            else
            {
                WriteLine("o is not an int so it cannot multiply!");
            }   
                
        }
    }
}
