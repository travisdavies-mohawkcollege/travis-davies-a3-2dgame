using MohawkGame2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace travis_davies_a3_game
{
    class Bricks
    {
        int[] brickX = [
                         25,
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
                         725
                       ];

        int[] brickY = [
                         60,
                         90,
                         110,
                         140,
                         160,
                         190,
                         210,
                         240,
                         260,
                         290,
                         310,
                         340
                        ];

        int brickW = 50;
        int brickH = 20;
        bool[] isDestroyed = new bool[168];
        Vector2[] brickStorage = new Vector2[168];
        

        public void BrickCreation()
        {
           for(int bricksPlaced = 0; bricksPlaced < 168; )
               
            {
                Draw.LineColor = Color.Black;
                Draw.FillColor = Color.Gray;
                
                for(int x = 0; x < 15; x++)
                {
                    for (int y = 0; y < 12; y++)
                    {
                        
                        
                        
                        
                        Draw.LineColor = Color.Black;
                        Draw.FillColor = Color.Gray;
                        Draw.Rectangle(brickX[x], brickY[y], brickW, brickH);
                        bricksPlaced++;
                    }
                }
                
                
                

            }
        }

    }
}
