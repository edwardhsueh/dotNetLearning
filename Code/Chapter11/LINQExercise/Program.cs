using Edward.tryLINQ;
namespace LINQExercise
{


    class Program
    {
        static void Main(string[] args)
        {
            // QueryScore.ListScoreOver(80);
            // QueryScore.ListScoreOverSort(1, 2);
            // QueryScore.ListScoreOverSort(2, 90);
            // QueryScore.ListScoreOverTransform(2);
            // testLINQFrom.CompoundFrom();
            // testLINQFrom.CrossJoin();
            // testLINQGroup.GroupByChar();
            // testLINQGroup.GroupByBool();

            JoinDemonstration app = new JoinDemonstration();

            app.InnerJoin();
            app.GroupJoin();
            app.GroupInnerJoin();
            app.GroupJoin3();
            // app.LeftOuterJoin();
            // app.LeftOuterJoin2();
            app.InnerJoinAndUpdate();

            // LeftJoinDemonstration ljApp = new LeftJoinDemonstration();
            // ljApp.LeftOuterJoin();


        }
    }
}
