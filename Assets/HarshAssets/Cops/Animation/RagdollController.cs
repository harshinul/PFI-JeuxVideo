using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    Rigidbody[] rigidbodies;
    Collider[] colliders;
    [SerializeField] Rigidbody centerOfBody;
    Animator animator;

    void Awake()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
        colliders = OnlyRagdollColliders();
        animator = GetComponent<Animator>();

        DisableRagdoll();
    }

    Collider[] OnlyRagdollColliders()
    {
        Collider collider = GetComponent<Collider>();
        List<Collider> onlyRagdoll = new List<Collider>();
        bool found = false;

        for (int i = 0; i < colliders.Length; i++)
        {
            if (found)
            {
                onlyRagdoll.Add(colliders[i]);
            }
            else if (colliders[i] != collider)
            {
                onlyRagdoll.Add(colliders[i]);
                found = true;
            }
        }

        return onlyRagdoll.ToArray();
    }

    public void EnableRagdoll()
    {
        animator.enabled = false;

        foreach (var rb in rigidbodies)
            rb.isKinematic = false;

        foreach (var col in colliders)
            col.enabled = true;
    }

    public void EnableRagdoll(Vector3 force, Vector3 hitPoint)
    {
        EnableRagdoll();

        //Rigidbody rb = GetClosestRigidbody(hitPoint);
        Rigidbody rb = centerOfBody;
        rb.AddForce(force, ForceMode.Impulse);
    }
    Rigidbody GetClosestRigidbody(Vector3 hitPoint)
    {
        Rigidbody[] rbs = GetComponentsInChildren<Rigidbody>();

        Rigidbody closest = null;
        float minDistance = Mathf.Infinity;

        foreach (var rb in rbs)
        {
            float dist = Vector3.Distance(rb.worldCenterOfMass, hitPoint);

            if (dist < minDistance)
            {
                minDistance = dist;
                closest = rb;
            }
        }

        return closest;
    }

    void DisableRagdoll()
    {
        foreach (var rb in rigidbodies)
            rb.isKinematic = true;

        foreach (var col in colliders)
            col.enabled = false;
    }
}
