using System;
using Packt.Shared;
namespace Exercise
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Person p1 = new Person();
            int x = 1;
            int y = 0;
            if(p1.Salary is null){
                Console.WriteLine("Salary is null");
            }
            if(p1.FirstName is null){
                Console.WriteLine("FirstName is null");
            }
            // if(p1.Savings is null){
            //     Console.WriteLine("Savings is null");
            // }
            // local function
            void Add1(ref int x, out int y) {
                x = x + 1;
                y = x + 2;
            }
            Add1(ref x, out y);
            Console.WriteLine("X:{0}, Y:{1}",x, y);
            Console.WriteLine("FirstName:{0}, LastName:{1}, Savings:{2}, Salary:{3}, Type:{4}", p1.FirstName, p1.LastName, p1.Savings, p1.Salary, p1.GetType());
        }
    }
}
