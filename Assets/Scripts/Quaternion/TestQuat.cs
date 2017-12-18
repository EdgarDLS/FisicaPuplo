using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TestQuat : MonoBehaviour {

    public MyQuat q1 = new MyQuat(3.5f, -10, 5.7f, 1.7f);
    public MyQuat q2 = new MyQuat(9, -5.3f, +4, -6.6f);
    public MyQuat multiply = new MyQuat(0, 0, 0, 0);
    public MyQuat m = new MyQuat(0, 0, 0, 0);
    public MyQuat q2a = new MyQuat(0, 0, 0, 0);
    public MyQuat q2aresult = new MyQuat(0.7071f, 0, 0, 0.7071f);

    void Start ()
    {
        // float value = quaternionTest.Modulus();

        // MULTIPLICATION
        /*
        m = multiply.Multiply(q1, q2);
        Debug.Log(m.x + ":" + m.y + ":" + m.z + ":" + m.w);
        */

        // Axis to Quaternion
        
        //m.Axis2Quad(120, new Vector3(-0.5774f, 0.5774f, 0.5774f));
        //Debug.Log(m.x + ":" + m.y + ":" + m.z + ":" + m.w);

        // Quaternion to Axis
        q2a = q2a.Quad2Axis(m);
        //Debug.Log(q2a.x + ":" + q2a.y + ":" + q2a.z + ":" + q2a.w);

        Quaternion angle1 = new Quaternion(3.5f, -10, 5.7f, 1.7f);
        Quaternion angle2 = new Quaternion(9, -5.3f, +4, -6.6f);

        Debug.Log("Ours: " + MyQuat.Angle(q1, q2));
        Debug.Log("Unity: " + Quaternion.Angle(angle1, angle2));
    }

}
