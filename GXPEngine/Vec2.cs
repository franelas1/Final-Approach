using System;
using GXPEngine; // Allows using Mathf functions

public struct Vec2
{
    public float x;
    public float y;

    public Vec2(float pX = 0, float pY = 0)
    {
        x = pX;
        y = pY;
    }

    public float Length()
    {
        return Mathf.Sqrt(x * x + y * y);
    }

    public void Normalize()
    {
        float length = Length();

        if (length != 0)
        {
            x = x * (1 / length);
            y = y * (1 / length);
        }

    }

    public Vec2 Normalized()
    {
        float length = Length();
        if (length != 0)
        {
            return new Vec2(x * (1 / length), y * (1 / length));
        }
        else return this;
    }

    public void SetXY(float _x, float _y)
    {
        x = _x; y = _y;
    }

    public static Vec2 operator +(Vec2 left, Vec2 right)
    {
        return new Vec2(left.x + right.x, left.y + right.y);
    }

    public static Vec2 operator -(Vec2 left, Vec2 right)
    {
        return new Vec2(left.x - right.x, left.y - right.y);
    }

    public static Vec2 operator *(float left, Vec2 right)
    {
        return new Vec2(left * right.x, left * right.y);
    }
    public static Vec2 operator *(Vec2 left, float right)
    {
        return new Vec2(left.x * right, left.y * right);
    }

    //Angle Stuff

    public static float Deg2Rad(float deg)
    {
        return deg * (Mathf.PI / 180);
    }

    public static float Rad2Deg(float rad)
    {
        return rad * (180 / Mathf.PI);
    }

    public static Vec2 GetUnitVectorDeg(float deg)
    {
        return GetUnitVectorRad(Deg2Rad(deg));
    }

    public static Vec2 GetUnitVectorRad(float rad)
    {
        return new Vec2(Mathf.Cos(rad), Mathf.Sin(rad));
    }

    public static Vec2 RandomUnitVector()
    {
        float random = (float)new Random().NextDouble() * 360;
        return GetUnitVectorDeg(random);
    }

    public void SetAngleDegrees(float deg)
    {
        SetAngleRadians(Deg2Rad(deg));
    }

    public void SetAngleRadians(float rad)
    {
        float length = Length();
        Vec2 unitVec = GetUnitVectorRad(rad);
        x = unitVec.x * length; y = unitVec.y * length;
    }

    public float GetAngleDegrees()
    {
        return Rad2Deg(GetAngleRadians());
    }

    public float GetAngleRadians()
    {
        return Mathf.Atan2(y, x);

    }

    public void RotateRadians(float rad)
    {
        SetAngleRadians(GetAngleRadians() + rad);
    }

    public void RotateDegrees(float deg)
    {
        RotateRadians(Deg2Rad(deg));
    }

    public void RotateAroundRadians(Vec2 point, float rad)
    {
        Vec2 tempVec = this - point;
        tempVec.RotateRadians(rad);
        this = tempVec + point;
    }

    public void RotateAroundDegrees(Vec2 point, float deg)
    {
        RotateAroundRadians(point, Deg2Rad(deg));
    }


    public override string ToString()
    {
        return String.Format("({0},{1})", x, y);
    }


}
