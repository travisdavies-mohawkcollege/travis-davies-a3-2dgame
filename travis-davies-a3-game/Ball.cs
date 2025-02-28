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
        public void BallManager()
        {
            Draw.LineSize = 1;
            Draw.FillColor = Color.Red;
            Draw.Circle(ballPos, 5);


        }
    }
}
