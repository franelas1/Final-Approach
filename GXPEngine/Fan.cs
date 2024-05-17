using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
public class Fan : AnimationSprite
{
    AnimationSprite wind = new AnimationSprite("wind.png", 2, 4);

    public MyGame myGame;

    public float top;
    public float bottom;
    public float left;
    public float right;
    public float blowDistance;

    public Vec2 position;
    bool isLeft = false;
    bool active;
    bool switched = false;
    Button button;
    SoundChannel fanSFX;
    
    public Fan(string filename, int cols, int rows, Vec2 position, bool isLeft, float blowDistance, Button button = null, int frames = -1, bool keepInCache = false, bool addCollider = true) : base(filename, cols, rows, frames, keepInCache, addCollider)
    {
        myGame = (MyGame)game; 
        this.isLeft = isLeft;
        this.button = button;
        this.blowDistance = blowDistance;
        
        SetOrigin(width / 2, height / 2);
        SetXY(position.x, position.y);

        if (isLeft) { Mirror(true, false); }

        wind.SetOrigin(this.width / 2, this.height/2);
        AddChild(wind);

        top = y - height / 2;
        bottom = y + height / 2;
        left = x - width / 2;
        right = x + width / 2;
    }

    public void Update()
    {
        
        if (button == null)
        {
            active = true;
        }
        else if (button.isActivated)
        {
            
            active = true;
        }
        else
        {
            active = false;
        }


        if (active)
        {
            if (!switched)
            {
                fanSFX = new Sound("sfx/25.wav", true).Play();
                myGame.soundChannels.Add(fanSFX);
                switched = true;
                SetCycle(0, 2, 2);
            }
            CollisionCheck();
        }
        else if (!active && switched)
        {
            fanSFX.Stop();
            myGame.soundChannels.Remove(fanSFX);
            switched = false;
            SetCycle(currentFrame, 1);
        }
        Animate();
        Wind();
    }

    private void CollisionCheck()
    {
        foreach (RigidBody other in myGame.rigidBodies)
        {
            if(isLeft)
            {
                if (other.top > top && other.top < bottom && other.right < right && other.isPushable && x - other.x < blowDistance)
                {
                    other.acceleration.SetXY(-0.23f, other.acceleration.y);
                }
            }
            if(!isLeft)
            {
                if(other.top > top && other.top < bottom && other.left > left && other.isPushable && x - other.x > -blowDistance)
                {
                    other.acceleration.SetXY(0.23f,other.acceleration.y);
                }
            }
        }
    }

    private void Wind()
    {
        wind.alpha = 0.7f;
        if (alpha == 0)
            wind.alpha = 0;
        if(active)
        {
            wind.SetCycle(0, 7, 5);

            if (isLeft)
            {
                wind.SetXY(-5f * 60, 0);
                wind.Mirror(true, false);
            }
            else
            {
                wind.SetXY(1.2f * 60, 0);
            }
        }
        else
        {
            wind.SetCycle(7, 1);
        }

        wind.Animate();
    }

}
