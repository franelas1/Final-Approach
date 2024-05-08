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
    public Fan(string filename, int cols, int rows, Vec2 position, bool isLeft, int frames = -1, bool keepInCache = false, bool addCollider = true) : base(filename, cols, rows, frames, keepInCache, addCollider)
    {
        myGame = (MyGame)game; 
        this.isLeft = isLeft;

        SetXY(position.x, position.y);

        if (isLeft) { Mirror(false, true); }

        top = y - height / 2;
        bottom = y + height / 2;
        left = x - width / 2;
        right = x + width / 2;
    }

    public void Update()
    {
        CollisionCheck();
    }

    private void CollisionCheck()
    {
        foreach (RigidBody other in myGame.rigidBodies)
        {
            if(isLeft)
            {
                
            }
            if(!isLeft)
            {
                if(other.top > top && other.top < bottom && (other.isPushable||other.isPlayer))
                {
                    other.acceleration.SetXY(0.1f,other.acceleration.y);
                }
            }
        }
    }

}
