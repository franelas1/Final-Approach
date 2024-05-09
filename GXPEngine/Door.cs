using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;


public class Door : RigidBody
{
    bool active;
    Button button;
   
    Vec2 origin;
    
    Vec2 target;
    float moveSpeed = 4f;


    public Door(string filename, int cols, int rows, Vec2 pos, bool moving, Button button, Vec2 target, int rotation = 0, bool keepInCache = false, bool addCollider = true) : base(filename, cols, rows, pos, moving, keepInCache, addCollider)
    {
        SetXY(pos.x, pos.y);
        this.button = button;
        this.position = pos;
        this.origin = pos;
        this.target = target;
        this.rotation = rotation;

        scaleX = 0.5f;
        scaleY = 4;
    }

    void MoveToTarget() 
    {
        if (!position.Equals(target))
        {
            velocity = (target - position).Normalized() * moveSpeed;
        }
        else velocity.SetXY(0, 0);
    }
    void MoveToOrigin() 
    {
        if (!position.Equals(origin))
        {
            velocity = (origin - position).Normalized() * moveSpeed;
        }
        else velocity.SetXY(0, 0);
    }

    public void Update()
    {
        base.Update();

        if(button.isActivated)
        {
            active = true;
        }
        else
        {
            active = false;
        }

        if (active)
        {
            MoveToTarget();
        }
        else MoveToOrigin();

        
    }
}

