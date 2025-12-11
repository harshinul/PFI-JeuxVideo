using UnityEngine;
using UnityEngine.AI;

public class PoliceRanged : BehaviorTree
{
    GameObject bulletPrefab;

    GameObject player;

    protected override void InitializeTree()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        //************************************* Conditions *************************************//
        Conditions shootingCondition = new WithinRange(agent.transform, player, 300f);
        Conditions RangedConditionInversed = new WithinRange(agent.transform, player, 300f,true);

        //************************************* Interrupt *************************************//
        Interrupt interrupt = new Interrupt(new Conditions[] {RangedConditionInversed }, this);

        //************************************* Nodes *************************************//
        GoToPlayer chasePlayer = new GoToPlayer(agent, player.transform, 50f, null, this);
        ShootingNode rangedAttack = new ShootingNode( player.transform, transform.Find("FirePoint"), 200f, 20f, new Conditions[] {shootingCondition}, this);

        //*************************************** Sequences *************************************//
        Sequence rangedSequence = new Sequence(new Node[] {  }, new Conditions[] {  }, this);
        //*************************************** Root Node *************************************//
        root = new Selector(new Node[] { chasePlayer }, null, this);
    }

}
