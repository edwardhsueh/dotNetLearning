using System;

namespace Arrays
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] names; // can reference any array of strings
            // allocating memory for four strings in an array
            names = new string[4];
            // storing items at index positions
            names[0] = "Kate";
            names[1] = "Jack";
            names[2] = "Rebecca";
            names[3] = "Tom";
            // looping through the names
            for (int i = 0; i < names.Length; i++)
            {
                // output the item at index position i
                Console.WriteLine(names[i]);        
            }
            int[] intArray = new int[4]; // can reference any array of strings
            // storing items at index positions
            intArray[0] = 5;
            intArray[1] = 7;
            intArray[2] = 11;
            intArray[3] = 9;
            // looping through the names
            for (int i = 0; i < intArray.Length; i++)
            {
                // output the item at index position i
                Console.WriteLine(intArray[i]);        
            }            
        }
    }
}
