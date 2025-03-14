using MohawkGame2D;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace travis_davies_a3_game
{
    class Bricks
    {
        public int[] brickX = [
                         
                         75,
                         125,
                         175,
                         225,
                         275,
                         325,
                         375,
                         425,
                         475,
                         525,
                         575,
                         625,
                         675,
                         
                       ];

       public  int[] brickY = [
                         
                         
                         
                         160,
                         190,
                         210,
                         240,
                         260,
                         290,
                         310,
                         340,
                         360,
                         390

                        ];



        public int brickW = 50;
        public int brickH = 20;
        public bool[] isDestroyed = new bool[131];
        public Rectangle[] bricks = new Rectangle[131];
        
        

        public void BrickCreation()
        {
           for(int bricksPlaced = 0; bricksPlaced < 130; )
               
            {
                Draw.LineColor = MohawkGame2D.Color.Black;
                Draw.FillColor = MohawkGame2D.Color.Gray;
                
                for(int x = 0; x < 13; x++)
                {
                    for (int y = 0; y < 10; y++)
                    {

                        bricksPlaced++;

                        if (isDestroyed[bricksPlaced] == false) 
                        {
                            Draw.LineColor = MohawkGame2D.Color.Black;
                            Draw.FillColor = MohawkGame2D.Color.Gray;
                            Draw.Rectangle(brickX[x], brickY[y], brickW, brickH);
                            bricks[bricksPlaced] = new Rectangle(brickX[x], brickY[y], brickW, brickH);
                            
                        }

                        else
                        {
                            
                        }


                       

                            
                    }
                }
                
                
                

            }
        }

        public void Reset()
        {
            for( int resetBrick = 0; resetBrick < bricks.Length; resetBrick++)
            {
                isDestroyed[resetBrick] = false;
            }
        }

    }
}
