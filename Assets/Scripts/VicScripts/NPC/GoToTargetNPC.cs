using UnityEngine.AI;
using UnityEngine;

public class GoToTargetNPC : Node
{
    Transform[] targets;
    Transform targetPos;
    float stoppingDistance;
    NavMeshAgent agent;

    public GoToTargetNPC(NavMeshAgent agent, Transform[] targets, float stoppingDistance, Conditions[] conditions, BehaviorTree BT)
        : base(conditions, BT)
    {
        this.agent = agent;
        this.targets = targets;
        this.stoppingDistance = stoppingDistance;
    }

    public override void ExecuteAction()
    {
        targetPos = targets[Random.Range(0, targets.Length)];
        agent.SetDestination(targetPos.position);
        agent.stoppingDistance = Random.Range(1, 10);
        base.ExecuteAction();
    }

    public override void Tick(float deltaTime)
    {
        if ((agent.transform.position - targetPos.position).sqrMagnitude < stoppingDistance * stoppingDistance)
        {
            FinishAction(true);
        }
    }

    public override void FinishAction(bool result)
    {
        agent.SetDestination(agent.transform.position);
        base.FinishAction(result);
    }

}