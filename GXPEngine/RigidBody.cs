using GXPEngine;
using System.Diagnostics.Eventing.Reader;

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
    public float top;
    public float bottom;
    public float left;
    public float right;
    public bool inBell = false;


    public bool onBox = false;

    public RigidBody(string filename, int cols, int rows, Vec2 pos, bool moving, bool keepInCache = false, bool addCollider = true) : base(filename, rows: 1, cols: 1)
    {
        myGame = (MyGame)game;
        SetOrigin(width / 2, height / 2);
        position = pos;
        velocity = new Vec2(0, 0);

        this.moving = moving;
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
            if ((onBox || (!isPlayer &&  y >= myGame.water.y)) && !inBell)
            {
                inWater = true;
                acceleration.SetXY(acceleration.x, 0);

                if (!belowPlayer)
                {
                    if (velocity.y > 0.23 && !isTurtle) { acceleration.SetXY(acceleration.x, velocity.y * -0.23f); }
                    else if (y - myGame.water.y > -5 && !isTurtle)
                    {
                        acceleration.SetXY(acceleration.x, -0.115f);
                    }
                    else if (onBox)
                    {
                        if (!Input.GetKey(Key.A) && !Input.GetKey(Key.D))
                        {
                            velocity.x = bcb.velocity.x * 1.05f;
                        }

                        

                        if (Input.GetKey(Key.DOWN))
                        {
                            velocity.y = myGame.water.y - bcb.y;
                            velocity.x = bcb.velocity.x * 1.15f;
                        }

                        else if (Input.GetKey(Key.UP))
                        {
                            velocity.y = -2.7f * myGame.waterSpeed;
                            velocity.x = bcb.velocity.x * 1.75f;

                        }
                         
                        
                        

                    }
                }

                else
                {

                    velocity.SetXY(velocity.x, myGame.water.y - y);

                }
            }


            if (acceleration.x == 0)
            {
                acceleration.x = -velocity.x / 12;
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
            //Euler Integration


        }


        if (!CheckCollisions())
        {
            velocity += acceleration;
            position += velocity;
        }
        SetXY(position.x, position.y);
        acceleration.x = 0;

        top = position.y - height / 2;
        bottom = position.y + height / 2;
        left = position.x - width / 2;
        right = position.x + width / 2;
    }

    /*public void SolveIntersections()
    {
        int closest = 0;
        foreach (RigidBody other in myGame.rigidBodies)
        {
            if (other.top > top && other.left <= left && other.right >= right && other.top < bottom)
            {
                position.y = other.top - (height / 2);
                acceleration.y = 0;
                velocity.y = 0;
            }
            if (other.top <= top && other.right < right && other.right > left && other.bottom >= bottom)
            {
                position.x = other.right + (width / 2);
                acceleration.x = 0;
                velocity.x = 0;
            }
            if (other.top < top && other.left <= left && other.right >= left && other.bottom > top)
            {
                position.y = other.bottom + (height / 2);
                    acceleration.y = 0;
                velocity.y = 0;
            }
            if (other.top <= bottom && other.left < right && other.right > right && other.bottom >= bottom)
            {
                position.x = other.x - other.width;
                acceleration.x = 0;
                velocity.x = 0;
            }
        }
    } */

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

            if (other == this || (!other.moving && !moving)) continue;

            if (other.top < sim.y + (height / 2) && other.left < right && other.right > left && other.bottom > top)
            {

                t = (Mathf.Abs(sim.y - position.y) / Mathf.Abs(other.top - position.y));



                if (isPlayer)
                    tempY = position.y;



                if (other.isPushable || other.isTurtle)
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


            if (other.isPushable && (left - 3 <= other.right && right > other.right) &&
                ((bottom > other.top && top < other.top) || (top < other.bottom && bottom > other.bottom))
               && (isPlayer) && ((Input.GetKey(Key.LEFT)) || Input.GetKey(Key.A))) other.acceleration.x = acceleration.x * 2;
            else if (other.isPushable && (right + 3 >= other.left && left < other.left) &&
                ((bottom > other.top && top < other.top) || (top < other.bottom && bottom > other.bottom))
               && (isPlayer) && ((Input.GetKey(Key.RIGHT)) || Input.GetKey(Key.D))) other.acceleration.x = acceleration.x * 2;


            if ((other.isPushable || other.isTurtle) && other.bottom >= myGame.water.y && ((other.left < left && other.right > left) || (other.right > right && other.left < right) || (other.left > left && other.right < right)) && (bottom - 4 < other.top && bottom + 4 > other.top) && isPlayer && bc && bcb == other) { onBox = true; position.y = other.top - (height / 2); }

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


