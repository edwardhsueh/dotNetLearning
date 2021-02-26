using System;
using Packt.Shared;
using static System.Console;
namespace PeopleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var harry = new Person { Name = "Harry" };
            var mary = new Person { Name = "Mary" };
            var jill = new Person { Name = "Jill" };
            var may  = new Person { Name = "May"};
            // call instance method
            var baby1 = mary.ProcreateWith(harry);
            baby1.Name = "Gary";
            // call static method
            var baby2 = Person.Procreate(harry, jill);
            WriteLine($"{harry.Name} has {harry.Children.Count} children.");
            WriteLine($"{mary.Name} has {mary.Children.Count} children.");
            WriteLine($"{jill.Name} has {jill.Children.Count} children.");
            for(int i=0;i < harry.Children.Count; i++){
                WriteLine(
                format: "{0}'s {1} child is named \"{2}\".",
                harry.Name, i, harry.Children[i].Name);
            }
            var baby3 = may * jill;
            WriteLine($"{may.Name} has {may.Children.Count} children.");
            for(int i=0;i < jill.Children.Count; i++){
                WriteLine(
                format: "{0}'s {1} child is named \"{2}\".",
                jill.Name, i, jill.Children[i].Name);
            }
            WriteLine($"Person Factorial 5: {may.Factorial(5)}");

        }
    }
}
