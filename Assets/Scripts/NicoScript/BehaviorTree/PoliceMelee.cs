using UnityEngine;
using UnityEngine.AI;

public class PoliceMelee : BehaviorTree
{
    [SerializeField] Transform[] targets;

    Interrupt Interrupt;

    GameObject player;

    Transform playerTransform;
    protected override void InitializeTree()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        //************************************* Conditions *************************************//
        Conditions meleeCondition = new WithinRange(agent.transform, player, 30f);
        

        //************************************* Interrupt *************************************//
        Interrupt = new Interrupt(new Conditions[] { meleeCondition, }, this);
        


        //************************************* Nodes *************************************//
        GoToTarget goTo1 = new GoToTarget(agent, targets[0], 4f, null, this);
        GoToTarget goTo2 = new GoToTarget(agent, targets[1], 4f, null, this);
        GoToTarget goTo3 = new GoToTarget(agent, targets[2], 4f, null, this);
        GoToTarget goTo4 = new GoToTarget(agent, targets[3], 4f, null, this);
        GoToTarget chasePlayer = new GoToTarget(agent, player.transform, 2f, null, this);
        Wait wait2 = new Wait(2, null, this);
        Wait wait4 = new Wait(2, null, this);


        //*************************************** Sequences *************************************//
        Sequence patrolSequence = new Sequence(new Node[] { goTo1, wait2, goTo2, wait2, goTo3, wait2, goTo4, wait2 }, null , this);
        Sequence meleeSequence = new Sequence(new Node[] { chasePlayer }, null, this);
        //*************************************** Root Node *************************************//
        root = new Selector(new Node[] { patrolSequence }, null, this);
    }

    private void OnDisable()
    {
        if(Interrupt != null)
            Interrupt.Stop();
    }

    private void OnEnable()
    {
        if(Interrupt != null)
            Interrupt.Start();
    }
    private void OnDrawGizmos()
    {

        Vector3 pos = transform.position;

        // Melee zone
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pos, 20f);

        if (player != null)
            Gizmos.DrawLine(pos, player.transform.position);
    }
}