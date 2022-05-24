using System;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Engine.Initialize(pictureBox1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Engine.StartNewGame();
        }
    }
}
