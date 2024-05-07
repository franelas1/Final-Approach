using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Managers;

public class RigidBody : AnimationSprite
    {
    private MyGame myGame;

    public Vec2 position;
    public Vec2 velocity;
    private float gravity = 0.1f;
    public Vec2 acceleration;
    public float bounciness = 0.4f;
    public bool followMouse = false;
    public bool moving;
    float t;

    public float top;
    public float bottom;
    public float left;
    public float right;

    public RigidBody(string filename, int cols, int rows, Vec2 pos, bool moving, bool keepInCache = false, bool addCollider = true) : base(filename, rows: 1, cols: 1)
    {
        myGame = (MyGame)game;
        SetOrigin(width / 2, height / 2);
        position = pos;
        velocity = new Vec2(0,0);
        
        this.moving = moving;
    }

    

    public void Update() 
    {
        
        acceleration.y = gravity;
        //Follow Mouse
        if (followMouse)
        {
            acceleration = (new Vec2(Input.mouseX, Input.mouseY) - position).Normalized();
            acceleration.SetXY(acceleration.x / 10, acceleration.y / 10);
        }
        //

        if (y >= myGame.water.y && moving)
        {
            acceleration.SetXY(acceleration.x, 0);
            if (velocity.y > 0.1) { acceleration.SetXY(acceleration.x, velocity.y * -0.1f); }
            else if (y - myGame.water.y > 5) { acceleration.SetXY(acceleration.x, -0.05f); Console.WriteLine("buh"); }

        }
        
        if (moving)
        {
            if (acceleration.x == 0)
            {
                acceleration.x = -velocity.x / 30;
            }
            //Euler Integration
            if (!CheckCollisions())
            {
                velocity += acceleration;
                position += velocity;
            }
            else
            {
                velocity.x += acceleration.x;
                position += velocity * (1 - t);
                
            }
        }
        SetXY(position.x, position.y);
            
        
        
        top = y - height / 2;
        bottom = y + height / 2;
        left = x - width / 2;
        right = x + width / 2;
    }

    public bool CheckCollisions()
    {
        t = 0;
        bool collided = false;

        Vec2 simV = velocity;
        Vec2 sim = position;

        Vec2 poi = position;

        simV += acceleration;
        sim += simV;

        foreach (RigidBody other in myGame.rigidBodies)
        {
            if (other == this) continue;

            if (other.top < sim.y + (height / 2) && other.left < right && other.right > left)
            {
                t = (Mathf.Abs((other.top) - top)) / (Mathf.Abs(sim.x - position.y));

                velocity.y = -velocity.y * bounciness;
                collided = true;
            }

            if (t != 0)
                position = position + t * velocity;
        }
        return collided;
    }

}


