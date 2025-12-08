using UnityEngine;
using UnityEngine.AI;

public class PoliceMelee : BehaviorTree
{
    [SerializeField] Transform[] targets;

    AllInterrupt allInterrupt;

    GameObject player;

    Transform playerTransform;

    bool isInterruptActive = false;
    protected override void InitializeTree()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        //************************************* Conditions *************************************//
        Conditions meleeCondition = new WithinRange(agent.transform, player, 30f);
        Conditions chaseCondition = new WithinRange(agent.transform, player, 200f);
        Conditions chaseConditionInversed = new WithinRange(agent.transform, player, 200f, true);

        Conditions crimeCondition = new IsIllegal(player);


        //************************************* Interrupt *************************************//
        allInterrupt = new AllInterrupt( new Conditions[] {crimeCondition,chaseCondition}, this);

        //************************************* Nodes *************************************//
        GoToTarget goTo1 = new GoToTarget(agent, targets[0], 4f, null, this);
        GoToTarget goTo2 = new GoToTarget(agent, targets[1], 4f, null, this);
        GoToTarget goTo3 = new GoToTarget(agent, targets[2], 4f, null, this);
        GoToTarget goTo4 = new GoToTarget(agent, targets[3], 4f, null, this);
        GoToTarget chasePlayer = new GoToTarget(agent, player.transform, 30f, null, this);
        Wait wait = new Wait(1, null, this);


        //*************************************** Sequences *************************************//
        Sequence patrolSequence = new Sequence(new Node[] { goTo1, wait, goTo2, wait, goTo3, wait, goTo4, wait }, new Conditions[] { chaseConditionInversed }, this);
        Sequence meleeSequence = new Sequence(new Node[] { chasePlayer }, new Conditions[] { crimeCondition }, this);
        //*************************************** Root Node *************************************//
        root = new Selector(new Node[] {patrolSequence, meleeSequence }, null, this);
    }

    private void OnDisable()
    {
        if(allInterrupt != null)
            allInterrupt.Stop();
    }

    private void OnEnable()
    {
        if(allInterrupt != null)
            allInterrupt.Start();
    }

    private void OnDrawGizmos()
    {

        Vector3 pos = transform.position;

        // Melee zone
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pos, 200f);

        if (player != null)
            Gizmos.DrawLine(pos, player.transform.position);
    }
}