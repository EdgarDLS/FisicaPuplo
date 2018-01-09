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

                            int tag = CheckColliderTag(environmentColliders[i]);

                            if (tag != 3)
                            {
                                arrowsStucked.Add(collider.gameObject);
                                NewTarget(tag);
                            }

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

    public void NewTarget(int tagValue)
    {
        int octopusLeg = UnityEngine.Random.Range(0, 2);
        GameObject target = null;

        if (tagValue == 0)
        {
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

            GameMaster.GM.NewShootingArea();
        }
        
        else if (tagValue == 1)
        {
            target = GameObject.Find("R_Tentacle").GetComponent<IK_FABRIK2>().target.gameObject;
            target.GetComponent<TargetRandomMovement>().newTargetPosition = manager.arrowsStucked[0].transform.position;
        }

        else if (tagValue == 2)
        {
            target = GameObject.Find("L_Tentacle").GetComponent<IK_FABRIK2>().target.gameObject;
            target.GetComponent<TargetRandomMovement>().newTargetPosition = manager.arrowsStucked[0].transform.position;
        }

        target.GetComponent<TargetRandomMovement>().itemStucked = manager.arrowsStucked[0];
        arrowsStucked.Remove(manager.arrowsStucked[0]);
        target.GetComponent<TargetRandomMovement>().arrowTarget = true;
    }

    public int CheckColliderTag(ColliderSphere colliderCollided)
    {
        if (colliderCollided.tag.Equals("Octopus"))
            return 0;
        else if (colliderCollided.tag.Equals("R_Leg"))
            return 1;
        else if (colliderCollided.tag.Equals("L_Leg"))
            return 2;

        return 3;
    }
}
