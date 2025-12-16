using UnityEngine;
using UnityEngine.AI;

public class PoliceRanged : BehaviorTree
{

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] AudioClip shootSound;
    [SerializeField] float shootVolume = 1.0f;
    [SerializeField] Transform firePoint;

    GameObject player;
    GameObject centerOfBodyPlayer;

    float angleVision = 150f;
    protected override void InitializeTree()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        centerOfBodyPlayer = GameObject.FindGameObjectWithTag("CenterOfBodyPlayer");

        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        LayerMask mask = LayerMask.GetMask("Default");

        //************************************* Conditions *************************************//
        Conditions shootingCondition = new WithinRange(agent.transform, centerOfBodyPlayer, 15);
        Conditions rangedConditionInversed = new WithinRange(agent.transform, centerOfBodyPlayer, 15,true);
        Conditions chaseInterruptCondition = new WithinRange(agent.transform, centerOfBodyPlayer, 10);
        Conditions hasVision = new HasVision(agent.transform, player, angleVision, mask);

        //************************************* Interrupt *************************************//
        Interrupt interrupt = new Interrupt(new Conditions[] {rangedConditionInversed,chaseInterruptCondition, hasVision }, this);

        //************************************* Nodes *************************************//
        //GoToPlayer chasePlayer = new GoToPlayer(agent, player.transform, 50f, null, this);
        //ShootingNode rangedAttack = new ShootingNode( player.transform, transform.Find("FirePoint"), 200f, 20f, new Conditions[] {shootingCondition}, this);
        GoToPlayer chasePlayer = new GoToPlayer(agent, centerOfBodyPlayer.transform, 3f, null, this);
        ShootingNode rangedAttack = new ShootingNode(shootSound,shootVolume,bulletPrefab,this.gameObject, centerOfBodyPlayer.transform,firePoint, new Conditions[] {shootingCondition,hasVision}, this);

        //*************************************** Sequences *************************************//
        //*************************************** Root Node *************************************//
        root = new Selector(new Node[] { rangedAttack ,chasePlayer }, null, this);
    }
    private void OnDrawGizmos()
    {

        Vector3 pos = transform.position;

        // Melee zone
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pos, 15);

        if (centerOfBodyPlayer != null)
            Gizmos.DrawLine(pos, centerOfBodyPlayer.transform.position);
    }
}
