using System;

namespace MetodaModificataNewtonSisteme
{
    class Program
    {
        static decimal x0, y0;
        static Func<decimal, decimal, decimal> F, G, DFx, DFy, DGx, DGy;

        static void Main(string[] args)
        {
            Ex1();
            Console.WriteLine();
            Ex2();
        }

        static void Ex1()
        {
            F = (x, y) => 2 * x * x * x - y * y - 1;
            G = (x, y) => x * y * y * y - y - 4;
            DFx = (x, y) => 6 * x * x;
            DFy = (x, y) => -2 * y;
            DGx = (x, y) => y * y * y;
            DGy = (x, y) => 3 * x * y * y - 1;

            Console.WriteLine("\t\t2x^3 - y^2 - 1 = 0");
            Console.WriteLine("\t\txy^3 - y - 4 = 0");
            Calls(1.2M, 1.7M);
        }

        static void Ex2()
        {
            F = (x, y) => x * x + y * y - 10;
            G = (x, y) => (decimal)Math.Sqrt((double)(x + y)) - 2;
            DFx = (x, y) => 2 * x;
            DFy = (x, y) => 2 * y;
            DGx = (x, y) => 1.0M / 2 / (decimal)Math.Sqrt((double)(x + y));
            DGy = (x, y) => 1.0M / 2 / (decimal)Math.Sqrt((double)(x + y));

            Console.WriteLine("\t\tx^2 + y^2 - 10 = 0");
            Console.WriteLine("\t\t√(x + y) - 2 = 0");
            Calls(0.9M, 3.1M);
            Calls(2.9M, 1.1M);
            Calls(1.48M, 2.52M);
        }

        static void Calls(decimal x, decimal y)
        {
            x0 = x; y0 = y;
            Console.WriteLine("\t\tx[0] = {0}\t\t\t\ty[0] = {1}", x, y);
            MetodaModificataNewtonSisteme(1e-4M);
            MetodaModificataNewtonSisteme(1e-8M);
            MetodaModificataNewtonSisteme(1e-12M);
        }

        static void MetodaModificataNewtonSisteme(decimal eps)
        {
            decimal xn_1 = x0, yn_1 = y0;
            decimal j = J();
            decimal xn = CalculateXN(xn_1, yn_1, j);
            decimal yn = CalculateYN(xn_1, yn_1, j);
            int n = 0;

            while (Math.Abs(xn - xn_1) >= eps || Math.Abs(yn - yn_1) >= eps)
            {
                n++;
                xn_1 = xn;
                yn_1 = yn;

                xn = CalculateXN(xn_1, yn_1, j);
                yn = CalculateYN(xn_1, yn_1, j);
            }
            Console.WriteLine("n = {0} iteratii: x[n+1] = {1}, y[n+1] = {2} (epsilon = {3})", n, xn, yn, eps);
        }

        static decimal CalculateXN(decimal xn_1, decimal yn_1, decimal j)
        {
            return xn_1 - 1.0M / j * (F(xn_1, yn_1) * DGy(x0, y0) - G(xn_1, yn_1) * DFy(x0, y0));
        }

        static decimal CalculateYN(decimal xn_1, decimal yn_1, decimal j)
        {
            return yn_1 - 1.0M / j * (DFx(x0, y0) * G(xn_1, yn_1) - DGx(x0, y0) * F(xn_1, yn_1));
        }

        static decimal J()
        {
            return DFx(x0, y0) * DGy(x0, y0) - DFy(x0, y0) * DGx(x0, y0);
        }
    }
}
