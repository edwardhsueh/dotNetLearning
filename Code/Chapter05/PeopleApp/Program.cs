// #error version
using System;
using Packt.Shared;
using static System.Console;
using System.Collections.Generic;
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
            WriteLine(
                format: "{0} was born on {1:dddd, dd/MM/yyyy}",
                arg0: bob.Name,
                arg1: bob.DateOfBirth);
            WriteLine(format:
            "{0}'s favorite wonder is {1}. Its integer is {2}.",
            arg0: bob.Name,
            arg1: bob.FavoriteAncientWonder,
            arg2: (int)bob.FavoriteAncientWonder);

            bob.Children.Add(new Person());
            bob.Children.Add(new Person { Name = "Zoe" });
            WriteLine(
                $"{bob.Name} has {bob.Children.Count} children:");
            for (int child = 0; child < bob.Children.Count; child++)
            {
                WriteLine($" {bob.Children[child].Name}");
            }
            WriteLine($"{bob.Name}'s 1st child is {bob.Children[0].Name}");
            // indexer
            WriteLine($"{bob.Name}'s 1st child is {bob[0].Name}");
            // const field
            WriteLine(
                $"{bob.Name} is a {Person.Species}");
            // Add Friend
            bob.addFriend(new Person("Edward", "Earth"));
            bob.addFriend(new Person("Edwin", "Mars"));
            List<Person> allFriends = bob.getFriend();
            for(int idx=0; idx< allFriends.Count; idx++){
                WriteLine($"{bob.Name}'s friend {idx}: {allFriends[idx].Name}");
            }
            for(int idx=0; idx< allFriends.Count; idx++){
                string idxString = idx.ToString();
                WriteLine($"{bob.Name}'s friend {idx}: {bob[idxString].Name}");
            }
            // calling Method
            WriteLine("** WriteLine(bob.GetOrigin())");
            WriteLine(bob.GetOrigin());
            WriteLine("** bob.WriteToConsole()");
            bob.WriteToConsole();
            var fruit = bob.GetFruit();
            WriteLine($"{fruit.Item1}, {fruit.Item2} there are.");
            var fruitNamed = bob.GetNamedFruit();
            WriteLine($"{fruitNamed.Name}, {fruitNamed.Number} there are.");
            (string fruitName, int fruitNumber) = bob.GetFruit();
            WriteLine($"Deconstructed: {fruitName}, {fruitNumber}");
            (string fruitName, int fruitNumber) dcFruit = bob.GetFruit();
            WriteLine($"Deconstructed 2: {dcFruit.fruitName}, {dcFruit.fruitNumber}");
            WriteLine(bob.SayHello());
            WriteLine(bob.SayHello("Edward"));
            WriteLine(bob.OptionalParameters());
            WriteLine(bob.OptionalParameters("Jump!", 98.5));
            WriteLine(bob.OptionalParameters(command: "ALL!", active: false));
            int d = 10;
            int e = 20;
            WriteLine(
            $"Before: d = {d}, e = {e}, f doesn't exist yet!");
            // simplified C# 7.0 syntax for the out parameter
            bob.PassingParameters(d, ref e, out int f);
            WriteLine($"After: d = {d}, e = {e}, f = {f}");
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
                format: "int: {0}, DateTime:{1}, string:{2}, {3},",
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

            // Try Property
            var sam = new Person(initialName:"Sam", homePlanet:"Mars", initBirth: new DateTime(1972, 1, 27));

            WriteLine(sam.Origin);
            WriteLine(sam.Greeting);
            WriteLine(sam.Age);
            sam.FavoriteIceCream = "Stars";
            WriteLine($"{sam.Name}'s favorite icecream is {sam.FavoriteIceCream}");
            sam.FavoritePrimaryColor="red";
            WriteLine($"{sam.Name}'s favorite Color is {sam.FavoritePrimaryColor}");
            try{
                sam.FavoritePrimaryColor="yellow";
            }
            catch (ArgumentException argEx){
                WriteLine($"{argEx.GetType()} says {argEx.Message}");
            }
            // --------------------------------------
            // Flight Patterns(object pattern matching)
            // --------------------------------------
            WriteLine("// -------------------------------");
            WriteLine("//Flight Patterns(object pattern matching");
            WriteLine("// -------------------------------");
            var passengers = new object[5]{
                new FirstClassPassenger { AirMiles = 1_419 },
                new FirstClassPassenger { AirMiles = 16_562 },
                new BusinessClassPassenger(),
                new CoachClassPassenger { CarryOnKG = 25.7 },
                new CoachClassPassenger { CarryOnKG = 0 },
            };
            foreach (object passenger in passengers)
            {
                decimal flightCost = passenger switch
                {
                    FirstClassPassenger p when p.AirMiles > 35000 => 1500M,
                    FirstClassPassenger p when p.AirMiles > 15000 => 1750M,
                    FirstClassPassenger _ => 2000M,
                    BusinessClassPassenger _ => 1000M,
                    CoachClassPassenger p when p.CarryOnKG < 10.0 => 500M,
                    CoachClassPassenger _ => 650M,
                    _ => 800M
                };
                WriteLine($"Flight costs {flightCost:C} for {passenger}");
                WriteLine($"Flight costs swith case{CalFlightCost(passenger: passenger):C} for {passenger}");
            }

            foreach(object passenger in passengers){
                decimal flightCost = passenger switch
                {
                    /* C# 8 syntax
                    FirstClassPassenger p when p.AirMiles > 35000 => 1500M,
                    FirstClassPassenger p when p.AirMiles > 15000 => 1750M,
                    FirstClassPassenger => 2000M, */
                    // C# 9 syntax
                    FirstClassPassenger p => p.AirMiles switch
                    {
                        > 35000 => 1500M,
                        > 15000 => 1750M,
                        _ => 2000M
                    },
                    BusinessClassPassenger => 1000M,
                    CoachClassPassenger p when p.CarryOnKG < 10.0 => 500M,
                    CoachClassPassenger => 650M,
                    _ => 800M
                };
                WriteLine($"c# 9 Flight costs {flightCost:C} for {passenger}");
            }

            // init-Only property with get/init only
            var jeff = new ImmutablePerson{
                FirstName = "Jeff",
                LastName = "Wang"
            };
            WriteLine($"FirstName:{jeff.FirstName}, LastName:{jeff.LastName}");
            // jeff.LastName = "Geoff";
            // Record(nonDestructive Mutation)
            WriteLine("****** Records");
            var car = new ImmutableVehicle
            {
                Brand = "Mazda MX-5 RF",
                Color = "Soul Red Crystal Metallic",
                Wheels = 4
            };
            var repaintedCar = car with { Color = "Polymetal Grey Metallic" };
            var newBrandCar = repaintedCar with { Brand = "Audi" };
            WriteLine($"car:{car.Brand}, {car.Color}, {car.Wheels}");
            WriteLine($"repaintedCar:{repaintedCar.Brand}, {repaintedCar.Color}, {repaintedCar.Wheels}");
            WriteLine($"newBrandCar:{newBrandCar.Brand}, {newBrandCar.Color}, {newBrandCar.Wheels}");

            var oscar = new ImmutableAnimal(Name:"Oscar", Species:"Labrador");
            var (who, what) = oscar; // calls Deconstruct method
            WriteLine($"{who} is a {what}.");
            WriteLine($"{oscar.Name} is a {oscar.Species}.");
        }
        static decimal CalFlightCost(object passenger){
            switch(passenger){
                case FirstClassPassenger p when p.AirMiles > 35000:
                    return 1500M;
                case FirstClassPassenger p when p.AirMiles > 15000:
                    return 1500M;
                case FirstClassPassenger p:
                    return 2000M;
                case BusinessClassPassenger p:
                    return 1500M;
                case CoachClassPassenger p when p.CarryOnKG < 10.0:
                    return 1500M;
                case CoachClassPassenger p:
                    return 650M;
                default:
                    return 800M;
            }
        }
    }
}
