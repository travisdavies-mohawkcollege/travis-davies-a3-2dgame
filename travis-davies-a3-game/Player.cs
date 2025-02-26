using MohawkGame2D;
using System;
using System.Collections.Generic;
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
        float playerPosX;
        float playerPosY = 550;


        public void Update()
        {

        }

        public void PlayerHandler()
        {
            playerPosX = Input.GetMouseX() - 30;
           
            Draw.LineSize = 1;
            Draw.FillColor = Color.Red;
            Draw.Rectangle(playerPosX, playerPosY, 60, 10);
        }
    }
}
