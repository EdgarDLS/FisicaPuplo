using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float velocity;
    public float rotationVelocity;

    [Space]
    public Vector3 launchVelocity;

    [Space]
    public GameObject throwingObject;

    private Transform launchPosition;

    private void Start()
    {
        launchPosition = GameObject.Find("launchPosition").transform;
    }

    void Update ()
    {
        // Input Movement
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.position += transform.forward * Time.deltaTime * -velocity;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            this.transform.position += transform.forward * Time.deltaTime * velocity;
        }

        if (Input.GetKey(KeyCode.A))
        {
            this.transform.eulerAngles += new Vector3(0, -rotationVelocity * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            this.transform.eulerAngles += new Vector3(0, rotationVelocity * Time.deltaTime, 0);
        }


        // Input Attack
        if (Input.GetMouseButtonDown(0))
        {
            GameObject projectile = Instantiate(throwingObject, launchPosition.position, launchPosition.rotation) as GameObject;
            projectile.GetComponentInChildren<ColliderSphere>().AddColliderToManager();
            //SetLaunchingParameters(projectile);  
        }
	}

    private void SetLaunchingParameters(GameObject projectile)
    {
        Vector3 newVelocity;

        newVelocity = new Vector3(launchVelocity.x * Mathf.Cos(this.transform.rotation.y) - launchVelocity.y * Mathf.Sin(this.transform.rotation.y),
            launchVelocity.y * Mathf.Cos(this.transform.rotation.y) + launchVelocity.x * Mathf.Sin(this.transform.rotation.y), launchVelocity.z); 

        projectile.GetComponent<Arrow>().SetInitialVelocity(newVelocity);
    }
}
