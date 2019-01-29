using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku
{
    class Game
    {

        public int[,] countPieceRecord = new int[3, 3]; // 紀錄八個方向相同顏色棋子個數

        private Board board = new Board();

        //預設黑棋先下
        private PieceType currentPlayer = PieceType.BLACK;

        private PieceType winner = PieceType.NONE;

        public PieceType Winner { get { return winner; } }

        public bool CanBePlaced(int x, int y)
        {
            return board.CanBePlaced(x, y);
        }

        public Piece PlaceAPiece(int x, int y)
        {
            
            Piece piece = board.PlaceAPiece(x, y, currentPlayer);
            if (piece != null)
            {
                //檢察是否現在下棋的人獲勝
                checkWinner();

                //交換選手
                if (currentPlayer == PieceType.BLACK)
                {
                    currentPlayer = PieceType.WHITE;
                }
                else if (currentPlayer == PieceType.WHITE)
                {
                    currentPlayer = PieceType.BLACK;
                }
                return piece;
            }

            return null;
        }



        private void checkWinner()
        {
            int centerX = board.LastPlacedNode.X;
            int centerY = board.LastPlacedNode.Y;

            int a = 0, b = 0, c = 0, d = 0;

            //檢查八個不同的方向
            for (int xDir = -1; xDir <= 1; xDir++)
            {
                for (int yDir = -1; yDir <= 1; yDir++)
                {
                    //排除中間的情況(也就是最後下的那顆棋子座標，不需檢查自己)
                    if (xDir == 0 && yDir == 0)
                    {
                        //直接進入下一個迴圈，不執行下方程式碼
                        continue;
                    }
                    

                    //紀錄現在看到幾顆相同顏色的棋子
                    int count = 1;
                    while (count < 5)
                    {
                        int targetX = centerX + count * xDir;
                        int targetY = centerY + count * yDir;

                        
                        //判斷是否會超出棋盤邊界
                        if (targetX < 0 || targetX >= Board.NODE_COUNT ||
                            targetY < 0 || targetY >= Board.NODE_COUNT ||
                            board.GetPieceType(targetX, targetY) != currentPlayer)
                        {
                            
                            break;
                        }
                     
                        count++;
                 
                    }
                    countPieceRecord[xDir + 1, yDir + 1] = count - 1;


                    if (isWinnerExist(countPieceRecord))
                    {
                        winner = currentPlayer;
                    }
                        
                }

            }
    
        }



        //check winner exist or not 
        private bool isWinnerExist(int[,] record)
        {
            // int recordCenter_X = 1; 
            // int recordCenter_Y = 1;

            int result1 = record[0, 1] + record[2, 1]; // 橫
            int result2 = record[1, 0] + record[1, 2]; // 豎
            int result3 = record[0, 2] + record[2, 0]; // 斜
            int result4 = record[0, 0] + record[2, 2]; // 反斜

            if (result1 == 4 ||
                result2 == 4 ||
                result3 == 4 ||
                result4 == 4)
            {
                // winner exist
                return true;
            }


            return false;
        }

    }
}
