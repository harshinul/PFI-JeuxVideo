using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    Rigidbody[] rigidbodies;
    Collider[] colliders;
    Collider bodyCollider;
    [SerializeField] Rigidbody centerOfBody;
    Animator animator;

    public bool isRagdollActive = false;

    void Awake()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
        colliders = OnlyRagdollColliders();
        animator = GetComponent<Animator>();

        DisableRagdoll();
    }

    private void OnEnable()
    {
        DisableRagdoll();
    }


    private void Update()
    {
        if(isRagdollActive)
        {
            EnableRagdoll(Vector3.zero);
        }
    }
    Collider[] OnlyRagdollColliders()
    {
        bodyCollider = GetComponent<Collider>();
        List<Collider> onlyRagdoll = new List<Collider>();
        bool found = false;

        for (int i = 0; i < colliders.Length; i++)
        {
            if (found)
            {
                onlyRagdoll.Add(colliders[i]);
            }
            else if (colliders[i] != bodyCollider)
            {
                onlyRagdoll.Add(colliders[i]);
                found = true;
            }
        }

        return onlyRagdoll.ToArray();
    }


    public void EnableRagdoll(Vector3 force)
    {
        animator.enabled = false;
        bodyCollider.enabled = false;

        foreach (var rbe in rigidbodies)
            rbe.isKinematic = false;

        foreach (var col in colliders)
            col.enabled = true;

        int randomIndex = Random.Range(0, 2);

        if (randomIndex == 0)
            force = Vector3.zero;
        Rigidbody rb = centerOfBody;
        rb.AddForce(force, ForceMode.Impulse);
        //bodyCollider.enabled = false;
    }

    void DisableRagdoll()
    {
        animator.enabled = true;
        bodyCollider.enabled = true;
        foreach (var rb in rigidbodies)
            rb.isKinematic = true;

        foreach (var col in colliders)
            col.enabled = false;
    }
}
