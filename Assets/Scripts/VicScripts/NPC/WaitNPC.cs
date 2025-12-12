using UnityEngine;

public class WaitNPC : Node
{
    float secondsToWait;
    float timer;
    Animator animator;

    public WaitNPC(float secondsToWait, Animator animator, Conditions[] condition, BehaviorTree BT) : base(condition, BT)
    {
        this.secondsToWait = secondsToWait;
        this.animator = animator;
    }

    public override void ExecuteAction()
    {
        
        timer = 0;
        base.ExecuteAction();
        animator.SetBool("isRelaxing", true);
    }
    public override void Tick(float deltaTime)
    {
        timer += deltaTime;
        if (timer >= secondsToWait)
        {
            animator.SetBool("isRelaxing", false);
            FinishAction(true);
        }
    }
}