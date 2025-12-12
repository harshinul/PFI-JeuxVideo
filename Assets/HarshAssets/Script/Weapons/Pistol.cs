using System.Collections;
using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] GameObject bulletPrefab;

    public int ammoInMagazine; //public pour debug
    public int ammoBank = 36; //public pour debug
    public int magazineSize = 12; //public pour debug

    public Pistol()
    {
        weaponName = WeaponName.Pistol;
        attackParameterName = "isPistolShooting";
        ammoInMagazine = magazineSize;
    }

    void Start()
    {
        ammoInMagazine = magazineSize;
    }

    public override void Equip()
    {
        base.Equip();
        playerAnimationComponent.EquipPistol();
    }

    public override void Reload()
    {
        if(ammoInMagazine == magazineSize) return;

        StartCoroutine(ReloadCouroutine());

    }

    IEnumerator ReloadCouroutine()
    {
        if (ammoBank >= 12)
        {
            playerAttackComponent.canReload = false;
            playerAttackComponent.canAttack = false;
            yield return new WaitForSeconds(2f);

            ammoBank -= magazineSize - ammoInMagazine;
            ammoInMagazine = magazineSize;

        }
        else
        {
            playerAttackComponent.canReload = false;
            playerAttackComponent.canAttack = false;
            yield return new WaitForSeconds(2f);
            ammoInMagazine += ammoBank;
            ammoBank = 0;
        }

        playerAttackComponent.canReload = true;
        playerAttackComponent.canAttack = true;

    }

    public override void Attack()
    {
        if (ammoInMagazine <= 0)
            return;
        Debug.Log("Pistol Attack");
        ammoInMagazine--;

    }

}
