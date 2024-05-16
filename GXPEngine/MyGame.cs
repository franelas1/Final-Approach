using GXPEngine;                                // GXPEngine contains the engine
using System;
using System.Collections.Generic;

public class MyGame : Game
{

    public List<RigidBody> rigidBodies = new List<RigidBody>();
    public List<Sprite> divingBells = new List<Sprite>();
    public List<SoundChannel> soundChannels = new List<SoundChannel>();
    Sprite bg;
    public AnimationSprite playButton;
    public AnimationSprite settingsButton;
    public AnimationSprite exitButton;
    public bool menu;
    public Sprite winScreen;
    public Player player;
    public Player player1;
    public int currentLevel;
    public float waterSpeed = 2f;
    public AnimationSprite water;
    public bool movingUp = false;
    public bool movingDown = false;

    public SoundChannel ambientSFX;
    public SoundChannel musicSFX;
    public SoundChannel raiseSFX;
    public SoundChannel lowerSFX;

    public MyGame() : base(1920, 1080, false, false)     // Create a window that's 800x600 and NOT fullscreen
    {
        winScreen = new Sprite("winScreen.png");
        winScreen.SetOrigin(winScreen.width / 2 + 30, winScreen.height / 2 + 50);

        //sounds
        ambientSFX = new Sound("sfx/17.wav", true, true).Play();
        ambientSFX.IsPaused = true;

        musicSFX = new Sound("sfx/27.wav", true, true).Play();


        lowerSFX = new Sound("sfx/24.wav", true, true).Play();
        lowerSFX.IsPaused = true;
        

        raiseSFX = new Sound("sfx/23.wav", true, true).Play();
        raiseSFX.IsPaused = true;
        

        LoadDemo();

    }
    // For every game object, Update is called every frame, by the engine:
    void Update()
    {
        BackgroundUpdate();

        if (currentLevel != 0)
            WaterControls();
        else
        {
            player.Animate();
            MenuControls();
        }
        if (Input.GetKeyDown(Key.ZERO)) { currentLevel = 0; Reload(); }
        if (Input.GetKeyDown(Key.ONE)) { currentLevel = 1; Reload(); }
        if (Input.GetKeyDown(Key.TWO)) { currentLevel = 2; Reload(); }
        if (Input.GetKeyDown(Key.THREE)) { currentLevel = 3; Reload(); }
        if (Input.GetKeyDown(Key.R) && currentLevel != 0) { player.reload = true; }
    }

    public void Reload()
    {
        player.deathPlayed = true;
        foreach (SoundChannel channel in soundChannels)
        {

            channel.IsPaused = true;

        }
        soundChannels.Clear();
        rigidBodies.Clear();
        divingBells.Clear();
        foreach (GameObject o in GetChildren())
        {
            RemoveChild(o);
            if (!o.Equals(winScreen))
            {
                o.Destroy();
                o.Remove();
            }
            
        }



        switch (currentLevel)
        {
            case 0:
                LoadDemo(); break;
            case 1:
                LoadLevel1(); break;
            case 2:
                LoadLevel2(); break;
            case 3:
                LoadLevel3(); break;
            default:
                break;
        }

        

        

    }

    void MenuControls()
    {
        if (Input.GetKeyDown(Key.SPACE))
        {
            if (playButton.currentFrame == 1)
            {
                currentLevel = 1;
                
                
            }
            else if (settingsButton.currentFrame == 1)
            {

            }
            else Destroy();
        }

        if (Input.GetKeyDown(Key.UP))
        {
            if (playButton.currentFrame == 1)
            {
                playButton.currentFrame = 0;
                exitButton.currentFrame = 1;
            }
            else if (settingsButton.currentFrame == 1)
            {
                settingsButton.currentFrame = 0;
                playButton.currentFrame = 1;
            }
            else
            {
                exitButton.currentFrame = 0;
                settingsButton.currentFrame = 1;
            }
        }

        if (Input.GetKeyDown(Key.DOWN))
        {
            if (playButton.currentFrame == 1)
            {
                playButton.currentFrame = 0;
                settingsButton.currentFrame = 1;
            }
            else if (settingsButton.currentFrame == 1)
            {
                settingsButton.currentFrame = 0;
                exitButton.currentFrame = 1;
            }
            else
            {
                exitButton.currentFrame = 0;
                playButton.currentFrame = 1;
            }
        }
    }

