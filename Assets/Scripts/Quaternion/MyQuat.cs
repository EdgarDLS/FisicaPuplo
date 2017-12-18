using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyQuat
{
    public float x, y, z, w;

    public MyQuat()
    {
        this.x = 0;
        this.y = 0;
        this.z = 0;
        this.w = 0;
    }

    public MyQuat(float x, float y, float z, float w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }

    public MyQuat(Quaternion q1)
    {
        this.x = q1.x;
        this.y = q1.y;
        this.z = q1.z;
        this.w = q1.w;
    }

    public void Inverse()
    {
        //MyQuat myQuat = new MyQuat(-this.x, -this.y, -this.z, this.w);
        this.x = -this.x;
        this.y = -this.y;
        this.z = -this.z;
    }

    public static MyQuat Multiply(MyQuat q1, MyQuat q2)
    {
        MyQuat quaternionMultiplied = new MyQuat(0, 0, 0, 0);

        quaternionMultiplied.w = (q2.w * q1.w - q2.x * q1.x - q2.y*q1.y - q2.z * q1.z);
        quaternionMultiplied.x = (q2.w * q1.x + q2.x * q1.w - q2.y * q1.z + q2.z * q1.y);
        quaternionMultiplied.y = (q2.w * q1.y + q2.x * q1.z + q2.y * q1.w - q2.z * q1.x);
        quaternionMultiplied.z = (q2.w * q1.z - q2.x * q1.y + q2.y * q1.x + q2.z * q1.w);

        return quaternionMultiplied;
    }

    public float Modulus()
    {
        return Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2) + Mathf.Pow(z, 2) + Mathf.Pow(w, 2));
    }

    public static float Angle(MyQuat q1, MyQuat q2)
    {
        q1.Normalization();
        q2.Normalization();

        q2.Inverse();
        MyQuat newQuaternion = new MyQuat();

        newQuaternion = MyQuat.Multiply(q1, q2);

        return 2 * Mathf.Acos(newQuaternion.w) * Mathf.Rad2Deg;
    }

    public void Normalization()
    {
        this.x = this.x / Mathf.Sqrt(Mathf.Pow(this.x, 2) + Mathf.Pow(this.y, 2) + Mathf.Pow(this.z, 2) + Mathf.Pow(this.w, 2));
        this.y = this.y / Mathf.Sqrt(Mathf.Pow(this.x, 2) + Mathf.Pow(this.y, 2) + Mathf.Pow(this.z, 2) + Mathf.Pow(this.w, 2));
        this.z = this.z / Mathf.Sqrt(Mathf.Pow(this.x, 2) + Mathf.Pow(this.y, 2) + Mathf.Pow(this.z, 2) + Mathf.Pow(this.w, 2));
        this.w = this.w / Mathf.Sqrt(Mathf.Pow(this.x, 2) + Mathf.Pow(this.y, 2) + Mathf.Pow(this.z, 2) + Mathf.Pow(this.w, 2));
    }

    public static MyQuat Axis2Quad(float angle, Vector3 v3)
    {
        MyQuat axisQuat = new MyQuat();

        // RADIANES

        v3.Normalize();

        float radianAngle = angle * Mathf.Deg2Rad;

        axisQuat.x = v3.x * Mathf.Sin(radianAngle / 2);
        axisQuat.y = v3.y * Mathf.Sin(radianAngle / 2);
        axisQuat.z = v3.z * Mathf.Sin(radianAngle / 2);
        axisQuat.w = Mathf.Cos(radianAngle / 2);

        axisQuat.Normalization();

        return axisQuat;
    }

    public MyQuat Quad2Axis(MyQuat q1)
    {
        MyQuat returnQuaternion = new MyQuat();

        returnQuaternion.x = q1.x / Mathf.Sqrt(1-q1.w*q1.w);
        returnQuaternion.y = q1.y / Mathf.Sqrt(1 - q1.w * q1.w);
        returnQuaternion.z = q1.z / Mathf.Sqrt(1 - q1.w * q1.w);
        returnQuaternion.w = 2 * Mathf.Acos(q1.w) * Mathf.Rad2Deg;

        return returnQuaternion;
    }

    public Quaternion ToUnityQuat()
    {
        Quaternion unityQuat = new Quaternion();

        unityQuat.x = this.x;
        unityQuat.y = this.y;
        unityQuat.z = this.z;
        unityQuat.w = this.w;

        return unityQuat;
    }
}
