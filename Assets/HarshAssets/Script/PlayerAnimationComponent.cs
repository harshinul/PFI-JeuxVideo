using UnityEngine;

public class PlayerAnimationComponent : MonoBehaviour
{
    Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();   
    }

    public void StartWalking()
    {
        anim.SetBool("isWalking", true);
    }

    public void StopWalking()
    {
        anim.SetBool("isWalking", false);
    }

    public void StartRunning()
    {
        anim.SetBool("isRunning", true);
    }

    public void StopRunning()
    {
        anim.SetBool("isRunning", false);
    }


}
