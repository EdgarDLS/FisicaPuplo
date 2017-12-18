using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TargetFollowing : MonoBehaviour {

    Transform myObject;
    float zValue;

    public float velocity = 1;

    void Start()
    {
        myObject = this.transform;
    }

    void Update ()
    {
        if (Input.GetKey(KeyCode.Space))
            zValue = 0.5f;
        else if (Input.GetKey(KeyCode.LeftShift))
            zValue = -0.5f;
        else
            zValue = 0;

        if (Input.GetKey(KeyCode.KeypadPlus))
            velocity += 0.2f;
        else if (Input.GetKey(KeyCode.KeypadMinus))
            velocity -= 0.2f;

        if (Input.GetKey(KeyCode.Keypad1))
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        else if (Input.GetKey(KeyCode.Keypad2))
            SceneManager.LoadScene(1, LoadSceneMode.Single);


        Vector3 movementInput = new Vector3(Input.GetAxis("Horizontal"), zValue, Input.GetAxis("Vertical"));

        myObject.position += (movementInput * velocity * Time.deltaTime);
	}
}
