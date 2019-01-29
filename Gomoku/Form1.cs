using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gomoku
{
    public partial class Form1 : Form
    {
        public int CountPlacedPiece = 0;
        public bool WinnerBorned = false;

        private Game game = new Game();

        public Form1()
        {
            InitializeComponent();
        }

        //放棋子
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {   

            //黑棋先下，然後交替
            Piece piece = game.PlaceAPiece(e.X, e.Y);
            if (piece != null )
            {

                CountPlacedPiece++;

                this.Controls.Add(piece);

                //每下一顆棋子就檢查是否有人獲勝
                if (game.Winner == PieceType.BLACK && CountPlacedPiece <= 81 && WinnerBorned == false)
                {
                    WinnerBorned = true;
                    MessageBox.Show("黑獲勝，結束遊戲");
                    Environment.Exit(Environment.ExitCode);
                }
                else if (game.Winner == PieceType.WHITE && CountPlacedPiece <= 81 && WinnerBorned == false)
                {
                    WinnerBorned = true;
                    MessageBox.Show("白獲勝，結束遊戲");
                    Environment.Exit(Environment.ExitCode);
                }
                else if (CountPlacedPiece == 81 && WinnerBorned == false)
                {
                    MessageBox.Show("棋盤上共有 " + CountPlacedPiece.ToString() + " 枚棋子");
                    MessageBox.Show("此局平手，結束遊戲");
                    Environment.Exit(Environment.ExitCode);
                }
            }

        }

        //滑鼠遊標靠近節點會變成手指的圖案
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (game.CanBePlaced(e.X, e.Y))
            {
                this.Cursor = Cursors.Hand;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