    void LoadDemo()
    {
        currentLevel = 0;
        

        bg = new Sprite("background.png");
        AddChild(bg);
        bg.SetOrigin(bg.width / 2, bg.height / 2);
        bg.scale = 1.2f;
        bg.SetXY(width / 2, height / 2);
        

        playButton = new AnimationSprite("menuButtonTemp.png", 2, 1);
        AddChild(playButton);
        playButton.SetXY(300, 300);
        playButton.SetFrame(1);

        settingsButton = new AnimationSprite("menuButtonTemp.png", 2, 1);
        AddChild(settingsButton);
        settingsButton.SetXY(300, 500);

        exitButton = new AnimationSprite("menuButtonTemp.png", 2, 1);
        AddChild(exitButton);
        exitButton.SetXY(300, 700);


        



        player = new Player("candle.png", 7, 7, new Vec2(120, 100), true);
        AddChild(player);
        player.SetScaleXY(3, 3);
        player.SetCycle(0, 6, 5);
        player.position.SetXY(1500, 540);


        water = new AnimationSprite("water.png", 2, 2);
        AddChild(water);
        water.SetCycle(0, 4, 15);
        water.alpha = 0.3f;
        water.x = -50;
        water.y = height - 200;
        water.width = width + 100;
        water.height = height;

        AddChild(winScreen);
        winScreen.scale = 35f;
    }
    void LoadLevel1()
    {

        //                                                              ----------NECESSITIES----------

        currentLevel = 1;

        //sounds
        ambientSFX.IsPaused = false;

        //background
        bg = new Sprite("background.png");
        AddChild(bg);
        bg.SetOrigin(bg.width / 2, bg.height / 2);
        bg.scale = 1.2f;
        bg.SetXY(width / 2, height / 2);

        Sprite edges = new Sprite("level1/edges.png");
        AddChild(edges);

        //exit to the next level
        Sprite exit = new Sprite("door.png");
        AddChild(exit);
        exit.SetOrigin(exit.width / 2, exit.height / 2);
        exit.scale = 0.8f;
        exit.SetXY(180, 180);
        divingBells.Add(exit);

        Sprite blank = new Sprite("testtile.png");
        AddChild(blank);
        blank.alpha = 0f;


        //                                                              ----------TERRAIN----------


        RigidBody wall = new RigidBody("level1/1x18.png", 1, 1, new Vec2(30, 540), false);
        AddChild(wall);
        rigidBodies.Add(wall);

        RigidBody wall1 = new RigidBody("level1/1x4.png", 1, 1, new Vec2(1350, 540), false);
        AddChild(wall1);
        rigidBodies.Add(wall1);

        RigidBody wall2 = new RigidBody("level1/2x6.png", 1, 1, new Vec2(1620, 480), false);
        AddChild(wall2);
        rigidBodies.Add(wall2);

        RigidBody wall3 = new RigidBody("level1/2x17.png", 1, 1, new Vec2(1860, 570), false);
        AddChild(wall3);
        rigidBodies.Add(wall3);

        RigidBody floor = new RigidBody("level1/31x1.png", 1, 1, new Vec2(16.5f * 60, 30), false);
        AddChild(floor);
        rigidBodies.Add(floor);

        RigidBody floor1 = new RigidBody("level1/19x5.png", 1, 1, new Vec2(570, 390), false);
        AddChild(floor1);
        rigidBodies.Add(floor1);

        RigidBody floor2 = new RigidBody("level1/8x1(1).png", 1, 1, new Vec2(23.5f * 60, 690), false);
        AddChild(floor2);
        rigidBodies.Add(floor2);

        RigidBody floor3 = new RigidBody("level1/8x1(2).png", 1, 1, new Vec2(23.5f * 60, 270), false);
        AddChild(floor3);
        rigidBodies.Add(floor3);

        RigidBody floor4 = new RigidBody("level1/13x4.png", 1, 1, new Vec2(7.5f * 60, 16 * 60), false);
        AddChild(floor4);
        rigidBodies.Add(floor4);

        RigidBody floor5 = new RigidBody("level1/7x1.png", 1, 1, new Vec2(17.5f * 60, 14.5f * 60), false);
        AddChild(floor5);
        rigidBodies.Add(floor5);

        RigidBody floor6 = new RigidBody("level1/3x4.png", 1, 1, new Vec2(22.5f * 60, 16 * 60), false);
        AddChild(floor6);
        rigidBodies.Add(floor6);

        RigidBody floor7 = new RigidBody("level1/4x1.png", 1, 1, new Vec2(26 * 60, 14.5f * 60), false);
        AddChild(floor7);
        rigidBodies.Add(floor7);

        RigidBody floor8 = new RigidBody("level1/8x3.png", 1, 1, new Vec2(15 * 60, 10.5f * 60), false);
        AddChild(floor8);
        rigidBodies.Add(floor8);


        //                                                              ----------BUTTONS----------


        Button buttonPurple = new Button("colors/purpleButton.png", 1, 1, new Vec2(8 * 60, 14 * 60));
        AddChild(buttonPurple);

        Button buttonRed = new Button("colors/redButton.png", 1, 1, new Vec2(15 * 60, 15 * 60));
        AddChild(buttonRed);
        buttonRed.rotation = 180;

        Button buttonGreen = new Button("colors/greenButton.png", 1, 1, new Vec2(24 * 60, 14 * 60));
        AddChild(buttonGreen);

        Button buttonBlue = new Button("colors/blueButton.png", 1, 1, new Vec2(24 * 60, 11 * 60));
        AddChild(buttonBlue);

        Button buttonOrange = new Button("colors/orangeButton.png", 1, 1, new Vec2(20 * 60, 11 * 60));
        AddChild(buttonOrange);

        Button buttonPink = new Button("colors/pinkButton.png", 1, 1, new Vec2(25 * 60, 240));
        AddChild(buttonPink);

        Button buttonYellow = new Button("colors/yellowButton.png", 1, 1, new Vec2(21 * 60, 240));
        AddChild(buttonYellow);


        //                                                              ----------GATES----------


        Door doorPurple = new Door("colors/purpleDoor.png", 1, 1, new Vec2(12.25f * 60, 13 * 60), false, buttonPurple, new Vec2(12.25f * 60, 10 * 60));
        blank.AddChild(doorPurple);
        rigidBodies.Add(doorPurple);

        Door doorRed1 = new Door("colors/redDoor.png", 1, 1, new Vec2(17f*60, 13*60), false, buttonRed, new Vec2(17f * 60, 16 * 60));
        blank.AddChild(doorRed1);
        rigidBodies.Add(doorRed1);

        Door doorRed2 = new Door("colors/redDoor.png", 1, 1, new Vec2(19f * 60, 16 * 60), false, buttonRed, new Vec2(19f * 60, 13 * 60));
        blank.AddChild(doorRed2);
        rigidBodies.Add(doorRed2);

        Door doorPink = new Door("colors/pinkDoorHor.png", 1, 1, new Vec2(20.5f * 60, 7.25f * 60), false, buttonPink, new Vec2(24.5f * 60, 7.25f * 60));
        blank.AddChild(doorPink);
        rigidBodies.Add(doorPink);

        Door doorYellow = new Door("colors/yellowDoorHor.png", 1, 1, new Vec2(24.5f * 60, 9.25f * 60), false, buttonYellow, new Vec2(20.5f * 60, 9.25f * 60));
        blank.AddChild(doorYellow);
        rigidBodies.Add(doorYellow);

        Door doorBlue = new Door("colors/blueDoor.png", 1, 1, new Vec2(17.75f * 60, 370), false, buttonBlue, new Vec2(17.75f * 60, 150));
        blank.AddChild(doorBlue);
        rigidBodies.Add(doorBlue);

        Door doorOrange = new Door("colors/orangeDoor.png", 1, 1, new Vec2(16*60 - 10, 150), false, buttonOrange, new Vec2(16*60 - 10, 370));
        blank.AddChild(doorOrange);
        rigidBodies.Add(doorOrange);


        //                                                              ----------FANS----------


        Fan fanGreen = new Fan("colors/greenFan.png", 1, 1, new Vec2(24.5f * 60, 16 * 60), false, 4f * 60, buttonGreen);
        AddChild(fanGreen);

        Fan fanPink = new Fan("colors/pinkFan.png", 1, 1, new Vec2(25.8f * 60, 356), true, 5f * 60, buttonPink);
        AddChild(fanPink);


        //                                                              ----------BOXES----------


        RigidBody box1 = new RigidBody("crate.png", 1, 1, new Vec2(15 * 60, 16.5f * 60), true);
        AddChild(box1);
        rigidBodies.Add(box1);
        box1.isPushable = true;
        box1.scale = 0.49f;

        RigidBody box2 = new RigidBody("crate.png", 1, 1, new Vec2(4.5f * 60, 13.5f * 60), true);
        AddChild(box2);
        rigidBodies.Add(box2);
        box2.isPushable = true;
        box2.scale = 0.49f;

        RigidBody box3 = new RigidBody("crate.png", 1, 1, new Vec2(24 * 60, 600), true);
        AddChild(box3);
        rigidBodies.Add(box3);
        box3.isPushable = true;
        box3.scale = 0.49f;

        RigidBody box4 = new RigidBody("crate.png", 1, 1, new Vec2(26 * 60, 16.5f * 60), true);
        AddChild(box4);
        rigidBodies.Add(box4);
        box4.isPushable = true;
        box4.scale = 0.49f;

        RigidBody box5 = new RigidBody("crate.png", 1, 1, new Vec2(25 * 60, 180), true);
        AddChild(box5);
        rigidBodies.Add(box5);
        box5.isPushable = true;
        box5.scale = 0.49f;


        //                                                        ----------WATER AND END SCREEN----------

        player = new Player("candle.png", 7, 7, new Vec2(120, 770), true);
        AddChild(player);
        rigidBodies.Add(player);

        water = new AnimationSprite("water.png", 2, 2);
        AddChild(water);
        water.SetCycle(0, 4, 15);
        water.alpha = 0.3f;
        water.x = -50;
        water.y = height - 70;
        water.width = width + 100;
        water.height = height;

        AddChild(winScreen);
        winScreen.scale = 0.25f;
    }

