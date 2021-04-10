using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;
namespace WorkingWithTasks
{
    class Program
    {
        static void MethodA()
        {
            WriteLine("Starting Method A...");
            Thread.Sleep(3000); // simulate three seconds of work
            WriteLine("Finished Method A.");
        }
        static void MethodB()
        {
            WriteLine("Starting Method B...");
            Thread.Sleep(2000); // simulate two seconds of work
            WriteLine("Finished Method B.");
        }
        static void MethodC()
        {
            WriteLine("Starting Method C...");
            Thread.Sleep(1000); // simulate one second of work
            WriteLine("Finished Method C.");
        }
        static void Main(string[] args)
        {
            // var timer = Stopwatch.StartNew();
            Stopwatch timer = new Stopwatch();
            timer.Restart();
            WriteLine("Running methods synchronously on one thread.");
            MethodA();
            MethodB();
            MethodC();
            timer.Stop();
            WriteLine($"{timer.ElapsedMilliseconds:#,##0}ms elapsed.");
            timer.Restart();
            WriteLine("Running methods asynchronously on multiple threads.");
            Task taskA = new Task(MethodA);
            taskA.Start();
            Task taskB = Task.Factory.StartNew(MethodB);
            Task taskC = Task.Run(new Action(MethodC));
            // timer.Stop();
            // WriteLine($"{timer.ElapsedMilliseconds:#,##0}ms elapsed.");
            Task[] tasks = { taskA, taskB, taskC };
            Task.WaitAll(tasks);
            timer.Stop();
            WriteLine($"{timer.ElapsedMilliseconds:#,##0}ms elapsed.");
        }
    }
}
