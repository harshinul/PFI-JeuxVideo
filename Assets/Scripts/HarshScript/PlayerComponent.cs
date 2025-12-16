using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerComponent : MonoBehaviour
{
    Animator anim;

    [SerializeField] float speed = 5f;
    [SerializeField] float rotationSpeed = 10f; // Vitesse de rotation

    CharacterController characterController;
    [SerializeField] Transform bulletSpawnPoint; // Point de spawn de la balle
    [SerializeField] float shootingSpeed = 1f; // Préfab de la balle
    Vector2 lastLookDirection = Vector2.zero; // Dernière direction de la caméra
    Vector2 direction = Vector2.zero;
    Vector2 lookDirection = Vector2.up; // Direction de la caméra
    bool isMoving = false;
    bool isLooking = false;
    bool isShooting = false;
    float elapseTime = 0;

    public bool isWanted = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        elapseTime += Time.deltaTime;
        if (isMoving || isLooking)
        {
            Movement();
        }
        if (isShooting && elapseTime >= shootingSpeed)
        {
            //Shoot();
            elapseTime = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isWanted = true;
            Debug.Log("ATTENTION: Player is now WANTED!");
        }
    }

    public void Movement()
    {
        Vector3 moveDirection = new Vector3(direction.x, 0, direction.y).normalized;
        Vector3 lookOrientation = new Vector3(lookDirection.x, 0, lookDirection.y).normalized;
        if (isLooking)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookOrientation);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);//https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Quaternion.Slerp.html
        }
        if (isMoving)
        {
            characterController.Move(moveDirection * speed * Time.deltaTime);
        }

    }

    public void ChangeAnimationForDirection()
    {
        if (lookDirection.y == 0 && lookDirection.x == 0)
        {
            lookDirection = lastLookDirection;
        }

        float angle = Vector3.SignedAngle(
            new Vector3(lookDirection.x, 0, lookDirection.y),
            new Vector3(direction.x, 0, direction.y),
            Vector3.up
        );

        anim.SetFloat("PlayerAngle", angle);

        if (angle < -30 && angle > -150)
        {
            anim.SetBool("WalkingUp", false);
            anim.SetBool("WalkingDown", false);
            anim.SetBool("WalkingLeft", true);
            anim.SetBool("WalkingRight", false);
        }
        else if (angle > 30 && angle < 150)
        {
            anim.SetBool("WalkingUp", false);
            anim.SetBool("WalkingDown", false);
            anim.SetBool("WalkingLeft", false);
            anim.SetBool("WalkingRight", true);
        }
        else if (((angle >= 0 && angle < 30) ||(angle > -30 && angle <= 0)) && isMoving)
        {
            anim.SetBool("WalkingUp", true);
            anim.SetBool("WalkingDown", false);
            anim.SetBool("WalkingLeft", false);
            anim.SetBool("WalkingRight", false);
        }
        else if (angle < -130 || angle > 130)
        {
            anim.SetBool("WalkingUp", false);
            anim.SetBool("WalkingDown", true);
            anim.SetBool("WalkingLeft", false);
            anim.SetBool("WalkingRight", false);
        }
        else
        {
            anim.SetBool("WalkingUp", false);
            anim.SetBool("WalkingDown", false);
            anim.SetBool("WalkingLeft", false);
            anim.SetBool("WalkingRight", false);
        }

    }

    public void Move(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            direction = ctx.ReadValue<Vector2>();
            isMoving = true;
        }
        else if (ctx.canceled)
        {
            direction = Vector2.zero;
            isMoving = false;
        }
    }

    public void Look(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            lookDirection = ctx.ReadValue<Vector2>();
            lastLookDirection = lookDirection;
            isLooking = true;
        }
        else if (ctx.canceled)
        {
            lookDirection = Vector2.zero;
            isLooking = false;
        }
    }

    public void Shoot(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            isShooting = true;
        }
        else if (ctx.canceled)
        {
            isShooting = false;
        }
    }
}
