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
    public class Ball
    {
         public  Vector2 ballPos = new Vector2(400, 400);
        public Vector2 velocity;
        public Vector2 speed = new Vector2(0, +100);
        public Player player = new Player();
        public float radius = 5;
        public float ballCount = 1;

        public Ball(Vector2 pos)
        {
            ballPos = pos;
        }
        
        
        public void BallManager()
        {
            
            Draw.LineSize = 1;
           //Draw.FillColor = MohawkGame2D.Color.Red;
            Draw.Circle(ballPos, radius);

            velocity = speed * Time.DeltaTime;
            ballPos += velocity;


            //check for collision on window
            bool touchWall = ballPos.X < 0 || ballPos.X > 800;
            bool touchCeiling = ballPos.Y < 0;
            bool touchFloor = ballPos.Y >= 600;

            // Update player bounds before checking for collision
           ///player.PlayerCollisionBounds();

           

            if (touchWall)
            {
                speed.X = -speed.X;
            }
            if (touchCeiling)
            {
                speed.Y = -speed.Y;
            }
            if (touchFloor)
            {
              
                // speed.Y = -speed.Y;
            }
        }

        public void ResetBall()
        {
            ballPos.X = 400;
            ballPos.Y = 400;
        }
    }

}

