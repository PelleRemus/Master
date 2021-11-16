using System;

namespace MetodaGaussSeidelSisteme
{
    class Program
    {
        static decimal x0, y0, z0;
        static Func<decimal, decimal, decimal, decimal> F1, F2, F3;

        static void Main(string[] args)
        {
            Ex1();
            Console.WriteLine();
            Ex2();
        }

        static void Ex1()
        {
            F1 = (x, y, z) => (decimal)Math.Sqrt((double)(y * z + 5 * x - 1) / 2.0);
            F2 = (x, y, z) => (decimal)Math.Sqrt((double)x * 2 + Math.Log((double)z));
            F3 = (x, y, z) => (decimal)Math.Sqrt((double)(x * y + 2 * z + 8));

            Console.WriteLine("\t2x^2 - yz - 5x + 1 = 0");
            Console.WriteLine("\t  y^2 - 2x - lnz   = 0");
            Console.WriteLine("\tz^2 - xy - 2z - 8  = 0");
            Calls(10, 10, 10, true);
        }

        static void Ex2()
        {
            F3 = null;
            Console.WriteLine("\tx^2 + y^2 = 5");
            Console.WriteLine("\t   xy     = 2");

            F1 = (x, y, z) => (decimal)Math.Sqrt((double)(5 - y * y));
            F2 = (x, y, z) => 2M / x;
            Calls(1.5M, 1.5M);

            F1 = (x, y, z) => 2M / y;
            F2 = (x, y, z) => (decimal)Math.Sqrt((double)(5 - x * x));
            Calls(1.5M, 1.5M);
        }

        static void Calls(decimal x, decimal y, decimal z = 0, bool shouldDisplayZ = false)
        {
            x0 = x; y0 = y; z0 = z;
            Console.WriteLine("\tx[0] = {0}\t\ty[0] = {1}{2}", x, y, shouldDisplayZ ? $"\t\tz[0] = {z}" : "");
            MetodaAproxSuccesiveSisteme(1e-4M);
            MetodaAproxSuccesiveSisteme(1e-8M);
            MetodaAproxSuccesiveSisteme(1e-12M);
        }

        static void MetodaAproxSuccesiveSisteme(decimal eps)
        {
            decimal xn_1 = x0, yn_1 = y0, zn_1 = z0;
            decimal xn = F1(x0, y0, z0);
            decimal yn = F2(x0, y0, z0);
            decimal zn = F3 != null ? F3(x0, y0, z0) : 0;
            int n = 1;

            while (Math.Abs(xn - xn_1) >= eps || Math.Abs(yn - yn_1) >= eps || Math.Abs(zn - zn_1) >= eps)
            {
                n++;
                xn_1 = xn;
                yn_1 = yn;
                zn_1 = zn;

                xn = F1(xn_1, yn_1, zn_1);
                yn = F2(xn, yn_1, zn_1);
                zn = F3 != null ? F3(xn, yn, zn_1) : 0;
            }
            Console.WriteLine("n = {0}: x[n] = {1}, y[n] = {2}{3} (epsilon = {4})", n, xn, yn, zn != 0 ? $", z[n] = {zn}" : "", eps.ToString("0e0"));
        }
    }
}
