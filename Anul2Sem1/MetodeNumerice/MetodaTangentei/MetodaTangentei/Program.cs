using System;

namespace MetodaTangentei
{
    class Program
    {
        static decimal a, b, dda, c, k;
        static Func<decimal, decimal> F, DF;

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
            a = 1; b = 2;
            dda = 6;
            F = x => x*x*x - x - 1;
            DF = x => 3*x*x - 1;

            Console.WriteLine("\t\tf(x) = x^3 - x - 1");
            Console.WriteLine("\t\tf'(x) = 3x^2 - 1");
            Calls();
        }

        static void Ex2()
        {
            c = 2;
            b = c > 1 ? c : 1;
            k = 2;
            F = x => Power(x, k) - c;
            DF = x => k * Power(x, k - 1);

            Console.WriteLine("\tCalculul radicalului de ordinul k din c:");
            Console.WriteLine("\t\tf(x) = x^k - c");
            Console.WriteLine("\t\tf'(x) = kx^(k-1)");
            Calls();
        }

        static void Ex3()
        {
            b = 2; // cu dda = 0, x[0] va merge in algoritm pe ramura b
            k = 2;
            F = x => x * (x*x + 3*b) / (3*x*x + b);
            DF = null;

            Console.WriteLine("\tCalculul radicalului de ordinul k din a:");
            Console.WriteLine("\t\tf(x) = x(x^2 + 3a) / (3x^2 + a)");
            Calls();
        }

        static void Calls()
        {
            MetodaTangentei(1e-4M);
            MetodaTangentei(1e-8M);
            MetodaTangentei(1e-12M);
        }

        static void MetodaTangentei(decimal eps)
        {
            decimal xn_1 = F(a) * dda > 0 ? a : b;
            decimal xn = DF == null ? F(xn_1) : xn_1 - F(xn_1) / DF(xn_1);
            int n = 1;

            while (Math.Abs(xn - xn_1) >= eps)
            {
                n++;
                xn_1 = xn;
                xn = DF == null ? F(xn_1) : xn_1 - F(xn_1) / DF(xn_1);
            }
            Console.WriteLine("n = {0} iteratii: xn = {1} (epsilon = {2})", n, xn, eps);
        }

        static decimal Power(decimal x, decimal k)
        {
            return (decimal)Math.Pow((double)x, (double)k);
        }
    }
}
