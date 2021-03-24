using Edward.tryLINQ;
namespace LINQExercise
{


    class Program
    {
        static void Main(string[] args)
        {
            QueryScore.ListScoreOver(80);
            // QueryScore.ListScoreOverSort(1, 2);
            // QueryScore.ListScoreOverSort(2, 90);
            // QueryScore.ListScoreOverTransform(2);
            testLINQFrom.CompoundFrom();
            testLINQFrom.GroupByChar();
            testLINQFrom.GroupByBool();
            testLINQFrom.CrossJoin();

        }
    }
}
