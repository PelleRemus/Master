using System;

namespace AlgoritmEuler
{
    class Program
    {
        static decimal x0, y0, t;
        static Func<decimal, decimal, decimal> F;
        static Func<decimal, decimal> Z;

        static void Main(string[] args)
        {
            Ex1();
            Console.WriteLine();
            Ex2();
        }

        static void Ex1()
        {
            F = (x, y) => x * x - y;
            Z = (x) => x * x - 2 * x + 2 - (decimal)Math.Pow(Math.E, -(double)x);
            t = 4;

            Console.WriteLine("\t\ty' = x^2 - y, x ∈ [0, 4]");
            Console.WriteLine("\t\t  y(0) = 1");
            Calls(0, 1);
        }

        static void Ex2()
        {
            F = (x, y) => y + x;
            Z = (x) => 2 * (decimal)Math.Pow(Math.E, (double)x) - x - 1;
            t = 1;

            Console.WriteLine("\t\ty' = y + x, x ∈ [0, 1]");
            Console.WriteLine("\t\t y(0) = 1");
            Calls(0, 1);
        }

        static void Calls(decimal x, decimal y)
        {
            x0 = x; y0 = y;
            AlgoritmEuler(16);
        }

        static void AlgoritmEuler(int n)
        {
            decimal h = t / (decimal)n;
            decimal xi_1, yi_1, xi = x0, yi = y0;

            for (int i = 1; i <= n; i++)
            {
                xi_1 = xi; yi_1 = yi;
                xi = x0 + h * i;
                yi = yi_1 + h * F(xi_1, yi_1);
                Console.WriteLine("x[{0}] = {1}, y[{0}] = {2}, z[{0}] = {3}", i, xi, yi, Z(xi));
            }
        }
    }
}
