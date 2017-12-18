using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyVector3
{
    public float x, y, z;


    public MyVector3()
    {
        this.x = 0;
        this.y = 0;
        this.z = 0;
    }
    public MyVector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public MyVector3(Vector3 v)
    {
        this.x = v.x;
        this.y = v.y;
        this.z = v.z;
    }

    public MyVector3 Normalize()
    {
        return new MyVector3(x / Module(), y / Module(), z / Module());
    }

    public float Module()
    {
        return Mathf.Sqrt(Mathf.Pow(this.x, 2) + Mathf.Pow(this.y, 2) + Mathf.Pow(this.z, 2));
    }

    public static float Dot(MyVector3 lhs, MyVector3 rhs)
    {
        return (lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z);
    }

    public static MyVector3 Cross(MyVector3 lhs, MyVector3 rhs)
    {
        return new MyVector3(lhs.y * rhs.z - lhs.z * rhs.y, lhs.x * rhs.z - lhs.z * rhs.x, lhs.x * rhs.y - lhs.y * rhs.x);
    }

    public static float Angle(MyVector3 lhs, MyVector3 rhs)
    {
        return Mathf.Acos(Dot(lhs, rhs) / (lhs.Module() * rhs.Module()));
    }

    //NO FUNCIONA ENCARA
    public static MyVector3 Lerp(MyVector3 origin, MyVector3 target, float time)
    {
        return (target - origin) * (Time.deltaTime / time);
    }

    //OPERADORS
    public static MyVector3 operator +(MyVector3 lhs, MyVector3 rhs)
    {
        return new MyVector3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.x + rhs.z);
    }

    public static MyVector3 operator -(MyVector3 lhs, MyVector3 rhs)
    {
        return new MyVector3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.x - rhs.z);
    }

    public static MyVector3 operator *(MyVector3 v, float f)
    {
        return new MyVector3(v.x * f, v.y * f, v.z * f);
    }

    public static MyVector3 operator *(float f, MyVector3 v)
    {
        return new MyVector3(v.x * f, v.y * f, v.z * f);
    }

    public static MyVector3 operator /(MyVector3 v, float f)
    {
        return new MyVector3(v.x / f, v.y / f, v.z / f);
    }

    public static bool operator ==(MyVector3 lhs, MyVector3 rhs)
    {
        return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
    }

    public static bool operator !=(MyVector3 lhs, MyVector3 rhs)
    {
        return lhs.x != rhs.x || lhs.y != rhs.y || lhs.z != rhs.z;
    }

    //CAST DE Vector3 a MyVector3
    public static explicit operator MyVector3(Vector3 v)
    {
        return new MyVector3(v);
    }

    public static explicit operator Vector3(MyVector3 v)
    {
        return new Vector3(v.x, v.y, v.z);
    }
}

