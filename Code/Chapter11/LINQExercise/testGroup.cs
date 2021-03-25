using System;
using System.Collections.Generic;
using System.Linq;

namespace Edward.tryLINQ{
    class testLINQGroup
    {
        // The element type of the data source.
        public class Student
        {
            public string First { get; set; }
            public string Last { get; set; }
            public int ID { get; set; }
            public List<int> Scores;
        }

        private static readonly List<Student> students = new List<Student>
        {
            new Student {First="Svetlana", Last="Omelchenko", ID=111, Scores= new List<int> {97, 72, 81, 60}},
            new Student {First="Claire", Last="O'Donnell", ID=112, Scores= new List<int> {75, 84, 91, 39}},
            new Student {First="Sven", Last="Mortensen", ID=113, Scores= new List<int> {99, 89, 91, 95}},
            new Student {First="Cesar", Last="Garcia", ID=114, Scores= new List<int> {72, 81, 65, 84}},
            new Student {First="Debra", Last="Garcia", ID=115, Scores= new List<int> {97, 89, 85, 82}}
        };
        private static char[] upperCase = { 'A', 'B', 'C' };
        private static char[] lowerCase = { 'x', 'y', 'z' };
        public static void GroupByChar()
        {
            // Group students by the first letter of their last name
            // Query variable is an IEnumerable<IGrouping<char, Student>>
            var studentQuery2 =
                from student in students
                group student by student.Last[0] into g
                orderby g.Key
                select g;
            // Execute the queries.
            Console.WriteLine("Group:");
            // Iterate group items with a nested foreach. This IGrouping encapsulates
            // a sequence of Student objects, and a Key of type char.
            // For convenience, var can also be used in the foreach statement.
            foreach (IGrouping<char, Student> studentGroup in studentQuery2)
            {
                Console.WriteLine(studentGroup.Key);
                // Explicit type for student could also be used here.
                foreach (var student in studentGroup)
                {
                    Console.WriteLine("   {0}, {1}", student.Last, student.First);
                }
            }
        }
        public static void GroupByBool()
        {
            // Group by true or false.
            // Query variable is an IEnumerable<IGrouping<bool, Student>>
            var booleanGroupQuery =
                from student in students
                group student by student.Scores.Average() >= 80; //pass or fail!

            // Execute the query and access items in each group
            foreach (var studentGroup in booleanGroupQuery)
            {
                Console.WriteLine(studentGroup.Key == true ? "High averages" : "Low averages");
                foreach (var student in studentGroup)
                {
                    Console.WriteLine("   {0}, {1}:{2}", student.Last, student.First, student.Scores.Average());
                }
            }

        }


    }
}
