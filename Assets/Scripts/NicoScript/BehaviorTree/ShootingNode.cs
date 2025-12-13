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


    private float shootingDelay = 1.5f;

    //*******************Not in constructor*****************//

    private float lastShootTime = 0f;



    public ShootingNode(GameObject bulletPrefab, GameObject owner, Transform target, Transform firePoint, Conditions[] conditions, BehaviorTree tree) : base(conditions, tree)
    {
        this.bulletPrefab = bulletPrefab;
        this.owner = owner;
        this.target = target;
        this.firePoint = firePoint;
    }

    public override void ExecuteAction()
    {
        if (Time.time < lastShootTime)
        {
            FinishAction(false);
            return;
        }

        base.ExecuteAction();

        var agent = owner.GetComponent<NavMeshAgent>();

        if (agent)
        {
            agent.isStopped = true;
            agent.ResetPath();
        }

    }

    public override void Tick(float deltaTime)
    {
        Vector3 to = target.position - owner.transform.position;
        to.y = 0f;
        if (to.sqrMagnitude > 0.001f)
        {
            owner.transform.rotation = Quaternion.Slerp(owner.transform.rotation, Quaternion.LookRotation(to), 10f * deltaTime);
        }

        lastShootTime -= deltaTime;

        if (lastShootTime <= 0f)
        {
            var projectile = ObjectPool.objectPoolInstance.GetPooledObject(bulletPrefab);
            projectile.transform.position = firePoint.position;
            projectile.transform.rotation = firePoint.rotation;
            projectile.SetActive(true);

            lastShootTime = shootingDelay;
            FinishAction(true);
        }
    }

}
