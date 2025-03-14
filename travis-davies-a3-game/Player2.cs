using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MohawkGame2D;

namespace travis_davies_a3_game
{
    public class Player2
    {
        public float player2PosX = 400;
        public float player2PosY = 50;
        public Vector2 player2PosRect1 = new Vector2();

        public Vector2 player2SizeRect1 = new Vector2();
        public float leftEdge2;
        public float rightEdge2;
        public float topEdge2;
        public float bottomEdge2;






        public void Update()
        {

        }

        public void Player2Handler()
        {
            if (Input.IsKeyboardKeyDown(KeyboardInput.Left))
            {
                player2PosX -= 5;
            }
            if (Input.IsKeyboardKeyDown(KeyboardInput.Right))
            {
                player2PosX += 5;
            }

            Draw.LineSize = 1;
            Draw.FillColor = MohawkGame2D.Color.Blue;
            Draw.Rectangle(player2PosX, player2PosY, 60, 10);

            //PlayerCollisionBounds();

            if (player2PosX < 0)
            {
                player2PosX = 0;
            }
            if (player2PosX > 740)
            {
                player2PosX = 740;
            }
            
                
            

        }

        public void Player2CollisionBounds()
        {
            player2PosRect1.X = player2PosX;
            player2PosRect1.Y = player2PosY;
            player2SizeRect1.X = 60;
            player2SizeRect1.Y = 10;


            leftEdge2 = player2PosRect1.X;
            rightEdge2 = player2PosRect1.X + player2SizeRect1.X;
            topEdge2 = player2PosRect1.Y;
            bottomEdge2 = player2PosRect1.Y + player2SizeRect1.Y;
            //Console.WriteLine("Left: " + leftEdge + " Right: " + rightEdge + " Top: " + topEdge + " Bottom: " + bottomEdge);




        }
    }
}
