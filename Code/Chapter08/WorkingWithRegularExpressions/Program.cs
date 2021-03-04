using System;
using System.Text.RegularExpressions;
using static System.Console;
namespace WorkingWithRegularExpressions
{
    class Program
    {
        static void Main(string[] args)
        {
            // regular expression should use verbatim string
            // Checking for digits entered as text
            Write("Enter your age: ");
            string input = ReadLine();
            var ageChecker = new Regex(@"^\d{1,4}$");
            if (ageChecker.IsMatch(input))
            {
                WriteLine("Thank you!");
            }
            else
            {
                WriteLine($"This is not a valid age: {input}");
            }
            WriteLine("Test regular expression for splitting:");
            string films = "\"M1onsters, Inc.\",\"I, Tonya\",\"Lock, Stock and Two Smoking Barrels\"";
            string[] filmsDumb = films.Split(',');
            WriteLine("Dumb attempt at splitting:");
            foreach (string film in filmsDumb)
            {
                WriteLine(film);
            }
            var csv = new Regex(
            "(?:^|,)(?=[^\"]|(\")?)\"?((?(1)[^\"]*|[^,\"]*))\"?(?=,|$)");
            MatchCollection filmsSmart = csv.Matches(films);
            WriteLine("Smart attempt at splitting:");
            foreach (Match film in filmsSmart)
            {
                WriteLine(film.Groups[2].Value);
            }
        }
    }
}
