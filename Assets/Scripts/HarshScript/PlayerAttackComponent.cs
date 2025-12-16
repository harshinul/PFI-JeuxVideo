using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerAttackComponent : MonoBehaviour
{
    //GameObject Weapon Models
    [SerializeField] List<Weapon> weapons;

    //UI
    public TextMeshProUGUI ammoDisplay;
    [SerializeField] Image[] weaponIcons;

    // Current Weapon
    int currentWeaponIndex = 0; //public pour debug
    public List<WeaponName> weaponList = new List<WeaponName> { WeaponName.None, WeaponName.BaseballBat, WeaponName.Pistol, WeaponName.Riffle };
    Weapon currentWeapon;
    float elapsedTime;
    float attackDelay;

    //Components
    PlayerAnimationComponent playerAnimationComponent;
    Movement movement;
    public ReloadUiScript reloadUiScript;

    // bool
    public bool canAttack;
    public bool wantsToAttack;
    public bool canSwitchWeapon;
    public bool canReload;
    public bool isReloading;


    void Awake()
    {
        // Get Components
        playerAnimationComponent = GetComponent<PlayerAnimationComponent>();
        movement = GetComponent<Movement>();
        reloadUiScript = GetComponent<ReloadUiScript>();
    }

    void Start()
    {
        elapsedTime = 0f;
        attackDelay = 0.5f;
        currentWeaponIndex = 0;
        canAttack = true;
        canReload = true;
        canSwitchWeapon = true;
        isReloading = false;
        ammoDisplay.enabled = false;
        FirstWeaponsAction();
    }

    void Update()
    {
        Attack();
    }

    void Attack()
    {
        elapsedTime += Time.deltaTime;
        if (!wantsToAttack)
            return;
        if (currentWeapon != null && movement.movementState != MovementState.Running && wantsToAttack && canAttack && !isReloading && elapsedTime > attackDelay)
        {
            elapsedTime = 0f;
            currentWeapon.Attack();

            GameManager.Instance.AfraidEveryone();
        }
    }

    void Reload()
    {
        if (!canReload || movement.movementState == MovementState.Running) return;
        if (currentWeapon != null)
        {
            currentWeapon.Reload();
        }
    }


    void ChangeWeapon()
    {
        HideWeapon();
        currentWeapon = null; // No weapon

        if (currentWeaponIndex == weaponList.Count - 1)
        {
            currentWeaponIndex = 0; // Reset to no weapon
            ammoDisplay.enabled = false;
            movement.SetWalkSpeed(null);
            playerAnimationComponent.EquipNone();
        }
        else
        {
            currentWeaponIndex++;
            WeaponName selectedWeapon = weaponList[currentWeaponIndex];
            foreach (var weapon in weapons)
            {
                if (weapon.weaponName == selectedWeapon)
                {
                    currentWeapon = weapon; // Set current weapon
                    weapon.gameObject.SetActive(true);
                    currentWeapon.Equip();
                }
                else
                {
                    weapon.isEquipped = false;
                    weapon.gameObject.SetActive(false);
                }
            }

            GameManager.Instance.AfraidEveryone();
        }

        ChangeWeaponIcon();

    }

    public void SetAttackDelay(float newDelay)
    {
        attackDelay = newDelay;
    }

    public void ShowWeapon()
    {
        if (currentWeapon != null)
        {
            currentWeapon.gameObject.SetActive(true);
        }
    }

    public void HideWeapon()
    {
        if (currentWeapon != null)
        {
            currentWeapon.gameObject.SetActive(false);
        }
    }

    void ChangeWeaponIcon()
    {
        for (int i = 0; i < weaponIcons.Length; i++)
        {
            if (i == currentWeaponIndex - 1)
            {
                weaponIcons[i].enabled = true; // Show current weapon icon
            }
            else
            {
                weaponIcons[i].enabled = false; // Hide other weapon icons
            }
        }

    }

    public void FirstWeaponsAction()
    {
        currentWeaponIndex = 0;
        playerAnimationComponent.EquipNone();

        foreach (var weapon in weapons)
        {
            weapon.gameObject.SetActive(false); // Hide all weapons
        }

        foreach (var icon in weaponIcons)
        {
            icon.enabled = false; // Hide all weapon icons
        }
    }

    public void AddAmmo()
    {
        foreach (var weapon in weapons)
        {

                weapon.AddAmmo();
            
        }
    }

    public void InputChangeWeapon(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!canSwitchWeapon) return;
            ChangeWeapon();
        }
    }

    public void InputAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            wantsToAttack = true;
        }
        else if (context.canceled)
        {
            wantsToAttack = false;
        }
    }

    public void InputReload(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (currentWeapon != null)
            {
                Reload();
            }
        }
    }
}

public enum WeaponName
{
    None,
    BaseballBat,
    Pistol,
    Riffle
}
