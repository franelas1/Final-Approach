using System;
using System.Xml;
using GXPEngine;

public class Player : RigidBody
{
    bool won = false;
    bool walking = false;
    bool levelStart = false;
    public bool deathPlayed = false;
    public bool reload = false;
    private bool turnedRight = true;
    private bool inAir = false;
    private SoundChannel deathSFX;
    private SoundChannel airSFX;
    private SoundChannel walkingSFX = new Sound("sfx/18.wav", true, true).Play();
    private Sound walkSFX = new Sound("sfx/1.wav");
    
    private AnimationSprite impactPFX = new AnimationSprite("particles/impact.png", 3, 2, 6);
    private AnimationSprite walkPFX = new AnimationSprite("particles/walk.png", 4, 3);
    
    
    private float jumpForce = 12f;

    public Player(string filename, int cols, int rows, Vec2 pos, bool moving, int frames = -1, bool keepInCache = false, bool addCollider = true) : base(filename, cols, rows, pos, moving, frames, keepInCache, addCollider)
    {
        bounciness = 0;
        isPlayer = true;
        airSFX = new Sound("sfx/19.wav", true, true).Play();
        walkingSFX.IsPaused = true;
        SetScaleXY(.75f, .75f);
        myGame.soundChannels.Add(airSFX);
        myGame.AddChild(walkPFX);
        impactPFX.SetOrigin(60,60);
        myGame.soundChannels.Add(walkingSFX);
        walkPFX.SetOrigin(walkPFX.width / 2, walkPFX.height / 2);
    }

    public void Update()
    {
        
        isWalling = false;
            if (myGame.currentLevel != 0)
            myGame.winScreen.SetXY(x, y);
            
            base.Update();
            
            

        inBell = false;
            if (!reload && !won)
            {
                if (myGame.winScreen.scale <= 35)
                {
                    myGame.winScreen.scale += .25f;
                }
                if (myGame.winScreen.scale > 10) levelStart = true;
            }

            
            if (!grounded && myGame.currentLevel != 0)
            {
                airSFX.IsPaused = false;

            }
            else
            {

                airSFX.IsPaused = true;

            }

            if (!deathPlayed && !won && levelStart && myGame.player.scale < 1)
            {
            
            if (Input.GetKey(Key.A) && Input.GetKey(Key.D))
                {
                    walking = false;
                
                acceleration.x = 0;
                }

                else if (Input.GetKey(Key.A))
                {
                    turnedRight = false;
                    if (!walking)
                    {
                        walkSFX.Play();
                    }
                    walking = true;
                
                acceleration.SetXY(-0.23f, acceleration.y);
                }
                else if (Input.GetKey(Key.D))
                {
                    turnedRight = true;
                    if (!walking)
                    {
                        walkSFX.Play();
                    }
                    walking = true;
                
                acceleration.SetXY(0.23f, acceleration.y);
                }
                else
                {
                
                walking = false;
                    acceleration.x = 0;
                }
                if (walking)
                walkingSFX.IsPaused = false;
                else walkingSFX.IsPaused = true;

            if ((Input.GetKeyDown(Key.SPACE) || (Input.GetKeyDown(Key.W))) && grounded && tempY + 13 > position.y && tempY - 13 < position.y)
                {
                    velocity.SetXY(velocity.x, -jumpForce);
                    grounded = false;
                    isPushing = false;
                    walkSFX.Play();
                    parent.AddChild(impactPFX);
                    impactPFX.SetXY(position.x, position.y - 19);
                    impactPFX.SetCycle(0, 5, 5);
                }
            

            impactPFX.Animate();
            if (impactPFX.currentFrame == 4)
                impactPFX.SetCycle(5,1);
                foreach (Sprite other in myGame.divingBells)
                {
                    if (y > other.y - other.height / 2 && y < other.y + other.height / 2 &&
                        x > other.x - other.width / 2 && x < other.x + other.height / 2)
                    {
                        if (other.alpha != 1f)
                        {
                            inBell = true;
                            continue;
                        }
                        else
                        {

                            won = true;
                        }
                    }
                }
                
            }
        if (y > myGame.water.y && !inBell) Death();
        if (reload)
            {
                

                if (myGame.winScreen.scale > 1f)
                    myGame.winScreen.scale -= 0.25f;
                else
                {

                    myGame.Reload();
                }
            }


            if (won)
            {
                
                if (myGame.winScreen.scale > 1f)
                    myGame.winScreen.scale -= 0.25f;
                else
                {
                    myGame.currentLevel++;
                    myGame.Reload();
                }
            }
            DirectionSetter(turnedRight);
            if (myGame.currentLevel != 0)
            Animation(walking, grounded, (isPushing || isWalling), deathPlayed);
    }

    public void LandParticle()
    {
        impactPFX.SetXY(position.x, position.y - 19);
        impactPFX.SetCycle(0, 5, 5);
        inAir = false;
    }

    public void Death()
    {
   
        if (deathSFX == null)
        {
            deathSFX = new Sound("sfx/16.wav", false).Play();
            deathPlayed = true;
            reload = true;
        }

    }

    void Animation(bool walking_temp, bool grounded_temp, bool isPushing_temp, bool deathPlayed_temp)
    {
        
        if (deathPlayed_temp) 
        {
            walkPFX.SetCycle(0, 1, 2);
            //myGame.light.SetCycle(5, 1);

            if (currentFrame == 46)
            {
                SetCycle(46, 1, 5);
            }
            else
            {
                SetCycle(35, 14, 5);
            }
        }

        else if (!grounded_temp)
        {
            if(velocity.y < -1f)
            {
                SetCycle(18, 1);
            }
            else if (velocity.y >= -1f && velocity.y <= 1f)
            {
                SetCycle(19, 1);
            }
            else if (velocity.y > 1f)
            {
                SetCycle(20, 3, 5);
                inAir = true;
                isPushing = false;
            }
            walkPFX.SetCycle(0, 1, 2);
        }
        
        else if(isPushing_temp)
        {
            SetCycle(23, 12, 3);
            walkPFX.SetCycle(0, 12, 5);
        }

        else if(walking_temp)
        {
            SetCycle(6, 12, 3);
            walkPFX.SetCycle(0, 12, 5);

        }

        else if (velocity.y > 4f)
        {
            SetCycle(20, 3, 5);
            walkPFX.SetCycle(0, 1, 2);
        }

        else
        {
            if (inAir)
                LandParticle();
            SetCycle(0, 6, 5);
            walkPFX.SetCycle(0, 1, 2);
        }
        Animate();
        walkPFX.Animate();
        //myGame.light.Animate();
    }

    void DirectionSetter(bool isTurnedRight)
    {
        if(isTurnedRight)
        {
            Mirror(false, false);
            
            impactPFX.Mirror(false, false);
            walkPFX.Mirror(false, false);
            if (walkPFX.currentFrame == 2)
            walkPFX.SetXY(x + 10, y - 12);
        }
        else
        {
            Mirror(true, false);
            impactPFX.Mirror(true, false);
            walkPFX.Mirror(true, false);
            if (walkPFX.currentFrame == 2)
            walkPFX.SetXY(x - 10, y - 12);
        }
    }

    

}

