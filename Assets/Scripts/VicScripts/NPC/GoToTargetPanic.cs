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
    Animator animator;
    public GoToTargetPanic(NavMeshAgent agent, Transform[] targets, Transform player, float stoppingDistance, Animator animator, Conditions[] conditions, BehaviorTree BT)
        : base(conditions, BT)
    {
        this.agent = agent;
        this.targets = targets;
        this.player = player;
        this.stoppingDistance = stoppingDistance;
        this.animator = animator;
    }

    public override void ExecuteAction()
    {
        Debug.Log("IM PANICKINNNNNG");
        timer = 0;
        targetPos = FindFarthestTarget();
        agent.speed = agent.speed * 2;
        agent.angularSpeed = agent.angularSpeed * 2;
        agent.SetDestination(targetPos.position);
        animator.SetBool("isAfraid", true);
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
            if (distance >= 25)
            {
                FinishAction(true);
            }
        }
    }

    public override void FinishAction(bool result)
    {
        animator.SetBool("isAfraid", false);
        BT.GetComponent<NpcComponent>().isAfraidNpc = false;
        agent.speed = agent.speed / 2;
        agent.angularSpeed = agent.angularSpeed / 2;
        agent.SetDestination(agent.transform.position);
        agent.stoppingDistance = Random.Range(1, 10);
        base.FinishAction(result);
    }

    public override void Interrupt()
    {
        animator.SetBool("isAfraid", false);
        BT.GetComponent<NpcComponent>().isAfraidNpc = false;
        agent.speed = agent.speed / 2;
        agent.angularSpeed = agent.angularSpeed / 2;
        agent.SetDestination(agent.transform.position);
        base.Interrupt();
    }
}