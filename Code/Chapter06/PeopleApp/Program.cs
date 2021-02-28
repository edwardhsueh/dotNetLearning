using System;
using Packt.Shared;
using static System.Console;
namespace PeopleApp
{
    class Program
    {
        private static void Harry_Shout(object sender, EventArgs e)
        {
            Person p = (Person)sender;
            WriteLine($"{p.Name} is this angry: {p.AngerLevel}.");
        }
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
            // event handler: assign Event Listener
            // When assigning a method to a delegate field, you should not use the simple assignment operator as we did in the preceding example, and as shown in the following code:
            // harry.Shout = Harry_Shout;
            //  If the Shout delegate field was already referencing one or more methods, by assigning a method, it would replace all the others. With delegates that are used for events
            // we usuallywant to make sure that a programmer only ever uses either the += operator or the -= operator to assign and remove methods:
            harry.Shout += Harry_Shout;
            harry.Shout += Harry_Shout;
            harry.Shout += Harry_Shout;
            harry.Shout -= Harry_Shout;
            harry.Poke();
            harry.Poke();
            harry.Poke();
            harry.Poke();
            // -----------------------------------
            // test coommon interface[Icomparable/Icompare] by Array.Sort
            // -----------------------------------
            Person[] people =
            {
                new Person { Name = "Simon" },
                new Person { Name = "Jenny" },
                new Person { Name = "Adam" },
                new Person { Name = "Richard" }
            };
            WriteLine("Initial list of people:");
            foreach (var person in people)
            {
                WriteLine($" {person.Name}");
            }
            WriteLine("Use Person's IComparable implementation to sort:");
            Array.Sort(people);
            foreach (var person in people)
            {
                WriteLine($" {person.Name}");
            }
            for(int idx=0;idx<people.Length;idx++){
                WriteLine($" {people[idx].Name}");
            }
            WriteLine("Use PersonComparer's IComparer implementation to sort:");
            Array.Sort(people, new PersonComparer());
            foreach (var person in people)
            {
                WriteLine($" {person.Name}");
            }
            // Defining interfaces with default implementations
            var dvdplayer = new DvdPlayer();
            dvdplayer.Play();
            dvdplayer.Pause();
            dvdplayer.Stop();
            dvdplayer.Error();
            dvdplayer.Error2();
            var odvdplayer = new DvdPlayerOld();
            odvdplayer.Play();
            odvdplayer.Pause();
            odvdplayer.Stop();
            odvdplayer.Error();
            // ----------------------------
            // test generics Type:
            // ----------------------------
            // Note the following:
            // • When instantiating an instance of a generic type, the developer must pass a
            // type parameter. In this example, we pass int as the type parameter for gt1 and
            // string as the type parameter for gt2, so wherever T appears in the GenericThing
            // class, it is replaced with int and string.
            // • When setting the Data field and passing the input parameter, the compiler
            // enforces the use of an int value, such as 42, for
            // ----------------------------
            var gt1 = new GenericThing<int>{
                Data = 42
            };
            WriteLine($"GenericThing with an integer: {gt1.Process(42)}");
            var gt2 = new GenericThing<string>();
            gt2.Data = "apple";
            WriteLine($"GenericThing with a string: {gt2.Process("apple")}");

            string number1 = "4.5";
            WriteLine("{0} squared is {1}",
            arg0: number1,
            arg1: Squarer.Square<string>(number1));
            byte number2 = 3;
            WriteLine("{0} squared is {1}",
            arg0: number2,
            arg1: Squarer.Square(number2));
        }
    }
}
