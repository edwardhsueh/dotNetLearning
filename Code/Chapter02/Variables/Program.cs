using System;

namespace Variables
{
    class Program
    {
        static void Main(string[] args)
        {
            object height = 1.88; // storing a double in an object
            object name = "Amir"; // storing a string in an object
            Console.WriteLine($"{name} is {height} metres tall.");
            // int length1 = name.Length; // gives compile error!
            int length2 = ((string)name).Length; // tell compiler it is a string
            Console.WriteLine($"{name} has {length2} characters.");
            // storing a string in a dynamic object
            dynamic anotherName = "Ahmed";
            int length3 = anotherName.Length; // tell compiler it is a string
            Console.WriteLine($"{anotherName} has {length3} characters.");
        }
    }
}
