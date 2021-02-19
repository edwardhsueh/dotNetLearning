using System;
using static System.Console;
namespace Exercise03
{
    class Program
    {
        static void Main(string[] args)
        {
            string res = String.Empty;
            for(int i=1; i<=100;i++){
                if( ((i%3) == 0) && ((i%5==0))){
                    res = "fizzbuzz";
                }
                else if((i%3==0)){
                    res = "fizz";
                }
                else if((i%5)==0){
                    res = "buzz";
                }
                else {
                    res = i.ToString();
                }
                Write($"{res},");
            }
        }
    }
}
