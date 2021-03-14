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
            // Test Casting
            // Person(base) casting to Employee(sub Class) is not ok
            int a = 100;
            WriteLine("INT type:{0}", a.GetType().Name);
            Person edwardInPerson = new Person { Name = "Edward" };
            if(edwardInPerson is Employee){
                Employee edwardInEmployee = (Employee)edwardInPerson;
            }
            else{
                WriteLine("EdwardInPerson is Not Employee, is {0}", edwardInPerson.GetType());
            }
            Employee YvonneInEmployee = new Employee{
                Name = "Alice", EmployeeCode = "AA123"
            };
            if(YvonneInEmployee is Employee){
                Person YvonneInPerson = YvonneInEmployee; // Employee casting to Person is ok
                WriteLine("YvonneInPerson is {0}", YvonneInPerson.GetType());
                WriteLine("YvonneInEmployee is {0}", YvonneInEmployee.GetType());
                if(YvonneInPerson is Employee){
                    WriteLine("YvonneInPerson is actually Employee!");
                    Employee explicitYvonneInPersonToEmployee = (Employee) YvonneInPerson;
                }
            }
            else{
                WriteLine("YvonneInEmployee is Not Employee, is {0}", edwardInPerson.ToString());
            }


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
            // ----------------------------------
            // test struct
            // ----------------------------------
            var dv1 = new DisplacementVector(3, 5);
            var dv2 = new DisplacementVector(-2, 7);
            var dv3 = dv1 + dv2;
            WriteLine($"({dv1.X}, {dv1.Y}) + ({dv2.X}, {dv2.Y}) = ({dv3.X},{dv3.Y})");
            // ----------------------------------
            // test  Inheriting from classes
            // ----------------------------------
            Employee john = new Employee{
                Name = "John Jones",
                DateOfBirth = new DateTime(1990, 7, 28)
            };
            WriteLine(format:"employee {0}'s birthdate is {1:dddd,dd/MM/yy}", john.Name, john.DateOfBirth);
            john.EmployeeCode = "JJ001";
            john.HireDate = new DateTime(2014, 11, 23);
            WriteLine($"{john.Name} was hired on {john.HireDate:dd/MM/yy}");
            john.WriteToConsole();

            WriteLine("==============================");
            WriteLine("Testing polymorphic");
            WriteLine("==============================");
            Person pMike = new Person();
            Employee aliceInEmployee = new Employee{
                Name = "Alice", EmployeeCode = "AA123"
            };

            // implicit casting
            Person aliceInPerson = aliceInEmployee;
            // When a method is hidden with new, the compiler is not smart enough to know that the object is an Employee, so it calls the WriteToConsole method in Person.
            aliceInEmployee.WriteToConsole();
            aliceInPerson.WriteToConsole();
            // When a method is overridden with virtual and override, the compiler is smart enough to know that although the variable is declared as a Person class, the object itself is an Employee class and, therefore, the Employee implementation of ToString is called.
            WriteLine($"Person ToString():{pMike.ToString()}");
            WriteLine($"aliceInEmployee ToString():{aliceInEmployee.ToString()}");
            WriteLine($"aliceInPerson ToString():{aliceInPerson.ToString()}");
            WriteLine("==============================");
            WriteLine("Testing casting");
            WriteLine("==============================");
            // checking type before casting using "is"
            if (aliceInPerson is Employee)
            {
                WriteLine($"{nameof(aliceInPerson)} IS an Employee");
                Employee explicitAlice = (Employee) aliceInPerson;
                explicitAlice.WriteToConsole();
            }
            else {
                WriteLine($"{nameof(pMike)} is NOT an Employee");
            }
            // checking type before casting  using "is"
            if (pMike is Employee)
            {
                WriteLine($"{nameof(pMike)} IS an Employee");
                Employee explicitMike = (Employee) pMike;
                explicitMike.WriteToConsole();
            }
            else {
                WriteLine($"{nameof(pMike)} is NOT an Employee");
            }
            // checking type before casting  using "as"
            WriteLine("*** checking type before casting  using 'is'");

            Employee aliceAsEmployee = aliceInPerson as Employee;
            if (aliceAsEmployee != null)
            {
                WriteLine($"{nameof(aliceInPerson)} AS an Employee");
            }
            else
            {
                WriteLine($"{nameof(aliceInPerson)} NOT as an Employee");
            }
            // checking type before casting  using "as"
            WriteLine("*** checking type before casting  using 'as'");
            Employee mikeAsEmployee = pMike as Employee;
            if (mikeAsEmployee != null)
            {
                WriteLine($"{nameof(pMike)} AS an Employee");
            }
            else
            {
                WriteLine($"{nameof(pMike)} NOT as an Employee");
            }
            try
            {
                john.TimeTravel(new DateTime(1999, 12, 31));
                john.TimeTravel(new DateTime(1950, 12, 25));
            }
            catch (PersonException ex)
            {
                WriteLine(ex.Message);
            }
            try
            {
                john.TimeTravel2(new DateTime(1999, 12, 31));
                john.TimeTravel2(new DateTime(1950, 12, 25));
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }
            WriteLine("===================================");
            WriteLine("Testing Extending types when you can't inherit");
            WriteLine("===================================");
            string email1 = "pamela@test.com";
            string email2 = "ian&test.com";
            WriteLine("*** Using static methods to reuse functionality");
            WriteLine(format:"{0} is a valid e-mail address: {1}",
            arg0: email1,
            arg1: StringExtensions.IsValidEmail(email1));
            WriteLine(format: "{0} is a valid e-mail address: {1}",
            arg0: email2,
            arg1: StringExtensions.IsValidEmail(email2));
            WriteLine("*** Using extension methods to reuse functionality");
            WriteLine(format:"{0} is a valid e-mail address: {1}",
            arg0: email1,
            arg1: email1.IsValidEmail());
            WriteLine(format: "{0} is a valid e-mail address: {1}",
            arg0: email2,
            arg1: email2.IsValidEmail());



        }
    }
}
