// Include the namespaces (code libraries) you need below.
using System;
using System.Numerics;
using Microsoft.Win32.SafeHandles;
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
        Player2 player2 = new Player2();
        Bricks bricks = new Bricks();
        Powerups[] powerups = new Powerups[6];
        Powerups powerup = new Powerups();
        Ball[] balls;
        int gameState = 0;
        int[] ballState = new int[15];
        Vector2[] powerupPos = new Vector2[6];
        int activePowerups = 0;


        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetSize(800, 600);
            Window.SetTitle("Brick Breaker Clone");
            gameState = 0;
            balls = new Ball[15];

        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(Color.White);
            if (gameState == 0)
            {
                //Run start menu
                player.PlayerHandler();
                player2.Player2Handler();
                Draw.LineSize = 1;
                balls[0] = new Ball(new Vector2(400, 400));
                Draw.FillColor = MohawkGame2D.Color.Gray;
                Draw.Circle(balls[0].ballPos, balls[0].radius);
                StartScene();
                if (Input.IsMouseButtonPressed(MouseInput.Left))
                {
                    gameState = 1;
                }
            }
            if (gameState == 1)
            {
                //Run game

                player.PlayerHandler();
                player.PlayerCollisionBounds();
                player2.Player2Handler();
                player2.Player2CollisionBounds();
                PlayerCollision();
                Player2Collision();
                bricks.BrickCreation();
                BrickCollision();
                for (int p = 0; p < activePowerups; p++)
                {
                   // Console.WriteLine("Powerup In Draw code");
                    // if (powerups[p] == null) return;
                   // Console.WriteLine(activePowerups + "Powerup Active");
                    powerups[p].DrawPowerup();
                }
                for (int i = 0; i < balls.Length; i++)
                {
                    if (balls[i] == null) return;
                    if (ballState[i] == 0)
                    {
                        Draw.FillColor = MohawkGame2D.Color.Gray;
                        balls[i].BallManager();
                    }
                    if (ballState[i] == 1)
                    {
                        Draw.FillColor = MohawkGame2D.Color.Red;
                        balls[i].BallManager();
                    }
                    if (ballState[i] == 2)
                    {
                        Draw.FillColor = MohawkGame2D.Color.Blue;
                        balls[i].BallManager();
                    }
                }


                




                if (balls[0].ballPos.Y >= 600)
                {
                    gameState = 2;
                }

            }
            if (gameState == 2)
            {

                //Run gameover screen
                GameOver();
                bricks.Reset();
                balls[0].ResetBall();
                if (Input.IsMouseButtonPressed(MouseInput.Left))
                {
                    gameState = 0;
                }
            }




        }


        public void PlayerCollision()
        {
            for (int i = 0; i < balls.Length; i++)
            {
                if (balls[i] == null) return;



                bool isWithinX = balls[i].ballPos.X + balls[i].radius > player.leftEdge && balls[i].ballPos.X - balls[i].radius < player.rightEdge;
                bool isWithinY = balls[i].ballPos.Y + balls[i].radius > player.topEdge && balls[i].ballPos.Y - balls[i].radius < player.bottomEdge;

                bool touchPlayer = isWithinX && isWithinY;

                if (touchPlayer)
                {

                    ballState[i] = 1;
                    Console.WriteLine("Touching Player");
                    balls[i].speed.Y = -balls[i].speed.Y;
                    balls[i].ballPos.Y = player.topEdge - balls[i].radius; // Correctly position ball on top of the player
                                                                           //check where on paddle ball is, change x accordingly
                    float playerCenter = player.leftEdge + player.rightEdge / 2;
                    float ballCenter = balls[i].ballPos.X;
                    float distance = ballCenter - playerCenter;
                    if (balls[i].ballPos.X < player.rightEdge && balls[i].ballPos.X > player.playerPosX + 30)
                    {
                        balls[i].speed.X += 50;
                    }
                    if (balls[i].ballPos.X > player.leftEdge && balls[i].ballPos.X < player.playerPosX + 30)
                    {
                        balls[i].speed.X -= 50;

                    }
                }
            }
        }

        public void Player2Collision()
        {
            for (int i = 0; i < balls.Length; i++)
            {
                if (balls[i] == null) return;
                bool isWithinX = balls[i].ballPos.X + balls[i].radius > player2.leftEdge2 && balls[i].ballPos.X - balls[i].radius < player2.rightEdge2;
                bool isWithinY = balls[i].ballPos.Y + balls[i].radius > player2.topEdge2 && balls[i].ballPos.Y - balls[i].radius < player2.bottomEdge2;

                bool touchPlayer = isWithinX && isWithinY;

                if (touchPlayer)
                {

                    ballState[i] = 2;
                    Console.WriteLine("Touching Player");
                    balls[i].speed.Y = -balls[i].speed.Y;
                    balls[i].ballPos.Y = player2.bottomEdge2 + balls[i].radius; // Correctly position ball on top of the player
                    //check where on paddle ball is, change x accordingly
                    float playerCenter2 = player2.leftEdge2 + player2.rightEdge2 / 2;
                    float ballCenter = balls[i].ballPos.X;
                    float distance = ballCenter - playerCenter2;
                    if (balls[i].ballPos.X < player2.rightEdge2 && balls[i].ballPos.X > player2.player2PosX + 30)
                    {
                        balls[i].speed.X += 50;
                    }
                    if (balls[i].ballPos.X > player2.leftEdge2 && balls[i].ballPos.X < player2.player2PosX + 30)
                    {
                        balls[i].speed.X -= 50;

                    }
                }
            }
        }

        public void BrickCollision()
        {
            for (int i = 0; i < balls.Length; i++)
            {
                if (balls[i] == null) return;

                for (int b = 0; b < bricks.bricks.Length; b++)
                {
                    bool isWithinX = balls[i].ballPos.X + balls[i].radius > bricks.bricks[b].X && balls[i].ballPos.X - balls[i].radius < bricks.bricks[b].X + bricks.brickW;
                    bool isWithinY = balls[i].ballPos.Y + balls[i].radius > bricks.bricks[b].Y && balls[i].ballPos.Y - balls[i].radius < bricks.bricks[b].Y + bricks.brickH;
                    bool touchRight = balls[i].ballPos.X > bricks.bricks[b].X + bricks.brickW;
                    bool touchLeft = balls[i].ballPos.X < bricks.bricks[b].X;
                    bool touchTop = balls[i].ballPos.Y - balls[i].radius < bricks.bricks[b].Y + bricks.brickH;
                    bool touchBottom = balls[i].ballPos.Y + balls[i].radius > bricks.bricks[b].Y;
                    bool touchBrick = isWithinX && isWithinY;
                    if (touchBrick && !bricks.isDestroyed[b])
                    {
                        powerup.doSpawnPowerup(powerup.doSpawn);
                        bool powerupSpawn = powerup.doSpawnPowerup(powerup.doSpawn);
                        Console.WriteLine("Game Class Powerup Check " + powerup.doSpawn);
                        Console.WriteLine("Game Class Powerup Check 2" + powerupSpawn);
                        if (powerupSpawn)
                        {
                            for (int ps = 0; ps < powerups.Length; ps++)
                            {
                                if (powerups[ps] == null)
                                {
                                    powerups[ps] = new Powerups();
                                    powerupPos[ps] = new Vector2(bricks.bricks[b].X, bricks.bricks[b].Y);
                                    powerups[ps].InitializePowerup(powerupPos[ps]);
                                    powerup.doSpawn = false;
                                    activePowerups++;
                                    Console.WriteLine("Powerup Position Logged, Active Powerups " + activePowerups);

                                    break;
                                }

                            }
                            Console.WriteLine("Powerup Spawned");
                        }
                        Console.WriteLine("Touching Brick,   TouchLeft " + touchLeft + " TouchRight " + touchRight);
                        if (touchRight)
                        {
                            balls[i].speed.X += 25;
                            balls[i].speed.Y = -balls[i].speed.Y;
                            if (balls[i].speed.X < 0)
                            {
                                balls[i].speed.X = -balls[i].speed.X;
                            }

                        }
                        if (touchLeft)
                        {
                            balls[i].speed.X -= 25;
                            balls[i].speed.Y = -balls[i].speed.Y;
                            if (balls[i].speed.X > 0)
                            {
                                balls[i].speed.X = -balls[i].speed.X;
                            }
                        }
                        else
                        {
                            balls[i].speed.Y = -balls[i].speed.Y;
                        }

                        bricks.isDestroyed[b] = true;




                    }
                }
            }
        }


        public void StartScene()
        {
            Text.Color = Color.Red;
            Text.Size = 25;
            Text.Draw("Left Click to Start!", 250, 200);
        }


        public void GameOver()
        {
            Text.Color = Color.Black;
            Text.Size = 50;
            Text.Draw("GAME", 350, 200);
            Text.Draw("OVER!", 350, 400);
        }
    }

}
