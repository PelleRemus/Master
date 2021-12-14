using System;

namespace AlgoritmulRungeKuttaOrd4
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
            F = (x, y) => y + x;
            Z = (x) => 2 * (decimal)Math.Pow(Math.E, (double)x) - x - 1;
            t = 1;

            Console.WriteLine("\t\ty' = y + x, x ∈ [0, 1]");
            Console.WriteLine("\t\t y'(0) = 1");
            Calls(0, 1, 100);
        }

        static void Ex2()
        {
            F = (x, y) => 1M / (1 + x * x) - 2 * y * y;
            Z = (x) => x / (1 + x * x);
            t = 4;

            Console.WriteLine("\t\ty' = 1 / (1 + x^2), x ∈ [0, 4]");
            Console.WriteLine("\t\t    y'(0) = 0");
            Calls(0, 0, 40);
        }

        static void Calls(decimal x, decimal y, int n)
        {
            x0 = x; y0 = y;
            RungeKutta4(n);
        }

        static void RungeKutta4(int n)
        {
            decimal h = t / n;
            decimal xi_1, yi_1, xi = x0, yi = y0;
            decimal k1i, k2i, k3i, k4i;

            for (int i = 1; i <= n; i++)
            {
                xi_1 = xi; yi_1 = yi;
                xi = x0 + h * i;

                k1i = h * F(xi_1, yi_1);
                k2i = h * F(xi_1 + h / 2, yi_1 + k1i / 2);
                k3i = h * F(xi_1 + h / 2, yi_1 + k2i / 2);
                k4i = h * F(xi_1 + h, yi_1 + k3i);

                yi = yi_1 + (k1i + 2 * k2i + 2 * k3i + k4i) / 6;
                Console.WriteLine("{1}\t{2}\t{3}\t{4}", i, xi, yi, Z(xi), yi-Z(xi));
            }
        }
    }
}
