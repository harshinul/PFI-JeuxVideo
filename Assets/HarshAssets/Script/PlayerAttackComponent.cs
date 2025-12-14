using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackComponent : MonoBehaviour
{
    //GameObject Weapon Models
    [SerializeField] List<Weapon> weapons;

    //UI
    public TextMeshProUGUI ammoDisplay;

    // Current Weapon
    public int currentWeaponIndex = 0; //public pour debug
    public List<WeaponName> weaponList = new List<WeaponName> { WeaponName.None, WeaponName.BaseballBat, WeaponName.Pistol, WeaponName.Riffle };
    Weapon currentWeapon;
    float elapsedTime;
    float attackDelay;

    //Components
    PlayerAnimationComponent playerAnimationComponent;
    Movement movement;

    // bool
    public bool canAttack;
    public bool wantsToAttack;
    public bool canSwitchWeapon;
    public bool canReload;
    public bool isReloading;


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        // Get Components
        playerAnimationComponent = GetComponent<PlayerAnimationComponent>();
        movement = GetComponent<Movement>();
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

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        elapsedTime += Time.deltaTime;
        if (currentWeapon != null && movement.movementState != MovementState.Running && wantsToAttack && canAttack && !isReloading && elapsedTime > attackDelay)
        {
            elapsedTime = 0f;
            currentWeapon.Attack();
        }
    }

    void Reload()
    {
        if(!canReload) return;
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
                    weapon.gameObject.SetActive(false);
                }
            }
        }

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

    public void FirstWeaponsAction()
    {
        currentWeaponIndex = 0;
        playerAnimationComponent.EquipNone();

        foreach (var weapon in weapons) 
        {
            weapon.gameObject.SetActive(false); // Hide all weapons
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
