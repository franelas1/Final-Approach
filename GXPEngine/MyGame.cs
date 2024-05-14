using GXPEngine;                                // GXPEngine contains the engine
using System.Collections.Generic;

public class MyGame : Game
{

    public List<RigidBody> rigidBodies = new List<RigidBody>();
    public List<Sprite> divingBells = new List<Sprite>();
    public List<SoundChannel> soundChannels = new List<SoundChannel>();

    public int currentLevel;
    public float waterSpeed = 2f;
    public AnimationSprite water;
    public bool movingUp = false;
    public bool movingDown = false;

    public SoundChannel ambientSFX;
    public SoundChannel raiseSFX;
    public SoundChannel lowerSFX;

    public MyGame() : base(1920, 1080, false, false)     // Create a window that's 800x600 and NOT fullscreen
    {
        LoadLevel1();

    }
    // For every game object, Update is called every frame, by the engine:
    void Update()
    {
        WaterControls();
        if (Input.GetKeyDown(Key.ZERO)) { currentLevel = 0; Reload(); }
        if (Input.GetKeyDown(Key.ONE)) { currentLevel = 1; Reload(); }
    }

    public void Reload()
    {

        foreach (SoundChannel channel in soundChannels)
        {

            channel.Stop();

        }
        soundChannels.Clear();
        rigidBodies.Clear();
        divingBells.Clear();
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
        ambientSFX = new Sound("sfx/17.wav", true, true).Play();
        soundChannels.Add(ambientSFX);

        lowerSFX = new Sound("sfx/24.wav", true, true).Play();
        lowerSFX.IsPaused = true;
        soundChannels.Add(lowerSFX);


        raiseSFX = new Sound("sfx/23.wav", true, true).Play();
        raiseSFX.IsPaused = true;
        soundChannels.Add(raiseSFX);

        currentLevel = 1;

        Sprite bg = new Sprite("background.png");
        AddChild(bg);

        Sprite edges = new Sprite("edges.png");
        AddChild(edges);

        Sprite exit = new Sprite("door.png");
        AddChild(exit);
        exit.SetOrigin(exit.width / 2, exit.height / 2);
        exit.scale = 0.8f;
        exit.SetXY(180, 180);
        divingBells.Add(exit);

        Sprite blank = new Sprite("square.png");
        AddChild(blank);
        blank.alpha = 0f;


        RigidBody wall = new RigidBody("piece1.png", 1, 1, new Vec2(30, 540), false);
        AddChild(wall);
        rigidBodies.Add(wall);
        

        RigidBody wall1 = new RigidBody("piece13.png", 1, 1, new Vec2(1350, 540), false);
        AddChild(wall1);
        rigidBodies.Add(wall1);
        

        RigidBody wall2 = new RigidBody("piece11.png", 1, 1, new Vec2(1620,480), false);
        AddChild(wall2);
        rigidBodies.Add(wall2);
        

        RigidBody wall3 = new RigidBody("piece12.png", 1, 1, new Vec2(1860, 570), false);
        AddChild(wall3);
        rigidBodies.Add(wall3);
        

        RigidBody floor = new RigidBody("piece6.png", 1, 1, new Vec2(16.5f*60, 30), false);
        AddChild(floor);
        rigidBodies.Add(floor);


        RigidBody floor1 = new RigidBody("piece3.png", 1, 1, new Vec2(570, 390), false);
        AddChild(floor1);
        rigidBodies.Add(floor1);


        RigidBody floor2 = new RigidBody("piece9.png", 1, 1, new Vec2(23.5f*60, 690), false);
        AddChild(floor2);
        rigidBodies.Add(floor2);
        

        RigidBody floor3 = new RigidBody("piece10.png", 1, 1, new Vec2(23.5f*60, 270), false);
        AddChild(floor3);
        rigidBodies.Add(floor3);
        

        RigidBody floor4 = new RigidBody("piece2.png", 1, 1, new Vec2(7.5f*60, 16*60), false);
        AddChild(floor4);
        rigidBodies.Add(floor4);
        

        RigidBody floor5 = new RigidBody("piece4.png", 1, 1, new Vec2(17.5f* 60, 14.5f* 60), false);
        AddChild(floor5);
        rigidBodies.Add(floor5);

        RigidBody floor6 = new RigidBody("piece7.png", 1, 1, new Vec2(22.5f * 60, 16*60), false);
        AddChild(floor6);
        rigidBodies.Add(floor6);

        RigidBody floor7 = new RigidBody("piece8.png", 1, 1, new Vec2(26*60, 14.5f*60), false);
        AddChild(floor7);
        rigidBodies.Add(floor7);

        RigidBody floor8 = new RigidBody("piece5.png", 1, 1, new Vec2(15 * 60, 10.5f * 60), false);
        AddChild(floor8);
        rigidBodies.Add(floor8);


        Button buttonPurple = new Button("purpleButton.png", 1, 1, new Vec2(8*60, 14*60));
        AddChild(buttonPurple);
        

        Button buttonRed = new Button("redButton.png", 1, 1, new Vec2(15*60, 15*60));
        AddChild(buttonRed);
        buttonRed.rotation = 180;
        

        Button buttonGreen = new Button("greenButton.png", 1, 1, new Vec2(24*60, 14*60));
        AddChild(buttonGreen);
        

        Button buttonBlue = new Button("blueButton.png", 1, 1, new Vec2(24*60, 11*60));
        AddChild(buttonBlue);
        

        Button buttonOrange = new Button("orangeButton.png", 1, 1, new Vec2(20*60, 11*60));
        AddChild(buttonOrange);
        

        Button buttonPink = new Button("pinkButton.png", 1, 1, new Vec2(25*60, 240));
        AddChild(buttonPink);
        

        Button buttonYellow = new Button("yellowButton.png", 1, 1, new Vec2(21*60, 240));
        AddChild(buttonYellow);
       

        Door doorPurple = new Door("purpleDoor.png", 1, 1, new Vec2(12.25f*60, 13*60), false, buttonPurple, new Vec2(12.25f * 60, 10 * 60));
        blank.AddChild(doorPurple);
        rigidBodies.Add(doorPurple);
        

        Door doorRed1 = new Door("redDoor.png", 1, 1, new Vec2(17.25f*60, 13*60), false, buttonRed, new Vec2(17.25f * 60, 16 * 60));
        blank.AddChild(doorRed1);
        rigidBodies.Add(doorRed1);
        

        Door doorRed2 = new Door("redDoor.png", 1, 1, new Vec2(19f * 60, 16 * 60), false, buttonRed, new Vec2(19f * 60, 13 * 60));
        blank.AddChild(doorRed2);
        rigidBodies.Add(doorRed2);
        

        Door doorPink = new Door("pinkDoorHor.png", 1, 1, new Vec2(20.5f * 60, 7.25f * 60), false, buttonPink, new Vec2(24.5f * 60, 7.25f * 60));
        blank.AddChild(doorPink);
        rigidBodies.Add(doorPink);
        
        

        Door doorYellow = new Door("yellowDoorHor.png", 1, 1, new Vec2(24.5f * 60, 9.25f * 60), false, buttonYellow, new Vec2(20.5f * 60, 9.25f * 60));
        blank.AddChild(doorYellow);
        rigidBodies.Add(doorYellow);
        
        

        Door doorBlue = new Door("blueDoor.png", 1, 1, new Vec2(17.75f*60, 370), false, buttonBlue, new Vec2(17.75f*60, 150));
        blank.AddChild(doorBlue);
        rigidBodies.Add(doorBlue);
        

        Door doorOrange = new Door("orangeDoor.png", 1, 1, new Vec2(16*60, 150), false, buttonOrange, new Vec2(16*60, 370));
        blank.AddChild(doorOrange);
        rigidBodies.Add(doorOrange);
        


        Fan fanGreen = new Fan("greenFan.png", 1, 1, new Vec2(24.5f*60, 16*60), false, buttonGreen);
        AddChild(fanGreen);
        

        Fan fanPink = new Fan("pinkFan.png", 1, 1, new Vec2(25.8f*60, 356), true, buttonPink);
        AddChild(fanPink);
        

        RigidBody box = new RigidBody("crate.png", 1, 1, new Vec2(4.5f*60, 13.5f*60), true);
        AddChild(box);
        rigidBodies.Add(box);
        box.isPushable = true;
        box.scale = 0.49f;
        

        RigidBody box1 = new RigidBody("crate.png", 1, 1, new Vec2(15*60, 16.5f*60), true);
        AddChild(box1);
        rigidBodies.Add(box1);
        box1.isPushable = true;
        box1.scale = 0.49f;
        

        /*
        RigidBody box2 = new RigidBody("square.png", 1, 1, new Vec2(1500, 990), true);
        AddChild(box2);
        rigidBodies.Add(box2);
        box2.isPushable = true;
        box2.scale = 0.99f;
        box2.SetColor(0.522f, 0.42f, 0.024f);
        */

        RigidBody box3 = new RigidBody("crate.png", 1, 1, new Vec2(24*60, 600), true);
        AddChild(box3);
        rigidBodies.Add(box3);
        box3.isPushable = true;
        box3.scale = 0.49f;
        //box3.SetColor(0.522f, 0.42f, 0.024f);

        RigidBody box4 = new RigidBody("crate.png", 1, 1, new Vec2(26*60, 16.5f*60), true);
        AddChild(box4);
        rigidBodies.Add(box4);
        box4.isPushable = true;
        box4.scale = 0.49f;
        //box4.SetColor(0.522f, 0.42f, 0.024f);

        RigidBody box5 = new RigidBody("crate.png", 1, 1, new Vec2(25*60, 180), true);
        AddChild(box5);
        rigidBodies.Add(box5);
        box5.isPushable = true;
        box5.scale = 0.49f;
        //box5.SetColor(0.522f, 0.42f, 0.024f);

        Player player = new Player("candle.png", 7, 7, new Vec2(120, 770), true);
        AddChild(player);
        rigidBodies.Add(player);

        water = new AnimationSprite("water.png", 2, 2);
        AddChild(water);
        water.SetCycle(0, 4, 15);
        water.alpha = 0.3f;
        water.x = -50;
        water.y = height - 20;
        water.width = width + 100;
        water.height = height;
    }
    void LoadDemo()
    {
        currentLevel = 0;

        Sprite bg = new Sprite("background.png");
        AddChild(bg);

        Sprite exit = new Sprite("door.png");
        AddChild(exit);
        exit.SetOrigin(exit.width / 2, exit.height / 2);
        //exit.scale = 4;
        exit.SetXY(1200, 750);
        divingBells.Add(exit);

        /*RigidBody ball1 = new RigidBody("square.png", 1, 1, new Vec2(300, 350), true);
        AddChild(ball1);
        rigidBodies.Add(ball1);
        ball1.isPushable = true;
        ball1.scale = 0.9f;*/

        RigidBody floor = new RigidBody("testtile.png", 1, 1, new Vec2(width / 2, 200), false);
        AddChild(floor);
        rigidBodies.Add(floor);
        floor.scaleX = 3;
        floor.scaleY = 0.3f;
        floor.followMouse = false;

        RigidBody floor1 = new RigidBody("testtile.png", 1, 1, new Vec2(width / 6, height - 70), false);
        AddChild(floor1);
        rigidBodies.Add(floor1);
        floor1.scaleX = 4;
        floor1.followMouse = false;

        Button buttonRed = new Button("redButton.png", 1, 1, new Vec2(500, 840));
        AddChild(buttonRed);

        /*Button button1 = new Button("checkers.png", 1, 1, new Vec2(650, 800));
        AddChild(button1);*/

        Fan fanRed = new Fan("redFan.png", 1, 1, new Vec2(50, 800), false, buttonRed);
        AddChild(fanRed);

        /* Door door = new Door("wall.png", 1, 1, new Vec2(800, 700), false, button1, new Vec2(800, 700));
         AddChild(door);
         rigidBodies.Add(door);*/

        Turtle turtle = new Turtle("tutel.png", 1, 1, new Vec2(600, 700), true);
        AddChild(turtle);
        rigidBodies.Add(turtle);

        /*
        RigidBody wall = new RigidBody("square.png", 1, 1, new Vec2(1200 , height / 2), false);
        AddChild(wall);
        rigidBodies.Add(wall);
        wall.scaleY = 40;
        wall.followMouse = false;
        */

        /*Player player = new Player("colors.png", 1, 1, new Vec2(100, 350), true);
        AddChild(player);
        rigidBodies.Add(player);*/

        Player player = new Player("candle.png", 7, 7, new Vec2(120, 700), true);
        AddChild(player);
        rigidBodies.Add(player);

        /*Sprite bell = new Sprite("wall.png");
        AddChild(bell);
        bell.SetOrigin(bell.width / 2, bell.height / 2);
        bell.SetXY(1200, 700);
        bell.SetScaleXY(5);
        bell.SetColor(1, 1, 0);
        bell.alpha = 0.5f;
        divingBells.Add(bell);*/

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
            if (!movingDown)
            {
                
                lowerSFX.IsPaused = false;
            }
            movingDown = true;
            water.y += waterSpeed;
            if (water.y > height)
                water.y = height;
        }
        else if (Input.GetKey(Key.UP))
        {
            if (!movingUp)
            {
                
                raiseSFX.IsPaused = false;
            }
            movingUp = true;
            water.y -= waterSpeed;
        }
        else
        {
            movingUp = false; movingDown = false;
            lowerSFX.IsPaused = true; raiseSFX.IsPaused = true;
        }

        water.Animate();
    }

    static void Main()                          // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();                   // Create a "MyGame" and start it
    }
}