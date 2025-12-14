using System.Threading;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;

public class ChasePlayer : Node
{
    Transform target;
    float stoppingDistance;
    NavMeshAgent agent;
    CopsAnimationComponent anim;

    public ChasePlayer(CopsAnimationComponent anim, NavMeshAgent agent, Transform target, float stoppingDistance, Conditions[] conditions, BehaviorTree BT) : base(conditions, BT)
    {
        this.anim = anim;
        this.agent = agent;
        this.target = target;
        this.stoppingDistance = stoppingDistance;
    }

    public override void ExecuteAction()
    {
        agent.SetDestination(target.position);
        base.ExecuteAction();
    }

    public override void Tick(float deltaTime)
    {
        if (Vector3.Distance(agent.transform.position, target.position) <= stoppingDistance)
        {
            FinishAction(true);
        }
        else
        {
            agent.SetDestination(target.position);
        }
    }

    public override void FinishAction(bool result)
    {
        agent.SetDestination(agent.transform.position);
        base.FinishAction(result);
    }

    public override void Interrupt()
    {
        agent.SetDestination(agent.transform.position);
        base.Interrupt();
    }
}
