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

    public MyVector3 initialVelocity;
    public int launchAngle;
    public float arrowMass;
    public bool underWater = false;
    public bool linearDragForce = true;

    [Space(30)]
    public float dragCoefficient;
    public MyVector3 dragForce;
    public MyVector3 actualVelocity;

    private bool arrowCollision = false;
    private float powerForce = 0;

    public bool renderForces = false;

    void Start()
    {
        if (!renderForces)
        {
            this.transform.GetChild(1).GetComponent<LineRenderer>().enabled = false;
            this.transform.GetChild(2).GetComponent<LineRenderer>().enabled = false;
            this.transform.GetChild(3).GetComponent<TrailRenderer>().enabled = false;
        }

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

        actualVelocity = new MyVector3(powerForce * -GameMaster.GM.player.transform.forward.x * Mathf.Cos(launchAngle * Mathf.Deg2Rad), powerForce * Mathf.Sin(launchAngle * Mathf.Deg2Rad), -powerForce * GameMaster.GM.player.transform.forward.z * Mathf.Cos(launchAngle * Mathf.Deg2Rad));
    }

    private void Update()
    {
        if (!arrowCollision)
        {
            if (!linearDragForce)
                dragForce = Utils.CuadraticDragForce(underWater, actualVelocity, dragCoefficient, 1);
            else
                dragForce = Utils.DragForce(underWater, actualVelocity, dragCoefficient, 1);

            actualVelocity = Utils.RefreshVelocity(actualVelocity, dragForce, arrowMass, Time.deltaTime);
            this.transform.position = (Vector3)Utils.RefreshPosition(new MyVector3(this.transform.position), arrowMass, dragForce, actualVelocity, Time.deltaTime);

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
        
        if (renderForces)
        {
            ForcesRender.DrawVector((new MyVector3(0, Utils.G, 0) + dragForce) * 0.5f, ForcesRender.Vectors.RED, this.transform);
            ForcesRender.DrawVector((MyVector3)actualVelocity * 0.5f, ForcesRender.Vectors.GREEN, this.transform);
            //ForcesRender.DrawVector((MyVector3)this.transform.position * 0.5f, ForcesRender.Vectors.BLUE, this.transform);
        }
    }

    public void SetParameters(float _powerForce, float _arrowMass,  int _angle, bool _dragForce, bool _renderForces, int dragValue)
    {
        powerForce = _powerForce;
        arrowMass = _arrowMass;
        launchAngle = _angle;
        linearDragForce = _dragForce;
        renderForces = _renderForces;

        switch (dragValue)
        {
            case 0:               // Sphere
                dragCoefficient = 0.47f;
                break;
            case 1:           // HalfSphere
                dragCoefficient = 0.42f;
                break;
            case 2:                 // Cone
                dragCoefficient = 0.50f;
                break;
            case 3:                 // Cube
                dragCoefficient = 1.05f;
                break;
            case 4:           // AngledCube
                dragCoefficient = 0.80f;
                break;
            case 5:         // LongCylinder
                dragCoefficient = 0.82f;
                break;
            case 6:        // ShortCylinder
                dragCoefficient = 1.15f;
                break;
            case 7:      // StreamlinedBody
                dragCoefficient = 0.04f;
                break;
            case 8:  // StreamlinedHalfbody
                dragCoefficient = 0.09f;
                break;
        }
    }

    public void StopArrow()
    {
        actualVelocity = new MyVector3(0, 0, 0);
        arrowCollision = true;

        renderForces = false;
        this.transform.GetChild(1).GetComponent<LineRenderer>().enabled = false;
        this.transform.GetChild(2).GetComponent<LineRenderer>().enabled = false;
        this.transform.GetChild(3).GetComponent<TrailRenderer>().enabled = false;
    }
}
