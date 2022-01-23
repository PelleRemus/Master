using System;
using System.Collections.Generic;

namespace MetodaFctGreenProbBilocalaOrd4
{
    class Program
    {
        //
        // Date de intrare  -------------------------------------------------------------------------------------------
        // a, b, y0, F (epsilon si n vor fi date ca parametru functiei GreenOrd4)
        //
        static decimal a, b, c, d, w, r;
        static Func<decimal, decimal, decimal> F;
        static Func<decimal, decimal> Z;

        static decimal H(decimal t, decimal s)
        {
            return (1.0M / 6) * (decimal)Math.Pow((double)((s - a) / (b - a)), 2)
                * (decimal)Math.Pow((double)(1 - (t - a) / (b - a)), 2)
                * (((t - a) / (b - a) - (s - a) / (b - a))
                    + 2 * (1 - (s - a) / (b - a)) * (t - a) / (b - a));
        }
        static decimal K(decimal t, decimal s)
        {
            return (1.0M / 6) * (decimal)Math.Pow((double)((t - a) / (b - a)), 2)
                * (decimal)Math.Pow((double)(1 - (s - a) / (b - a)), 2)
                * (((s - a) / (b - a) - (t - a) / (b - a))
                    + 2 * (1 - (t - a) / (b - a)) * (s - a) / (b - a));
        }

        static void Main(string[] args)
        {
            Ex1();
        }

        static void Ex1()
        {
            //
            // Pasul 1 ----------------------------------------------------------------------------------------------------
            // Citim datele de intrare
            //
            F = (t, x) => 23.0M / (decimal)Math.Pow((double)t + 1, 5) + (decimal)Math.Pow((double)x, 5);
            Z = (t) => 1.0M / (t + 1);
            a = 0; b = 1; c = 1; d = 0.5M; w = -1; r = -0.25M;

            GreenOrd4(100, 1e-14M);
        }

        static void GreenOrd4(int n, decimal eps)
        {
            //
            // Pasul 2 ----------------------------------------------------------------------------------------------------
            // Construieste noturile t[i] si g[i]
            //
            var x = new List<decimal[]>();
            var t = new decimal[n + 1];
            var g = new decimal[n + 1];

            for (int i = 0; i <= n; i++)
            {
                t[i] = a + i * (b - a) / n;
                g[i] = c * (b - t[i]) * (b - t[i]) * (2 * (t[i] - a) + (b - a)) / (b - a) / (b - a) / (b - a)
                    + d * (t[i] - a) * (t[i] - a) * (2 * (b - t[i]) + (b - a)) / (b - a) / (b - a) / (b - a)
                    + w * (b - t[i]) * (b - t[i]) * (t[i] - a) / (b - a) / (b - a)
                    + r * (t[i] - a) * (t[i] - a) * (b - t[i]) / (b - a) / (b - a);
            }

            //
            // Pasul 3 ----------------------------------------------------------------------------------------------------
            // Calculeaza x[0,i], pentru i=0,n
            //
            x.Add(new decimal[n + 1]);
            for (int i = 0; i <= n; i++)
                x[0][i] = g[i];

            //
            // Pasul 4 ----------------------------------------------------------------------------------------------------
            // (m >= 1)
            // Calculeaza y[m + 1,0]
            // Pentru i=1,n, calculeaza y[m + 1, i], 
            // Pana cand maximul pasului < eps
            //
            int m = 1;
            do
            {
                x.Add(new decimal[n + 1]);
                x[m][0] = c;
                x[m][n] = d;

                for (int i = 1; i < n; i++)
                {
                    decimal sum1 = 0, sum2=0;
                    for (int j = 1; j <= i; j++)
                        sum1 += H(t[i], t[j - 1]) * F(t[j - 1], x[m - 1][j - 1])
                            + H(t[i], t[j]) * F(t[j], x[m - 1][j]);

                    for (int j = i + 1; j <= n; j++)
                        sum2 += K(t[i], t[j - 1]) * F(t[j - 1], x[m - 1][j - 1])
                            + K(t[i], t[j]) * F(t[j], x[m - 1][j]);

                    x[m][i] = g[i] + (b - a) * sum1 / (2 * n) + (b - a) * sum2 / (2 * n);
                }
                m++;
            } while (CalculateMax(x, n, m - 1) >= eps);

            //
            // Pasul 5 ------------------------------------------------------------------------------------------------
            // Tipareste rezultatul
            //
            Console.WriteLine("Ultima iteratie este m = {0}", m);
            for (int i = 0; i <= n; i++)
                Console.WriteLine("{0}\t{1}\t{2}\t\t{3}", t[i], x[m - 1][i], Z(t[i]), x[m - 1][i] - Z(t[i]));
        }

        //
        // Conditie de oprire -----------------------------------------------------------------------------------------
        //
        static decimal CalculateMax(List<decimal[]> x, int length, int m)
        {
            decimal max = 0;
            for (int i = 1; i <= length; i++)
                if (max < Math.Abs(x[m][i] - x[m - 1][i]))
                    max = Math.Abs(x[m][i] - x[m - 1][i]);
            return max;
        }
    }
}
