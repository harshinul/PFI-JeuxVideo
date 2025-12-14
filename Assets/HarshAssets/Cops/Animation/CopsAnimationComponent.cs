using UnityEngine;

public class CopsAnimationComponent : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void StartAttack()
    {
        animator.SetBool("isAttacking", true);
    }

    public void StopAttack()
    {
        animator.SetBool("isAttacking", false);
    }

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
