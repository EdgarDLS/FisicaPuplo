using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollisionManager : MonoBehaviour
{
    public static CollisionManager manager;

    public List<ColliderSphere> environmentColliders;
    public List<ColliderSphere> projectileColliders;
    public List<GameObject> arrowsStucked;

    void Awake()
    {
        if (manager != null)
            GameObject.Destroy(manager);
        else
            manager = this;
    }

    void Update ()
    {
        if (projectileColliders != null)
        {
            foreach (ColliderSphere collider in projectileColliders)
            {
                for (int i = 0; i < environmentColliders.Count; i++)
                {
                    try
                    {
                        if (collider.GetRadius() + environmentColliders[i].GetRadius() > (collider.transform.position - environmentColliders[i].transform.position).magnitude)
                        {
                            collider.transform.parent.GetComponent<Arrow>().StopArrow();
                            arrowsStucked.Add(collider.gameObject);
                            NewTarget();

                            projectileColliders.Remove(collider);                  
                        }
                    }
                    catch (Exception e) { }
                }
            }
        }
	}

    public void AddProjectileCollider(ColliderSphere newProjectile)
    {
        projectileColliders.Add(newProjectile);
    }

    public void NewTarget()
    {
        int octopusLeg = UnityEngine.Random.Range(0, 2);
        GameObject target;

        if (octopusLeg == 0)
        {
            target = GameObject.Find("L_Tentacle").GetComponent<IK_FABRIK2>().target.gameObject;
            target.GetComponent<TargetRandomMovement>().newTargetPosition = manager.arrowsStucked[0].transform.position;
        }
        else
        {
            target = GameObject.Find("R_Tentacle").GetComponent<IK_FABRIK2>().target.gameObject;
            target.GetComponent<TargetRandomMovement>().newTargetPosition = manager.arrowsStucked[0].transform.position;
        }

        target.GetComponent<TargetRandomMovement>().itemStucked = manager.arrowsStucked[0];
        target.GetComponent<TargetRandomMovement>().arrowTarget = true;
    }
}
