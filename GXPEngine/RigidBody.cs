using GXPEngine;
using System;

public class RigidBody : AnimationSprite
{
    public MyGame myGame;

    public Vec2 position;
    public Vec2 velocity;
    private float maxVertSpeed = 12f;
    private float gravity = 0.5f;
    public float maxSpeed = 7;
    public Vec2 acceleration;
    public float bounciness = 0;
    public bool followMouse = false;
    public bool moving;
    float t;
    public float tempY;
    public RigidBody bcb;
    public bool belowPlayer;
    public bool isPlayer;
    public bool isPushable = false;
    public bool isTurtle = false;
    public bool inWater = false;
    public bool flipped = false;
    public bool grounded;
    public bool isPushing;
    public bool isWalling;
    public bool pushed;
    public float top;
    public float bottom;
    public float left;
    public float right;
    public bool inBell = false;

    public Sound landSFX = new Sound("sfx/3.wav", false, true);
    public Sound landBoxSFX = new Sound("sfx/15.wav", false, true);
    public Sound splashSFX = new Sound("sfx/8.wav", false, true);
    public SoundChannel pushSFX;

    public bool splashed = false;

    public bool onBox = false;

    public RigidBody(string filename, int cols, int rows, Vec2 pos, bool moving, int frames = -1, bool keepInCache = false, bool addCollider = true) : base(filename, rows, cols)
    {
        myGame = (MyGame)game;
        SetOrigin(width / 2, height / 2);
        position = pos;
        velocity = new Vec2(0, 0);

        pushSFX = new Sound("sfx/20.wav", true, true).Play();
        pushSFX.IsPaused = true;
        myGame.soundChannels.Add(pushSFX);

        this.moving = moving;
        if(isPushable)
        {
            maxVertSpeed = .1f;
        }
    }



    public void Update()
    {

        
        inWater = false;
        //Follow Mouse
        if (followMouse)
        {
            acceleration = (new Vec2(Input.mouseX, Input.mouseY) - position).Normalized();
            acceleration.SetXY(acceleration.x / 10, acceleration.y / 10);
        }
        //



        if (moving)
        {
            acceleration.y = gravity;
            if (bcb != null)
            {
                if (bcb.onBox)
                {
                    bcb = null;
                }
            }

            if ((onBox || (!onBox && y >= myGame.water.y)) && !inBell)
            {
                if(isPushable)
                Console.WriteLine(isPushable);
                inWater = true;
                acceleration.SetXY(acceleration.x, 0);
                
                if (!belowPlayer)
                {
                    
                    if (velocity.y > 0.23 && !isTurtle)
                    {
                        acceleration.SetXY(acceleration.x, velocity.y * -0.23f);
                    }
                    else if (y - myGame.water.y > -5 && !isTurtle)
                    {
                        if (!splashed && isPushable)
                            splashSFX.Play();
                        splashed = true;
                        acceleration.SetXY(acceleration.x, -0.115f);
                    }
                    else if (onBox && !pushed)
                    {

                        Console.WriteLine(isPushable);
                        Console.WriteLine("buh");
                        if (isPlayer)
                        {
                            if (!Input.GetKey(Key.A) && !Input.GetKey(Key.D))
                            {
                                velocity.y = 0.1f;
                                velocity.x = bcb.velocity.x /** 1.05f*/;
                            }

                            if (Input.GetKey(Key.DOWN))
                            {
                                velocity.y = Mathf.Max(1, myGame.water.y - bcb.y);
                                velocity.x = bcb.velocity.x * 1.5f;
                            }

                            else if (Input.GetKey(Key.UP))
                            {
                                velocity.y = -2.7f * myGame.waterSpeed;
                                velocity.x = bcb.velocity.x * 3f;
                            }
                        }
                        else
                        {
                            if (Input.GetKey(Key.DOWN))
                            {
                                velocity.y = Mathf.Max(1, myGame.water.y - bcb.y);
                                velocity.x = bcb.velocity.x * 1.17f;
                            }

                            else if (Input.GetKey(Key.UP))
                            {

                                velocity.y = -2.7f * myGame.waterSpeed;
                                velocity.x = bcb.velocity.x * 4f;
                            }
                            else
                            {
                                velocity.y = 0.1f;
                                velocity.x = bcb.velocity.x;
                            }
                            
                        }

                    }
                }
                else
                {
                    
                    velocity.SetXY(velocity.x, myGame.water.y - y);
                }
            }

            if (isPushing)
            {
                pushSFX.IsPaused = false;
            }
            else pushSFX.IsPaused = true;

            if (acceleration.x == 0)
            {
                acceleration.x = -velocity.x / 5;
            }

            if (velocity.x >= maxSpeed)
            {
                velocity.x = maxSpeed;
            }

            if (velocity.x <= -maxSpeed)
            {
                velocity.x = -maxSpeed;
            }

            if (velocity.y >= maxVertSpeed)
            {

                velocity.y = maxVertSpeed;
            }
            if (velocity.y <= -maxVertSpeed)
            {
                velocity.y = -maxVertSpeed;
            }
            


        }

        top = position.y - height / 2;
        bottom = position.y + height / 2;
        if (isTurtle)
        {
            bottom = (position.y + height / 2) - 40;
        }
        left = position.x - width / 2;
        right = position.x + width / 2;

        if (!CheckCollisions())
        {
            velocity += acceleration;
            position += velocity;
        }
        SetXY(position.x, position.y);
        acceleration.x = 0;

        
    }

