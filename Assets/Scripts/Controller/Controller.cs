using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public GameObject optionCanvas;
    public Text angleValue;
    public Text massValue;
    public Text dragText;
    public Text dragValue;
    private bool showOptions = false;

    private GameObject mainCamera;
    private GameObject floatingCamera;

    [Space]
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

    [Space]
    public float arrowMass = 1;
    public int angle = 45;
    public bool linearDragForce = false;
    public bool renderForces = false;
    public int dragCoeffValue;

    private void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        floatingCamera = GameObject.Find("FloatingCamera");

        launchPosition = GameObject.Find("launchPosition").transform;

        floatingCamera.SetActive(false);
        //optionCanvas.SetActive(false);
    }

    void Update ()
    {
        if (!showOptions)
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
                if (powerVariable < 100)
                {
                    powerVariable += Time.deltaTime * 20;
                    powerText.text = powerVariable.ToString("F0") + " %";
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                GameObject projectile = Instantiate(throwingObject, launchPosition.position, launchPosition.rotation) as GameObject;
                projectile.GetComponentInChildren<ColliderSphere>().AddColliderToManager();
                SetLaunchingParameters(projectile);

                powerText.text = "0 %";
                powerVariable = 0;
            }
        }

        // Scroll Up
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
        {
            if (angle < 80)
            {
                angle++;
                angleValue.text = angle.ToString();
            } 
        }
        // Scroll Down
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {
            if (angle > 0)
            {
                angle--;
                angleValue.text = angle.ToString();
            }
        }


        // Options
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            showOptions = !showOptions;

            Animator anim = optionCanvas.GetComponent<Animator>();

            if (showOptions)
                anim.Play("inMenu");
            else
                anim.Play("outMenu");

        }
        else if(Input.GetKeyDown(KeyCode.F1))
        {
            mainCamera.SetActive(true);
            floatingCamera.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            mainCamera.SetActive(false);
            floatingCamera.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.F5))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void SetLaunchingParameters(GameObject projectile)
    {
        projectile.GetComponent<Arrow>().SetParameters(powerVariable, arrowMass, angle, linearDragForce, renderForces, dragCoeffValue);
    }

    public void ModifyMass(float newValue)
    {
        arrowMass += newValue;

        massValue.text = arrowMass.ToString();
    }

    public void ModifyDragForce()
    {
        linearDragForce = !linearDragForce;

        if (linearDragForce)
            dragText.text = "Linear";
        else
            dragText.text = "Quadratic";
    }

    public void ModifyRenderForce()
    {
        renderForces = !renderForces;
    }

    public void ModifyDragCoefficient(int _dragCoeffValue)
    {
        float dragCoefficient = 0;

        switch (_dragCoeffValue)
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

        dragCoeffValue = _dragCoeffValue;
        dragValue.text = dragCoefficient.ToString();
    }
}
