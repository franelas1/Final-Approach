using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;


public class Door : RigidBody
{
    bool active;
    bool switched = false;
    Button button;
   
    Vec2 origin;
    
    Vec2 target;
    float moveSpeed = 5f;

    Sound moveSFX = new Sound("sfx/11.wav");

    public Door(string filename, int cols, int rows, Vec2 pos, bool moving, Button button, Vec2 target,int frames = -1, int rotation = 0, bool keepInCache = false, bool addCollider = true) : base(filename, cols, rows, pos, moving, frames, keepInCache, addCollider)
    {
        SetXY(pos.x, pos.y);
        this.button = button;
        this.position = pos;
        this.origin = pos;
        this.target = target;
        this.rotation = rotation;
        SetScaleXY(0.6f, 0.6f);        
    }

    void MoveToTarget() 
    {
        if (!position.Equals(target))
        {
            Console.WriteLine("moving");
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
            if (!switched)
            {
                moveSFX.Play();
                switched = true;
            }
            active = true;
        }
        else
        {
            if (switched)
            {
                moveSFX.Play();
                switched = false;
            }
            active = false;
        }

        if (active)
        {
            MoveToTarget();
        }
        else MoveToOrigin();

        
    }
}

