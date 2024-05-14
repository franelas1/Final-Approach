using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;

public class Turtle : RigidBody
{
    SoundChannel turtleSFX;
    
    public Turtle(string filename, int cols, int rows, Vec2 pos, bool moving, int frames = -1, bool keepInCache = false, bool addCollider = true) : base(filename, cols, rows, pos, moving, frames, keepInCache, addCollider)
    {
        isTurtle = true;

        turtleSFX = new Sound("sfx/26.wav", true, true).Play();
        turtleSFX.IsPaused = true;
        myGame.soundChannels.Add(turtleSFX);
    }

    public void Update()
    {
        
        base.Update();

        if (y >= myGame.water.y - 15)
        {
            velocity.SetXY(velocity.x, myGame.water.y - y);
            inWater = true;
        }

        if (inWater) 
        {
            turtleSFX.IsPaused = false;
            
            if (!flipped)
            {
                Mirror(false, false);
                velocity.SetXY(2.3f, velocity.y);
                
            }

            else
            {
                Mirror(true, false);
                velocity.SetXY(-2.3f, velocity.y);
            }
            
        }
        else { velocity.SetXY(0,velocity.y); turtleSFX.IsPaused = true; }
    }
}

