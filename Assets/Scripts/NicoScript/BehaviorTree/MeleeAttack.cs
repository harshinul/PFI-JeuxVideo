using UnityEngine;
using UnityEngine.AI;

public class MeleeAttack : Node
{
    GameObject attackObject;
    GameObject owner;
    Transform target;

    private float attackDuration = 1f;
    private float attackTimer = 0f;
    private bool isAttacking = false;
    private float attackRange;
    private NavMeshAgent agent;

    public float damage = 10f;

    public MeleeAttack(GameObject owner, GameObject attackObject, Transform target, float attackRange, NavMeshAgent agent, Conditions[] conditions, BehaviorTree BT) : base(conditions, BT)
    {
        this.attackObject = attackObject;
        this.owner = owner;
        this.target = target;
        this.attackRange = attackRange;
        this.agent = agent;
    }

    override public void ExecuteAction()
    {

        float d = Vector3.Distance(owner.transform.position, target.position);

        isAttacking = true;
        attackTimer = attackDuration;
        var agent = owner.GetComponent<NavMeshAgent>();
        if (agent)
        {
            agent.isStopped = true;
            agent.ResetPath();
        }
        base.ExecuteAction();

        attackObject.SetActive(true);
    }

    public override void Tick(float deltaTime)
    {
        if (!isAttacking)
        {
            FinishAction(false);
            return;
        }
        attackTimer -= deltaTime;

        Vector3 DistanceToTarget = target.position - owner.transform.position;
        DistanceToTarget.y = 0f;
        if (DistanceToTarget.sqrMagnitude > 0.001f)
            owner.transform.rotation = Quaternion.Slerp(owner.transform.rotation, Quaternion.LookRotation(DistanceToTarget), 10f * deltaTime);

        if (attackTimer <= 0f)
        {
            isAttacking = false;
            attackObject.SetActive(false);
            FinishAction(true);
        }
    }
    public override void FinishAction(bool result)
    {
        isAttacking = false;
        attackObject.SetActive(false);

        agent.isStopped = false;

        base.FinishAction(result);
    }
}
