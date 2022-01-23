using System;
using System.Collections.Generic;

namespace MetodaFctGreenProbBilocaleCauchyOrdSup
{
    class Program
    {
        //
        // Date de intrare  -------------------------------------------------------------------------------------------
        // a, b, y0, F (epsilon si n vor fi date ca parametru functiei GreenCauchySup)
        //
        static decimal a, b, y0, y0_;
        static Func<decimal, decimal, decimal> F;
        static Func<decimal, decimal> Z;

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
            F = (x, y) => 2 * (decimal)Math.Pow(Math.E, (double)x) / 3 + y / 3;
            Z = (x) => (decimal)Math.Pow(Math.E, (double)x);
            a = 0; b = 1;
            y0 = 1; y0_ = 1;

            Console.WriteLine("\t\ty'' = (2/3)e^x + y/3, x ∈ [0, 1]");
            Console.WriteLine("\t\ty(0) = 1, y'(0) = 1");
            GreenCauchySup(50, 1e-14M);
        }

        static void GreenCauchySup(int n, decimal eps)
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
                y[0][i] = y0 + (x[i] - x[0]) * y0_;

            //
            // Pasul 5 ----------------------------------------------------------------------------------------------------
            // (m >= 1)
            // Calculeaza y[m + 1,0]
            // Pentru i=1,n, calculeaza y[m + 1, i], 
            // Pana cand maximul pasului < eps
            // Tipareste rezultatul
            //
            int m = 1;
            do
            {
                y.Add(new decimal[n + 1]);
                y[m][0] = y0;

                for (int i = 1; i <= n; i++)
                {
                    decimal sum = 0;
                    for (int j = 1; j <= i; j++)
                        sum += (x[i] - x[j - 1]) * F(x[j - 1], y[m - 1][j - 1])
                                + (x[i] - x[j]) * F(x[j], y[m - 1][j]);

                    y[m][i] = y[0][i] + h * sum / 2;
                }
                m++;
            } while (CalculateMax(y, n, m - 1) >= eps);

            Console.WriteLine("Ultima iteratie este m = {0}", m);
            for (int i = 0; i <= n; i++)
                Console.WriteLine("{0}\t{1}\t{2}\t\t{3}", x[i], y[m - 1][i], Z(x[i]), y[m - 1][i] - Z(x[i]));
        }

        //
        // Conditie de oprire -----------------------------------------------------------------------------------------
        //
        static decimal CalculateMax(List<decimal[]> y, int length, int m)
        {
            decimal max = 0;
            for (int i = 1; i <= length; i++)
                if (max < Math.Abs(y[m][i] - y[m - 1][i]))
                    max = Math.Abs(y[m][i] - y[m - 1][i]);
            return max;
        }
    }
}
