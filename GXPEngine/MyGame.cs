using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;
using System.Collections.Generic;
using TiledMapParser;                           // System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game {

	public List<RigidBody> rigidBodies = new List<RigidBody>();
    public List<Sprite> divingBells = new List<Sprite>();
    public int currentLevel;
    float waterSpeed = 1.5f;
	public AnimationSprite water;
    public MyGame() : base(1920, 1080, false, false)     // Create a window that's 800x600 and NOT fullscreen
	{
        LoadLevel1();
		
    }
	// For every game object, Update is called every frame, by the engine:
	void Update() 
	{
		WaterControls();
        if (Input.GetKeyDown(Key.ZERO)) { currentLevel = 0; Reload();  }
        if (Input.GetKeyDown(Key.ONE)) { currentLevel = 1; Reload();  }
    }

    void Reload() 
    {
        rigidBodies.Clear();
       foreach (GameObject o in GetChildren())
        {
            o.LateDestroy();
        }

       switch (currentLevel) 
        {
            case 0:
                LoadDemo(); break;
                case 1:
                LoadLevel1(); break;
            default:
                break;
        }
    }
    void LoadLevel1()
    {
        currentLevel = 1;

        Sprite blank = new Sprite("square.png");
        AddChild(blank);
        blank.alpha = 0f;
        
        

        RigidBody wall = new RigidBody("square.png", 1, 1, new Vec2(width, height / 2), false);
        AddChild(wall);
        rigidBodies.Add(wall);
        wall.scaleY = 40;

        RigidBody wall1 = new RigidBody("square.png", 1, 1, new Vec2(1665, 460), false);
        AddChild(wall1);
        rigidBodies.Add(wall1);
        wall1.scaleX = 0.7f;
        wall1.scaleY = 7;

        RigidBody wall2 = new RigidBody("square.png", 1, 1, new Vec2(1360, 530), false);
        AddChild(wall2);
        rigidBodies.Add(wall2);
        wall2.scaleX = 0.3f;
        wall2.scaleY = 4.5f;

        RigidBody wall3 = new RigidBody("square.png", 1, 1, new Vec2(-30, height / 2), false);
        AddChild(wall3);
        rigidBodies.Add(wall3);
        wall3.scaleY = 40;

        RigidBody floor = new RigidBody("square.png", 1, 1, new Vec2(width / 2, 0), false);
        AddChild(floor);
        rigidBodies.Add(floor);
        floor.scaleX = 40;

        RigidBody floor1 = new RigidBody("square.png", 1, 1, new Vec2(540, 460), false);
        AddChild(floor1);
        rigidBodies.Add(floor1);
        floor1.scaleX = 17;
        floor1.scaleY = 7;

        RigidBody floor2 = new RigidBody("square.png", 1, 1, new Vec2(1340, 258), false);
        AddChild(floor2);
        rigidBodies.Add(floor2);
        floor2.scaleX = 11;
        floor2.scaleY = 0.7f;

        RigidBody floor3 = new RigidBody("square.png", 1, 1, new Vec2(1340, 660), false);
        AddChild(floor3);
        rigidBodies.Add(floor3);
        floor3.scaleX = 11;
        floor3.scaleY = 0.7f;

        RigidBody floor4 = new RigidBody("square.png", 1, 1, new Vec2(350, 950), false);
        AddChild(floor4);
        rigidBodies.Add(floor4);
        floor4.scaleX = 11;
        floor4.scaleY = 2.5f;

        RigidBody floor5 = new RigidBody("square.png", 1, 1, new Vec2(960, 908), false);
        AddChild(floor5);
        rigidBodies.Add(floor5);
        floor5.scaleX = 8.2f;
        floor5.scaleY = 1.2f;

        RigidBody floor6 = new RigidBody("square.png", 1, 1, new Vec2(1308, 956), false);
        AddChild(floor6);
        rigidBodies.Add(floor6);
        floor6.scaleX = 2.7f;
        floor6.scaleY = 2.7f;

        RigidBody floor7 = new RigidBody("square.png", 1, 1, new Vec2(1545, 913), false);
        AddChild(floor7);
        rigidBodies.Add(floor7);
        floor7.scaleX = 4.6f;
        floor7.scaleY = 1.35f;

        Button button = new Button("checkers.png", 1, 1, new Vec2(250, 880));
        AddChild(button);
        button.SetColor(0.1f,0.6f,0.1f);

        Button button1 = new Button("checkers.png", 1, 1, new Vec2(800, 940));
        AddChild(button1);
        button1.SetColor(1f, 1f, 0);

        Button button2 = new Button("checkers.png", 1, 1, new Vec2(1370, 880));
        AddChild(button2);
        button2.SetColor(0.8f, 0.1f, 0.8f);

        Button button3 = new Button("checkers.png", 1, 1, new Vec2(1600, 640));
        AddChild(button3);
        button3.SetColor(1f, 0, 0.1f);

        Button button4 = new Button("checkers.png", 1, 1, new Vec2(1140, 640));
        AddChild(button4);
        button4.SetColor(0, 0, 1);

        Button button5 = new Button("checkers.png", 1, 1, new Vec2(1380, 240));
        AddChild(button5);
        button5.SetColor(1f, 0.6f, 0f);

        Button button6 = new Button("checkers.png", 1, 1, new Vec2(1200, 240));
        AddChild(button6);
        button6.SetColor(0.4f, 0.8f, 0.6f);

        Door door = new Door("wall.png", 1, 1, new Vec2(450, 800), false, button, new Vec2(450, 600));
        blank.AddChild(door);
        rigidBodies.Add(door);
        door.SetColor(0.1f, 0.6f, 0.1f);

        Door door1 = new Door("wall.png", 1, 1, new Vec2(960, 800), false, button1, new Vec2(960, 1000));
        blank.AddChild(door1);
        rigidBodies.Add(door1);
        door1.SetColor(1f, 1f, 0);

        Door door2 = new Door("wall.png", 1, 1, new Vec2(1150, 1000), false, button1, new Vec2(1150, 800));
        blank.AddChild(door2);
        rigidBodies.Add(door2);
        door2.SetColor(1f, 1f, 0);

        Door door3 = new Door("wall.png", 1, 1, new Vec2(1220, 500), false, button5, new Vec2(1500, 500));
        blank.AddChild(door3);
        rigidBodies.Add(door3);
        door3.SetColor(1f, 0.6f, 0f);
        door3.scaleY = 0.5f;
        door3.scaleX = 4.5f;

        Door door4 = new Door("wall.png", 1, 1, new Vec2(1500, 380), false, button6, new Vec2(1220, 380));
        blank.AddChild(door4);
        rigidBodies.Add(door4);
        door4.SetColor(0.4f, 0.8f, 0.6f);
        door4.scaleY = 0.5f;
        door4.scaleX = 4.5f;

        Door door5 = new Door("wall.png", 1, 1, new Vec2(1000, 370), false, button3, new Vec2(1000, 150));
        blank.AddChild(door5);
        rigidBodies.Add(door5);
        door5.SetColor(1f, 0, 0.1f);

        Door door6 = new Door("wall.png", 1, 1, new Vec2(800, 150), false, button4, new Vec2(800, 370));
        blank.AddChild(door6);
        rigidBodies.Add(door6);
        door6.SetColor(0, 0, 1);


        Fan fan = new Fan("fan.png", 1, 1, new Vec2(1400, 990), false, button2);
        AddChild(fan);
        fan.SetColor(0.8f, 0.1f, 0.8f);

        Fan fan1 = new Fan("fan.png", 1, 1, new Vec2(1670, 320), true, button5);
        AddChild(fan1);
        fan1.SetColor(1f, 0.6f, 0f);

        RigidBody box = new RigidBody("square.png", 1, 1, new Vec2(120, 810), true);
        AddChild(box);
        rigidBodies.Add(box);
        box.isPushable = true;
        box.scale = 0.99f;
        box.SetColor(0.522f,0.42f,0.024f);

        RigidBody box1 = new RigidBody("square.png", 1, 1, new Vec2(800, 1030), true);
        AddChild(box1);
        rigidBodies.Add(box1);
        box1.isPushable = true;
        box1.scale = 0.99f;
        box1.SetColor(0.522f, 0.42f, 0.024f);

        /*
        RigidBody box2 = new RigidBody("square.png", 1, 1, new Vec2(1500, 990), true);
        AddChild(box2);
        rigidBodies.Add(box2);
        box2.isPushable = true;
        box2.scale = 0.99f;
        box2.SetColor(0.522f, 0.42f, 0.024f);
        */

        RigidBody box3 = new RigidBody("square.png", 1, 1, new Vec2(1600, 990), true);
        AddChild(box3);
        rigidBodies.Add(box3);
        box3.isPushable = true;
        box3.scale = 0.99f;
        box3.SetColor(0.522f, 0.42f, 0.024f);

        RigidBody box4 = new RigidBody("square.png", 1, 1, new Vec2(1600, 550), true);
        AddChild(box4);
        rigidBodies.Add(box4);
        box4.isPushable = true;
        box4.scale = 0.99f;
        box4.SetColor(0.522f, 0.42f, 0.024f);

        RigidBody box5 = new RigidBody("square.png", 1, 1, new Vec2(1380, 150), true);
        AddChild(box5);
        rigidBodies.Add(box5);
        box5.isPushable = true;
        box5.scale = 0.99f;
        box5.SetColor(0.522f, 0.42f, 0.024f);

        Player player = new Player("colors.png", 1, 1, new Vec2(35, 810), true);
        AddChild(player);
        rigidBodies.Add(player);

        water = new AnimationSprite("square.png", 1, 1);
        AddChild(water);
        water.alpha = 0.3f;
        water.x = -50;
        water.y = height - 20;
        water.width = width + 100;
        water.height = height;
    }
    void LoadDemo()
    {
        currentLevel = 0;
        RigidBody ball1 = new RigidBody("square.png", 1, 1, new Vec2(300, 350), true);
        AddChild(ball1);
        rigidBodies.Add(ball1);
        ball1.isPushable = true;
        ball1.scale = 0.9f;

        RigidBody floor = new RigidBody("square.png", 1, 1, new Vec2(width / 2, 200), false);
        AddChild(floor);
        rigidBodies.Add(floor);
        floor.scaleX = 40;
        floor.followMouse = false;

        RigidBody floor1 = new RigidBody("square.png", 1, 1, new Vec2(width / 6, height - 200), false);
        AddChild(floor1);
        rigidBodies.Add(floor1);
        floor1.scaleX = 40;
        floor1.followMouse = false;

        Button button = new Button("checkers.png", 1, 1, new Vec2(500, 800));
        AddChild(button);

        Button button1 = new Button("checkers.png", 1, 1, new Vec2(650, 800));
        AddChild(button1);

        Fan fan = new Fan("fan.png", 1, 1, new Vec2(100, 800), false, button);
        AddChild(fan);

        Door door = new Door("wall.png", 1, 1, new Vec2(800, 700), false, button1, new Vec2(800, 700));
        AddChild(door);
        rigidBodies.Add(door);

        Turtle turtle = new Turtle("colors.png", 1, 1, new Vec2(600, 500), true);
        AddChild(turtle);
        rigidBodies.Add(turtle);

        /*
        RigidBody wall = new RigidBody("square.png", 1, 1, new Vec2(1200 , height / 2), false);
        AddChild(wall);
        rigidBodies.Add(wall);
        wall.scaleY = 40;
        wall.followMouse = false;
        */

        Player player = new Player("colors.png", 1, 1, new Vec2(50, 350), true);
        AddChild(player);
        rigidBodies.Add(player);

        Sprite bell = new Sprite("wall.png");
        AddChild(bell);
        bell.SetOrigin(bell.width / 2, bell.height / 2);
        bell.SetXY(1200, 700);
        bell.SetScaleXY(5);
        bell.SetColor(1, 1, 0);
        bell.alpha = 0.5f;
        divingBells.Add(bell);

        water = new AnimationSprite("square.png", 1, 1);
        AddChild(water);
        water.alpha = 0.3f;
        water.x = -50;
        water.y = height - 100;
        water.width = width + 100;
        water.height = height;
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