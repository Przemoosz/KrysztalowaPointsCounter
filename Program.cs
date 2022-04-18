using System;

namespace KrysztalowaKreda
{
    internal sealed class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello There");
            KrysztalowaKredaSolution solution = KrysztalowaKredaSolution.GetInstance();
            solution.Calculate();
        }
    }
}