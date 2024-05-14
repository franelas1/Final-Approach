using GXPEngine;

public class Player : RigidBody
{
    bool won = false;
    bool walking = false;
    bool deathPlayed = false;
    private SoundChannel deathSFX;
    private Sound walkSFX = new Sound("sfx/1.wav");
    private SoundChannel airSFX;
    
    /*bool moving = false;*/
    private float jumpForce = 12f;

    public Player(string filename, int cols, int rows, Vec2 pos, bool moving, int frames = -1, bool keepInCache = false, bool addCollider = true) : base(filename, cols, rows, pos, moving, frames, keepInCache, addCollider)
    {
        bounciness = 0;
        isPlayer = true;
        airSFX = new Sound("sfx/19.wav", true).Play();
        myGame.soundChannels.Add(airSFX);
    }

    public void Update()
    {
        base.Update();

        inBell = false;
        
        

        if (!grounded)
        {
            airSFX.IsPaused = false;

        }
        else
        {

            airSFX.IsPaused = true;

        }

        if (Input.GetKey(Key.A))
        {
            if (!walking)
            {
                walkSFX.Play();
            }
            walking = true;
            acceleration.SetXY(-0.23f, acceleration.y);
        }
        else if (Input.GetKey(Key.D))
        {
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

        if ((Input.GetKeyDown(Key.SPACE) || (Input.GetKeyDown(Key.W))) && grounded && tempY+13 > position.y && tempY-13 < position.y) 
        {
            velocity.SetXY(velocity.x, -jumpForce); grounded = false; 
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
        if (won)
        {
            myGame.currentLevel++;
            myGame.Reload();
        }

        /*Animate(moving, grounded);*/
    }

    public void Death()
    {


        if (deathSFX == null)
        {
            deathSFX = new Sound("sfx/16.wav").Play();
            deathPlayed = true;
        }

        if (!deathSFX.IsPlaying && deathPlayed)
        {
            myGame.Reload();
        }

    }

    void Animate(bool moving_temp, bool grounded_temp)
    {
        if(!moving_temp)
        {
            SetCycle(0, 5);
        }
    }

    

}

