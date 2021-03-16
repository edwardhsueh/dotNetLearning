using System; // DateTime
using System.Collections.Generic; // List<T>, HashSet<T>
using System.Xml.Serialization; // XmlSerializer
using System.IO; // FileStream
using Packt.Shared; // Person
using static System.Console;
using static System.Environment;
using static System.IO.Path;
using System.Threading.Tasks;
using System.Text.Json;
namespace WorkingWithSerialization
{
    class Program
    {
        static async Task Main(string[] args)
        {
            List<int> numbers = null;
            int? a = null;

            (numbers ??= new List<int>()).Add(5);
            Console.WriteLine(string.Join(" ", numbers));  // output: 5

            numbers.Add(a ??= 0);
            Console.WriteLine(string.Join(" ", numbers));  // output: 5 0
            Console.WriteLine(a);  // output: 0
            var child1 = new Person(0M) {
                        FirstName = "Sally",
                        LastName = "Cox",
                        DateOfBirth = new DateTime(2000, 7, 12)
                        };
            var child2 = new Person(0M) {
                        FirstName = "John",
                        LastName = "Yang",
                        DateOfBirth = new DateTime(2000, 7, 12)
                        };
            var people = new List<Person>
            {
                new Person(30000M) {
                    FirstName = "Alice",
                    LastName = "Smith",
                    DateOfBirth = new DateTime(1974, 3, 14)
                },
                new Person(40000M) {
                    FirstName = "Bob",
                    LastName = "Jones",
                    DateOfBirth = new DateTime(1969, 11, 23)
                },
                new Person(20000M) {
                    FirstName = "Charlie",
                    LastName = "Cox",
                    DateOfBirth = new DateTime(1984, 5, 4),
                    Children = new HashSet<Person>
                    { child1,
                      child2,

                    }
                }
            };
            // create object that will format a List of Persons as XML
            var xs = new XmlSerializer(typeof(List<Person>));
            // create a file to write to
            string path = Combine(CurrentDirectory, "people.xml");
            using (FileStream stream = File.Create(path))
            {
                // serialize the object graph to the stream
                xs.Serialize(stream, people);
            }
            WriteLine("Written {0:N0} bytes of XML to {1}",
            arg0: new FileInfo(path).Length,
            arg1: path);
            WriteLine();
            // Display the serialized object graph
            WriteLine(File.ReadAllText(path));

            using (FileStream xmlLoad = File.Open(path, FileMode.Open))
            {
                // deserialize and cast the object graph into a List of Person
                var loadedPeople = (List<Person>)xs.Deserialize(xmlLoad);
                foreach (var item in loadedPeople)
                {
                    WriteLine("{0} has {1} children.",
                    item.LastName, item.Children.Count);
                }
            }
            string jsonPath = Combine(CurrentDirectory, "people.json");
            WriteLine("---------------------------------------------");
            WriteLine("object to/from JSON string in File{0}", jsonPath);
            WriteLine("---------------------------------------------");

            // ----------------------------------------------------------------
            //  using System.Text.Json.SerializeAsync to serial object to file
            // ----------------------------------------------------------------
            using (FileStream jsonWrite = File.Create(jsonPath) )
            {
                await JsonSerializer.SerializeAsync(
                utf8Json : jsonWrite,
                value: people,
                inputType: typeof(List<Person>));
            }
            WriteLine();
            // Display the serialized object graph
            WriteLine(File.ReadAllText(jsonPath));
            // ----------------------------------------------------------------
            //  using System.Text.Json.DeserializeAsync from file to object
            // ----------------------------------------------------------------
            using (FileStream jsonLoad = File.Open(
            jsonPath, FileMode.Open))
            {
            // deserialize object graph into a List of Person
                var loadedPeople = (List<Person>)
                    await JsonSerializer.DeserializeAsync(
                    utf8Json: jsonLoad,
                    returnType: typeof(List<Person>));
                foreach (var item in loadedPeople)
                {
                    WriteLine("{0} has {1} children.",
                    item.LastName, item.Children?.Count ?? 0);
                }
            }
            // ----------------------------------------------------------------
            //  using System.Text.Json.Serialize to serial object to string
            // ----------------------------------------------------------------
            WriteLine("---------------------------------------------");
            WriteLine("object to/from JSON string");
            WriteLine("---------------------------------------------");
            var jsonString = JsonSerializer.Serialize(
                                value: people,
                                inputType: typeof(List<Person>));
            WriteLine("jsonString");
            // Display the serialized object graph
            WriteLine(jsonString);
            var jsonStringPerson = (List<Person>)
                JsonSerializer.Deserialize(
                json: jsonString,
                returnType: typeof(List<Person>));
            foreach (var item in jsonStringPerson)
            {
                WriteLine("{0} has {1} children.",
                item.LastName, item.Children?.Count ?? 0);
            }


        }
    }
}
