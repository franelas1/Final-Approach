using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;

public class Player : RigidBody
{
    private float maxSpeed = 3;
    
    public Player(string filename, int cols, int rows, Vec2 pos, bool moving, bool keepInCache = false, bool addCollider = true) : base(filename, cols, rows, pos, moving, keepInCache, addCollider)
    {
        bounciness = 0;
    }

    public void Update()
    {
        base.Update();
        
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
        
        if (velocity.x >= maxSpeed)
        {
            
            velocity.x = maxSpeed;
        }
        if (velocity.x <= -maxSpeed)
        {
            velocity.x = -maxSpeed;
        }
        Console.WriteLine(acceleration.ToString());
    }

}

