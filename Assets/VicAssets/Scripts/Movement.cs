using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    // speed
    [SerializeField] float walkSpeed;
    [SerializeField] float rotationSpeed = 10f;

    // camera
    [SerializeField] Camera mainCamera;
    [SerializeField] LayerMask ground;

    CharacterController characControl;

    // input
    Vector2 move = Vector2.zero;
    public Vector3 direction = Vector3.zero; // public for debug

    void Start()
    {
        characControl = GetComponent<CharacterController>();
    }

    void Update()
    {
        MovementCharacter();

        if (Input.GetMouseButtonDown(1))
        {
            RotateCharacterToMouse();
            return;
        }

            RotationCharacter();
        

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
            characControl.Move(direction * walkSpeed * Time.deltaTime);
    }

    void RotationCharacter()
    {
        if (!Input.GetMouseButton(1))
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation , targetRotation, rotationSpeed* Time.deltaTime);
            return;
        }
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit hit, 100f, ground))
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

    void RotateCharacterToMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, ground))
        {
            Vector3 targetPosition = hit.point;

            Vector3 direction = targetPosition - transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);
            }
        }
    }

    public void InputMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }
}
