using System;

namespace MetodaLuiSteffensen
{
    class Program
    {
        static decimal a, b, dda;
        static Func<decimal, decimal> F;

        static void Main(string[] args)
        {
            Ex1();
        }

        static void Ex1()
        {
            a = 1; b = 2;
            dda = 6;
            F = x => x * x * x - x - 1;

            Console.WriteLine("\t\tf(x) = x^3 - x - 1");
            MetodaLuiSteffensen(1e-4M);
            MetodaLuiSteffensen(1e-8M);
            MetodaLuiSteffensen(1e-12M);
        }

        static void MetodaLuiSteffensen(decimal eps)
        {
            Console.WriteLine("epsilon = {0}", eps);
            decimal xn_1 = F(a) * dda > 0 ? a : b;
            int n = 1;
            decimal xn = CalculateXN(xn_1, n);

            while (Math.Abs(xn - xn_1) >= eps)
            {
                n++;
                xn_1 = xn;
                xn = CalculateXN(xn_1, n);
            }
            Console.WriteLine("n = {0} iteratii: xn = {1}", n, xn);
            Console.WriteLine();
        }

        static decimal CalculateXN(decimal xn_1, int n)
        {
            decimal f_xn_1 = F(xn_1);
            Console.WriteLine("x[{0}] = {1}; f(x[{0}]) = {2}", n, xn_1, f_xn_1);
            decimal xn = xn_1 - f_xn_1 * f_xn_1
                              / (F(xn_1 + f_xn_1) - f_xn_1);
            return xn;
        }
    }
}
