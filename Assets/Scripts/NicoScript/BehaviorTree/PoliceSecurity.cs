using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceSecurity : BehaviorTree
{
    Transform[] targets;
    GameObject[] POI;

    AllInterrupt allInterrupt;

    GameObject cops;
    GameObject player;

    CopsAnimationComponent animComp;
    protected override void InitializeTree()
    {
        this.enabled = true;
        cops = this.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");

        POI = GameObject.FindGameObjectsWithTag("POI");
        List<Transform> poiList = new List<Transform>();

        foreach (GameObject poi in POI)
        {
            poiList.Add(poi.transform);
        }

        targets = poiList.ToArray();

        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        animComp = GetComponent<CopsAnimationComponent>();

        //************************************* Conditions *************************************//

        Conditions legalCondition = new IsIllegal(player);
        Conditions legalConditionInversed = new IsIllegal(player,true);
        Conditions isPanicking = new IsAfraidCops(cops);

        //************************************* Interrupt *************************************//
        allInterrupt = new AllInterrupt(new Conditions[] { legalCondition,legalConditionInversed }, this);

        //************************************* Nodes *************************************//
        GoToTarget goTo = new GoToTarget(agent, targets, 4f, null, this);
        GoToTargetPanicCops panic = new GoToTargetPanicCops(agent, targets, player.transform, 15f, null, this);
        Wait wait = new Wait(1, null, this);

        //*************************************** Sequences *************************************//
        Sequence patrolSequence = new Sequence(new Node[] { goTo, wait },null, this);
        Sequence panicSequence = new Sequence(new Node[] { panic }, new Conditions[] {isPanicking}, this);
        //*************************************** Root Node *************************************//
        root = new Selector(new Node[] { panicSequence, patrolSequence}, null, this);
    }

    private void OnDisable()
    {
        if(allInterrupt != null)
            allInterrupt.Stop();
    }

    private void OnEnable()
    {
        this.enabled = true;
        InitializeTree();
        EvaluateTree();
        if (allInterrupt != null)
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