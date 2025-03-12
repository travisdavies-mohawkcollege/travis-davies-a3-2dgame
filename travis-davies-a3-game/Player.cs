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
    public class Player
    {
        //Vector2 playerPos1 = new Vector2(400,540);
        // Vector2 playerPos2 = new Vector2(400, 560);
        public float playerPosX = 400;
        public float playerPosY = 550;
        public Vector2 playerPosRect1 = new Vector2();
        
        public Vector2 playerSizeRect1 = new Vector2();
        public float leftEdge;
        public float rightEdge;
        public float topEdge;
        public float bottomEdge;






        public void Update()
        {

        }

        public void PlayerHandler()
        {
            //playerPosX = Input.GetMouseX() - 30;
           if(Input.IsKeyboardKeyDown(KeyboardInput.A))
            {
                playerPosX -= 5;
            }
            if (Input.IsKeyboardKeyDown(KeyboardInput.D))
            {
                playerPosX += 5;
            }


            Draw.LineSize = 1;
            Draw.FillColor = MohawkGame2D.Color.Red;
            Draw.Rectangle(playerPosX, playerPosY, 60, 10);

            //PlayerCollisionBounds();

        }

        public void PlayerCollisionBounds()
        {
            playerPosRect1.X = playerPosX;
            playerPosRect1.Y = playerPosY;
            playerSizeRect1.X = 60;
            playerSizeRect1.Y = 10;


            leftEdge = playerPosRect1.X;
            rightEdge = playerPosRect1.X + playerSizeRect1.X;
            topEdge = playerPosRect1.Y;
            bottomEdge = playerPosRect1.Y + playerSizeRect1.Y;
            //Console.WriteLine("Left: " + leftEdge + " Right: " + rightEdge + " Top: " + topEdge + " Bottom: " + bottomEdge);
            



        }
    }
}
