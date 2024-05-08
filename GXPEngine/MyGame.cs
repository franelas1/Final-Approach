using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;
using System.Collections.Generic;
using TiledMapParser;                           // System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game {

	public List<RigidBody> rigidBodies = new List<RigidBody>();

    float waterSpeed = 0.5f;
	public AnimationSprite water;
    public MyGame() : base(1920, 1080, false)     // Create a window that's 800x600 and NOT fullscreen
	{
		
		RigidBody ball1 = new RigidBody("square.png", 1, 1, new Vec2(300, 350), true);
		AddChild(ball1);
		rigidBodies.Add(ball1);
        ball1.isPushable = true;
        ball1.scale = 0.9f;

        RigidBody floor = new RigidBody("square.png", 1, 1, new Vec2(width/2, 200), false);
        AddChild(floor);
        rigidBodies.Add(floor);
		floor.scaleX = 40;
		floor.followMouse = false;

        RigidBody floor1 = new RigidBody("square.png", 1, 1, new Vec2(width / 6, height - 200), false);
        AddChild(floor1);
        rigidBodies.Add(floor1);
        floor1.scaleX = 40;
        floor1.followMouse = false;

        Fan fan = new Fan("checkers.png", 1, 1, new Vec2(100, 800), false);
        AddChild(fan);

        /*
        RigidBody wall = new RigidBody("square.png", 1, 1, new Vec2(1200 , height / 2), false);
        AddChild(wall);
        rigidBodies.Add(wall);
        wall.scaleY = 40;
        wall.followMouse = false;
        */

        Player player = new Player("colors.png", 1,1, new Vec2(50, 350), true);
		AddChild(player); 
		rigidBodies.Add(player);

		water = new AnimationSprite("square.png", 1, 1);
		AddChild(water);
		water.alpha = 0.3f;
		water.x = -50;
		water.y = height - 100;
		water.width = width + 100;
		water.height = height;
    }
	// For every game object, Update is called every frame, by the engine:
	void Update() 
	{
		WaterControls();
		
	}

    void WaterControls()
	{
        if (Input.GetKey(Key.DOWN))
        {
            water.y += waterSpeed;
        }
        if (Input.GetKey(Key.UP))
        {
            water.y -= waterSpeed;
        }
    }

    static void Main()                          // Main() is the first method that's called when the program is run
	{
		new MyGame().Start();                   // Create a "MyGame" and start it
	}
}