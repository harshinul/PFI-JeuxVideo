using UnityEngine;
using UnityEngine.AI;

public class HangingAround : BehaviorTree
{
    [SerializeField] Transform[] targets;

    AllInterrupt allInterrupt;

    GameObject player;

    protected override void InitializeTree()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        //************************************* Conditions *************************************//
        Conditions afraidCondition = new WithinRange(agent.transform, player, 250f);
        //Conditions isPanicking = new 



        //************************************* Interrupt *************************************//
        allInterrupt = new AllInterrupt(new Conditions[] {  }, this);

        //************************************* Nodes *************************************//
        GoToTargetNPC goToRandom = new GoToTargetNPC(agent, targets, 4f, null, this);
        WaitNPC wait = new WaitNPC(Random.Range(1, 10), null, this);

        //*************************************** Sequences *************************************//
        Sequence hangingSequence = new Sequence(new Node[] { goToRandom, wait }, null, this);
        //Sequence panicSequence = new Sequence(new Node[] {  }, new Conditions[] { legalCondition }, this);
        //*************************************** Root Node *************************************//
        root = new Selector(new Node[] { hangingSequence }, null, this);
    }

    private void OnDisable()
    {
        if (allInterrupt != null)
            allInterrupt.Stop();
    }

    private void OnEnable()
    {
        if (allInterrupt != null)
            allInterrupt.Start();
    }

    private void OnDrawGizmos()
    {

        Vector3 pos = transform.position;

        // Zone ou ils entendent le fusil
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pos, 250f);

        if (player != null)
            Gizmos.DrawLine(pos, player.transform.position);
    }
}
