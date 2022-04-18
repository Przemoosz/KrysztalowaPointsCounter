using System;
using System.Diagnostics;

namespace KrysztalowaKreda
{
    internal sealed class Program
    {
        public static void Main(string[] args)
        {
            
            Stopwatch timer = new Stopwatch();
            Console.WriteLine("Hello There");
            KrysztalowaKredaSolution solution = KrysztalowaKredaSolution.GetInstance();
            // timer.Start();
            // solution.Calculate();
            // timer.Stop();
            // Console.WriteLine($"Elapsed time in one thread -- {timer.ElapsedMilliseconds}ms");
            // timer.Reset();
            // solution.ResetDict();
            timer.Start();
            solution.CalculateWithMultiThreading();
            timer.Stop();
            Console.WriteLine($"Elapsed time in multi thread -- {timer.ElapsedMilliseconds}ms");
            
            solution.ExportDataResult();
            Console.WriteLine("Program finished - check cwd/result.txt for result");
            Console.WriteLine("Type enter to close program");
            Console.ReadKey();
        }
    }
}