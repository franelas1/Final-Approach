using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;

public class Turtle : RigidBody
{
    SoundChannel turtleSFX;
    AnimationSprite animation;
    public Turtle(string filename, int cols, int rows, Vec2 pos, bool moving, int frames = -1, bool keepInCache = false, bool addCollider = true) : base(filename, cols, rows, pos, moving, frames, keepInCache, addCollider)
    {
        isTurtle = true;

        turtleSFX = new Sound("sfx/26.wav", true, true).Play();
        turtleSFX.IsPaused = true;
        myGame.soundChannels.Add(turtleSFX);

        animation = new AnimationSprite("turtle.png", 3, 3, 7);
        AddChild(animation);
        animation.SetCycle(0,7,7);
        animation.SetXY(-85, -39);

    }

    public void Update()
    {
        
        base.Update();

        if (y >= myGame.water.y - 15)
        {
            velocity.SetXY(velocity.x, myGame.water.y - y);
            inWater = true;
            animation.Animate();
        }

        if (inWater && y < myGame.water.y + 5) 
        {
            turtleSFX.IsPaused = false;
            
            if (!flipped)
            {
                Mirror(false, false);
                animation.Mirror(false, false);
                velocity.SetXY(2.3f, velocity.y);
                animation.SetXY(-85, -39);

            }

            else
            {
                Mirror(true, false);
                animation.Mirror(true, false);
                velocity.SetXY(-2.3f, velocity.y);
                animation.SetXY(-157, -39);
            }
            
        }
        else { velocity.SetXY(0,velocity.y); turtleSFX.IsPaused = true; }
    }
}

