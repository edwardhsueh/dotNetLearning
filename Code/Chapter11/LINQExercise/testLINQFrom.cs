using System;
using System.Collections.Generic;
using System.Linq;

namespace Edward.tryLINQ{
    class testLINQFrom
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
        public static void CompoundFrom()
        {

            // Use a collection initializer to create the data source. Note that
            // each element in the list contains an inner sequence of scores.

            // Use a compound from to access the inner sequence within each element.
            // Note the similarity to a nested foreach statement.
            var scoreQuery = from student in students
                             from score in student.Scores
                                where score > 90
                                select new { Last = student.Last, score };

            // Execute the queries.
            Console.WriteLine("scoreQuery:");
            // Rest the mouse pointer on scoreQuery in the following line to
            // see its type. The type is IEnumerable<'a>, where 'a is an
            // anonymous type defined as new {string Last, int score}. That is,
            // each instance of this anonymous type has two members, a string
            // (Last) and an int (score).
            foreach (var student in scoreQuery)
            {
                Console.WriteLine("{0} Score: {1}", student.Last, student.score);
            }

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
        public static void  CrossJoin() {
            char[] upperCase = { 'A', 'B', 'C' };
            char[] lowerCase = { 'x', 'y', 'z' };

            // The type of joinQuery1 is IEnumerable<'a>, where 'a
            // indicates an anonymous type. This anonymous type has two
            // members, upper and lower, both of type char.
            var joinQuery1 =
                from upper in upperCase
                from lower in lowerCase
                select new { upper, lower };

            // The type of joinQuery2 is IEnumerable<'a>, where 'a
            // indicates an anonymous type. This anonymous type has two
            // members, upper and lower, both of type char.
            var joinQuery2 =
                from lower in lowerCase
                where lower != 'x'
                from upper in upperCase
                select new { lower, upper };

            // Execute the queries.
            Console.WriteLine("Cross join:");
            // Rest the mouse pointer on joinQuery1 to verify its type.
            foreach (var pair in joinQuery1)
            {
                Console.WriteLine("{0} is matched to {1}", pair.upper, pair.lower);
            }

            Console.WriteLine("Filtered non-equijoin:");
            // Rest the mouse pointer over joinQuery2 to verify its type.
            foreach (var pair in joinQuery2)
            {
                Console.WriteLine("{0} is matched to {1}", pair.lower, pair.upper);
            }

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

    }
}
