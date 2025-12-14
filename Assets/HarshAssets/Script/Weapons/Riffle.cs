using System.Collections;
using TMPro;
using UnityEngine;

public class Riffle : Weapon
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;

    int ammoInMagazine; 
    public int ammoBank = 72; //public pour debug
    public int magazineSize = 24; //public pour debug
    float reloadSpeed = 2f;

    public Riffle()
    {
        weaponName = WeaponName.Riffle;
        attackParameterName = "isRiffleShooting";
        ammoInMagazine = magazineSize;
    }

    void Start()
    {
        ammoInMagazine = magazineSize;
    }

    public override void Equip()
    {
        base.Equip();
        playerAnimationComponent.EquipRiffle();
        playerAttackComponent.ammoDisplay.enabled = true;
        playerAttackComponent.ammoDisplay.text = ammoInMagazine + " / " + ammoBank;
    }

    public override void Reload()
    {
        if (ammoInMagazine == magazineSize) return;

        StartCoroutine(ReloadCouroutine());

        playerAttackComponent.ReloadDisplay(reloadSpeed);

    }

    IEnumerator ReloadCouroutine()
    {
        playerAttackComponent.canReload = false; // Lock player actions
        movement.canRun = false;
        yield return new WaitForSeconds(reloadSpeed);

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
        playerAttackComponent.canReload = true; // Unlock player actions
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
        projectile.SetActive(true);
        ammoInMagazine--;
        playerAttackComponent.ammoDisplay.text = ammoInMagazine + " / " + ammoBank;
    }
}
