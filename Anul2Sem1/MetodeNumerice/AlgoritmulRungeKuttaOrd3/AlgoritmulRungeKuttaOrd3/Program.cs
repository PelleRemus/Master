using System;

namespace AlgoritmulRungeKuttaOrd3
{
    class Program
    {
        static decimal x0, y0, t;
        static Func<decimal, decimal, decimal> F;
        static Func<decimal, decimal> Z, G;

        static void Main(string[] args)
        {
            Ex1();
            Console.WriteLine();
            Ex2();
            Console.WriteLine();
            Ex3();
        }

        static void Ex1()
        {
            F = (x, y) => y + x;
            Z = (x) => 2 * (decimal)Math.Pow(Math.E, (double)x) - x - 1;
            t = 1;

            Console.WriteLine("\t\ty' = y + x, x ∈ [0, 1]");
            Console.WriteLine("\t\t y(0) = 1");
            Calls(0, 1, true);
        }

        static void Ex2()
        {
            G = (x) => 1.0M / (x + 1);
            Z = (x) => (decimal)Math.Log(2);
            t = 1;
            Calls(0, 0);
        }

        static void Ex3()
        {
            G = (x) => 4.0M / (x * x + 1);
            Z = (x) => (decimal)Math.PI;
            t = 1;
            Calls(0, 0);
        }

        static void Calls(decimal x, decimal y, bool shouldUseF = false)
        {
            x0 = x; y0 = y;
            RungeKutta3(10, shouldUseF);
        }

        static void RungeKutta3(int n, bool shouldUseF)
        {
            decimal h = t / (decimal)n;
            decimal xi_1, yi_1, xi = x0, yi = y0;
            decimal k1i, k2i, k3i;

            for (int i = 1; i <= n; i++)
            {
                xi_1 = xi; yi_1 = yi;
                xi = x0 + h * i;

                if (shouldUseF)
                {
                    k1i = h * F(xi_1, yi_1);
                    k2i = h * F(xi_1 + h / 2, yi_1 + k1i / 2);
                    k3i = h * F(xi_1 + h, yi_1 + k1i);
                }
                else
                {
                    k1i = h * G(xi_1);
                    k2i = h * G(xi_1 + h / 2);
                    k3i = h * G(xi_1 + h);
                }

                yi = yi_1 + (k1i + 4 * k2i + k3i) / 6;
                Console.WriteLine("x[{0}] = {1}, y[{0}] = {2}, z[{0}] = {3}", i, xi, yi, Z(xi));
            }
        }
    }
}