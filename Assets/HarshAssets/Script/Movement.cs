using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    // speed
    [SerializeField] float baseWalkSpeed;
    float walkSpeed; //public pour changer la vitesse en fontion de l'arme
    [SerializeField] float baseRunSpeed;
    public float currentSpeed; //public pour debug
    [SerializeField] float rotationSpeed = 10f;

    // gravity
    [SerializeField] float gravity = -9.81f;
    Vector3 velocity;

    // camera
    [SerializeField] Camera mainCamera;
    [SerializeField] LayerMask ground;


    //components
    CharacterController characControl;
    PlayerAnimationComponent animComp;
    PlayerAttackComponent attackComp;

    //bool
    bool wantsToRun = false;
    public bool canRun = true;
    public bool isWanted = false;

    // input
    Vector2 move = Vector2.zero;
    Vector3 direction = Vector3.zero;

    // state
    public MovementState movementState = MovementState.Idle;

    void Start()
    {
        characControl = GetComponent<CharacterController>();
        animComp = GetComponent<PlayerAnimationComponent>();
        attackComp = GetComponent<PlayerAttackComponent>();
        walkSpeed = baseWalkSpeed;
        currentSpeed = walkSpeed;

    }

    void Update()
    {
        MovementCharacter();

        RotationCharacter();

        HandlePlayerLogic();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isWanted = true;
            Debug.Log("ATTENTION: Player is now WANTED!");
        }
    }

    void MovementCharacter()
    {
        direction = mainCamera.transform.forward * move.y + mainCamera.transform.right * move.x;

        if (direction.magnitude > 0)
        {
            direction = new Vector3(direction.x, 0, direction.z);
            direction = direction.normalized;
        }

        if (characControl != null)
        {
            if (characControl.isGrounded && velocity.y < 0)
            {

                velocity.y = -1f; // Garde le joueur collé au sol
            }

            velocity.y += gravity * Time.deltaTime; //Gravité tire vers le bas

            characControl.Move(velocity * Time.deltaTime);
            characControl.Move((direction * currentSpeed + velocity) * Time.deltaTime);

            // gestion etat de deplacement
            if (direction.magnitude == 0)// pas de deplacement
            {
                movementState = MovementState.Idle;
            }
            else
            {
                if (wantsToRun)
                {
                    movementState = MovementState.Running;
                }
                else
                {
                    movementState = MovementState.Walking;
                }
            }
        }
    }

    void RotationCharacter()
    {
        if (!Input.GetMouseButton(1) || movementState == MovementState.Running) // rotation vers la direction de deplacement
        {
            if (direction == Vector3.zero)
                return;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation , targetRotation, rotationSpeed* Time.deltaTime);
            return;
        }

        if(movementState == MovementState.Running) // rotation bloquée en courant
            return;

        // rotation vers la souris
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit hit, 10000000f, ground))
        {
            Vector3 targetPosition = hit.point;

            Vector3 direction = targetPosition - transform.position;
            direction.y = 0f;

            if(direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);
            }
        }
    }

    void HandlePlayerLogic()// gestion d'animation, de m�canique, de visuel 
    {
        switch(movementState)
        {
            case MovementState.Idle:
                animComp.StopWalking();
                animComp.StopRunning();
                attackComp.ShowWeapon();
                break;
            case MovementState.Walking:
                animComp.StartWalking();
                animComp.StopRunning();
                attackComp.ShowWeapon();
                currentSpeed = walkSpeed;
                break;
            case MovementState.Running:
                animComp.StopWalking();
                animComp.StartRunning();
                attackComp.HideWeapon();
                currentSpeed = baseRunSpeed;
                break;
        }
    }

    public void SetWalkSpeed(float? newSpeed)
    {
        walkSpeed = newSpeed ?? baseWalkSpeed;
    }

    public void InputMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void InputRun(InputAction.CallbackContext context)
    {
        if (!canRun) return;
        if (context.performed)
        {
            wantsToRun = true;
            
        }
        else if (context.canceled)
        {
            wantsToRun = false;
        }
    }
}

public enum MovementState
{
    Idle,
    Walking,
    Running
}
