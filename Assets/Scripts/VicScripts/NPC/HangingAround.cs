using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HangingAround : BehaviorTree
{
    Transform[] targets;
    GameObject[] POI;

    AllInterrupt allInterrupt;

    GameObject npc;
    GameObject player;
    Animator animator;

    protected override void InitializeTree()
    {
        npc = this.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");

        POI = GameObject.FindGameObjectsWithTag("POI");
        List<Transform> poiList = new List<Transform>();

        foreach (GameObject poi in POI)
        {
            poiList.Add(poi.transform);
        }

        targets = poiList.ToArray();

        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        //************************************* Conditions *************************************//
        Conditions isPanicking = new IsAfraid(npc);

        //************************************* Interrupt *************************************//
        allInterrupt = new AllInterrupt(new Conditions[] { isPanicking }, this);

        //************************************* Nodes *************************************//
        GoToTargetNPC goToRandom = new GoToTargetNPC(agent, targets, 4f, null, this);
        WaitNPC wait = new WaitNPC(Random.Range(1, 10), animator, null, this);
        GoToTargetPanic goToPanic = new GoToTargetPanic(agent, targets, player.transform, 4f, animator, null, this);

        //*************************************** Sequences *************************************//
        Sequence hangingSequence = new Sequence(new Node[] { goToRandom, wait }, null, this);
        Sequence panicSequence = new Sequence(new Node[] { goToPanic }, new Conditions[] { isPanicking }, this);
        //*************************************** Root Node *************************************//
        root = new Selector(new Node[] { panicSequence, hangingSequence }, null, this);
    }


    private void OnDisable()
    {
        if (allInterrupt != null)
            allInterrupt.Stop();
    }

    private void OnEnable()
    {
        this.enabled = true;
        InitializeTree();
        EvaluateTree();
        animator = GetComponent<Animator>();
        if (allInterrupt != null)
            allInterrupt.Start();
    }
    //private void OnDrawGizmos()
    //{

    //    Vector3 pos = transform.position;

    //    // Zone ou ils entendent le fusil
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireSphere(pos, 250f);

    //    if (player != null)
    //        Gizmos.DrawLine(pos, player.transform.position);
    //}
}
