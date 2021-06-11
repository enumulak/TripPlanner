using System;

namespace TripPlanner
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] T = new int[7];

            int N = T.Length;

            T[0] = 1;
            T[1] = 2;
            T[2] = 3;
            T[3] = 3;
            T[4] = 2;
            T[5] = 1;
            T[6] = 4;

            TripPlanner tp = new TripPlanner();

            int[] res = tp.Solver(2, T);

            Console.WriteLine(res.Length);

            Console.ReadLine();
        }
    }
}
