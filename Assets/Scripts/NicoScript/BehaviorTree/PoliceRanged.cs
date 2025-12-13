using UnityEngine;
using UnityEngine.AI;

public class PoliceRanged : BehaviorTree
{
    [SerializeField] GameObject bulletPrefab;

    [SerializeField] Transform firePoint;

    CopsAnimationComponent animationComponent;

    GameObject player;

    float angleVision = 150f;
    protected override void InitializeTree()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        animationComponent = GetComponent<CopsAnimationComponent>();
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        LayerMask mask = LayerMask.GetMask("Default");

        //************************************* Conditions *************************************//
        Conditions shootingCondition = new WithinRange(agent.transform, player, 300f);
        Conditions rangedConditionInversed = new WithinRange(agent.transform, player, 300f,true);
        Conditions chaseInterruptCondition = new WithinRange(agent.transform, player, 200f);
        Conditions hasVision = new HasVision(agent.transform, player, angleVision,mask);

        //************************************* Interrupt *************************************//
        Interrupt interrupt = new Interrupt(new Conditions[] {rangedConditionInversed,chaseInterruptCondition, hasVision }, this);

        //************************************* Nodes *************************************//
        GoToPlayer chasePlayer = new GoToPlayer(agent, player.transform, 10f, null, this);
        ShootingNode rangedAttack = new ShootingNode(bulletPrefab,this.gameObject, player.transform,firePoint, new Conditions[] {shootingCondition,hasVision}, this);

        //*************************************** Sequences *************************************//
        //*************************************** Root Node *************************************//
        root = new Selector(new Node[] { rangedAttack ,chasePlayer }, null, this);
    }
    private void OnDrawGizmos()
    {

        Vector3 pos = transform.position;

        // Melee zone
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pos, 300f);

        if (player != null)
            Gizmos.DrawLine(pos, player.transform.position);
    }
}
