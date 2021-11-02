using System;

namespace AproxRadicalilorPatratici
{
    class Program
    {
        static decimal a;

        static void Main(string[] args)
        {
            Ex1();
            Console.WriteLine();
            Ex2();
        }

        static void Ex1()
        {
            a = 2;
            Console.WriteLine("\t\t√2", a);
            Calls();
        }

        static void Ex2()
        {
            a = 0.5M;
            Console.WriteLine("\t\t√(1/2)");
            Calls();
        }

        static void Calls()
        {
            AproxRadicalilorPatratici(1e-4M);
            AproxRadicalilorPatratici(1e-8M);
            AproxRadicalilorPatratici(1e-12M);
        }

        static void AproxRadicalilorPatratici(decimal eps)
        {
            decimal xn_1 = a > 1 ? a : 1;
            decimal xn = CalculateXN(xn_1);
            int n = 0;

            while (Math.Abs(xn - xn_1) >= eps)
            {
                n++;
                xn_1 = xn;
                xn = CalculateXN(xn_1);
            }
            Console.WriteLine("n = {0} iteratii: x[n+1] = {1} (epsilon = {2})", n, xn, eps);
        }

        static decimal CalculateXN(decimal xn_1)
        {
            return xn_1 * (xn_1 * xn_1 + 3 * a)
                / (3 * xn_1 * xn_1 + a);
        }
    }
}
