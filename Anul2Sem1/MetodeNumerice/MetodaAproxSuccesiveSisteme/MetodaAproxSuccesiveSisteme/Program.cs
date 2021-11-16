using System;

namespace MetodaAproxSuccesiveSisteme
{
    class Program
    {
        static decimal x0, y0;
        static Func<decimal, decimal, decimal> F, G;

        static void Main(string[] args)
        {
            Ex1();
            Console.WriteLine();
            Ex2();
        }

        static void Ex1()
        {
            F = (x, y) => (decimal)Math.Sqrt((double)(x * (y + 5) - 1) / 2);
            G = (x, y) => (decimal)Math.Sqrt((double)x + 3 * Math.Log10((double)x));

            Console.WriteLine("\t\tf(x,y) = 2x^2 - xy - 5x + 1 = 0");
            Console.WriteLine("\t\tg(x,y) =   x + 3lgx - y^2   = 0");
            Calls(3.5M, 2.2M);
        }

        static void Ex2()
        {
            Console.WriteLine("\t\tx^2 + y^2 = 5");
            Console.WriteLine("\t\t   xy     = 2");

            F = (x, y) => (decimal)Math.Sqrt((double)(5 - y * y));
            G = (x, y) => 2M / x;
            Calls(1.5M, 1.5M);

            F = (x, y) => 2M / y;
            G = (x, y) => (decimal)Math.Sqrt((double)(5 - x * x));
            Calls(1.5M, 1.5M);

            F = (x, y) => -(decimal)Math.Sqrt((double)(5 - y * y));
            G = (x, y) => 2M / x;
            Calls(-1.5M, -1.5M);

            F = (x, y) => 2M / y;
            G = (x, y) => -(decimal)Math.Sqrt((double)(5 - x * x));
            Calls(-1.5M, -1.5M);
        }

        static void Calls(decimal x, decimal y)
        {
            x0 = x; y0 = y;
            Console.WriteLine();
            Console.WriteLine("\t\tx[0] = {0}\t\t\t\ty[0] = {1}", x, y);
            MetodaAproxSuccesiveSisteme(1e-4M);
            MetodaAproxSuccesiveSisteme(1e-8M);
            MetodaAproxSuccesiveSisteme(1e-12M);
        }

        static void MetodaAproxSuccesiveSisteme(decimal eps)
        {
            decimal xn_1 = x0, yn_1 = y0;
            decimal xn = F(x0, y0);
            decimal yn = G(x0, y0);
            int n = 0;

            while (Math.Abs(xn - xn_1) >= eps || Math.Abs(yn - yn_1) >= eps)
            {
                n++;
                xn_1 = xn;
                yn_1 = yn;

                xn = F(xn_1, yn_1);
                yn = G(xn_1, yn_1);
            }
            Console.WriteLine("n = {0} iteratii: x[n+1] = {1}, y[n+1] = {2} (epsilon = {3})", n, xn, yn, eps);
        }
    }
}
