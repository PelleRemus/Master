using System;

namespace AlgoritmulRungeKuttaOrd3_8
{
    class Program
    {
        static decimal x0, y0, t;
        static Func<decimal, decimal, decimal> F;
        static Func<decimal, decimal> Z;

        static void Main(string[] args)
        {
            Ex1();
        }

        static void Ex1()
        {
            F = (x, y) => y + x;
            Z = (x) => 2 * (decimal)Math.Pow(Math.E, (double)x) - x - 1;
            t = 1;

            Console.WriteLine("\t\ty' = y + x, x ∈ [0, 1]");
            Console.WriteLine("\t\t y'(0) = 1");
            Calls(0, 1, 10);
        }

        static void Calls(decimal x, decimal y, int n)
        {
            x0 = x; y0 = y;
            RungeKutta3_8(n);
        }

        static void RungeKutta3_8(int n)
        {
            decimal h = t / n;
            decimal xi_1, yi_1, xi = x0, yi = y0;
            decimal k1i, k2i, k3i, k4i;

            for (int i = 1; i <= n; i++)
            {
                xi_1 = xi; yi_1 = yi;
                xi = x0 + h * i;

                k1i = F(xi_1, yi_1);
                k2i = F(xi_1 + h / 3, yi_1 + h * k1i / 3);
                k3i = F(xi_1 + 2 * h / 3, yi_1 + h * (-k1i / 3 + k2i));
                k4i = F(xi_1 + h, yi_1 + h * (k1i - k2i + k3i));

                yi = yi_1 + h * (k1i + 3 * k2i + 3 * k3i + k4i) / 8;
                Console.WriteLine("x[{0}] = {1}, y[{0}] = {2}, z[{0}] = {3}", i, xi, yi, Z(xi));
            }
        }
    }
}
