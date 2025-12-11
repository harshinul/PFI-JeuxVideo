using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;
using UnityEngine.AI;

public class ShootingNode : Node
{
    //***************Add to Shooting Node*****************//
    GameObject bulletPrefab;

    GameObject owner;

    Transform target;

    Transform firePoint;
    
    private float shootingDelay = 0.5f;

    private ParticleSystem shootingParticle;

    private TrailRenderer bulletTrail;

    //*******************Not in constructor*****************//
    private bool addBulletSpread = false;

    private Vector3 BulletSpread  = new Vector3(0.1f,0.1f,0.1f);

    private float lastShootTime;



    public ShootingNode(GameObject bulletPrefab,GameObject owner, Transform target, Transform firePoint, ParticleSystem shootingParticle, TrailRenderer bulletTrail, Conditions[] conditions, BehaviorTree BT) : base(conditions, BT)
    {
        this.bulletPrefab = bulletPrefab;
        this.owner = owner;
        this.target = target;
        this.firePoint = firePoint;
        this.shootingParticle = shootingParticle;
        this.bulletTrail = bulletTrail;
    }

    public override void ExecuteAction()
    {
        if (Time.deltaTime > shootingDelay)
        {
            FinishAction(false);
            return;
        }

        lastShootTime = Time.time + shootingDelay;

        base.ExecuteAction();

        var projectile = ObjectPool.objectPoolInstance.GetPooledObject(bulletPrefab);

        var agent = owner.GetComponent<NavMeshAgent>();
        if (agent)
        {
            agent.isStopped = true;
            agent.ResetPath();
        }

    }
}
