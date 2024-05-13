using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;

public class Player : RigidBody
{
    bool won = false;
    private float jumpForce = 12f;
    
    public Player(string filename, int cols, int rows, Vec2 pos, bool moving, bool keepInCache = false, bool addCollider = true) : base(filename, cols, rows, pos, moving, keepInCache, addCollider)
    {
        bounciness = 0;
        isPlayer = true;
    }

    public void Update()
    {
        base.Update();
        inBell = false;

        /*
        if (Input.GetKey(Key.LEFT))
        {
           velocity.SetXY(maxSpeed, velocity.y);
        }
        else if (Input.GetKey(Key.RIGHT))
        {
            velocity.SetXY(maxSpeed, velocity.y);
        }
        */


        if (Input.GetKey(Key.A))
        {
            acceleration.SetXY(-0.23f, acceleration.y);
        }
        else if (Input.GetKey(Key.D))
        {
            acceleration.SetXY(0.23f, acceleration.y);
        }
        else
        {
            acceleration.x = 0;
        }

        if ((Input.GetKeyDown(Key.SPACE) || (Input.GetKeyDown(Key.W))) && grounded && tempY+13 > position.y && tempY-13 < position.y) { velocity.SetXY(velocity.x, -jumpForce); grounded = false; }

        foreach (Sprite other in myGame.divingBells)
        {
            if (y > other.y - other.height / 2 && y < other.y + other.height / 2 &&
                x > other.x - other.width / 2 && y < other.x + other.height / 2)
            {
                if(other.alpha != 1f)
                {
                    inBell = true;
                    continue;
                }
                else
                {
                    won = true;
                }
            }
        }
        
        if (y > myGame.water.y && !inBell) Death();
        if (won)
        {
            myGame.currentLevel++;
            Death();
        }
    }

    public void Death()
    {
        myGame.rigidBodies.Remove(this);
        LateDestroy();
        myGame.Reload();
    }

    

}

