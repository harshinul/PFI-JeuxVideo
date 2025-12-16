using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class BaseballBat : Weapon
{
    [SerializeField] float attackRadius = 1.5f;
    [SerializeField] Transform[] attackPoint;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float attackDamage = 100f;
    [SerializeField] float attackForce = 500f;

    [SerializeField] Collider playerCollider;
    Collider weaponCollider;
    public bool canDealDamage = false; //public pour debug
    [SerializeField] float delayBeforeCanDealDamage = 1.0f;
    [SerializeField] float attackDuration = 2.2f;

    public BaseballBat()
    {
        weaponName = WeaponName.BaseballBat;
        attackParameterName = "isMeleeAttacking";
    }

    private void Start()
    {
        weaponCollider = GetComponent<Collider>();
        if (weaponCollider != null && playerCollider != null)
            Physics.IgnoreCollision(weaponCollider, playerCollider);
    }


    public override void Equip()
    {
        base.Equip();
        playerAnimationComponent.EquipBaseballBat();
        playerAttackComponent.ammoDisplay.enabled = false;
    }

    public override void Reload()
    {

    }
    public override void Attack()
    {
        playerAnimationComponent.StartAttack(attackParameterName);
        playerAttackComponent.canAttack = false;
        playerAttackComponent.canSwitchWeapon = false;
        movement.canRun = false;
        StartCoroutine(StopAttackAfterDelay(attackDuration));
    }
    IEnumerator StopAttackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delayBeforeCanDealDamage);
        AttackHit();
        yield return new WaitForSeconds(delay - delayBeforeCanDealDamage);
        playerAnimationComponent.StopAttack(attackParameterName);
        playerAttackComponent.canAttack = true;
        playerAttackComponent.canSwitchWeapon = true;
        movement.canRun = true;
    }


    void AttackHit()
    {
        List<Collider> hits = new List<Collider>();

        foreach (Transform attackPoint in attackPoint)
        {
            Collider[] pointHits = Physics.OverlapSphere(
                attackPoint.position,
                attackRadius,
                enemyLayer
            );
            hits.AddRange(pointHits);
        }

        foreach (Collider hit in hits)
        {
            Vector3 direction = (hit.transform.position - transform.position).normalized;
            hit.gameObject.GetComponent<NPCHealthComponent>()?.TakeDamage(attackDamage, direction * attackForce);
            Debug.Log("Baseball Bat hit NPC via OverlapSphere");
        }
    }

    //void OnDrawGizmosSelected()
    //{
    //    if (attackPoint == null) return;

    //    Gizmos.color = Color.red;
    //    //Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    //}


}
