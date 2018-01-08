using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRandomMovement : MonoBehaviour
{
    public float NEW_POSITION = 0.8f;

    public GameObject TargetMovement;

    public Vector3 newTargetPosition;

    private float timer = 999999;

    public bool arrowTarget = false;

    public GameObject itemStucked;

    [Space]
    public GameObject tentacleFinalPoint;

    // Update is called once per frame
    void Update ()
    {
        if (timer > NEW_POSITION && !arrowTarget)
        {
            NewPosition();

            timer = 0;
        }

        transform.position = Vector3.Lerp(transform.position, newTargetPosition, Time.deltaTime);

        timer += Time.deltaTime;

        if (arrowTarget && (transform.position - newTargetPosition).magnitude < 0.2f)
        {
            arrowTarget = !arrowTarget;
            CollisionManager.manager.arrowsStucked.Remove(itemStucked);

            itemStucked.gameObject.transform.parent.transform.parent = tentacleFinalPoint.transform;

            Destroy(itemStucked.transform.parent.gameObject, 5);
        }
	}

    private void NewPosition()
    {
        newTargetPosition = TargetMovement.transform.position + Random.insideUnitSphere * 10;

        NEW_POSITION = Random.Range(0.75f, 0.85f);
    }
}
