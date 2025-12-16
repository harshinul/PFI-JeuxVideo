using System.Collections;
using TMPro;
using UnityEngine;

public class Riffle : Weapon
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;

    int ammoInMagazine; 
    int ammoBank = 72;
    int magazineSize = 24;
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
        SFXManager.Instance.PlaySFX(equipAudioClip, transform, equipAudioVolume);
    }

    public override void Reload()
    {
        if (ammoInMagazine == magazineSize) return;

        StartCoroutine(ReloadCouroutine());

    }

    public override void AddAmmo()
    {
        ammoBank += magazineSize;

        if(isEquipped)
        {
            playerAttackComponent.ammoDisplay.text = ammoInMagazine + " / " + ammoBank;
        }
    }

    IEnumerator ReloadCouroutine()
    {

        playerAttackComponent.canReload = false;
        movement.canRun = false;
        playerAttackComponent.canSwitchWeapon = false;
        playerAttackComponent.isReloading = true;
        StartCoroutine(playerAttackComponent.reloadUiScript.FillReloadBar(reloadTime));
        SFXManager.Instance.PlaySFX(reloadAudioClip, transform, reloadAudioVolume);
        yield return new WaitForSeconds(reloadTime);

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
        playerAttackComponent.canSwitchWeapon = true;
        playerAttackComponent.isReloading = false;

    }

    public override void Attack()
    {
        if (ammoInMagazine <= 0)
            return;
        SFXManager.Instance.PlaySFX(attackAudioClip, transform, attackAudioVolume);
        var projectile = ObjectPool.objectPoolInstance.GetPooledObject(bulletPrefab);
        projectile.transform.position = firePoint.position;
        projectile.transform.rotation = firePoint.rotation;
        projectile.SetActive(true);
        ammoInMagazine--;
        playerAttackComponent.ammoDisplay.text = ammoInMagazine + " / " + ammoBank;
    }
}
