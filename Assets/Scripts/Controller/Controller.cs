using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public float velocity;
    public float rotationVelocity;

    [Space]
    public Vector3 launchVelocity;

    [Space]
    public GameObject throwingObject;

    private Transform launchPosition;

    [Space]
    private float powerVariable = 0;
    public Text powerText;

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
        if (Input.GetMouseButton(0))
        {
            powerVariable += Time.deltaTime * 20;
            powerText.text = powerVariable.ToString("F0") + " %";
        }

        else if(Input.GetMouseButtonUp(0))
        {
            GameObject projectile = Instantiate(throwingObject, launchPosition.position, launchPosition.rotation) as GameObject;
            projectile.GetComponentInChildren<ColliderSphere>().AddColliderToManager();
            SetLaunchingParameters(projectile);  

            powerText.text = "0 %";
            powerVariable = 0;
        }
    }

    private void SetLaunchingParameters(GameObject projectile)
    {
        projectile.GetComponent<Arrow>().SetInitialVelocity(powerVariable);
    }
}
