using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.GridLayoutGroup;

public class PoliceMelee : BehaviorTree
{
    Interrupt interrupt;

    [SerializeField] GameObject meleeObject;
    [SerializeField] float damageAmount = 10f;

    GameObject player;
    HealthComponent playerhealthComponent;

    CopsAnimationComponent animComp;
    [SerializeField] AudioClip meleeHitSound;
    [SerializeField] float meleeHitVolume = 1f;

    // transformer les 3 en 15

    protected override void InitializeTree()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerhealthComponent = player.GetComponent<HealthComponent>();

        animComp = GetComponent<CopsAnimationComponent>();
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        //************************************* Conditions *************************************//
        Conditions meleeConditionCloseEnough = new WithinRange(agent.transform, player, 2);


        Conditions chaseConditionInversed = new WithinRange(agent.transform, player, 200f, true);

        //************************************* Interrupt *************************************//
        interrupt = new Interrupt(new Conditions[] { meleeConditionCloseEnough }, this);

        //************************************* Nodes *************************************//
        GoToPlayer chasePlayer = new GoToPlayer(agent, player.transform, 2, null, this);
        Wait wait = new Wait(1, null, this);

        MeleeAttack meleeAttack = new MeleeAttack(animComp, this.gameObject, meleeObject, player.transform, 2, agent, new Conditions[] { meleeConditionCloseEnough }, this);

        //*************************************** Root Node *************************************//
        root = new Selector(new Node[] { meleeAttack, chasePlayer }, null, this);
    }

    void AttackPlayer() // animation event
    {
        if(Vector3.Distance(transform.position, player.transform.position) < 3f)
        {
            playerhealthComponent.TakeDamage(damageAmount);
            SFXManager.Instance.PlaySFX(meleeHitSound, transform, meleeHitVolume);
        }
            
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
