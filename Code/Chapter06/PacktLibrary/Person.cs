using System;
using System.Collections.Generic;
using static System.Console;
namespace Packt.Shared
{
    public class Person
    {
        // fields
        public string Name {get;set;}
        public DateTime DateOfBirth {get;set;}
        public List<Person> Children = new List<Person>();
        // methods
        public void WriteToConsole()
        {
            WriteLine($"{Name} was born on a {DateOfBirth:dddd}.");
        }
        // static method to "multiply"
        public static Person Procreate(Person p1, Person p2)
        {
            var baby = new Person
            {
                Name = $"Baby of {p1.Name} and {p2.Name}"
            };
            p1.Children.Add(baby);
            p2.Children.Add(baby);
            return baby;
        }
        // operator to "multiply"
        public static Person operator *(Person p1, Person p2)
        {
            return Person.Procreate(p1, p2);
        }
        // instance method to "multiply"
        public Person ProcreateWith(Person partner)
        {
            return Procreate(this, partner);
        }
        public int Factorial(int number)
        {
            if (number < 0)
            {
                throw new ArgumentException(
                $"{nameof(number)} cannot be less than zero.");
            }
            else {
                return localFactorial(number);
                int localFactorial(int localNumber) // local function
                {
                    if (localNumber < 1) return 1;
                    return localNumber * localFactorial(localNumber - 1);
                }
            }

        }
    }
}
