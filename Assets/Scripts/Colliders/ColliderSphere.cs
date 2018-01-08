using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderSphere : MonoBehaviour
{
    public float radius;

    public void AddColliderToManager()
    {
        CollisionManager.manager.AddProjectileCollider(this);
    }

    public float GetRadius()
    {
        return radius;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
