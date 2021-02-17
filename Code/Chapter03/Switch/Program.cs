using System;
using static System.Console;
using System.IO;
namespace Switch
{
    class Program
    {
        static void Main(string[] args)
        {
            // string path = "/Users/markjprice/Code/Chapter03";
            string path = @"F:\github\dotNetLearning\Code\Chapter03";
            Write("Press R for readonly or W for write: ");
            ConsoleKeyInfo key = ReadKey();
            WriteLine();
            Stream s = null;
            if (key.Key == ConsoleKey.R)
            {
                s = File.Open(
                Path.Combine(path, "file.txt"),
                FileMode.OpenOrCreate,
                FileAccess.Read);
            }
            else
            {
                s = File.Open(
                Path.Combine(path, "file.txt"),
                FileMode.OpenOrCreate,
                FileAccess.Write);
            }
            string message = string.Empty;
            WriteLine($"s={s}");
            switch (s)
            {
                case FileStream fs when fs.CanWrite:
                    message = $"The stream is a file that I can write to.{fs.CanWrite}";
                    break;
                case FileStream when (s.CanWrite == false):
                    message = $"The stream is a read-only file.{s.CanWrite}";
                    break;
                case MemoryStream ms:
                    message = "The stream is a memory address.";
                    break;
                default: // always evaluated last despite its current position
                    message = "The stream is some other type.";
                    break;
                case null:
                    message = "The stream is null.";
                    break;
            }
            WriteLine(message);

            string[] names = { "Adam", "Barry", "Charlie" };
            foreach (string name in names)
            {
                WriteLine($"{name} has {name.Length} characters.");
            }
            for (int y = 1; y <= 10; y++)
            {
                WriteLine(y);
            }
        }
    }
}
