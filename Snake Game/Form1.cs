using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Snake_Game
{
    public partial class Form1 : Form
    {
        private Game game;
        private AI ai;
        public Form1()
        {
            InitializeComponent();
            this.ClientSize = new Size(150, 150);
            game = new Game(ClientRectangle);
            BackColor = Color.Black;

            ai = new AI(game);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            game.Move();
            if (!game.GameOver)
            {
                this.Invalidate();
            }
            else
            {
                timer1.Stop();
                MessageBox.Show("You Died!\r\nYour Score:" + game.Score.ToString(), "Game Over!");
                Application.Exit();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g=e.Graphics;
            game.Draw(g);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
            switch (e.KeyCode)
            {
                case Keys.W:
                    if (!(game.currentDirection == Direction.Down))
                    {
                        timer1.Stop();
                        game.Move(Direction.Up);
                    }
                    break;
                case Keys.S:
                    if (!(game.currentDirection == Direction.Up))
                    {
                        timer1.Stop();
                        game.Move(Direction.Down);
                    }
                    break;
                case Keys.A:
                    if (!(game.currentDirection == Direction.Right))
                    {
                        timer1.Stop();
                        game.Move(Direction.Left);
                    }
                    break;
                case Keys.D:
                    if (!(game.currentDirection == Direction.Left))
                    {
                        timer1.Stop();
                        game.Move(Direction.Right);
                    }
                    break;
            }
            if (!game.GameOver)
            {
                this.Invalidate();
                timer1.Start();
            }
            else
            {
                timer1.Stop();
                MessageBox.Show("You Died!\r\nYour Score:" + game.Score.ToString(), "Game Over!");
                Application.Exit();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            ai.Move();
            if (!game.GameOver)
            {
                this.Invalidate();
            }
            else
            {
                timer2.Stop();
                MessageBox.Show("You Died!\r\nYour Score:" + game.Score.ToString(), "Game Over!");
                Application.Exit();
            }
            label1.Text = game.Score.ToString();
        }


    }
}
