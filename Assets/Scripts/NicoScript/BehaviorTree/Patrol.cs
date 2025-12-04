using UnityEngine;
using UnityEngine.AI;

public class Patrol : BehaviorTree
{
    [SerializeField] Transform[] targets;
    Interrupt interrupt;
    protected override void InitializeTree()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        //************************************* Conditions *************************************//


        //************************************* Interrupt *************************************//
        interrupt = new Interrupt(null, this);


        //************************************* Nodes *************************************//
        GoToTarget goTo1 = new GoToTarget(agent, targets[0], 4f, null, this);
        GoToTarget goTo2 = new GoToTarget(agent, targets[1], 4f, null, this);
        GoToTarget goTo3 = new GoToTarget(agent, targets[2], 4f, null, this);
        GoToTarget goTo4 = new GoToTarget(agent, targets[3], 4f, null, this);
        Wait wait2 = new Wait(2, null, this);
        Wait wait4 = new Wait(4, null, this);


        //*************************************** Sequences *************************************//
        Sequence patrolSequence = new Sequence(new Node[] { goTo1, wait2, goTo2, wait2, goTo3, wait2, goTo4, wait2 }, null , this);

        //*************************************** Root Node *************************************//
        root = new Selector(new Node[] { patrolSequence, }, null, this);
    }

    private void OnDisable()
    {
        interrupt.Stop();
    }

    private void OnEnable()
    {
        if (interrupt != null)
            interrupt.Start();
    }
}