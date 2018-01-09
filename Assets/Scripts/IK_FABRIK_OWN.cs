using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class IK_FABRIK_OWN : MonoBehaviour
{
    //public const int MAX_ITERATIONS = 100;

    //public GameObject tentacleArm;
    //public Rigidbody[] tentacleChilds;

    public float maxAngleRotation = 40;

    [Space]
    public float minSwingClamp = 0.9f;
    public float maxSwingClamp = 1.1f;
    public float minTwistClamp = 0.9f;
    public float maxTwistClamp = 1.1f;

    [Space]
    public Transform[] joints;
    public Transform target;

    //public int numIterations;

    private MyVector3[] copy;

    //private Vector3[] copy;
    private float[] distances;
    private bool done;

    float tresholdCondition = 0.1f;

    void Start()
    {
        //tentacleChilds = tentacleArm.GetComponentsInChildren<Rigidbody>();

        distances = new float[joints.Length - 1];
        copy = new MyVector3[joints.Length];

        //copy = new Vector3[joints.Length];
    }

    void Update()
    {
        // Copy the joints positions to work with
        //TODO
        copy[0] = new MyVector3(joints[0].position);

        //copy[0] = joints[0].position;

        for (int i = 0; i < joints.Length - 1; i++)
        {
            copy[i + 1] = new MyVector3(joints[i + 1].position);
            distances[i] = (copy[i + 1] - copy[i]).Module();

            //copy[i + 1] = joints[i + 1].position;
            //distances[i] = (copy[i + 1] - copy[i]).magnitude;
        }
        // CALCULATE ALSO THE DISTANCE BETWEEN JOINTS

        //done = TODO
        done = (copy[copy.Length - 1] - new MyVector3(target.position)).Module() < tresholdCondition;

        //done = (copy[copy.Length - 1] - target.position).magnitude < tresholdCondition;

        if (!done)
        {
            float targetRootDist = (copy[0] - new MyVector3(target.position)).Module();

            //float targetRootDist = Vector3.Distance(copy[0], target.position);

            // Update joint positions
            if (targetRootDist > distances.Sum())
            {
                // The target is unreachable
                for (int i = 0; i < copy.Length - 1; i++)
                {
                    float r = (new MyVector3(target.position) - copy[i]).Module();

                    //float r = (target.position - copy[i]).magnitude;
                    float lambda = distances[i] / r;

                    copy[i + 1] = (1 - lambda) * copy[i] + (lambda * new MyVector3(target.position));

                    //copy[i + 1] = (1 - lambda) * copy[i] + (lambda * target.position);
                }

            }
            else
            {
                MyVector3 b = copy[0];

                //Vector3 b = copy[0];

                // The target is reachable
                //while (TODO)

                float difference = (copy[copy.Length - 1] - new MyVector3(target.position)).Module();

                //float difference = (copy[copy.Length - 1] - target.position).magnitude;

                while (difference > tresholdCondition) // treshold = tolerance
                {
                    // numIterations++;

                    // STAGE 1: FORWARD REACHING
                    //TODO
                    copy[copy.Length - 1] = new MyVector3(target.position);

                    //copy[copy.Length - 1] = target.position;

                    for (int i = copy.Length - 2; i > 0; i--)
                    {
                        float r = (copy[i + 1] - copy[i]).Module();

                        //float r = (copy[i + 1] - copy[i]).magnitude;
                        float lambda = distances[i] / r;

                        copy[i] = (1 - lambda) * copy[i + 1] + lambda * copy[i];



                    }

                    // STAGE 2: BACKWARD REACHING
                    //TODO
                    copy[0] = b;

                    for (int i = 0; i < copy.Length - 1; i++)
                    {
                        float r = (copy[i + 1] - copy[i]).Module();

                        //float r = (copy[i + 1] - copy[i]).magnitude;
                        float lambda = distances[i] / r;

                        copy[i + 1] = (1 - lambda) * copy[i] + lambda * copy[i + 1];
                    }

                    difference = (copy[copy.Length - 1] - new MyVector3(target.position)).Module();

                    //difference = (copy[copy.Length - 1] - target.position).magnitude;
                }
            }

            // Update original joint rotations
            for (int i = 0; i <= joints.Length - 2; i++)
            {
                // float originalAngle = joints[i].rotation.w;

                MyQuat parentRotation = new MyQuat(joints[i + 1].rotation);
                MyQuat childRotation = new MyQuat(joints[i].rotation);

                //TODO
                // Rotation
                MyVector3 vectorA = new MyVector3(joints[i + 1].position) - new MyVector3(joints[i].position);
                MyVector3 vectorB = copy[i + 1] - copy[i];

                //Vector3 vectorA = joints[i + 1].position - joints[i].position;
                //Vector3 vectorB = copy[i + 1] - copy[i];

                // float angle = Mathf.Acos(Vector3.Dot(vectorA.normalized, vectorB.normalized)) * Mathf.Rad2Deg;
                float cosA = (MyVector3.Dot(vectorA.Normalize(), vectorB.Normalize()));
                float sinA = MyVector3.Cross(vectorA.Normalize(), vectorB.Normalize()).Module();

                //float cosA = (Vector3.Dot(vectorA.normalized, vectorB.normalized));
                //float sinA = Vector3.Cross(vectorA.normalized, vectorB.normalized).magnitude;

                // Atan = Cos | Atan2 = denominador y...
                float angle = Mathf.Atan2(sinA, cosA) * Mathf.Rad2Deg;

                MyVector3 axis = MyVector3.Cross(vectorA, vectorB).Normalize();

                //Vector3 axis = Vector3.Cross(vectorA, vectorB).normalized;

                // joints[i].rotation = Quaternion.AngleAxis(angle, axis) * joints[i].rotation;

                joints[i].rotation = MyQuat.Multiply(MyQuat.Axis2Quad(angle, axis), childRotation).ToUnityQuat();


                //joints[i].rotation = MyQuat.Multiply(MyQuat.Axis2Quad(angle, axis), childRotation).ToUnityQuat();

                childRotation = new MyQuat(joints[i].rotation);

                float angleTest = MyQuat.Angle(parentRotation, childRotation);

                if (Mathf.Abs(angleTest) > maxAngleRotation)
                    joints[i + 1].rotation = joints[i].rotation;


                joints[i + 1].position = (Vector3)copy[i + 1];


                //joints[i + 1].position = copy[i + 1];
            }
        }
    }
}
