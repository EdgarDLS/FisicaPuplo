using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public static class Utils
{
    public static readonly float G = 9.81f;

    public static Vector3 DragForce(bool underWater, Vector3 velocity, float dragCoefficient, float crossSectionalArea)
    {
        Vector3 resultantVector = new Vector3(0, 0, 0);
        float density = 1.225f;

        if (underWater) density = 1000f;

        resultantVector.x = 0.5f * density * Mathf.Pow(velocity.x, 2) * dragCoefficient * crossSectionalArea;
        resultantVector.y = 0.5f * density * Mathf.Pow(velocity.y, 2) * dragCoefficient * crossSectionalArea;
        resultantVector.z = 0.5f * density * Mathf.Pow(velocity.z, 2) * dragCoefficient * crossSectionalArea;

        return resultantVector;
    }

    public static Vector3 CuadraticDragForce(bool underWater, Vector3 velocity, float dragCoefficient, float crossSectionalArea)
    {
        Vector3 resultantVector = new Vector3(0, 0, 0);
        float density = 1.225f;

        if (underWater) density = 1000f;

        resultantVector.x = 1 / 2 * density * velocity.x * dragCoefficient * crossSectionalArea;
        resultantVector.y = 1 / 2 * density * velocity.y * dragCoefficient * crossSectionalArea;
        resultantVector.z = 1 / 2 * density * velocity.z * dragCoefficient * crossSectionalArea;

        return resultantVector;
    }

    public static Vector3 refreshPosition(Vector3 position, float mass, Vector3 dragForce, Vector3 initialVelocity, Vector3 actualVelocity, float time)
    {
        Vector3 newPosition = new Vector3();

        newPosition.x = mass / dragForce.x * actualVelocity.x * (1 - Mathf.Exp((-dragForce.x / mass) * 0.02f));
        newPosition.y = mass / dragForce.y * (mass * G / dragForce.y + actualVelocity.y) * (1 - Mathf.Exp((-dragForce.y / mass) * 0.02f)) - mass * G/dragForce.y * 0.02f;



        Debug.Log("Dragforce.x: " + dragForce.x + " Dragforce.y: " + dragForce.y +" EXPX: " + Mathf.Exp((-dragForce.y / mass) * time) + " EXPY: " + Mathf.Exp((-dragForce.x / mass) * time)); 

        return newPosition;
    }
}
