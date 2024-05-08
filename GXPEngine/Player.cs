using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;

public class Player : RigidBody
{
    
    private float jumpForce = 6f;
    
    public Player(string filename, int cols, int rows, Vec2 pos, bool moving, bool keepInCache = false, bool addCollider = true) : base(filename, cols, rows, pos, moving, keepInCache, addCollider)
    {
        bounciness = 0;
        isPlayer = true;
    }

    public void Update()
    {
        base.Update();

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

        
        if (Input.GetKey(Key.LEFT))
        {
            acceleration.SetXY(-0.1f, acceleration.y);
        }
        else if (Input.GetKey(Key.RIGHT))
        {
            acceleration.SetXY(0.1f, acceleration.y);
        }
        else
        {
            acceleration.x = 0;
        }

        if (Input.GetKeyDown(Key.SPACE) && grounded && tempY+5 > position.y && tempY-5 < position.y) { velocity.SetXY(velocity.x, -jumpForce); grounded = false; }


        if (y > myGame.water.y) Death();
        

    }

    public void Death()
    {
        myGame.rigidBodies.Remove(this);
        LateDestroy();
    }

}

