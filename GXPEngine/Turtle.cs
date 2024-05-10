using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;

public class Turtle : RigidBody
{

    
    public Turtle(string filename, int cols, int rows, Vec2 pos, bool moving, bool keepInCache = false, bool addCollider = true) : base(filename, cols, rows, pos, moving, keepInCache, addCollider)
    {
        isTurtle = true;
        SetScaleXY(2,0.5f);
    }

    public void Update()
    {
        base.Update();

        if (y >= myGame.water.y - 5)
        {
            velocity.SetXY(velocity.x, myGame.water.y - y);
            inWater = true;
        }

        if (inWater) 
        {
            Console.WriteLine(flipped);
            if (!flipped)
            {
                Mirror(false, false);
                velocity.SetXY(1, velocity.y);
            }

            else
            {
                Mirror(true, false);
                velocity.SetXY(-1, velocity.y);
            }
            
        }
        else { velocity.SetXY(0,velocity.y); }
    }
}

