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
        int[] powerUpDirection = new int[6];
        Vector2[] powerupPos = new Vector2[6];
        int activePowerups = 0;
        int activeBalls = 0;


        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetSize(800, 600);
            Window.SetTitle("Brick Breaker Clone");
            gameState = 0;
            balls = new Ball[1000];
            activeBalls = 2;
            
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
                balls[0] = new Ball(new Vector2(400, 500), ballState[0]);
                balls[1] = new Ball(new Vector2(400, 100), ballState[1]);
                Draw.FillColor = MohawkGame2D.Color.Yellow;
                Draw.Circle(balls[0].ballPos, balls[0].radius);
                Draw.FillColor = MohawkGame2D.Color.Red;
                Draw.Circle(balls[1].ballPos, balls[1].radius);
                ballState[0] = 1;
                ballState[1] = 2;
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
                PlayerPowerupCollision();
                player2.Player2Handler();
                player2.Player2CollisionBounds();
                Player2PowerupCollision();
                PlayerCollision();
                Player2Collision();
                bricks.BrickCreation();
                BrickCollision();
                OutOfBounds();
                //Handle each powerup
                for (int p = 0; p < powerups.Length; p++)
                {
                    // Console.WriteLine("Powerup In Draw code");
                    if (powerups[p] == null) continue;
                    // Console.WriteLine(activePowerups + "Powerup Active");
                    powerups[p].DrawPowerup();
                }
                //Handle Each Ball
                if (balls[0].ballPos.Y > 600)
                {
                    gameState = 2;
                }
                if (balls[1].ballPos.Y < 0)
                {
                    gameState = 2;
                }
                if (balls[0].ballPos.Y <= 0)
                {
                    balls[0].speed.Y = -balls[0].speed.Y;
                }
                if (balls[1].ballPos.Y >= 600)
                {
                    balls[1].speed.Y = -balls[1].speed.Y;
                }

                for (int i = 0; i < balls.Length; i++)
                {
                    if (balls[i] == null) return;
                    if (ballState[i] == 0)
                    {

                        Draw.FillColor = MohawkGame2D.Color.Gray;
                        if (i == 0)
                        {
                            Draw.FillColor = MohawkGame2D.Color.Yellow;

                        }
                        if (i == 1)
                        {
                            Draw.FillColor = MohawkGame2D.Color.Red;

                        }

                        balls[i].BallManager();
                    }
                    if (ballState[i] == 1)
                    {

                        Draw.FillColor = MohawkGame2D.Color.Red;
                        if (i == 0)
                        {
                            Draw.FillColor = MohawkGame2D.Color.Yellow;

                        }
                        if (i == 1)
                        {
                            Draw.FillColor = MohawkGame2D.Color.Red;

                        }

                        balls[i].BallManager();
                    }
                    if (ballState[i] == 2)
                    {
                        Draw.FillColor = MohawkGame2D.Color.Blue;
                        if (i == 0)
                        {
                            Draw.FillColor = MohawkGame2D.Color.Yellow;

                        }
                        if (i == 1)
                        {
                            Draw.FillColor = MohawkGame2D.Color.Red;

                        }

                        balls[i].BallManager();
                    }

                }









            }
            if (gameState == 2)
            {

                //Run gameover screen
                GameOver();
                bricks.Reset();
                
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
                    //Console.WriteLine("Touching Player");
                    balls[i].speed.Y = -balls[i].speed.Y;
                    balls[i].ballPos.Y = player.topEdge - balls[i].radius; // Correctly position ball on top of the player
                                                                           //check where on paddle ball is, change x accordingly
                    float playerCenter = player.leftEdge + player.rightEdge / 2;
                    float ballCenter = balls[i].ballPos.X;
                    float distance = ballCenter - playerCenter;
                    if (balls[i].ballPos.X < player.rightEdge && balls[i].ballPos.X > player.playerPosX)
                    {
                        balls[i].speed.X += 50;
                    }
                    if (balls[i].ballPos.X > player.leftEdge && balls[i].ballPos.X < player.playerPosX)
                    {
                        balls[i].speed.X -= 50;

                    }
                }
            }


        }

        public void PlayerPowerupCollision()
        {
            for (int p1 = 0; p1 < activePowerups; p1++)
            {
                if (powerups[p1] == null) continue;
                bool isWithinX = powerups[p1].powerupPosX > player.leftEdge && powerups[p1].powerupPosX < player.rightEdge;
                bool isWithinY = powerups[p1].powerupPosY > player.topEdge && powerups[p1].powerupPosY < player.bottomEdge;


                bool touchPlayer = isWithinX && isWithinY;

                if (touchPlayer)
                {
                    powerups[p1] = null;
                    activePowerups--;
                    Console.WriteLine("Player 1 powerup get!");
                    for (int b = 0; b < balls.Length; b++)
                    {

                        if (balls[b] == null) return;
                        for (b = 0; b < balls.Length; b++)
                        {
                            if (balls[b] == null)
                            {
                                for (int c = 0; c < 4; c++)
                                {
                                    balls[b] = new Ball(balls[0].ballPos, 3);
                                    ballState[b] = 1;
                                    activeBalls++;
                                    break;
                                }

                                break;
                            }
                        }
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
                    if (balls[i].ballPos.X < player2.rightEdge2 && balls[i].ballPos.X > player2.player2PosX)
                    {
                        balls[i].speed.X += 50;
                    }
                    if (balls[i].ballPos.X > player2.leftEdge2 && balls[i].ballPos.X < player2.player2PosX)
                    {
                        balls[i].speed.X -= 50;

                    }
                }
            }
        }

        public void Player2PowerupCollision()
        {
            for (int p1 = 0; p1 < activePowerups; p1++)
            {
                if (powerups[p1] == null) continue;
                bool isWithinX = powerups[p1].powerupPosX > player2.leftEdge2 && powerups[p1].powerupPosX < player2.rightEdge2;
                bool isWithinY = powerups[p1].powerupPosY > player2.topEdge2 && powerups[p1].powerupPosY < player2.bottomEdge2;


                bool touchPlayer = isWithinX && isWithinY;

                if (touchPlayer)
                {
                    powerups[p1] = null;
                    activePowerups--;
                    Console.WriteLine("Player 2 powerup get!");
                    for (int b = 0; b < balls.Length; b++)
                    {

                        if (balls[b] == null) return;
                        for (b = 0; b < balls.Length; b++)
                        {
                            if (balls[b] == null)
                            {
                                for (int c = 0; c < 4; c++)
                                {
                                    balls[b] = new Ball(balls[1].ballPos, 4);
                                    ballState[b] = 2;
                                    activeBalls++;
                                    break;

                                }

                                break;
                            }
                        }
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
                        // Console.WriteLine("Game Class Powerup Check " + powerup.doSpawn);
                        // Console.WriteLine("Game Class Powerup Check 2" + powerupSpawn);
                        if (powerupSpawn)
                        {
                            for (int ps = 0; ps < powerups.Length; ps++)
                            {
                                if (powerups[ps] == null)
                                {
                                    powerups[ps] = new Powerups();
                                    powerupPos[ps] = new Vector2(bricks.bricks[b].X, bricks.bricks[b].Y);
                                    powerUpDirection[ps] = ballState[i];
                                    powerups[ps].InitializePowerup(powerupPos[ps], powerUpDirection[ps]);
                                    powerup.doSpawn = false;
                                    activePowerups++;
                                    //  Console.WriteLine("Powerup Position Logged, Active Powerups " + activePowerups);

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

        public void OutOfBounds()
        {
            bool[] deactivated = new bool[balls.Length];
            for (int i = 0; i < balls.Length; i++)
            {
                if (balls[i] == null) continue;
                if (balls[i].ballPos.Y > 600 || balls[i].ballPos.Y < 0)
                {
                    if (deactivated[i] == false)
                    {
                        activeBalls--;

                    }
                    deactivated[i] = true;
                }
            }

        }

       

    }
}
