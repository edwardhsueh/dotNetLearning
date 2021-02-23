using System;
using Packt.Shared;
using static System.Console;

namespace PeopleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Person bob = new Person();
            WriteLine(bob.ToString());
            bob.Name = "Bob Smith";
            bob.DateOfBirth = new DateTime(1965, 12, 2); 
            bob.FavoriteAncientWonder = WondersOfTheAncientWorld.StatueOfZeusAtOlympia;
            bob.Children.Add(new Person { Name = "Alfred" });
            bob.Children.Add(new Person { Name = "Zoe" });
            WriteLine(
                format: "{0} was born on {1:dddd, dd/MM/yyyy}",
                arg0: bob.Name,
                arg1: bob.DateOfBirth);
            WriteLine(format:
            "{0}'s favorite wonder is {1}. Its integer is {2}.",
            arg0: bob.Name,
            arg1: bob.FavoriteAncientWonder,
            arg2: (int)bob.FavoriteAncientWonder);                
            WriteLine(
                $"{bob.Name} has {bob.Children.Count} children:");
            for (int child = 0; child < bob.Children.Count; child++)
            {
                WriteLine($" {bob.Children[child].Name}");
            }      
            WriteLine(
                $"{bob.Name} is a {Person.Species}");

            var alice = new Person
            {
                Name = "Alice Jones",
                DateOfBirth = new DateTime(1998, 3, 7)
            };                
            WriteLine(
                format: "{0} was born on {1:dddd, dd/MM/yyyy}",
                arg0: alice.Name,
                arg1: alice.DateOfBirth);
            
            var blankPerson = new Person();
            WriteLine(format:"{0} of {1} was created at {2:hh:mm:ss} on a {2:dddd}.",
                arg0: blankPerson.Name,
                arg1: blankPerson.HomePlanet,
                arg2: blankPerson.Instantiated);
            var gunny = new Person(initialName:"Gunny", homePlanet:"Mars");
            WriteLine(
                format:"{0} of {1} was created at {2:hh:mm:ss} on a {2:dddd}.",
                arg0: gunny.Name,
                arg1: gunny.HomePlanet,
                arg2: gunny.Instantiated);
            // Things of Default
            var default1 = new ThingOfDefaults();
            WriteLine("** Thing of Default");
            WriteLine(
                format: "{0}, {1}, {2}, {3},",
                default1.Population, default1.When, 
                default1.Name, default1.People.Count
                );
            // ----------------------    
            // BankAccount    
            // ----------------------    
            BankAccount.InterestRate = 0.012M; // store a shared value
            var jonesAccount = new BankAccount
                {
                    AccountName= "Mrs. Jones",
                    Balance= 2400
                };

            WriteLine(format: "{0}'s Balanace is {1:N} earned {2:C} interest.",
                arg0: jonesAccount.AccountName,
                arg1: jonesAccount.Balance,
                arg2: jonesAccount.Balance * BankAccount.InterestRate);                
            var gerrierAccount = new BankAccount();
            gerrierAccount.AccountName = "Ms. Gerrier";
            gerrierAccount.Balance = 98;
            WriteLine(format: "{0}'s Balanace is {1:N} earned {2:C} interest.",
                arg0: gerrierAccount.AccountName,
                arg1: gerrierAccount.Balance,
                arg2: gerrierAccount.Balance * BankAccount.InterestRate);                


        }
    }
}
