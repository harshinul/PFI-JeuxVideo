using System.Threading;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;

public class GoToTarget : Node
{
    Transform[] targets;
    Transform targetPos;
    float stoppingDistance;
    NavMeshAgent agent;

    public GoToTarget(NavMeshAgent agent, Transform[] targets, float stoppingDistance, Conditions[] conditions, BehaviorTree BT) : base(conditions, BT)
    {
        this.agent = agent;
        this.targets = targets;
        this.stoppingDistance = stoppingDistance;
    }

    public override void ExecuteAction()
    {
        targetPos = targets[Random.Range(0, targets.Length)];
        agent.SetDestination(targetPos.position);
        base.ExecuteAction();
    }

    public override void Tick(float deltaTime)
    {
        if ((agent.transform.position - targetPos.position).sqrMagnitude < stoppingDistance * stoppingDistance)
        {
            FinishAction(true);
        }
        else
        {
            if (!agent.SetDestination(targetPos.position))
            {
                FinishAction(false);
            }
        }
    }

    public override void FinishAction(bool result)
    {
        agent.SetDestination(agent.transform.position);
        base.FinishAction(result);
    }
}