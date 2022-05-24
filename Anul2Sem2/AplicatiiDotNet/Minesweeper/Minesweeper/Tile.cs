using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
    public class Tile
    {
        public int Line { get; set; }
        public int Column { get; set; }
        public int Value { get; set; }

        public bool WasVisited { get; set; }
        public bool IsFlagged { get; set; }

        public Button Button { get; set; }

        public Tile(int line, int column)
        {
            Line = line;
            Column = column;

            Button = new Button();
            Button.Parent = Engine.Display;
            Button.Size = new Size(Engine.Size, Engine.Size);
            Button.Location = new Point(column * Engine.Size, line * Engine.Size);
            Button.BackColor = Color.LightGray;
            Button.Font = new Font("Arial", Engine.Size / 2, FontStyle.Bold);
            Button.MouseClick += Button_MouseClick;
            Button.MouseWheel += Button_MouseWheel;
        }

        public void Reset()
        {
            Value = 0;
            WasVisited = false;
            IsFlagged = false;

            Button.Enabled = true;
            Button.Text = "";
            Button.ForeColor = Color.Black;
            Button.BackColor = Color.LightGray;
        }

        public void Visit()
        {
            WasVisited = true;
            Button.BackColor = Color.White;
            if (Value != 0)
                Button.Text = Value.ToString();
        }

        private void Button_MouseClick(object sender, MouseEventArgs e)
        {
            if (WasVisited || IsFlagged)
                return;

            if (Value == 9)
                Engine.GameOver();
            else
                Engine.Traverse(Line, Column);

            Engine.CheckIfYouWon();
        }

        private void Button_MouseWheel(object sender, MouseEventArgs e)
        {
            if (WasVisited)
                return;

            if (IsFlagged)
                Button.BackColor = Color.LightGray;
            else
                Button.BackColor = Color.Red;

            IsFlagged = !IsFlagged;
            Engine.CheckIfYouWon();
        }
    }
}
