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
            Console.WriteLine("FirstName:{0}, LastName:{1}, Savings:{2}, Salary:{3}, Type:{4}", p1.FirstName, p1.LastName, p1.Savings, p1.Salary, p1.GetType());
        }
    }
}
