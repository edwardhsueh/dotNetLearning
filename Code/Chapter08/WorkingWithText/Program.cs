using System;
using static System.Console;
#nullable disable
namespace WorkingWithText
{
    class Program
    {
        static void Main(string[] args)
        {
            string city = "London";
            WriteLine($"{city} is {city.Length} characters long.");
            // Get Character out of string
            WriteLine($"First char is {city[0]} and third is {city[2]}.");
            // Split a string and recombine
            WriteLine("** Test Split/Join");
            string cities = "Paris,Berlin,Madrid,New York";
            string[] citiesArray = cities.Split(',');
            WriteLine($"** Tested Splite String:{cities}");
            foreach (string item in citiesArray)
            {
                WriteLine(item);
            }
            string recombined = string.Join(" => ", citiesArray);
            WriteLine(recombined);
            // Test IndexOf and SubString
            WriteLine("** Test IndexOf/SubString");
            string fullName = "Alan Jones";
            int indexOfTheSpace = fullName.IndexOf(' ');
            string firstName = fullName.Substring(
            startIndex: 0, length: indexOfTheSpace);
            string lastName = fullName.Substring(
            startIndex: indexOfTheSpace + 1);
            WriteLine($"fullName: {fullName}");
            WriteLine($"{lastName}, {firstName}");
            fullName = "Jones, Alan";
            indexOfTheSpace = fullName.IndexOf(',');
            firstName = fullName.Substring(
            startIndex: 0, length: indexOfTheSpace);
            lastName = fullName.Substring(
            startIndex: indexOfTheSpace+2);
            WriteLine($"fullName: {fullName}");
            WriteLine($"{lastName} {firstName}");
            // Test
            WriteLine("** Test StartsWith/EndsWith/Constains");
            string company = "Microsoft";
            bool startsWithM = company.StartsWith("M");
            bool startsWithT = company.StartsWith("T");
            bool endsWitht = company.EndsWith("t");
            bool containsN = company.Contains("N");
            WriteLine($"Starts with M: {startsWithM}, Starts with T: {startsWithT}, Ends with t: {endsWitht}, contains an N: {containsN}");
            string aaa = null;
            bool aaaIsNull = (aaa == null);
            bool companyIsNull = (company == null);
            WriteLine($"String aaa[{aaa}] is null?{aaaIsNull}");
            WriteLine($"String Microsoft[{company}] is null?{companyIsNull}");
        }
    }
}
