using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public bool isEquipped = false;
    [SerializeField] float walkingSpeed;
    [SerializeField] float attackDelay;
    public WeaponName weaponName;
    protected PlayerAnimationComponent playerAnimationComponent;
    protected PlayerAttackComponent playerAttackComponent;
    protected Movement movement;
    protected string attackParameterName = "";


    public virtual void Equip() 
    { 
        movement.SetWalkSpeed(walkingSpeed); 
        playerAttackComponent.SetAttackDelay(attackDelay);
        isEquipped = true;
    }

    public virtual void AddAmmo() { }
    public abstract void Attack();
    public virtual void Reload() { }

    void Awake()
    {
        playerAnimationComponent = GetComponentInParent<PlayerAnimationComponent>();
        playerAttackComponent = GetComponentInParent<PlayerAttackComponent>();
        movement = GetComponentInParent<Movement>();
    }

}
