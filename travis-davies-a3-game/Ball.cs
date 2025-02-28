using MohawkGame2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace travis_davies_a3_game
{
    public class Ball
    {
        Vector2 ballPos = new Vector2(400, 400);
        Vector2 velocity;
        Vector2 speed = new Vector2(0, +35);
        Player player = new Player();
        float radius = 5;

        public void BallManager()
        {
            Draw.LineSize = 1;
            Draw.FillColor = Color.Red;
            Draw.Circle(ballPos, radius);

            velocity = speed * Time.DeltaTime;
            ballPos += velocity;

            bool touchWall = ballPos.X < 0 || ballPos.X > 800;
            bool touchCeiling = ballPos.Y < 0;
            bool touchTopPlayer = player.topEdge < ballPos.Y + radius;
            bool touchBottomPlayer = player.bottomEdge > ballPos.Y - radius;
            bool touchLeftPlayer = player.leftEdge < ballPos.X + radius;
            bool touchRightPlayer = player.rightEdge > ballPos.X - radius;

            bool touchPlayer = touchBottomPlayer && touchLeftPlayer && touchRightPlayer && touchTopPlayer;
            if (touchPlayer)
            {
                speed.Y = -speed.Y;
                ballPos.Y = player.playerPosY - radius;
            }

            if (touchWall)
            {
                speed.X = -speed.X;
            }
            if (touchCeiling)
            {
                speed.Y = -speed.Y;
            }

        }
            
    }
}
