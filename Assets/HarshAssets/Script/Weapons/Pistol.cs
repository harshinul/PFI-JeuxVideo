using System.Collections;
using TMPro;
using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;


    int ammoInMagazine; 
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
        playerAttackComponent.ammoDisplay.enabled = true;
        playerAttackComponent.ammoDisplay.text = ammoInMagazine + " / " + ammoBank;
    }

    public override void Reload()
    {
        if(ammoInMagazine == magazineSize) return;

        StartCoroutine(ReloadCouroutine());

    }

    IEnumerator ReloadCouroutine()
    {
        playerAttackComponent.canReload = false;
        movement.canRun = false;
        yield return new WaitForSeconds(2f);

        if (ammoBank >= 12) // full reload
        {
            ammoBank -= magazineSize - ammoInMagazine;
            ammoInMagazine = magazineSize;

        }
        else // partial reload
        {
            ammoInMagazine += ammoBank;
            ammoBank = 0;
        }

        playerAttackComponent.ammoDisplay.text = ammoInMagazine + " / " + ammoBank;
        playerAttackComponent.canReload = true;
        movement.canRun = true;

    }

    public override void Attack()
    {
        if (ammoInMagazine <= 0)
            return;
        Debug.Log("Pistol Attack");
        var projectile = ObjectPool.objectPoolInstance.GetPooledObject(bulletPrefab);
        projectile.transform.position = firePoint.position;
        projectile.transform.rotation = firePoint.rotation;
        //projectile.transform.position = Vector3.zero;
        //projectile.transform.rotation = Quaternion.identity;
        projectile.SetActive(true);
        ammoInMagazine--;
        playerAttackComponent.ammoDisplay.text = ammoInMagazine + " / " + ammoBank;

    }

}
