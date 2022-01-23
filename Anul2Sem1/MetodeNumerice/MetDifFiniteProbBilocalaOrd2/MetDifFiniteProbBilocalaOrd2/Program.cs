using System;

namespace MetDifFiniteProbBilocalaOrd2
{
    class Program
    {
        static decimal a, b;
        static Func<decimal, decimal> F, R;
        static Func<decimal, decimal> Z;

        static void Main(string[] args)
        {
            Ex1();
        }

        static void Ex1()
        {
            R = (t) => 1;
            F = (t) => 2 * (decimal)Math.Pow(Math.E, -(double)t) * (decimal)Math.Cos((double)t)
                        + (decimal)Math.Pow(Math.E, -(double)t) * (decimal)Math.Sin((double)t);
            Z = (t) => (decimal)Math.Pow(Math.E, -(double)t) * (decimal)Math.Sin((double)t);
            a = 0; b = 3.1415926535897932384626433832M;

            DifFiniteBilocala2(100, 1e-14M);
        }

        static void DifFiniteBilocala2(int n, decimal eps)
        {
            var t = new decimal[n + 1];
            var rr = new decimal[n + 1];
            var ff = new decimal[n + 1];
            var y = new decimal[n + 1];
            var u = new decimal[n + 1];
            var w = new decimal[n + 1];
            var p = new decimal[n + 1];
            var v = new decimal[n + 1];

            decimal h = (b - a) / n;
            decimal z = -1M;

            for (int i = 0; i <= n; i++)
                t[i] = a + i * h;

            for (int i = 1; i < n; i++)
            {
                rr[i] = R(t[i]) * h * h;
                ff[i] = F(t[i]) * h * h;
                y[i] = 2.0M + rr[i];
            }

            u[1] = z / y[1];
            for (int i = 2; i <= n - 2; i++)
            {
                w[i] = y[i] - u[i - 1] * z;
                u[i] = z / w[i];
            }
            w[n - 1] = y[n - 1] - u[n - 2] * z;

            p[1] = ff[1] / 2;
            for (int i = 2; i < n; i++)
                p[i] = (ff[i] - z * p[i - 1]) / w[i];

            v[0] = 0; v[n] = 0;
            v[n - 1] = p[n - 1];
            for (int i = n - 2; i >= 1; i--)
                v[i] = p[i] - u[i] * v[i + 1];

            for (int i = 0; i <= n; i++)
                Console.WriteLine("{0}\t{1}\t{2}\t\t{3}", t[i].ToString("0.000000"), v[i], Z(t[i]), v[i] - Z(t[i]));
        }
    }
}