    void LoadLevel2()
    {
        //                                                              ----------NECESSITIES----------

        currentLevel = 2;

        //sounds
        ambientSFX.IsPaused = false;

        //background
        bg = new Sprite("background.png");
        AddChild(bg);
        bg.SetOrigin(bg.width / 2, bg.height / 2);
        bg.scale = 1.2f;
        bg.SetXY(width / 2, height / 2);

        Sprite edges = new Sprite("level2/edges.png");
        AddChild(edges);
        //edges.SetXY(-1, -1);

        //door to the next level
        Sprite exit = new Sprite("door.png");
        AddChild(exit);
        exit.SetOrigin(exit.width / 2, exit.height / 2);
        exit.scale = 0.8f;
        exit.SetXY(2f * 60, 6.5f * 60);
        divingBells.Add(exit);

        Sprite blank = new Sprite("testtile.png");
        AddChild(blank);
        blank.alpha = 0f;

        


        //                                                              ----------TERRAIN----------

        RigidBody block1 = new RigidBody("level2/1x1(1).png", 1, 1, new Vec2(1.5f * 60, 12.5f * 60), false);
        AddChild(block1);
        rigidBodies.Add(block1);

        RigidBody block2 = new RigidBody("level2/1x1(2).png", 1, 1, new Vec2(18.5f * 60, 11.5f * 60), false);
        AddChild(block2);
        rigidBodies.Add(block2);

        RigidBody block3 = new RigidBody("level2/1x1(3).png", 1, 1, new Vec2(23.5f * 60, 7.5f * 60), false);
        AddChild(block3);
        rigidBodies.Add(block3);

        RigidBody wall1 = new RigidBody("level2/1x12.png", 1, 1, new Vec2(0.5f * 60, 7f * 60), false);
        AddChild(wall1);
        rigidBodies.Add(wall1);

        RigidBody wall2 = new RigidBody("level2/2x3.png", 1, 1, new Vec2(11f * 60, 2.5f * 60), false);
        AddChild(wall2);
        rigidBodies.Add(wall2);

        RigidBody wall3 = new RigidBody("level2/1x2.png", 1, 1, new Vec2(15.5f * 60, 2f * 60), false);
        AddChild(wall3);
        rigidBodies.Add(wall3);

        RigidBody wall4 = new RigidBody("level2/1x3(1).png", 1, 1, new Vec2(19.5f * 60, 7.5f * 60), false);
        AddChild(wall4);
        rigidBodies.Add(wall4);

        RigidBody wall5 = new RigidBody("level2/1x17.png", 1, 1, new Vec2(31.5f * 60, 9.5f * 60), false);
        AddChild(wall5);
        rigidBodies.Add(wall5);

        RigidBody wall6 = new RigidBody("level2/5x8.png", 1, 1, new Vec2(28.5f * 60, 14f * 60), false);
        AddChild(wall6);
        rigidBodies.Add(wall6);

        RigidBody wall7 = new RigidBody("level2/3x5.png", 1, 1, new Vec2(24.5f * 60, 15.5f * 60), false);
        AddChild(wall7);
        rigidBodies.Add(wall7);

        RigidBody wall8 = new RigidBody("level2/1x3(2).png", 1, 1, new Vec2(23.5f * 60, 2.5f * 60), false);
        AddChild(wall8);
        rigidBodies.Add(wall8);

        RigidBody wall9 = new RigidBody("level2/1x3(2).png", 1, 1, new Vec2(-0.5f * 60, 14f * 60), false);
        AddChild(wall9);
        rigidBodies.Add(wall9);

        RigidBody floor1 = new RigidBody("level2/19x3.png", 1, 1, new Vec2(9.5f * 60, 16.5f * 60), false);
        AddChild(floor1);
        floor1.scaleX = 1.05f;
        rigidBodies.Add(floor1);

        RigidBody floor2 = new RigidBody("level2/5x1.png", 1, 1, new Vec2(3.5f * 60, 11.5f * 60), false);
        AddChild(floor2);
        rigidBodies.Add(floor2);

        RigidBody floor3 = new RigidBody("level2/18x4.png", 1, 1, new Vec2(10f * 60, 9f * 60), false);
        AddChild(floor3);
        rigidBodies.Add(floor3);

        RigidBody floor4 = new RigidBody("level2/32x1.png", 1, 1, new Vec2(16f * 60, 0.5f * 60), false);
        AddChild(floor4);
        rigidBodies.Add(floor4);

        RigidBody floor5 = new RigidBody("level2/2x1.png", 1, 1, new Vec2(2f * 60, 4.5f * 60), false);
        AddChild(floor5);
        rigidBodies.Add(floor5);

        RigidBody floor6 = new RigidBody("level2/4x1.png", 1, 1, new Vec2(22f * 60, 8.5f * 60), false);
        AddChild(floor6);
        rigidBodies.Add(floor6);


        //                                                              ----------BUTTONS----------

        Button buttonPink = new Button("colors/pinkButton.png", 1, 1, new Vec2(7f * 60, 11f * 60));
        buttonPink.rotation = 180;
        AddChild(buttonPink);

        Button buttonOrange = new Button("colors/orangeButton.png", 1, 1, new Vec2(17f * 60, 11f * 60));
        buttonOrange.rotation = 180;
        AddChild(buttonOrange);

        Button buttonGreen = new Button("colors/greenButton.png", 1, 1, new Vec2(25f * 60, 13f * 60));
        AddChild(buttonGreen);

        Button buttonYellow = new Button("colors/yellowButton.png", 1, 1, new Vec2(30f * 60, 10f * 60));
        AddChild(buttonYellow);

        Button buttonBlue = new Button("colors/blueButton.png", 1, 1, new Vec2(13f * 60, 7f * 60));
        AddChild(buttonBlue);

        Button buttonRed = new Button("colors/redButton.png", 1, 1, new Vec2(11f * 60, 4f * 60));
        buttonRed.rotation = 180;
        AddChild(buttonRed);

        Button buttonPurple = new Button("colors/purpleButton.png", 1, 1, new Vec2(12f * 60, 2f * 60));
        buttonPurple.rotation = 90;
        AddChild(buttonPurple);

        Button buttonBlack = new Button("colors/purpleButton.png", 1, 1, new Vec2(10f * 60, 2f * 60));
        buttonBlack.rotation = 270;
        buttonBlack.SetColor(0.1f, 0.1f, 0.1f);
        AddChild(buttonBlack);


        //                                                              ----------GATES----------

        Door doorPink1 = new Door("colors/pinkDoor.png", 1, 1, new Vec2(8.75f * 60, 13.5f * 60), false, buttonPink, new Vec2(8.75f * 60, 18f * 60));
        blank.AddChild(doorPink1);
        rigidBodies.Add(doorPink1);

        Door doorPink2 = new Door("colors/pinkDoorHor.png", 1, 1, new Vec2(3.5f * 60, 12.25f * 60), false, buttonPink, new Vec2(6.5f * 60, 12.25f * 60));
        blank.AddChild(doorPink2);
        rigidBodies.Add(doorPink2);

        Door doorGreen = new Door("colors/greenDoorHor.png", 1, 1, new Vec2(24.5f * 60, 14.5f * 60), false, buttonGreen, new Vec2(22f * 60, 14.5f * 60));
        blank.AddChild(doorGreen);
        rigidBodies.Add(doorGreen);

        Door doorYellow1 = new Door("colors/yellowDoor.png", 1, 1, new Vec2(23.75f * 60, 5.5f * 60), false, buttonYellow, new Vec2(23.75f * 60, 2.5f * 60));
        blank.AddChild(doorYellow1);
        rigidBodies.Add(doorYellow1);

        Door doorYellow2 = new Door("colors/yellowDoor.png", 1, 1, new Vec2(26.25f * 60, 12.5f * 60), false, buttonYellow, new Vec2(26.25f * 60, 9.5f * 60));
        blank.AddChild(doorYellow2);
        rigidBodies.Add(doorYellow2);

        Door doorYellow3 = new Door("colors/yellowDoor.png", 1, 1, new Vec2(27.25f * 60, 13f * 60), false, buttonYellow, new Vec2(27.25f * 60, 10f * 60));
        blank.AddChild(doorYellow3);
        rigidBodies.Add(doorYellow3);

        Door doorYellow4 = new Door("colors/yellowDoor.png", 1, 1, new Vec2(28.25f * 60, 13.5f * 60), false, buttonYellow, new Vec2(28.25f * 60, 10.5f * 60));
        blank.AddChild(doorYellow4);
        rigidBodies.Add(doorYellow4);

        Door doorRed1 = new Door("colors/redDoor.png", 1, 1, new Vec2(19.25f * 60, 4.5f * 60), false, buttonRed, new Vec2(19.25f * 60, 7.5f * 60));
        blank.AddChild(doorRed1);
        rigidBodies.Add(doorRed1);

        Door doorRed2 = new Door("colors/redDoor.png", 1, 1, new Vec2(19.75f * 60, 4.5f * 60), false, buttonRed, new Vec2(19.75f * 60, 7.5f * 60));
        blank.AddChild(doorRed2);
        rigidBodies.Add(doorRed2);

        Door doorPurple1 = new Door("colors/purpleDoorHor.png", 1, 1, new Vec2(11.2f * 60, 3.25f * 60), false, buttonPurple, new Vec2(13f * 60, 3.25f * 60));
        doorPurple1.scaleX = 0.4f;
        blank.AddChild(doorPurple1);
        rigidBodies.Add(doorPurple1);

        Door doorPurple2 = new Door("colors/purpleDoor.png", 1, 1, new Vec2(3.75f * 60, 5.5f * 60), false, buttonPurple, new Vec2(3.75f * 60, 9f * 60));
        blank.AddChild(doorPurple2);
        rigidBodies.Add(doorPurple2);

        Door notDoorPurple1 = new Door("colors/purpleDoorHor.png", 1, 1, new Vec2(10.8f * 60, 3.25f * 60), false, buttonBlack, new Vec2(9f * 60, 3.25f * 60));
        notDoorPurple1.scaleX = 0.4f;
        notDoorPurple1.SetColor(0.1f, 0.1f, 0.1f);
        blank.AddChild(notDoorPurple1);
        rigidBodies.Add(notDoorPurple1);

        Door notDoorPurple2 = new Door("colors/purpleDoor.png", 1, 1, new Vec2(3.25f * 60, 5.5f * 60), false, buttonBlack, new Vec2(3.25f * 60, 9f * 60));
        notDoorPurple2.SetColor(0.1f, 0.1f, 0.1f);
        blank.AddChild(notDoorPurple2);
        rigidBodies.Add(notDoorPurple2);


        //                                                              ----------FANS----------

        Fan fanOrange1 = new Fan("colors/orangeFan.png", 1, 1, new Vec2(19f * 60, 11f * 60), false, 10f * 60, buttonOrange);
        AddChild(fanOrange1);

        Fan fanOrange2 = new Fan("colors/orangeFan.png", 1, 1, new Vec2(24f * 60, 8f * 60), false, 10f * 60, buttonOrange);
        AddChild(fanOrange2);

        Fan fanBlue1 = new Fan("colors/blueFan.png", 1, 1, new Vec2(1f * 60, 2f * 60), false, 9f * 60, buttonBlue);
        AddChild(fanBlue1);

        Fan fanBlue2 = new Fan("colors/blueFan.png", 1, 1, new Vec2(15f * 60, 2f * 60), true, 3f * 60, buttonBlue);
        AddChild(fanBlue2);

        Fan fanBlue3 = new Fan("colors/blueFan.png", 1, 1, new Vec2(23f * 60, 3f * 60), true, 11f * 60, buttonBlue);
        AddChild(fanBlue3);


        //                                                              ----------BOXES----------

        RigidBody box1 = new RigidBody("crate.png", 1, 1, new Vec2(2.5f * 60, 14.5f * 60), true);
        AddChild(box1);
        rigidBodies.Add(box1);
        box1.isPushable = true;
        box1.scale = 0.49f;

        RigidBody box2 = new RigidBody("crate.png", 1, 1, new Vec2(11.5f * 60, 14.5f * 60), true);
        AddChild(box2);
        rigidBodies.Add(box2);
        box2.isPushable = true;
        box2.scale = 0.49f;

        RigidBody box3 = new RigidBody("crate.png", 1, 1, new Vec2(16.5f * 60, 14.5f * 60), true);
        AddChild(box3);
        rigidBodies.Add(box3);
        box3.isPushable = true;
        box3.scale = 0.49f;

        RigidBody box4 = new RigidBody("crate.png", 1, 1, new Vec2(20.5f * 60, 7.5f * 60), true);
        AddChild(box4);
        rigidBodies.Add(box4);
        box4.isPushable = true;
        box4.scale = 0.49f;

        RigidBody box5 = new RigidBody("crate.png", 1, 1, new Vec2(6.5f * 60, 6.5f * 60), true);
        AddChild(box5);
        rigidBodies.Add(box5);
        box5.isPushable = true;
        box5.scale = 0.49f;


        


        //                                                        ----------PLAYER AND WATER----------

        player = new Player("candle.png", 7, 7, new Vec2(60, 14.1f * 60), true);
        AddChild(player);
        rigidBodies.Add(player);


        //                                                            ----------BUBBLES----------

        Sprite bubble1 = new Sprite("bubble.png");
        AddChild(bubble1);
        bubble1.SetOrigin(bubble1.width / 2, bubble1.height / 2);
        bubble1.SetXY(5f * 60, 14f * 60);
        bubble1.alpha = 0.99f;
        divingBells.Add(bubble1);

        Sprite bubble2 = new Sprite("bubble.png");
        AddChild(bubble2);
        bubble2.SetOrigin(bubble2.width / 2, bubble2.height / 2);
        bubble2.SetXY(15f * 60, 14f * 60);
        bubble2.alpha = 0.99f;
        divingBells.Add(bubble2);

        Sprite bubble3 = new Sprite("bubble.png");
        AddChild(bubble3);
        bubble3.SetOrigin(bubble3.width / 2, bubble3.height / 2);
        bubble3.SetXY(13f * 60, 6f * 60);
        bubble3.alpha = 0.99f;
        divingBells.Add(bubble3);


        //                                                        ----------WATER AND WIN SCREEN----------

        water = new AnimationSprite("water.png", 2, 2);
        AddChild(water);
        water.SetCycle(0, 4, 15);
        water.alpha = 0.3f;
        water.x = -50;
        water.y = height - 70;
        water.width = width + 100;
        water.height = height;

        AddChild(winScreen);
        winScreen.scale = 0.25f;
    }

