using System;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
    public static class Engine
    {
        public static int Lines { get; set; }
        public static int Columns { get; set; }
        public static int Size { get; set; }

        public static Tile[,] Matrix { get; set; }

        public static Random Random { get; set; }
        public static PictureBox Display { get; set; }

        public static void Initialize(PictureBox pictureBox)
        {
            Random = new Random();
            Display = pictureBox;

            Lines = 8;
            Columns = 10;
            Size = Display.Height / Lines;

            InitializeMatrix();
        }

        public static void StartNewGame()
        {
            ResetMatrix();
            AddMines();
            CalculateMatrixValues();
        }

        public static void Traverse(int line, int column)
        {
            if (line >= 0 && line < Lines && column >= 0 && column < Columns && !Matrix[line, column].WasVisited)
            {
                Matrix[line, column].Visit();
                if (Matrix[line, column].Value == 0)
                {
                    Traverse(line - 1, column);
                    Traverse(line, column + 1);
                    Traverse(line + 1, column);
                    Traverse(line, column - 1);
                }
            }
        }

        public static void CheckIfYouWon()
        {
            bool ok = true;
            for (int i = 0; i < Lines; i++)
                for (int j = 0; j < Columns; j++)
                {
                    if (Matrix[i, j].Value == 9 && !Matrix[i, j].IsFlagged)
                        ok = false;
                    if (Matrix[i, j].Value != 9 && Matrix[i, j].IsFlagged)
                        ok = false;
                }

            if (ok)
            {
                MessageBox.Show("You Win!");
                Display.Enabled = false;
            }
        }

        public static void GameOver()
        {
            for (int i = 0; i < Lines; i++)
                for (int j = 0; j < Columns; j++)
                {
                    Matrix[i, j].Button.Enabled = false;
                    if (Matrix[i, j].Value == 9)
                        Matrix[i, j].Button.Text = "X";
                }

            MessageBox.Show("You Lose!");
        }

        private static void InitializeMatrix()
        {
            Matrix = new Tile[Lines, Columns];

            for (int i = 0; i < Lines; i++)
                for (int j = 0; j < Columns; j++)
                {
                    Matrix[i, j] = new Tile(i, j);
                }
        }

        private static void ResetMatrix()
        {
            Display.Enabled = true;
            for (int i = 0; i < Lines; i++)
                for (int j = 0; j < Columns; j++)
                {
                    Matrix[i, j].Reset();
                }
        }

        private static void AddMines()
        {
            int nrOfMines = 10;

            for (int k = 0; k < nrOfMines; k++)
            {
                int i, j;
                do
                {
                    i = Random.Next(Lines);
                    j = Random.Next(Columns);
                } while (Matrix[i, j].Value == 9);

                Matrix[i, j].Value = 9;
            }
        }

        private static void CalculateMatrixValues()
        {
            for (int i = 0; i < Lines; i++)
                for (int j = 0; j < Columns; j++)
                {
                    if (Matrix[i, j].Value == 9)
                        continue;

                    for (int k = Math.Max(i - 1, 0); k <= Math.Min(i + 1, Lines - 1); k++)
                        for (int l = Math.Max(j - 1, 0); l <= Math.Min(j + 1, Columns - 1); l++)
                        {
                            if (Matrix[k, l].Value == 9)
                                Matrix[i, j].Value++;
                        }

                    switch (Matrix[i, j].Value)
                    {
                        case 1: Matrix[i, j].Button.ForeColor = Color.FromArgb(20, 100, 200); break;
                        case 2: Matrix[i, j].Button.ForeColor = Color.Green; break;
                        case 3: Matrix[i, j].Button.ForeColor = Color.Red; break;
                        case 4: Matrix[i, j].Button.ForeColor = Color.DarkBlue; break;
                        case 5: Matrix[i, j].Button.ForeColor = Color.DarkRed; break;
                        case 6: Matrix[i, j].Button.ForeColor = Color.DarkTurquoise; break;
                        case 7: Matrix[i, j].Button.ForeColor = Color.Black; break;
                        default: break;
                    }
                }
        }
    }
}
