using System;

namespace HermiticaMatrix
{
    public class Complex
    {
        public float real, imaginar;

        public Complex(float real = 0, float imaginar = 0)
        {
            this.real = real;
            this.imaginar = imaginar;
        }
        public Complex(string s)
        {
            real = 0;
            imaginar = 0;

            try { real = float.Parse(s); }
            catch
            {
                if (s == "i")
                    imaginar = 1;
                else if (s.Split('i')[0] == "-")
                    imaginar = -1;
                else
                {
                    try { imaginar = float.Parse(s.Split('i')[0]); }
                    catch { }
                }
            }

            if (s.Split('+').Length == 2)
            {
                string[] values = s.Split('+');
                real = float.Parse(values[0]);

                try { imaginar = float.Parse(values[1].Split('i')[0]); }
                catch { imaginar = 1; }
            }
            if (s.Split('-').Length == 2)
            {
                try
                {
                    string[] values = s.Split('-');
                    real = float.Parse(values[0]);

                    try { imaginar = -float.Parse(values[1].Split('i')[0]); }
                    catch { imaginar = -1; }
                }
                catch { }
            }
            if (s.Split('-').Length == 3)
            {
                string[] values = s.Split('-');
                real = -float.Parse(values[1]);

                try { imaginar = -float.Parse(values[2].Split('i')[0]); }
                catch { imaginar = -1; }
            }
        }

        public Complex Conjugatul()
        {
            return new Complex(real, -imaginar);
        }

        public override bool Equals(object obj)
        {
            Complex c = (Complex)obj;
            if (c.real == real && c.imaginar == imaginar)
                return true;
            return false;
        }

        public override string ToString()
        {
            string s = null;
            if (real == 0)
            {
                if (imaginar == 1)
                    s = "i";
                else if (imaginar == -1)
                    s = "-i";
                else if (imaginar == 0)
                    s = "0";
                else
                    s = imaginar.ToString() + "i";
            }
            else
            {
                s += real;
                if (imaginar > 0)
                {
                    s += "+";
                    if (imaginar != 1)
                        s += imaginar;
                    s += "i";
                }
                else if (imaginar < 0)
                {
                    if (imaginar == -1) s += "-i";
                    else
                        s += imaginar + "i";
                }
            }
            return s;
        }
    }

    class Program
    {
        static Complex[,] A;
        static int n;

        static void Main(string[] args)
        {
            Console.Write("Introduceti dimensiunea matricei: ");
            n = int.Parse(Console.ReadLine());

            A = new Complex[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    Console.Write("A[{0},{1}] = ", i + 1, j + 1);
                    string s = Console.ReadLine();
                    A[i, j] = new Complex(s);
                }
            Afisare(A);

            IsHermitian();
            Console.ReadKey();
        }

        static void IsHermitian()
        {
            bool isHermitian = true;

            for (int i = 0; i < n; i++)
                for (int j = i; j < n; j++)
                    if (!A[i, j].Equals(A[j, i].Conjugatul()))
                        isHermitian = false;

            if (isHermitian)
                Console.WriteLine("Matricea este hermitica.");
            else
                Console.WriteLine("Matricea nu este hermitica.");
        }

        static void Afisare(Complex[,] matrix)
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