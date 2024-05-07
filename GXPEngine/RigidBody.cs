using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Managers;

public class RigidBody
    {
    private Vec2 position;
    private Vec2 velocity;
    private Vec2 acceleration;

    private RigidBody() { }
    
    public void Update() 
        {
        //Euler Integration
        velocity += acceleration;
        position += velocity;
        }   
    }


