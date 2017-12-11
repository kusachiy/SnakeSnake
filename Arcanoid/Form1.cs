using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Arcanoid
{
    public partial class Form1 : Form
    {
        Vector2 _speedVector;
        Graphics _board;
        Thread _thread;
        Game game;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            _board = pictureBox1.CreateGraphics();
            game = new Game();
            game.DrawApple += DrawApple;
            game.DrawPalyer += DrawPlayer;
            game.ClearBoard += Clear;
            game.EndGame += EndGame;
            _thread = new Thread(game.Run);            
            _thread.Start();
            FormClosed += Form1_FormClosed;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _thread.Abort();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {           
            char c = e.KeyChar;
            switch (c)
            {
                case 'w':
                case 'ц':
                    game.Direction = Vector2.Up;
                    break;
                case 's':
                case 'ы':
                    game.Direction = Vector2.Down;
                    break;
                case 'a':
                case 'ф':
                    game.Direction = Vector2.Left;
                    break;
                case 'd':
                case 'в':
                    game.Direction = Vector2.Right;
                    break;
            }
        }

        private void Clear()
        {
            try
            {
                Invoke(
               new Action(() =>
               {
                   _board.Clear(Color.LightYellow);
               }
               ));
            }
            catch (Exception)
            {

            }          
        }           

        private void DrawPlayer(Player player)
        {
            try
            {
                Invoke(
              new Action(() =>
              {
                  foreach (var item in player.Body)
                  {
                      _board.DrawRectangle(Pens.DarkOliveGreen, item.ToRectangle());
                  }
                  textBox1.Text = player.GetLength.ToString();
              }
              ));
            }
            catch (Exception)
            {

            }
        }
        private void DrawApple(Apple apple)
        {
            try
            {
                Invoke(
                   new Action(() =>
                   {
                       _board.FillEllipse(Brushes.Red, apple.Position.ToRectangle());
                   }
                   ));
            }
            catch (Exception)
            {

            }
        }
        private void EndGame()
        {
            MessageBox.Show("Game Over");
        }
        
    }
}
