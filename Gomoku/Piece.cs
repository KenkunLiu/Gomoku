using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Gomoku
{
    //設定成抽象類別，避免建立Piece物件，因為還未決定是黑棋或白棋
    abstract class Piece : PictureBox
    {
        //設定棋子邊長為常數
        private static readonly int IMAGE_WIDTH = 50;
        public Piece(int x, int y)
        {
            //棋子背景顏色
            this.BackColor = Color.Transparent;

            //棋子位置
            this.Location = new Point(x - (IMAGE_WIDTH/2), y - (IMAGE_WIDTH/2));

            //棋子大小
            this.Size = new Size(IMAGE_WIDTH, IMAGE_WIDTH);
        }

        //為了讓棋盤回應交叉點的棋子是什麼顏色，用於之後判斷勝負
        //多型的應用，同一個method，跟去每個物件做法不同，引發出不同的效果
        public abstract PieceType GetPieceType();

    }
}
