using UnityEngine;
using UnityEngine.AI;

public class PoliceMelee : BehaviorTree
{
    Interrupt interrupt;

    [SerializeField] GameObject meleeObject;

    GameObject player;

    Transform playerTransform;
    protected override void InitializeTree()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        //************************************* Conditions *************************************//
        Conditions meleeCondition = new WithinRange(agent.transform, player, 15f);


        Conditions chaseConditionInversed = new WithinRange(agent.transform, player, 200f, true);

        //************************************* Interrupt *************************************//
        interrupt = new Interrupt(new Conditions[] { meleeCondition }, this);

        //************************************* Nodes *************************************//
        GoToPlayer chasePlayer = new GoToPlayer(agent, player.transform, 15f, null, this);
        Wait wait = new Wait(1, null, this);

        MeleeAttack meleeAttack = new MeleeAttack(this.gameObject, meleeObject, player.transform, 15f, agent, new Conditions[] { meleeCondition }, this);

        //*************************************** Root Node *************************************//
        root = new Selector(new Node[] { meleeAttack, chasePlayer }, null, this);
    }

    private void OnDisable()
    {
        if (interrupt != null)
            interrupt.Stop();
    }

    private void OnEnable()
    {
        if (interrupt != null)
            interrupt.Start();
    }
}
