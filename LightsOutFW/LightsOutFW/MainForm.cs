using System;
using System.Drawing;
using System.Windows.Forms;
//Anthony Hackworth writing LightsOut

namespace LightsOutFW
{
    public partial class MainForm : Form
    {
        private const int GridOffset = 25;
        private const int GridLength = 200;
        private int CellLength;
        private LightsOutGame game;
        public MainForm()
        {
            InitializeComponent();
            game = new LightsOutGame();
            game.NewGame();
            CellLength = GridLength / game.GridSize;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void MenuNewGameButton_Click(object sender, EventArgs e)
        {
            newGameButton_Click(sender, e);
        }

        private void GameToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void newGameButton_Click(object sender, EventArgs e)
        {
            game.NewGame();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            for(int i = 0; i < game.GridSize;i++)
            {
                for(int j = 0; j < game.GridSize; j++)
                {
                    Brush brush;
                    Pen pen;

                    if(game.GetGridValue(i,j))
                    {
                        pen = Pens.Black;
                        brush = Brushes.White;
                    }
                    else
                    {
                        pen = Pens.White;
                        brush = Brushes.Black;
                    }

                    int x = j * CellLength + GridOffset;
                    int y = i * CellLength + GridOffset;

                    g.DrawRectangle(pen, x, y, CellLength, CellLength);
                    g.FillRectangle(brush, x + 1, y + 1, CellLength - 1, CellLength - 1);
                }
            }
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X < GridOffset || e.X > CellLength * game.GridSize + GridOffset || e.Y < GridOffset || 
                e.Y > CellLength * game.GridSize + GridOffset)
                return;

            int r = (e.Y - GridOffset) / CellLength;
            int c = (e.X - GridOffset) / CellLength;
            game.Move(r, c);
            this.Invalidate();

            if(game.IsGameOver())
            {
                MessageBox.Show(this, "Congratulations! You've won!", "Lights Out!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitButton_Click(sender, e);
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutbox = new AboutForm();
            aboutbox.ShowDialog(this);
        }

        private void SizeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
