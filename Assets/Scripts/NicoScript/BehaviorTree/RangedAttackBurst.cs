using UnityEngine;
using UnityEngine.AI;

public class RangedAttackBurst : Node
{
    //***************Add to Constructor*****************//
    GameObject bulletPrefab;

    GameObject owner;

    Transform target;

    Transform firePoint;

    private int bulletsPerBurst = 3;



    //*******************Not in constructor*****************//
    private float timeBetweenShots = 0.1f;

    private float burstCooldown = 0.8f;

    private int bulletsLeftInBurst;
    private float shotTimer;
    private float burstCooldownTimer;
    private bool isBursting;

    public RangedAttackBurst(GameObject bulletPrefab, GameObject owner, Transform target, Transform firePoint,int bulletPerBurst, Conditions[] conditions, BehaviorTree tree) : base(conditions, tree)
    {
        this.bulletPrefab = bulletPrefab;
        this.owner = owner;
        this.target = target;
        this.firePoint = firePoint;
        this.bulletsPerBurst = bulletPerBurst;
    }

    public override void ExecuteAction()
    {
        if (burstCooldownTimer > 0f)
        {
            FinishAction(false);
            return;
        }

        base.ExecuteAction();

        bulletsLeftInBurst = bulletsPerBurst;
        shotTimer = 0f;
        isBursting = true;

        var agent = owner.GetComponent<NavMeshAgent>();
        if (agent)
        {
            agent.isStopped = true;
            agent.ResetPath();
        }
    }


    public override void Tick(float deltaTime)
    {
        Vector3 rotationToTarget = target.position - owner.transform.position;
        rotationToTarget.y = 0f;

        if (rotationToTarget.sqrMagnitude > 0.001f)
        {
            owner.transform.rotation = Quaternion.Slerp(
                owner.transform.rotation,
                Quaternion.LookRotation(rotationToTarget),
                10f * deltaTime
            );
        }
        if (!isBursting)
        {
            burstCooldownTimer -= deltaTime;
            return;
        }

        shotTimer -= deltaTime;

        if (shotTimer <= 0f && bulletsLeftInBurst > 0)
        {
            FireProjectile();
            bulletsLeftInBurst--;
            shotTimer = timeBetweenShots;
        }
        if (bulletsLeftInBurst <= 0)
        {
            isBursting = false;
            burstCooldownTimer = burstCooldown;
            FinishAction(true);
        }
    }

    private void FireProjectile()
    {
        Vector3 dirToTarget = (target.position - firePoint.position).normalized;
        dirToTarget.y += 0.05f;

        var projectile = ObjectPool.objectPoolInstance.GetPooledObject(bulletPrefab);
        projectile.transform.position = firePoint.position;
        projectile.transform.rotation = Quaternion.LookRotation(dirToTarget);
        projectile.SetActive(true);
    }
}
