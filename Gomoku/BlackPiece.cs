using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku
{
    class BlackPiece : Piece
    {
        public BlackPiece(int x, int y) : base(x, y)
        {
            //引用圖片素材
            this.Image = Properties.Resources.black;
        }

        //實現base類別的抽象method
        public override PieceType GetPieceType()
        {
            return PieceType.BLACK;
        }
    }
}
