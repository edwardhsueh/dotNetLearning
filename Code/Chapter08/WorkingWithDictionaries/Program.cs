using System;
using System.Collections.Generic;
using static System.Console;
namespace WorkingWithDictionaries
{
    class Program
    {
        static void Main(string[] args)
        {
            var keywords = new Dictionary<string, string>(){
                { "byte", "unsinged 8-bit Int"},
                { "dobule", "Double precision floating point number"},
            };
            keywords.Add("int", "32-bit integer data type");
            keywords.Add("long", "64-bit integer data type");
            keywords.Add("float", "Single precision floating point number");
            WriteLine("Keywords and their definitions");
            foreach (KeyValuePair<string, string> item in keywords)
            {
                WriteLine($" {item.Key}: {item.Value}");
            }
            WriteLine($"The definition of long is {keywords["long"]}");

            keywords.Remove("byte", out string removed);
            WriteLine($"key: bytes, value:{removed} is removed");
            WriteLine("after Remove: Keywords and their definitions");
            foreach (KeyValuePair<string, string> item in keywords)
            {
                WriteLine($" {item.Key}: {item.Value}");
            }
        }
    }
}