    public void SolveIntersections()
    {
        
        foreach (RigidBody other in myGame.rigidBodies)
        {
            if ((isPlayer || isPushable) && other.top < bottom && other.top > top && other.left < right && other.right > right)
            {
                position.y = other.top - (height / 2);
                acceleration.y = 0;
                velocity.y = 0;
            }
            if ((isPlayer || isPushable) && other.top < bottom && other.top > top && other.right < right && other.right > left)
            {
                position.y = other.top - (height / 2);
                acceleration.y = 0;
                velocity.y = 0;
            }
            if ((isPlayer || isPushable) && other.bottom > top && other.bottom < bottom && other.left < right && other.right > right)
            {
                position.y = other.bottom + (height / 2);
                acceleration.y = 0;
                velocity.y = 0;
            }
            if ((isPlayer || isPushable) && other.bottom > top && other.bottom < bottom && other.right < right && other.right > left)
            {
                position.y = other.bottom + (height / 2);
                acceleration.y = 0;
                velocity.y = 0;
            }
        }
    } 

    public bool CheckCollisions()
    {
        onBox = false;
        t = 0;
        bool collided = false;
        bool lrc = false;
        bool bc = false;
        bool tc = false;
        Vec2 simV = velocity;
        Vec2 sim = position;

        Vec2 poi = position;

        simV += acceleration;
        sim += simV;

        foreach (RigidBody other in myGame.rigidBodies)
        {
            SolveIntersections();
            if (other == this || (!other.moving && !moving)) continue;

            if (other.top < sim.y + (height / 2) && other.left < right && other.right > left && other.bottom > top)
            {

                t = (Mathf.Abs(sim.y - position.y) / Mathf.Abs(other.top - position.y));



                if (isPlayer)
                    tempY = position.y;

                if (other == bcb && bcb.inWater)
                {
                    onBox = true;
                }

                if ((other.isPushable || other.isTurtle) && !isTurtle)
                {
                    
                    if (bcb != other)
                    {
                        if (bcb != null)
                            bcb.belowPlayer = false;
                        bcb = other;
                        
                        bcb.belowPlayer = true;
                    }
                    velocity.y = 0;
                }





                else
                {
                    velocity.y = -velocity.y * bounciness;
                    if (bcb != null)
                    {
                        bcb.belowPlayer = false;
                        bcb = null;
                    }
                }

                bc = true;
                collided = true;

            }
            if (other.top < sim.y + (height / 2) + 10 && other.left < right && other.right > left && other.bottom > top)
            {
                if (!grounded)
                    if (isPlayer)
                        landSFX.Play();
                    else if (isPushable) landBoxSFX.Play();
                grounded = true;
            }

            if (other.bottom > sim.y - (height / 2) && other.left < right && other.right > left && other.top < bottom)
            {
                if (Mathf.Abs(position.y - sim.y) / (Mathf.Abs(position.y - other.bottom)) > t || t == 0)
                {
                    t = Mathf.Abs(position.y - sim.y) / Mathf.Abs(position.y - other.bottom);
                }

                
                velocity.y = -velocity.y * bounciness;

                tc = true;
                collided = true;

            }

            if (other.right > sim.x - (width / 2) && other.top < bottom && other.bottom > top && other.left < right)
            {
                if ((Mathf.Abs(position.x - sim.x)) / (Mathf.Abs(position.x - other.right)) > t || t == 0)
                {
                    t = Mathf.Abs(position.x - sim.x) / Mathf.Abs(position.x - other.right);
                }

                if(!other.moving && isPlayer)
                {
                    isWalling = true;
                }

                if (other.isPushable)
                {
                    velocity.x = 0;
                    flipped = !flipped;
                }
                else
                {
                    velocity.x = -velocity.x * bounciness;
                    flipped = !flipped;
                }
                collided = true;
                lrc = true;


            }


            if (other.left < sim.x + (width / 2) && other.top < bottom && other.bottom > top && other.right > left)
            {
                if (Mathf.Abs(sim.x - position.x) / (Mathf.Abs(other.left - position.x)) > t || t == 0)
                {
                    t = Mathf.Abs(sim.x - position.x) / Mathf.Abs(other.left - position.x);
                }

                if (!other.moving && isPlayer)
                {
                    isWalling = true;
                }

                if (other.isPushable)
                {
                    velocity.x = 0;
                    flipped = !flipped;
                }
                else
                {
                    velocity.x = -velocity.x * bounciness;
                    flipped = !flipped;
                }
                collided = true;
                lrc = true;


            }
            other.pushed = false;
            if (Input.GetKey(Key.A) && Input.GetKey(Key.D)) isPushing = false;

            else if (Input.GetKey(Key.A) || Input.GetKey(Key.D))
            {
                if (other.isPushable && (left - 3 <= other.right && right > other.right) &&
                    ((bottom > other.top && top < other.top) || (top < other.bottom && bottom > other.bottom))
                   && (isPlayer) && Input.GetKey(Key.A))
                {
                    isPushing = true;
                    other.acceleration.x = velocity.x * 4;
                    other.pushed = true;
                }
                else if (other.isPushable && (right + 3 >= other.left && left < other.left) &&
                    ((bottom > other.top && top < other.top) || (top < other.bottom && bottom > other.bottom))
                   && (isPlayer) && Input.GetKey(Key.D))
                {
                    isPushing = true;
                    other.acceleration.x = velocity.x * 4;
                    other.pushed = true;
                }

                
            }
            else isPushing = false; 



            if (other.bottom >= myGame.water.y && ((other.left < left && other.right > left) || (other.right > right && other.left < right) || (other.left > left && other.right < right)) && (bottom - 4 < other.top && bottom + 4 > other.top) && (isPlayer || isPushable)  && bc && bcb == other) { onBox = true; position.y = other.top - (height / 2); }

        }

        if (t != 0)
        {


            if (lrc && !bc && !tc) velocity.y += acceleration.y;
            if (!lrc && bc) velocity.x += acceleration.x;
            //position = position + t * velocity;
        }

        if (collided)
            position += velocity * (1 - t);

        return collided;
    }

}


