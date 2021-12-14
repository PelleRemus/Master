using System;
using System.Collections.Generic;

namespace MetodaAproxSuccCauchyOrd1
{
    class Program
    {
//
// Date de intrare  -------------------------------------------------------------------------------------------
// a, b, y0, F (epsilon si n vor fi date ca parametru functiei Cauchy1)
//
        static decimal a, b, y0;
        static Func<decimal, decimal, decimal> F;
        static Func<decimal, decimal> Z;

        static void Main(string[] args)
        {
            Ex1();
            Console.WriteLine();
//
// Exemplul 2 nu functioneaza ---------------------------------------------------------------------------------
// Motiv: nu stim deocamdata
//
            // Ex2();
        }

        static void Ex1()
        {
//
// Pasul 1 ----------------------------------------------------------------------------------------------------
// Citim datele de intrare
//
            F = (x, y) => x + y;
            Z = (x) => 2 * (decimal)Math.Pow(Math.E, (double)x) - x - 1;
            a = 0; b = 1;

            Console.WriteLine("\t\ty' = y + x, x ∈ [0, 1]");
            Console.WriteLine("\t\t y(0) = 1");
            Calls(1, 100, 1e-14M);
        }

        static void Ex2()
        {
//
// Pasul 1 ----------------------------------------------------------------------------------------------------
// Citim datele de intrare
//
            F = (x, y) => (1M / (1 + x * x)) - (2 * y * y);
            Z = (x) => x / (1 + x * x);
            a = 0; b = 4;

            Console.WriteLine("\t\ty' = 1 / (1 + x^2), x ∈ [0, 4]");
            Console.WriteLine("\t\t    y'(0) = 0");
            Calls(0, 16, 1e-14M);
        }

        static void Calls(decimal y, int n, decimal eps)
        {
            y0 = y;
            Cauchy1(n, eps);
        }

        static void Cauchy1(int n, decimal eps)
        {
//
// Pasul 2 ----------------------------------------------------------------------------------------------------
// Calculeaza x[0] si h
//
            var x = new decimal[n + 1];
            var y = new List<decimal[]>();
            x[0] = a;
            decimal h = (b - a) / n;

//
// Pasul 3 ----------------------------------------------------------------------------------------------------
// Calculeaza x[i], pentru i=1,n
//
            for (int i = 1; i <= n; i++)
                x[i] = a + i * h;

//
// Pasul 4 ----------------------------------------------------------------------------------------------------
// Calculeaza y[0,i], pentru i=0,n
//
            y.Add(new decimal[n + 1]);
            for (int i = 0; i <= n; i++)
                y[0][i] = y0;

//
// Pasul 5 ----------------------------------------------------------------------------------------------------
// Calculeaza y[1,0]
//
            y.Add(new decimal[n + 1]);
            y[1][0] = y0;

//
// Pasul 6 ----------------------------------------------------------------------------------------------------
// Calculeaza y[1,i], pentru i=1,n
//
            for (int i = 1; i <= n; i++)
            {
                decimal sum = 0;
                for (int j = 1; j <= i; j++)
                    sum += F(x[j - 1], y0) + F(x[j], y0);

                y[1][i] = y0 + (b - a) / (2 * n) * sum;
            }

//
// Pasul 7 ----------------------------------------------------------------------------------------------------
// (m >= 1) Cat timp maximul pasului trecut >= eps
// Calculeaza y[m + 1,0]
// Calculeaza y[m + 1, i], pentru i=1,n
// Tipareste rezultatul
//
            int m = 1;
            while (CalculateMax(y, n, m) >= eps)
            {
                y.Add(new decimal[n + 1]);
                y[m + 1][0] = y0;

                for (int i = 1; i <= n; i++)
                {
                    decimal sum = 0;
                    for (int j = 1; j <= i; j++)
                        sum += F(x[j - 1], y[m][j - 1]) + F(x[j], y[m][j]);

                    y[m + 1][i] = y0 + (b - a) / (2 * n) * sum;
                }
                m++;
            }

            Console.WriteLine("Ultima iteratie este m = {0}", m);
            for (int i = 0; i <= n; i++)
                Console.WriteLine("{0}\t{1}\t{2}\t\t{3}", x[i], y[m][i], Z(x[i]), y[m][i] - Z(x[i]));
        }

//
// Conditie de oprire -----------------------------------------------------------------------------------------
//
        static decimal CalculateMax(List<decimal[]> y, int length, int m)
        {
            decimal max = 0;
            for (int i = 1; i <= length; i++)
                if (max < Math.Abs(y[m][1] - y[m - 1][1]))
                    max = Math.Abs(y[m][1] - y[m - 1][1]);
            return max;
        }
    }
}
