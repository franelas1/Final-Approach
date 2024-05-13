using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
public class Fan : AnimationSprite
{
    public MyGame myGame;

    public float top;
    public float bottom;
    public float left;
    public float right;

    public Vec2 position;
    bool isLeft = false;
    bool active;
    Button button;
    public Fan(string filename, int cols, int rows, Vec2 position, bool isLeft, Button button = null, int frames = -1, bool keepInCache = false, bool addCollider = true) : base(filename, cols, rows, frames, keepInCache, addCollider)
    {
        myGame = (MyGame)game; 
        this.isLeft = isLeft;
        this.button = button;

        SetOrigin(width / 2, height / 2);
        SetXY(position.x, position.y);

        if (isLeft) { Mirror(true, false); }

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
            CollisionCheck();
        }
    }

    private void CollisionCheck()
    {
        foreach (RigidBody other in myGame.rigidBodies)
        {
            if(isLeft)
            {
                if (other.top > top && other.top < bottom && other.right < left && (other.isPushable /*|| other.isPlayer*/))
                {
                    other.acceleration.SetXY(-0.23f, other.acceleration.y);
                }
            }
            if(!isLeft)
            {
                if(other.top > top && other.top < bottom && other.left > right && (other.isPushable /*|| other.isPlayer*/))
                {
                    other.acceleration.SetXY(0.23f,other.acceleration.y);
                }
            }
        }
    }

}
