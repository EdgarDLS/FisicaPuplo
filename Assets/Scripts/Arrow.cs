using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    enum HeadForm
    {
        Sphere = 0,
        HalfSphere = 1,
        Cone = 2,
        Cube = 3,
        AngledCube = 4,
        LongCylinder = 5,
        ShortCylinder = 6,
        StreamlinedBody = 7,
        StreamlinedHalfbody = 8
    };

    [SerializeField]
    HeadForm arrowHeadForm = HeadForm.Sphere;

    public Vector3 initialVelocity;
    public float launchAngle;
    public float arrowMass;
    public bool underWater = false;

    public float time = 0;
    public float timePlus = 0;

    [Space(30)]
    public float dragCoefficient;
    public Vector3 dragForce;
    public Vector3 actualVelocity;

    private bool arrowCollision = false;

    void Start()
    {
        switch(arrowHeadForm)
        {
            case HeadForm.Sphere:               // Sphere
                dragCoefficient = 0.47f;
                break;
            case HeadForm.HalfSphere:           // HalfSphere
                dragCoefficient = 0.42f;
                break;
            case HeadForm.Cone:                 // Cone
                dragCoefficient = 0.50f;
                break;
            case HeadForm.Cube:                 // Cube
                dragCoefficient = 1.05f;
                break;
            case HeadForm.AngledCube:           // AngledCube
                dragCoefficient = 0.80f;
                break;
            case HeadForm.LongCylinder:         // LongCylinder
                dragCoefficient = 0.82f;
                break;
            case HeadForm.ShortCylinder:        // ShortCylinder
                dragCoefficient = 1.15f;
                break;
            case HeadForm.StreamlinedBody:      // StreamlinedBody
                dragCoefficient = 0.04f;
                break;
            case HeadForm.StreamlinedHalfbody:  // StreamlinedHalfbody
                dragCoefficient = 0.09f;
                break;
        }

        actualVelocity = initialVelocity;
    }

    private void Update()
    {
        if (!arrowCollision)
        {
            dragForce = Utils.DragForce(underWater, actualVelocity, dragCoefficient, 1);
            this.transform.position = Utils.RefreshPosition(this.transform.position, arrowMass, dragForce, actualVelocity, timePlus);
            actualVelocity = Utils.RefreshVelocity(actualVelocity, dragForce, arrowMass, timePlus);

            if (this.transform.position.y < -15)
            {
                CollisionManager.manager.projectileColliders.Remove(this.GetComponent<ColliderSphere>());
                Destroy(this.gameObject);
            }

            timePlus += Time.deltaTime;
        }    
    }

    public void SetInitialVelocity(Vector3 _initialVelocity)
    {
        initialVelocity = _initialVelocity;
    }

    public void StopArrow()
    {
        actualVelocity = new Vector3(0, 0, 0);
        arrowCollision = true;
    }
}
