using UnityEngine;

public class WaitNPC : Node
{
    float secondsToWait;
    float timer;

    public WaitNPC(float secondsToWait, Conditions[] condition, BehaviorTree BT) : base(condition, BT)
    {
        this.secondsToWait = secondsToWait;
    }

    public override void ExecuteAction()
    {
        
        timer = 0;
        base.ExecuteAction();
        BT.Animator.SetBool("isRelaxing", true);
    }
    public override void Tick(float deltaTime)
    {
        timer += deltaTime;
        if (timer >= secondsToWait)
        {
            BT.Animator.SetBool("isRelaxing", false);
            FinishAction(true);
        }
    }
}