using System;

namespace MatrixPolinomCaracteristic
{
    class Program
    {
        static int[,] A;
        static int n;
        static float x1, x2, x3, x;
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            n = 2;
            A = new int[,] {
                { 1, 2 },
                { 2, 4 }
            };
            Afisare(A);

            if (n == 2)
                OrdinDoi();
            else
                OrdinTrei();

            Console.ReadKey();
        }

        static void OrdinDoi()
        {
            int a = 1;
            int b = -A[0, 0] - A[1, 1];
            int c = A[0, 0] * A[1, 1] - A[0, 1] * A[1, 0];
            Console.WriteLine("pA = lam^2 + ({0})lam + ({1})", b, c);
            Console.WriteLine();

            int delta = b * b - 4 * a * c;
            if (delta > 0)
            {
                x1 = (-b + (float)Math.Sqrt(delta)) / (2 * a);
                x2 = (-b - (float)Math.Sqrt(delta)) / (2 * a);
                Console.WriteLine("A are doua valori proprii reale.");
                Console.WriteLine("lam1 = {0}, lam2 = {1}", x1, x2);
            }
            else if (delta == 0)
            {
                x = -b / (2 * a);
                Console.WriteLine("A are o valoare proprie reala.");
                Console.WriteLine("lam = " + x);
            }
            else
            {
                Console.WriteLine("A are doua valori proprii complexe.");
            }
        }

        static void OrdinTrei()
        {
            int a = -1;
            int b = A[0, 0] + A[1, 1] + A[2, 2];
            int c = -A[0, 0] * A[1, 1] - A[0, 0] * A[2, 2] - A[1, 1] * A[2, 2] + A[0, 2] * A[2, 0] + A[1, 2] * A[2, 1] + A[0, 1] * A[1, 0];
            int d = A[0, 0] * A[1, 1] * A[2, 2] + A[0, 1] * A[1, 2] * A[2, 0] + A[0, 2] * A[1, 0] * A[2, 1] - A[0, 2] * A[2, 0] * A[1, 1] - A[1, 2] * A[2, 1] * A[0, 0] - A[0, 1] * A[1, 0] * A[2, 2];
            Console.WriteLine("pA = -lam^3 + ({0})lam^2 + ({1})lam + ({2})", b, c, d);
            Console.WriteLine();

            int delta = 18 * a * b * c * d - 4 * b * b * b * d + b * b * c * c - 4 * a * c * c * c - 27 * a * a * d * d;
            int d0 = b * b - 3 * a * c;
            int d1 = 2 * b * b * b - 9 * a * b * c + 27 * a * a * d;

            if (delta > 0)
            {
                x1 = -b / (3 * a);
                double r2 = (1 / 2) * (2 * b * b * b - 9 * a * b * c + 27 * a * a * d + Math.Sqrt(Math.Pow((2 * b * b * b - 9 * a * b * c + 27 * a * a * d), 2) - 4 * Math.Pow((b * b - 3 * a * c), 3)));
                x2 = (-1 / (3 * a)) * (float)Math.Pow(r2, (1.0 / 3));
                double r3 = (1 / 2) * (2 * b * b * b - 9 * a * b * c + 27 * a * a * d - Math.Sqrt(Math.Pow((2 * b * b * b - 9 * a * b * c + 27 * a * a * d), 2) - 4 * Math.Pow((b * b - 3 * a * c), 3)));
                x3 = (-1 / (3 * a)) * (float)Math.Pow(r3, (1.0 / 3));
                Console.WriteLine("A are trei valori proprii reale.");
                Console.WriteLine("lam1 = {0}, lam2 = {1}, lam3 = {2}", x1, x2, x3);
            }
            else if (delta == 0)
            {
                if (d0 == 0)
                {
                    x = -b / (3 * a);
                    Console.WriteLine("A are o valoare proprie reala.");
                    Console.WriteLine("lam = " + x);
                }
                else
                {
                    x1 = (9 * a * d - b * c) / (2 * d0);
                    x2 = (4 * a * b * c - 9 * a * a * d - b * b * b) / (a * d0);
                    Console.WriteLine("A are doua valoari proprii reale.");
                    Console.WriteLine("lam1 = {0}, lam2 = {1}", x1, x2);
                }
            }
            else
                Console.WriteLine("A are o valoare proprie reala si doua complexe.");
        }

        static void Afisare(int[,] matrix)
        {
            Console.WriteLine();
            int m = matrix.GetLength(0);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                    Console.Write(matrix[i, j] + "\t");
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}