using UnityEngine.AI;
using UnityEngine;

public class GoToTargetPanicCops : Node
{
    Transform[] targets;
    Transform targetPos;
    float stoppingDistance;
    NavMeshAgent agent;
    Transform player;
    float timer;
    public GoToTargetPanicCops(NavMeshAgent agent, Transform[] targets, Transform player, float stoppingDistance, Conditions[] conditions, BehaviorTree BT)
        : base(conditions, BT)
    {
        this.agent = agent;
        this.targets = targets;
        this.player = player;
        this.stoppingDistance = stoppingDistance;
    }

    public override void ExecuteAction()
    {
        timer = 0;
        targetPos = FindFarthestTarget();
       // targetPos.position = new Vector3(-173.57f, 62.9f, -90.6f);
        agent.speed = agent.speed * 2;
        agent.angularSpeed = agent.angularSpeed * 2;
        agent.SetDestination(targetPos.position);
        base.ExecuteAction();
    }

    Transform FindFarthestTarget()
    {
        Transform farthest = null;
        float maxDist = float.MinValue;

        foreach (Transform transf in targets)
        {
            float sqrDist = (transf.position - player.position).sqrMagnitude;

            if (sqrDist > maxDist)
            {
                maxDist = sqrDist;
                farthest = transf;
            }
        }

        return farthest;
    }

    public override void Tick(float deltaTime)
    {
        timer += deltaTime;
        float distance = Vector3.Distance(player.transform.position, agent.transform.position);
        if (timer >= 10)
        {
            if (distance > 250)
            {
                FinishAction(true);
            }
        }
    }

    public override void FinishAction(bool result)
    {
        GameManager.Instance.isAfraidCops = false;
        agent.speed = agent.speed / 2;
        agent.angularSpeed = agent.angularSpeed / 2;
        agent.SetDestination(agent.transform.position);
        agent.stoppingDistance = Random.Range(1, 10);
        base.FinishAction(result);
    }

    public override void Interrupt()
    {
        GameManager.Instance.isAfraidCops = false;
        agent.speed = agent.speed / 2;
        agent.angularSpeed = agent.angularSpeed / 2;
        agent.SetDestination(agent.transform.position);
        base.Interrupt();
    }
}