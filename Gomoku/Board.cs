using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Gomoku
{
    class Board
    {

        public static readonly int NODE_COUNT = 9;

        //沒有其他點會用到(-1,-1)這個點
        private static readonly Point NO_MATCH_NODE = new Point(-1, -1);

        //可落子的邊緣到棋盤邊緣的距離
        private static readonly int OFFSET = 75;
        //交點的半徑
        private static readonly int NODE_RADIUS = 10;
        //交點和交點間的距離
        private static readonly int NODE_DISTANCE = 75;

        //存放棋子的陣列
        private Piece[,] pieces = new Piece[NODE_COUNT, NODE_COUNT];

        //用於判斷勝負
        private Point lastPlacedNode = NO_MATCH_NODE;
        public Point LastPlacedNode { get { return lastPlacedNode; } }


        //為了讓棋盤回應交叉點的棋子是什麼顏色，用於之後判斷勝負
        public PieceType GetPieceType(int nodeIDX, int nodeIDY)
        {
            if (pieces[nodeIDX, nodeIDY] == null)
            {
                return PieceType.NONE;
            }

            return pieces[nodeIDX, nodeIDY].GetPieceType();
        }


        public bool CanBePlaced(int x, int y)
        {
            //找出最近的節點(Node)
            Point nodeID = findTheClosetNode(x, y);
            //如果沒有，則回傳false
            if (nodeID == NO_MATCH_NODE)
            {
                return false;
            }
            //如果有，檢查是否已經有棋子存在，則回傳true
            return true;
        }

        public Piece PlaceAPiece(int x, int y, PieceType type)
        {
            //找出最近的節點(Node)
            Point nodeID = findTheClosetNode(x, y);


            //如果沒有，則回傳null
            if (nodeID == NO_MATCH_NODE)
            {
                return null;
            }

            //檢查是否已經有棋子存在
            //如果有
            if (pieces[nodeID.X, nodeID.Y] != null)
            {
                return null;
            }

            //根據type產生對應的棋子
            Point formPos = convertToFormPosition(nodeID);
            if (type == PieceType.BLACK)
            {
                //那要丟什麼座標進去產生棋子?(交叉點實際上的座標)
                //(x, y)是滑鼠點到的座標
                //(nodeID.X, nodeID.Y)是假想的座標編號
                pieces[nodeID.X, nodeID.Y] = new BlackPiece(formPos.X, formPos.Y);
            }
            else if (type == PieceType.WHITE)
            {
                pieces[nodeID.X, nodeID.Y] = new WhitePiece(formPos.X, formPos.Y);
            }

            //紀錄最後落子的位置
            lastPlacedNode = nodeID;

            return pieces[nodeID.X, nodeID.Y];
        }


        //讓棋子落在交叉點上，而不是鼠標點的位置
        private Point convertToFormPosition(Point nodeID)
        {
            Point formPosition = new Point();
            formPosition.X = nodeID.X * NODE_DISTANCE + OFFSET;
            formPosition.Y = nodeID.Y * NODE_DISTANCE + OFFSET;
            return formPosition;
        }




        //二維的版本，判斷落子是落在哪個交點
        private Point findTheClosetNode(int x, int y)
        {
            int nodeIDX = findTheClosetNode(x);

            //nodeIDX和nodeIDY 是介在0-8之間的整數
            if ((nodeIDX == -1) || (nodeIDX >= NODE_COUNT))
            {
                return NO_MATCH_NODE;
            }

            int nodeIDY = findTheClosetNode(y);
            if (nodeIDY == -1 || (nodeIDY >= NODE_COUNT))
            {
                return NO_MATCH_NODE;
            }

            return new Point(nodeIDX, nodeIDY);
        }


        //一維的版本，判斷落子是落在哪個交點
        private int findTheClosetNode(int pos)
        {

            if (pos <= (OFFSET - NODE_RADIUS))
            {
                return -1;
            }

            pos = pos - OFFSET;

            int quotient = pos / NODE_DISTANCE;
            int remainder = pos % NODE_DISTANCE;

            if (remainder <= NODE_RADIUS)
            {
                return quotient;
            }
            else if (remainder >= (NODE_DISTANCE - NODE_RADIUS))
            {
                return (quotient + 1);
            }
            else
            {
                return -1;
            }
        }
    }
}
