using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;

public class Button : AnimationSprite
{
    public bool isActivated = false;
    MyGame myGame;

    public float top;
    public float bottom;
    public float left;
    public float right;

    public Button(string filename, int cols, int rows, Vec2 position, int frames = -1, bool keepInCache = false, bool addCollider = true) : base(filename, cols, rows, frames, keepInCache, addCollider)
    {
        myGame = (MyGame)game;
        
        SetOrigin(width / 2, height / 2);
        SetXY(position.x, position.y);
        top = y - height / 2;
        bottom = y + height / 2;
        left = x - width / 2;
        right = x + width / 2;
    }

    public void Update()
    {
        foreach (RigidBody other in myGame.rigidBodies) 
        {
            if (((other.left < left && other.right > left) || (other.right > right && other.left < right) || (other.left > left && other.right < right)) 
                && ((other.bottom > top && other.top < top) || (other.top < bottom && other.bottom > top)) 
                && (other.isPushable || other.isPlayer))
            {
                isActivated = true;
                break;
            }
            else isActivated = false;
        }
    }
}

