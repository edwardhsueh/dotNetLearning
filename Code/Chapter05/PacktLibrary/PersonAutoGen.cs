using System;
using System.Collections.Generic;
using static System.Console;
namespace Packt.Shared
{
    public partial class Person : object
    {
        // ----------------------------
        // Read Only Property
        // ----------------------------
        // a property defined using C# 1 - 5 syntax
        public string Origin
        {
            get
            {
                return $"{Name} was born on {HomePlanet}";
            }
        }
        // two properties defined using C# 6+ lambda expression syntax
        public string Greeting => $"{Name} says 'Hello!'";
        public string Age => $"{Name}'s Age is {System.DateTime.Today.Year - DateOfBirth.Year}";
         // ----------------------------
        // R/W Property
        // ----------------------------
        public string FavoriteIceCream { get; set; } // auto-syntax
        private string favoritePrimaryColor;
        public string FavoritePrimaryColor
        {
            get
            {
                return favoritePrimaryColor;
            }
            set
            {
                switch (value.ToLower())
                {
                    case "red":
                    case "green":
                    case "blue":
                        favoritePrimaryColor = value;
                        break;
                    default:
                        throw new System.ArgumentException(
                        $"{value} is not a primary color. " +
                        "Choose from: red, green, blue.");
                }
            }
        }        
   }
}