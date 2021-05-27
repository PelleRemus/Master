using System;

namespace OrdinElementGrupZn
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Introduceti valoarea lui n: ");
            int n = int.Parse(Console.ReadLine());

            Console.Write("Introduceti valoarea numarului pentru care se calculeaza ordinul: ");
            int a = int.Parse(Console.ReadLine());
            Console.WriteLine();

            if (n < a)
                Console.WriteLine("{0} nu apartine multimii Z{1}", a, n);
            else
            {
                int p = 1, m = 0;
                while (m < 100)
                {
                    m++;
                    p *= a;
                    Console.WriteLine("{0} modulo {1} la puterea {2} = {3} modulo {1}", a, n, m, p % n);
                    if (p % n == 1)
                    {
                        break;
                    }
                }

                Console.WriteLine();
                Console.WriteLine("Ordinul numarului {0} modulo {1} este {2}", a, n, m == 100 ? "Infinit" : m.ToString());
            }

            Console.ReadKey();
        }
    }
}
