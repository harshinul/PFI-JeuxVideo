using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public bool isEquipped = false;
    [SerializeField] float walkingSpeed;
    [SerializeField] float attackDelay;
    [SerializeField] protected float reloadTime = 2f;
    public WeaponName weaponName;
    protected PlayerAnimationComponent playerAnimationComponent;
    protected PlayerAttackComponent playerAttackComponent;
    protected Movement movement;
    protected string attackParameterName = "";
    [SerializeField] protected AudioClip attackAudioClip;
    [SerializeField] protected float attackAudioVolume = 1.0f;
    [SerializeField] protected AudioClip equipAudioClip;
    [SerializeField] protected float equipAudioVolume = 1.0f;
    [SerializeField] protected AudioClip reloadAudioClip;
    [SerializeField] protected float reloadAudioVolume = 1.0f;


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
