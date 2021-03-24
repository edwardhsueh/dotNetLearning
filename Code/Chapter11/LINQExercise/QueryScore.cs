using System;
using System.Collections.Generic;
using System.Linq;
namespace Edward.tryLINQ{
    class QueryScore
    {
        private static readonly int[] scores = new int[] { 90, 71, 82, 93, 75, 82  };
        public static void ListScoreOver(int lowerLimit = 80)
        {
            // Query Expression.
            IEnumerable<int> scoreQuery = //query variable
                from score in scores //required
                where score > lowerLimit // optional
                orderby score descending // optional
                select score; //must end with select or group
            // Execute the query to produce the results
            var scoreResult = scoreQuery.ToList();
            var scoreCount = scoreResult.Count;

            // Execute the query.
            Console.WriteLine("** scoreCount:{0}",scoreCount);
            Console.WriteLine("** ListScoreOver({0})",lowerLimit);
            foreach (int i in scoreResult)
            {
                Console.WriteLine(i + " ");
            }
        }
        public static void ListScoreOverSort(int sortType, int lowerLimit = 80)
        {
            // Specify the data source.
            // Define the query expression.
            IEnumerable<int> highScoresQuery = null;
            if(sortType == 1){
                highScoresQuery =
                    from score in scores
                    where score > lowerLimit
                    orderby score descending
                    select score;

            }
            else {
                highScoresQuery =
                    from score in scores
                    where score > lowerLimit
                    orderby score ascending
                    select score;
            }

            Console.WriteLine("** ListScoreOverSort({0})", sortType);
            // Execute the query.
            foreach (int i in highScoresQuery)
            {
                Console.WriteLine(i + " ");
            }
        }
        public static void ListScoreOverTransform(int lowerLimit = 80)
        {
            // Specify the data source.
            // Define the query expression.
            IEnumerable<string> highScoresQuery2 =
                from score in scores
                where score > lowerLimit
                orderby score descending
                select $"The score is {score}";

            // Execute the query.
            Console.WriteLine("** ListScoreOverTransform({0})",lowerLimit);
            foreach (string i in highScoresQuery2)
            {
                Console.WriteLine(i + " ");
            }
        }

    }
}
