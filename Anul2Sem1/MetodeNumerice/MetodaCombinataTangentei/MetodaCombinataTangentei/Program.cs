using System;

namespace MetodaCombinataTangentei
{
    class Program
    {
        static decimal a, b, x0;
        static Func<decimal, decimal> F, DF;

        static void Main(string[] args)
        {
            Ex1();
            Console.WriteLine();
            Ex2();
        }

        static void Ex1()
        {
            a = 1; b = 2;
            x0 = b;
            F = x => x * x - 2;
            DF = x => 2 * x;

            Console.WriteLine("\t\tf(x) = x^2 - 2");
            Console.WriteLine("\t\tf'(x) = 2x");
            Calls();
        }

        static void Ex2()
        {
            a = 1; b = 2;
            x0 = b;
            F = x => x * x * x - x - 1;
            DF = x => 3 * x * x - 1;

            Console.WriteLine("\t\tf(x) = x^3 - x - 1");
            Console.WriteLine("\t\tf'(x) = 3x^2 - 1");
            Calls();
        }

        static void Calls()
        {
            MetodaCombinataATangentei(1e-4M);
            MetodaCombinataATangentei(1e-8M);
            MetodaCombinataATangentei(1e-12M);
        }

        static void MetodaCombinataATangentei(decimal eps)
        {
            decimal xn_1 = x0;
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
            decimal f_xn_1 = F(xn_1);
            decimal df_xn_1 = DF(xn_1);

            decimal xn = xn_1 - f_xn_1 / df_xn_1
                - F(xn_1 - f_xn_1 / df_xn_1)
                / df_xn_1;
            return xn;
        }
    }
}
