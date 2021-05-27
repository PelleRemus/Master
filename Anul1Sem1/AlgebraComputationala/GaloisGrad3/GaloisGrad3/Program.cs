using System;

namespace GaloisGrad3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("f(x) = ax^3 + bx^2 + cx +d");
            Console.WriteLine();
            Console.WriteLine("Introduceti valorile polinomului:");

            Console.Write("a = ");
            float a = float.Parse(Console.ReadLine());
            Console.Write("b = ");
            float b = float.Parse(Console.ReadLine());
            Console.Write("c = ");
            float c = float.Parse(Console.ReadLine());
            Console.Write("d = ");
            float d = float.Parse(Console.ReadLine());
            Console.WriteLine();

            Console.WriteLine("f(x) = ({0})x^3 + ({1})x^2 + ({2})x + ({3})", a, b, c, d);
            Console.WriteLine("f'(x) = ({0})x^2 + ({1})x + ({2})", 3 * a, 2 * b, c);
            float[,] R = new float[,]
            {
                { a, b, c, d, 0 },
                { 0, a, b, c, d },
                { 3*a, 2*b, c, 0, 0 },
                { 0, 3*a, 2*b, c, 0 },
                { 0, 0, 3*a, 2*b, c }
            };

            Console.WriteLine();
            Console.WriteLine("Rezutatul polinoamelor f si f': R(f, f') este determinantul matricei urmatoare:");
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                    Console.Write(R[i, j] + "\t");
                Console.WriteLine();
            }
            Console.WriteLine();

            float detR = CalculDeterminant(R);
            Console.WriteLine("R(f, f') = " + detR);

            float D = (float)Math.Pow(-1, 3 * (3 - 1) / 2) * detR / a;
            Console.WriteLine("Discriminantul polinomului f: D(f) = " + D);
            Console.WriteLine();

            if (isSquare(D))
                Console.WriteLine("Grupul Gaolis asociat polinomului f: Gf = A3");
            else
                Console.WriteLine("Grupul Gaolis asociat polinomului f: Gf = S3");
            Console.ReadKey();
        }

        static float CalculDeterminant(float[,] matrix)
        {
            float det = 0;
            if (matrix.GetLength(0) == 2)
                det += matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            else
                for (int i = 0; i < matrix.GetLength(0); i++)
                    det += (float)Math.Pow(-1, i) * matrix[0, i] * CalculDeterminant(Scurtare(0, i, matrix));
            return det;
        }

        static float[,] Scurtare(int x, int y, float[,] matrix)
        {
            int m = matrix.GetLength(0);
            float[,] aux = new float[m - 1, m - 1];
            for (int i = 0; i < m - 1; i++)
                for (int j = 0; j < m - 1; j++)
                {
                    if (i < x && j < y)
                        aux[i, j] = matrix[i, j];
                    if (i < x && j >= y)
                        aux[i, j] = matrix[i, j + 1];
                    if (i >= x && j < y)
                        aux[i, j] = matrix[i + 1, j];
                    if (i >= x && j >= y)
                        aux[i, j] = matrix[i + 1, j + 1];
                }
            return aux;
        }

        static bool isSquare(float value)
        {
            for (int i = 0; i * i <= value; i++)
                if (i * i == value)
                    return true;
            return false;
        }
    }
}