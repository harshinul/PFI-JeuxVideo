using UnityEngine;
using UnityEngine.InputSystem;

public class MovementVic : MonoBehaviour
{
    // speed
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    public float currentSpeed; //public pour debug
    [SerializeField] float rotationSpeed = 10f;
    bool wantsToRun = false;

    // camera
    [SerializeField] Camera mainCamera;
    [SerializeField] LayerMask ground;


    //components
    CharacterController characControl;
    PlayerAnimationComponent animComp;

    // input
    Vector2 move = Vector2.zero;
    Vector3 direction = Vector3.zero;

    // state
    public MovementState movementState = MovementState.Idle;

    void Start()
    {
        characControl = GetComponent<CharacterController>();
        animComp = GetComponent<PlayerAnimationComponent>();
        currentSpeed = walkSpeed;

    }

    void Update()
    {
        MovementCharacter();

        RotationCharacter();

        //HandleAnimation();

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
            characControl.Move(direction * currentSpeed * Time.deltaTime);

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
        if (!Input.GetMouseButton(1)) // rotation vers la direction de deplacement
        {
            if (direction == Vector3.zero)
                return;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation , targetRotation, rotationSpeed* Time.deltaTime);
            return;
        }

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

    //void HandleAnimation()
    //{
    //    switch(movementState)
    //    {
    //        case MovementState.Idle:
    //            animComp.StopWalking();
    //            animComp.StopRunning();
    //            break;
    //        case MovementState.Walking:
    //            animComp.StartWalking();
    //            animComp.StopRunning();
    //            break;
    //        case MovementState.Running:
    //            animComp.StopWalking();
    //            animComp.StartRunning();
    //            break;
    //    }
    //}

    public void InputMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void InputRun(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            wantsToRun = true;
            currentSpeed = runSpeed;
        }
        else if (context.canceled)
        {
            wantsToRun = false;
            currentSpeed = walkSpeed;
        }
    }
}

public enum MovementState
{
    Idle,
    Walking,
    Running
}
