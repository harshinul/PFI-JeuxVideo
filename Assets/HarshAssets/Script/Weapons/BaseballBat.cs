using System.Collections;
using UnityEngine;

public class BaseballBat : Weapon
{
    public BaseballBat()
    {
        weaponName = WeaponName.BaseballBat;
        attackParameterName = "isMeleeAttacking";
    }

    public override void Equip()
    {
        base.Equip();
        playerAnimationComponent.EquipBaseballBat();
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
        StartCoroutine(StopAttackAfterDelay(2f));
    }
    IEnumerator StopAttackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay/2);
        // Here you can add logic to detect hit on enemy if needed
        yield return new WaitForSeconds(delay/2);
        playerAnimationComponent.StopAttack(attackParameterName);
        playerAttackComponent.canAttack = true;
        playerAttackComponent.canSwitchWeapon = true;
        movement.canRun = true;
    }
}
