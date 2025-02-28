﻿using MohawkGame2D;
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
        bool[] isDestroyed = new bool[181];
        Rectangle[] bricks = new Rectangle[181];
        
        

        public void BrickCreation()
        {
           for(int bricksPlaced = 0; bricksPlaced < 180; )
               
            {
                Draw.LineColor = MohawkGame2D.Color.Black;
                Draw.FillColor = MohawkGame2D.Color.Gray;
                
                for(int x = 0; x < 15; x++)
                {
                    for (int y = 0; y < 12; y++)
                    {
                        
                        
                        
                        
                        Draw.LineColor = MohawkGame2D.Color.Black;
                        Draw.FillColor = MohawkGame2D.Color.Gray;
                        Draw.Rectangle(brickX[x], brickY[y], brickW, brickH);
                        bricksPlaced++;
                        isDestroyed[bricksPlaced] = false;
                        bricks[bricksPlaced] = new Rectangle(brickX[x], brickY[y], brickW, brickH);

                            
                    }
                }
                
                
                

            }
        }

    }
}
