using System;
using static System.Math;
using static System.Console;
namespace Exercise4_2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] primeNumbers = new int[12]{2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37};
            WriteLine("Please input an interger:");
            if(int.TryParse(ReadLine(), out int number)){
                if(number > 1000){
                    WriteLine("Number Too Large") ; 
                }
                else{
                    int nowPrimeNumber = primeNumbers[0];
                    byte numberOfMatched = 0;
                    string result = "";
                    for(int i=0; i<primeNumbers.Length;i++){
                        nowPrimeNumber = primeNumbers[i];
                        while((number % nowPrimeNumber) == 0){
                            if(numberOfMatched == 0){
                                result = result + nowPrimeNumber;
                            }
                            else {
                                result = result + " X " + nowPrimeNumber;
                            }
                            numberOfMatched++;
                            number/=nowPrimeNumber;
                        }

                    } 
                    WriteLine($"{number} = {result}");
                }
            }
            else {
                WriteLine("Your Input is NOT a number");
            }
        }
    }
}

