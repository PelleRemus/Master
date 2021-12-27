using System;
using System.Collections.Generic;

namespace MetodaAproxSuccBilocalaOrd2
{
    class Program
    {
        static decimal a, b, alpha, beta;
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
            F = (t, x) => (decimal)Math.Pow(Math.E, (double)t) * 2.0M / 3 + x / 3;
            Z = (t) => (decimal)Math.Pow(Math.E, (double)t);
            a = 0; b = 1;
            alpha = 1; beta = (decimal)Math.E;

            Console.WriteLine("\t\tExemplul 1");
            Bilocala2(10, 1e-14M);
        }

        static void Ex2()
        {
            F = (t, x) => -(decimal)Math.Cos((double)t) / 2 - x / 2;
            Z = (t) => (decimal)Math.Cos((double)t);
            a = 0; b = (decimal)Math.PI / 4;
            alpha = 1; beta = (decimal)Math.Pow(2, 0.5) / 2;

            Console.WriteLine("\t\tExemplul 2");
            Bilocala2(100, 1e-14M);
        }

        static void Bilocala2(int n, decimal eps)
        {
            var t = new decimal[n + 1];
            var x = new List<decimal[]>();

            for (int i = 0; i <= n; i++)
                t[i] = a + i * (b - a) / n;

            x.Add(new decimal[n + 1]);
            for (int i = 0; i <= n; i++)
                x[0][i] = beta * (t[i] - a) / (b - a) + alpha * (b - t[i]) / (b - a);

            int m = 0;
            do
            {
                x.Add(new decimal[n + 1]);
                x[m + 1][0] = alpha; x[m + 1][n] = beta;

                for (int i = 1; i < n; i++)
                {
                    decimal suma = 0;
                    for (int j = 1; j <= i; j++)
                        suma += F(t[j - 1], x[m][j - 1]) * (t[j - 1] - a) * (b - t[i]) / (b - a)
                            + F(t[j], x[m][j]) * (t[j] - a) * (b - t[i]) / (b - a);

                    for (int j = i + 1; j <= n; j++)
                        suma += F(t[j - 1], x[m][j - 1]) * (t[i] - a) * (b - t[j - 1]) / (b - a)
                            + F(t[j], x[m][j]) * (t[i] - a) * (b - t[j]) / (b - a);

                    x[m + 1][i] = beta * (t[i] - a) / (b - a) + alpha * (b - t[i]) / (b - a) - suma * (b - a) / (2 * n);
                }

                m++;
            } while (CalculateMax(x, n, m) >= eps);

            Console.WriteLine("Ultima iteratie este m = {0}", m);
            for (int i = 0; i <= n; i++)
                Console.WriteLine("{0}\t{1}\t{2}\t\t{3}", t[i], x[m][i], Z(t[i]), x[m][i] - Z(t[i]));
        }

        static decimal CalculateMax(List<decimal[]> x, int length, int m)
        {
            decimal max = 0;
            for (int i = 1; i < length; i++)
                if (max < Math.Abs(x[m][i] - x[m - 1][i]))
                    max = Math.Abs(x[m][i] - x[m - 1][i]);
            return max;
        }
    }
}
