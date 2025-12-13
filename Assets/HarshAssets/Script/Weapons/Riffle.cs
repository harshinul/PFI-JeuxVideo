using System.Collections;
using UnityEngine;

public class Riffle : Weapon
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;

    public int ammoInMagazine; //public pour debug
    public int ammoBank = 72; //public pour debug
    public int magazineSize = 24; //public pour debug
    public Riffle()
    {
        weaponName = WeaponName.Riffle;
        attackParameterName = "isRiffleShooting";
    }

    void Start()
    {
        ammoInMagazine = magazineSize;
    }

    public override void Equip()
    {
        base.Equip();
        playerAnimationComponent.EquipRiffle();
    }

    public override void Reload()
    {
        if (ammoInMagazine == magazineSize) return;

        StartCoroutine(ReloadCouroutine());

    }

    IEnumerator ReloadCouroutine()
    {
        playerAttackComponent.canReload = false; // Lock player actions
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

        playerAttackComponent.canReload = true; // Unlock player actions

    }

    public override void Attack()
    {
        if (ammoInMagazine <= 0)
            return;
        Debug.Log("Pistol Attack");
        var projectile = ObjectPool.objectPoolInstance.GetPooledObject(bulletPrefab);
        projectile.transform.position = firePoint.position;
        projectile.transform.rotation = firePoint.rotation;
        projectile.SetActive(true);
        ammoInMagazine--;

    }
}
