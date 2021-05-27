using System;

namespace GaloisGrad4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("f(x) = ax^4 + bx^3 + cx^2 +dx + e");
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
            Console.Write("e = ");
            float e = float.Parse(Console.ReadLine());
            Console.WriteLine();

            Console.WriteLine("f(x) = ({0})x^4 + ({1})x^3 + ({2})x^2 + ({3})x + ({4})", a, b, c, d, e);
            Console.WriteLine("f'(x) = ({0})x^3 + ({1})x^2 + ({2})x + ({3})", 4 * a, 3 * b, 2 * c, d);
            float[,] R = new float[,]
            {
                { a, b, c, d, e, 0, 0 },
                { 0, a, b, c, d, e, 0 },
                { 0, 0, a, b, c, d, e },
                { 4*a, 3*b, 2*c, d, 0, 0, 0 },
                { 0, 4*a, 3*b, 2*c, d, 0, 0 },
                { 0, 0, 4*a, 3*b, 2*c, d, 0 },
                { 0, 0, 0, 4*a, 3*b, 2*c, d },
            };

            Console.WriteLine();
            Console.WriteLine("Rezutatul polinoamelor f si f': R(f, f') este determinantul matricei urmatoare:");
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                    Console.Write(R[i, j] + "\t");
                Console.WriteLine();
            }
            Console.WriteLine();

            float detR = -CalculDeterminant(R);
            Console.WriteLine("R(f, f') = " + detR);

            float D = (float)Math.Pow(-1, 4 * (4 - 1) / 2) * detR / a;
            Console.WriteLine("Discriminantul polinomului f: D(f) = " + D);
            Console.WriteLine();

            if (!isSquare(D))
            {
                if (!hasFloatValues(e))
                    Console.WriteLine("Grupul Gaolis al polinomului f: Gf = S4");
                else
                    Console.WriteLine("Grupul Gaolis al polinomului f: Gf = conjugatul grupului B'4 = { e, t1, t2, t3, s, t1s, t2s, t3s }");
            }
            else
            {
                if (!hasFloatValues(e))
                    Console.WriteLine("Grupul Gaolis al polinomului f: Gf = A4");
                else
                    Console.WriteLine("Grupul Gaolis al polinomului f: Gf = B4 (Grupul lui Klein)");
            }
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

        static bool hasFloatValues(float e)
        {
            if (e > 1)
                return false;
            return true;
        }
    }
}