    void LoadLevel3()
    {
        //                                                              ----------NECESSITIES----------

        currentLevel = 3;

        //sounds
        ambientSFX.IsPaused = false;

        //background
        bg = new Sprite("background.png");
        AddChild(bg);
        bg.SetOrigin(bg.width / 2, bg.height / 2);
        bg.scale = 1.2f;
        bg.SetXY(width / 2, height / 2);

        Sprite edges = new Sprite("level3/edges.png");
        AddChild(edges);

        //door to the next level
        Sprite exit = new Sprite("door.png");
        AddChild(exit);
        exit.SetOrigin(exit.width / 2, exit.height / 2);
        exit.scale = 0.8f;
        exit.SetXY(2.5f * 60, 13.5f * 60);
        divingBells.Add(exit);

        Sprite blank = new Sprite("testtile.png");
        AddChild(blank);
        blank.alpha = 0f;


        //                                                              ----------TERRAIN----------

        RigidBody block1 = new RigidBody("level3/4x4.png", 1, 1, new Vec2(3f * 60, 16f * 60), false);
        AddChild(block1);
        rigidBodies.Add(block1);

        RigidBody floor1 = new RigidBody("level3/4x3.png", 1, 1, new Vec2(3f * 60, 5.5f * 60), false);
        AddChild(floor1);
        rigidBodies.Add(floor1);

        RigidBody floor2 = new RigidBody("level3/13x1.png", 1, 1, new Vec2(11.5f * 60, 6.5f * 60), false);
        AddChild(floor2);
        rigidBodies.Add(floor2);

        RigidBody floor3 = new RigidBody("level3/12x3.png", 1, 1, new Vec2(16f * 60, 4.5f * 60), false);
        AddChild(floor3);
        rigidBodies.Add(floor3);

        RigidBody floor4 = new RigidBody("level3/30x1.png", 1, 1, new Vec2(16f * 60, 0.5f * 60), false);
        AddChild(floor4);
        rigidBodies.Add(floor4);

        RigidBody floor5 = new RigidBody("level3/26x1.png", 1, 1, new Vec2(18f * 60, 17.5f * 60), false);
        AddChild(floor5);
        rigidBodies.Add(floor5);

        RigidBody floor6 = new RigidBody("level3/3x1.png", 1, 1, new Vec2(26.5f * 60, 4.5f * 60), false);
        AddChild(floor6);
        rigidBodies.Add(floor6);

        RigidBody floor7 = new RigidBody("level3/10x1.png", 1, 1, new Vec2(23f * 60, 12.5f * 60), false);
        AddChild(floor7);
        rigidBodies.Add(floor7);

        RigidBody floor8 = new RigidBody("level3/4x1.png", 1, 1, new Vec2(23f * 60, 8.5f * 60), false);
        AddChild(floor8);
        rigidBodies.Add(floor8);

        RigidBody floor9 = new RigidBody("level3/16x2.png", 1, 1, new Vec2(21.5f * 60, 16f * 60), false);
        AddChild(floor9);
        rigidBodies.Add(floor9);

        RigidBody floor10 = new RigidBody("level3/6x2.png", 1, 1, new Vec2(11f * 60, 11f * 60), false);
        AddChild(floor10);
        rigidBodies.Add(floor10);

        RigidBody wall1 = new RigidBody("level3/1x18(1).png", 1, 1, new Vec2(0.5f * 60, 9f * 60), false);
        AddChild(wall1);
        rigidBodies.Add(wall1);

        RigidBody wall2 = new RigidBody("level3/1x4.png", 1, 1, new Vec2(4.5f * 60, 12f * 60), false);
        AddChild(wall2);
        rigidBodies.Add(wall2);

        RigidBody wall3 = new RigidBody("level3/1x6.png", 1, 1, new Vec2(17.5f * 60, 10f * 60), false);
        AddChild(wall3);
        rigidBodies.Add(wall3);

        RigidBody wall4 = new RigidBody("level3/1x18(2).png", 1, 1, new Vec2(31.5f * 60, 9f * 60), false);
        AddChild(wall4);
        rigidBodies.Add(wall4);

        RigidBody wall5 = new RigidBody("level3/1x10.png", 1, 1, new Vec2(28.5f * 60, 8f * 60), false);
        AddChild(wall5);
        rigidBodies.Add(wall5);

        RigidBody wall6 = new RigidBody("level3/1x5.png", 1, 1, new Vec2(13.5f * 60, 14.5f * 60), false);
        AddChild(wall6);
        rigidBodies.Add(wall6);


        //                                                              ----------BUTTONS----------

        Button buttonPink = new Button("colors/pinkButton.png", 1, 1, new Vec2(7.5f * 60, 6f * 60));
        AddChild(buttonPink);

        Console.WriteLine(buttonPink.isActivated);


        //                                                              ----------GATES----------

        Door doorPink = new Door("colors/pinkDoor.png", 1, 1, new Vec2(12f * 60, 5f * 60), false, buttonPink, new Vec2(12f * 60, 2f * 60));
        blank.AddChild(doorPink);
        rigidBodies.Add(doorPink);

        //                                                              ----------FANS----------

        //                                                              ----------BOXES----------

        //                                                             ----------TURTLES----------

        Turtle turtle = new Turtle("tutel.png", 1, 1, new Vec2(7.5f * 60, 5f * 60), true);
        AddChild(turtle);
        rigidBodies.Add(turtle);

        //                                                        ----------WATER AND WIN SCREEN----------

        player = new Player("candle.png", 7, 7, new Vec2(2f * 60, 3f * 60), true);
        AddChild(player);
        rigidBodies.Add(player);

        water = new AnimationSprite("water.png", 2, 2);
        AddChild(water);
        water.SetCycle(0, 4, 15);
        water.alpha = 0.3f;
        water.x = -50;
        water.y = height - 170;
        water.width = width + 100;
        water.height = height;

        AddChild(winScreen);
        winScreen.scale = 0.25f;
    }

    void BackgroundUpdate()
    {
        bg.SetXY(width / 2 - (player.position.x / 10 - 192), height / 2);
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