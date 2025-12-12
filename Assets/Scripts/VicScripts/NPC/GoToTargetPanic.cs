using UnityEngine.AI;
using UnityEngine;

public class GoToTargetPanic : Node
{
    Transform[] targets;
    Transform targetPos;
    float stoppingDistance;
    NavMeshAgent agent;
    Transform player;
    float timer;

    public GoToTargetPanic(NavMeshAgent agent, Transform[] targets, Transform player, float stoppingDistance, Conditions[] conditions, BehaviorTree BT)
        : base(conditions, BT)
    {
        this.agent = agent;
        this.targets = targets;
        this.player = player;
        this.stoppingDistance = stoppingDistance;
    }

    public override void ExecuteAction()
    {
        Debug.Log("IM PANICKINNNNNG");
        timer = 0;
        targetPos = FindFarthestTarget();
        agent.speed = agent.speed * 2;
        agent.SetDestination(targetPos.position);
        BT.Animator.SetBool("isAfraid", true);
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
        if(timer >= 10)
        {
            if (distance > 250)
            {
                FinishAction(true);
            }
        }
    }

    public override void FinishAction(bool result)
    {
        BT.Animator.SetBool("isAfraid", false);
        BT.GetComponent<NpcComponent>().isAfraid = false;
        agent.SetDestination(agent.transform.position);
        agent.stoppingDistance = Random.Range(1, 10);
        base.FinishAction(result);
    }

    public override void Interrupt()
    {
        BT.Animator.SetBool("isAfraid", false);
        BT.GetComponent<NpcComponent>().isAfraid = false;
        agent.SetDestination(agent.transform.position);
        base.Interrupt();
    }
}