using System.Collections.Generic;
using System.Linq;
using System;
namespace Edward.tryLINQ{
  class LeftJoinDemonstration{
    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    class Pet
    {
        public string Name { get; set; }
        public Person Owner { get; set; }
    }
    static Person magnus = new Person { FirstName = "Magnus", LastName = "Hedlund" };
    static Person terry = new Person { FirstName = "Terry", LastName = "Adams" };
    static Person charlotte = new Person { FirstName = "Charlotte", LastName = "Weiss" };
    static Person arlene = new Person { FirstName = "Arlene", LastName = "Huff" };

    static Pet barley = new Pet { Name = "Barley", Owner = terry };
    static Pet boots = new Pet { Name = "Boots", Owner = terry };
    static Pet whiskers = new Pet { Name = "Whiskers", Owner = charlotte };
    static Pet bluemoon = new Pet { Name = "Blue Moon", Owner = terry };
    static Pet daisy = new Pet { Name = "Daisy", Owner = magnus };

    // Create two lists.
    List<Person> people = new List<Person> { magnus, terry, charlotte, arlene };
    List<Pet> pets = new List<Pet> { barley, boots, whiskers, bluemoon, daisy };

    public void LeftOuterJoin()
    {
        var query = from person in people
                    join pet in pets on person equals pet.Owner into gj
                    from subpet in gj.DefaultIfEmpty()
                    select new { person.FirstName, PetName = subpet?.Name ?? String.Empty };

        foreach (var v in query)
        {
            Console.WriteLine($"{v.FirstName+":",-15}{v.PetName}");
        }
    }

    // This code produces the following output:
    //
    // Magnus:        Daisy
    // Terry:         Barley
    // Terry:         Boots
    // Terry:         Blue Moon
    // Charlotte:     Whiskers
    // Arlene:


  }
}
