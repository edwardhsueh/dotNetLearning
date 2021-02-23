using System;
using static System.Console;
namespace WritingFunctions
{
    class Program
    {
        static void TimesTable(byte number)
        {
            WriteLine($"This is the {number} times table:");
            for (int row = 1; row <= 9; row++)
            {
                WriteLine($"{row} x {number} = {row * number}");
            }
            WriteLine();
        }
        static void RunTimesTable()
        {
            bool isNumber;
            do
            {
                Write("Enter a number between 0 and 255: ");
                isNumber = byte.TryParse(ReadLine(), out byte number);
                if (isNumber)
                {
                    TimesTable(number);
                }
                else
                {
                    WriteLine("You did not enter a valid number!");
                }
            }
            while (isNumber);
        }

        static decimal CalculateTax(decimal amount, string twoLetterRegionCode){
            decimal rate = twoLetterRegionCode switch
            {
                "CH" => 0.08M,
                "DK" => 0.25M,
                "NO" => 0.25M,
                _ => 0.0M, 
            };
            return amount * rate;
        }
        /// <summary>
        /// Pass a 32-bit integer and it will return Factorial Result
        /// </summary>
        /// <param name="number">Number is the Factorial Input</param>
        /// <returns>Factorial Result(N!)</returns>
        static int Factorial(int number){
            switch(number){
                case 0:
                    return 1;
                case 1:
                    return 1;
                default:
                    checked{
                     return number * Factorial(number - 1);
                    }
            }
        }
        static void RunFactorial()
        {
            for (int i = 1; i < 15; i++)
            {
                try{
                    WriteLine($"{i}! = {Factorial(i):N0}");
                }
                catch(Exception ex){
                    WriteLine($"{ex.GetType()} says {ex.Message}");
                }
            }
        }    
        static int FibImperative(int term)
        {
            if (term == 1)
            {
                return 0;
            }
            else if (term == 2)
            {
                return 1;
            }
            else
            {
                return FibImperative(term - 1) + FibImperative(term - 2);
            }
        }        
        static void RunFibImperative()
        {
            for (int i = 1; i <= 30; i++)
            {
                WriteLine("The {0} term of the Fibonacci sequence is {1:N0}.",
                arg0: i,
                arg1: FibImperative(term: i));
            }  
        }          
       
        static Func<int, int> FibFunctional_s = (int term) => {
            int result = term switch
            {
                1 => 0,
                2 => 1,
                _ => FibFunctional(term - 1) + FibFunctional(term - 2)
            };  
            return result; 
        };    
        static int FibFunctional(int term) =>
            term switch
            {
            1 => 0,
            2 => 1,
            _ => FibFunctional(term - 1) + FibFunctional(term - 2)
            };        
        static void RunFibFunctional()
        {
            for (int i = 1; i <= 30; i++)
            {
                WriteLine("The {0} term of the Fibonacci sequence is {1:N0}.",
                arg0: i,
                arg1: FibFunctional(i));
            } 
        }                    
        static void Main(string[] args)
        {
            // RunTimesTable();
            // Write("Enter an amount: ");
            // string amountInText = ReadLine();
            // Write("Enter a two-letter region code: ");
            // string region = ReadLine();
            // if (decimal.TryParse(amountInText, out decimal amount))
            // {
            //     decimal taxToPay = CalculateTax(amount, region);
            //     WriteLine($"You must pay {taxToPay} in sales tax.");
            // }
            // else
            // {
            //     WriteLine("You did not enter a valid amount!");
            // }            
            Random rnd = new Random();
            int number = 5;
            int result = Factorial(number);
            WriteLine($"Factorial({number}) = {result}");
            RunFibFunctional();
          
        }

    }
}
