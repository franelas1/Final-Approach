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

        lowerSFX = new Sound("sfx/24.wav", true).Play();
        lowerSFX.IsPaused = true;
        soundChannels.Add(lowerSFX);


        raiseSFX = new Sound("sfx/23.wav", true).Play();
        raiseSFX.IsPaused = true;
        soundChannels.Add(raiseSFX);

        currentLevel = 1;

        Sprite bg = new Sprite("background.png");
        AddChild(bg);

        Sprite exit = new Sprite("door.png");
        AddChild(exit);
        exit.SetOrigin(exit.width / 2, exit.height / 2);
        exit.scale = 1;
        exit.SetXY(100, 180);
        divingBells.Add(exit);

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

        Button buttonPurple = new Button("purpleButton.png", 1, 1, new Vec2(250, 870));
        AddChild(buttonPurple);
        //button.SetColor(0.1f,0.6f,0.1f);

        Button buttonRed = new Button("redButton.png", 1, 1, new Vec2(800, 950));
        AddChild(buttonRed);
        buttonRed.rotation = 180;
        //buttonRed.SetColor(1f, 1f, 0);

        Button buttonGreen = new Button("greenButton.png", 1, 1, new Vec2(1370, 870));
        AddChild(buttonGreen);
        //buttonGreen.SetColor(0.8f, 0.1f, 0.8f);

        Button buttonBlue = new Button("blueButton.png", 1, 1, new Vec2(1550, 640));
        AddChild(buttonBlue);
        //buttonBlue.SetColor(1f, 0, 0.1f);

        Button buttonOrange = new Button("orangeButton.png", 1, 1, new Vec2(1160, 640));
        AddChild(buttonOrange);
        //buttonOrange.SetColor(0, 0, 1);

        Button buttonPink = new Button("pinkButton.png", 1, 1, new Vec2(1380, 240));
        AddChild(buttonPink);
        //buttonPink.SetColor(1f, 0.6f, 0f);

        Button buttonYellow = new Button("yellowButton.png", 1, 1, new Vec2(1200, 240));
        AddChild(buttonYellow);
        //button6.SetColor(0.4f, 0.8f, 0.6f);

        Door doorPurple = new Door("purpleDoor.png", 1, 1, new Vec2(550, 800), false, buttonPurple, new Vec2(550, 600));
        blank.AddChild(doorPurple);
        rigidBodies.Add(doorPurple);
        //door.SetColor(0.1f, 0.6f, 0.1f);

        Door doorRed1 = new Door("redDoor.png", 1, 1, new Vec2(960, 800), false, buttonRed, new Vec2(960, 1000));
        blank.AddChild(doorRed1);
        rigidBodies.Add(doorRed1);
        //door1.SetColor(1f, 1f, 0);

        Door doorRed2 = new Door("redDoor.png", 1, 1, new Vec2(1150, 1000), false, buttonRed, new Vec2(1150, 800));
        blank.AddChild(doorRed2);
        rigidBodies.Add(doorRed2);
        //door2.SetColor(1f, 1f, 0);

        Door doorPink = new Door("pinkDoorHor.png", 1, 1, new Vec2(1220, 500), false, buttonPink, new Vec2(1500, 500));
        blank.AddChild(doorPink);
        rigidBodies.Add(doorPink);
        //door3.SetColor(1f, 0.6f, 0f);
        

        Door doorYellow = new Door("yellowDoorHor.png", 1, 1, new Vec2(1500, 380), false, buttonYellow, new Vec2(1220, 380));
        blank.AddChild(doorYellow);
        rigidBodies.Add(doorYellow);
        //door4.SetColor(0.4f, 0.8f, 0.6f);
        

        Door doorBlue = new Door("blueDoor.png", 1, 1, new Vec2(1000, 370), false, buttonBlue, new Vec2(1000, 150));
        blank.AddChild(doorBlue);
        rigidBodies.Add(doorBlue);
        //door5.SetColor(1f, 0, 0.1f);

        Door doorOrange = new Door("orangeDoor.png", 1, 1, new Vec2(800, 150), false, buttonOrange, new Vec2(800, 370));
        blank.AddChild(doorOrange);
        rigidBodies.Add(doorOrange);
        //doorOrange.SetColor(0, 0, 1);


        Fan fanGreen = new Fan("greenFan.png", 1, 1, new Vec2(1400, 1010), false, buttonGreen);
        AddChild(fanGreen);
        //fan.SetColor(0.8f, 0.1f, 0.8f);

        Fan fanPink = new Fan("pinkFan.png", 1, 1, new Vec2(1635, 335), true, buttonPink);
        AddChild(fanPink);
        //fan1.SetColor(1f, 0.6f, 0f);

        RigidBody box = new RigidBody("crate.png", 1, 1, new Vec2(250, 790), true);
        AddChild(box);
        rigidBodies.Add(box);
        box.isPushable = true;
        box.scale = 0.49f;
        //box.SetColor(0.522f,0.42f,0.024f);

        RigidBody box1 = new RigidBody("crate.png", 1, 1, new Vec2(800, 1030), true);
        AddChild(box1);
        rigidBodies.Add(box1);
        box1.isPushable = true;
        box1.scale = 0.49f;
        //box1.SetColor(0.522f, 0.42f, 0.024f);

        /*
        RigidBody box2 = new RigidBody("square.png", 1, 1, new Vec2(1500, 990), true);
        AddChild(box2);
        rigidBodies.Add(box2);
        box2.isPushable = true;
        box2.scale = 0.99f;
        box2.SetColor(0.522f, 0.42f, 0.024f);
        */

        RigidBody box3 = new RigidBody("crate.png", 1, 1, new Vec2(1600, 1030), true);
        AddChild(box3);
        rigidBodies.Add(box3);
        box3.isPushable = true;
        box3.scale = 0.49f;
        //box3.SetColor(0.522f, 0.42f, 0.024f);

        RigidBody box4 = new RigidBody("crate.png", 1, 1, new Vec2(1550, 550), true);
        AddChild(box4);
        rigidBodies.Add(box4);
        box4.isPushable = true;
        box4.scale = 0.49f;
        //box4.SetColor(0.522f, 0.42f, 0.024f);

        RigidBody box5 = new RigidBody("crate.png", 1, 1, new Vec2(1380, 150), true);
        AddChild(box5);
        rigidBodies.Add(box5);
        box5.isPushable = true;
        box5.scale = 0.49f;
        //box5.SetColor(0.522f, 0.42f, 0.024f);

        Player player = new Player("candle.png", 7, 7, new Vec2(80, 790), true);
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

        Player player = new Player("colors.png", 1, 1, new Vec2(100, 350), true);
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
    }

    static void Main()                          // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();                   // Create a "MyGame" and start it
    }
}