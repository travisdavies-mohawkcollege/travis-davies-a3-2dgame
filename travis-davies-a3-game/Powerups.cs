using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MohawkGame2D;

namespace travis_davies_a3_game
{
    public class Powerups
    {
        public bool doSpawn;
        public float powerupPosX;
        public float powerupPosY;
        public int powerupDirection = 0;
        public bool doSpawnPowerup(bool doSpawn)
        {
            bool chance = MohawkGame2D.Random.Bool();
            if (chance)
            {
                doSpawn = true;
                Console.WriteLine("Powerup Spawn True");    
            }
            if (!chance)
            {
                doSpawn = false;
                Console.WriteLine("Powerup Spawn False");
            }
            return doSpawn;
        }

        public void InitializePowerup(Vector2 position, int direction )
        {
            
            powerupPosX = position.X +25;
            powerupPosY = position.Y +10;
            powerupDirection = direction;
            Console.WriteLine("Powerup X: " + powerupPosX + " Powerup Y: " + powerupPosY);



        }

        public void DrawPowerup()
        {
           
            
            
            Draw.FillColor = MohawkGame2D.Color.Green;
            Draw.Circle(powerupPosX, powerupPosY, 5);
            
            if (powerupDirection == 1)
            {
                powerupPosY += 25 * Time.DeltaTime;
            }
            if (powerupDirection == 2)
            {
                powerupPosY -= 25 * Time.DeltaTime;
            }
        }

        
    }
}
