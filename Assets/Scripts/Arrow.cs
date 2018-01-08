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

    [Space(30)]
    public float dragCoefficient;
    public Vector3 dragForce;
    public Vector3 actualVelocity;

    private bool arrowCollision = false;
    private float powerForce = 0;

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

        actualVelocity = new Vector3(powerForce * -GameMaster.GM.player.transform.forward.x * Mathf.Cos(launchAngle * Mathf.Deg2Rad), powerForce * Mathf.Sin(launchAngle * Mathf.Deg2Rad), -powerForce * GameMaster.GM.player.transform.forward.z * Mathf.Cos(launchAngle * Mathf.Deg2Rad));
    }

    private void Update()
    {
        if (!arrowCollision)
        {

            dragForce = Utils.DragForce(underWater, actualVelocity, dragCoefficient, 1);

            actualVelocity = Utils.RefreshVelocity(actualVelocity, dragForce, arrowMass, Time.deltaTime);
            this.transform.position = Utils.RefreshPosition(this.transform.position, arrowMass, dragForce, actualVelocity, Time.deltaTime);

            if (this.transform.position.y < -15)
            {
                CollisionManager.manager.projectileColliders.Remove(this.GetComponent<ColliderSphere>());
                Destroy(this.gameObject);
            }
            else if (this.transform.position.y < -3.20)
            {
                underWater = true;
            }
        }    
    }

    public void SetInitialVelocity(float _powerForce)
    {
        powerForce = _powerForce;
    }

    public void StopArrow()
    {
        actualVelocity = new Vector3(0, 0, 0);
        arrowCollision = true;
    }
}
