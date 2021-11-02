using System;

namespace MetodaLuiCebisev
{
    class Program
    {
        static decimal a, b;
        static Func<decimal, decimal> F, DF, DDF;

        static void Main(string[] args)
        {
            Ex1();
        }

        static void Ex1()
        {
            a = 1; b = 2;
            F = x => x * x * x - x - 1;
            DF = x => 3 * x * x - 1;
            DDF = x => 6 * x;

            Console.WriteLine("\t\tf(x) = x^3 - x - 1");
            Console.WriteLine("\t\tf'(x) = 3x^2 - 1");
            Console.WriteLine("\t\tf''(x) = 6x");
            MetodaLuiCebisev(1e-4M);
            MetodaLuiCebisev(1e-8M);
            MetodaLuiCebisev(1e-28M);
        }

        static void MetodaLuiCebisev(decimal eps)
        {
            decimal xn_1 = F(a) * DDF(a) > 0 ? a : b;
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
                - f_xn_1 * f_xn_1 * DDF(xn_1)
                / (2 * df_xn_1 * df_xn_1 * df_xn_1);
            return xn;
        }
    }
}
