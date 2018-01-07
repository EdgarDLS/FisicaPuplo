using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRandomMovement : MonoBehaviour
{
    public readonly float NEW_POSITION = 0.8f;

    public GameObject TargetMovement;

    private Vector3 newTargetPosition;

    private float timer = 999999;

	// Update is called once per frame
	void Update ()
    {

        if (timer > NEW_POSITION)
        {
            NewPosition();

            timer = 0;
        }

        transform.position = Vector3.Lerp(transform.position, newTargetPosition, Time.deltaTime);

        timer += Time.deltaTime;
	}

    private void NewPosition()
    {
        newTargetPosition = TargetMovement.transform.position + Random.insideUnitSphere * 10;
    }
}
