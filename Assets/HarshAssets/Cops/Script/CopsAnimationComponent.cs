using UnityEngine;
using UnityEngine.AI;

public class CopsAnimationComponent : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;
    MovementState currentState;
    [SerializeField] float speedToWalk = 3.0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        float speed = agent.velocity.magnitude;
        //Debug.Log("Cops Speed: " + speed);

        if (speed >= speedToWalk)
        {
            currentState = MovementState.Walking;
        }
        else
        {
            currentState = MovementState.Idle;
        }

        ChangeAnimationByState();

    }

    void ChangeAnimationByState()
    {
        switch (currentState)
        {
            case MovementState.Idle:
                StopWalking();
                break;
            case MovementState.Walking:
                StartWalking();
                break;
        }
    }

    public void StartAttacking()
    {
        animator.SetTrigger("Attack");
    }

    //public void StopAttacking()
    //{
    //    animator.SetBool("isAttacking", false);
    //}

    public void StartWalking()
    {
        animator.SetBool("isWalking", true);
    }

    public void StopWalking()
    {
        animator.SetBool("isWalking", false);
    }

    public void Death()
    {
        animator.SetTrigger("Death");
    }
}
