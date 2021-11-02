using System;

namespace MetodaSecantei
{
    class Program
    {
        static decimal x0, x1;
        static Func<decimal, decimal> F;

        static void Main(string[] args)
        {
            Ex1();
            Console.WriteLine();
            Ex2();
        }

        static void Ex1()
        {
            x0 = 2;
            x1 = 1.4M;
            F = x => x * x - 2;

            Console.WriteLine("\t\tf(x) = x^2 - 2");
            Calls();
        }

        static void Ex2()
        {
            x0 = 2;
            x1 = 1.5M;
            F = x => x * x * x - x - 1;

            Console.WriteLine("\t\tf(x) = x^3 - x - 1");
            Calls();
        }

        static void Calls()
        {
            MetodaSecantei(1e-4M);
            MetodaSecantei(1e-8M);
            MetodaSecantei(1e-12M);
        }

        static void MetodaSecantei(decimal eps)
        {
            decimal xn_2 = x0;
            decimal xn_1 = x1;
            decimal xn = 0;
            int n = 0;
            do
            {
                n++;
                xn = xn_1 - F(xn_1) * (xn_1 - xn_2) 
                          / (F(xn_1) - F(xn_2));
                xn_2 = xn_1;
                xn_1 = xn;

            } while (Math.Abs(xn_1 - xn_2) >= eps);

            Console.WriteLine("n = {0} iteratii: xn = {1} (epsilon = {2})", n, xn, eps);
        }
    }
}
