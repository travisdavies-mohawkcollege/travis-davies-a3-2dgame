// Include the namespaces (code libraries) you need below.
using System;
using System.Numerics;
using travis_davies_a3_game;

// The namespace your code is in.
namespace MohawkGame2D
{
    /// <summary>
    ///     Your game code goes inside this class!
    /// </summary>
    public class Game
    {
        // Place your variables here:
        Player player = new Player();
        Bricks bricks = new Bricks();
        Ball ball = new Ball();

        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetSize(800, 600);
            Window.SetTitle("Brick Breaker Clone");

        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(Color.White);
            player.PlayerHandler();
            player.PlayerCollisionBounds();
            PlayerCollision();
            bricks.BrickCreation();
            BrickCollision();
            ball.BallManager();

        }


        public void PlayerCollision()
        {
            bool isWithinX = ball.ballPos.X + ball.radius > player.leftEdge && ball.ballPos.X - ball.radius < player.rightEdge;
            bool isWithinY = ball.ballPos.Y + ball.radius > player.topEdge && ball.ballPos.Y - ball.radius < player.bottomEdge;

            bool touchPlayer = isWithinX && isWithinY;

            if (touchPlayer)
            {
                Console.WriteLine("Touching Player");
                ball.speed.Y = -ball.speed.Y;
                ball.ballPos.Y = player.topEdge - ball.radius; // Correctly position ball on top of the player
            }
        }

        public void BrickCollision()
        {
            for (int i = 0; i < bricks.bricks.Length; i++)
            {
                bool isWithinX = ball.ballPos.X + ball.radius > bricks.bricks[i].X && ball.ballPos.X - ball.radius < bricks.bricks[i].X + bricks.brickW;
                bool isWithinY = ball.ballPos.Y + ball.radius > bricks.bricks[i].Y && ball.ballPos.Y - ball.radius < bricks.bricks[i].Y + bricks.brickH;
                bool touchBrick = isWithinX && isWithinY;
                if (touchBrick && !bricks.isDestroyed[i])
                {
                    Console.WriteLine("Touching Brick");
                    ball.speed.Y = -ball.speed.Y;
                    bricks.isDestroyed[i] = true;
                }
            }
        }

    }
}
