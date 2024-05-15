using System;
using System.Xml;
using GXPEngine;

public class Player : RigidBody
{
    bool won = false;
    bool walking = false;
    bool deathPlayed = false;
    bool levelStart = false;
    private bool turnedRight = true;
    private SoundChannel deathSFX;
    private Sound walkSFX = new Sound("sfx/1.wav");
    private SoundChannel airSFX;
    
    private float jumpForce = 12f;

    public Player(string filename, int cols, int rows, Vec2 pos, bool moving, int frames = -1, bool keepInCache = false, bool addCollider = true) : base(filename, cols, rows, pos, moving, frames, keepInCache, addCollider)
    {
        bounciness = 0;
        isPlayer = true;
        airSFX = new Sound("sfx/19.wav", true, true).Play();
        SetScaleXY(.75f, .75f);
        myGame.soundChannels.Add(airSFX);
    }

    public void Update()
    {
        isWalling = false;
        myGame.winScreen.SetXY(x, y);
        
        base.Update();

        inBell = false;
        if (!deathPlayed)
        {
            if (myGame.winScreen.scale <= 35)
            {
                myGame.winScreen.scale += .25f;
            }
            if (myGame.winScreen.scale > 10) levelStart = true;
        }
        
        
        if (!grounded)
        {
            airSFX.IsPaused = false;

        }
        else
        {

            airSFX.IsPaused = true;

        }

        if (!deathPlayed && !won && levelStart)
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

            if ((Input.GetKeyDown(Key.SPACE) || (Input.GetKeyDown(Key.W))) && grounded && tempY + 13 > position.y && tempY - 13 < position.y)
            {
                velocity.SetXY(velocity.x, -jumpForce);
                grounded = false;
                isPushing = false;
                walkSFX.Play();
            }

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
            if (y > myGame.water.y && !inBell) Death();
        }

        if(deathPlayed)
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
            myGame.winScreen.scale -= 0.25f;
            if (myGame.winScreen.scaleX == 0.5f)
            {
                myGame.currentLevel++;
                myGame.Reload();
            }
        }
        DirectionSetter(turnedRight);
        Animation(walking, grounded, (isPushing || isWalling), deathPlayed);
    }

    public void Death()
    {
   
        if (deathSFX == null)
        {
            deathSFX = new Sound("sfx/16.wav", false).Play();
            deathPlayed = true;
        }

    }

    void Animation(bool walking_temp, bool grounded_temp, bool isPushing_temp, bool deathPlayed_temp)
    {
        

        if (deathPlayed) 
        {
            
            if (currentFrame == 46)
            {
                SetCycle(46, 1, 5);
            }
            else
            SetCycle(35, 14, 5);
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
                isPushing = false;
            }
        }

        
        else if(isPushing_temp)
        {
            SetCycle(23, 12, 3);
        }
        else if(walking_temp)
        {
            SetCycle(6, 12, 3);
        }
        else if (velocity.y > 4f)
        {
            SetCycle(20, 3, 5);
            
        }
        else
        {
            SetCycle(0, 6, 5);
        }
        Animate();
    }

    void DirectionSetter(bool isTurnedRight)
    {
        if(isTurnedRight)
        {
            Mirror(false, false);
        }
        else
        {
            Mirror(true, false);
        }
    }

    

}

