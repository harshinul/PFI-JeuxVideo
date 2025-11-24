using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] float walkSpeed;
    [SerializeField] GameObject perso;
    [SerializeField] Camera mainCamera;
    [SerializeField] LayerMask ground;

    CharacterController characControl;

    Vector2 move = Vector2.zero;

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

        if (Input.GetMouseButton(1))
            RotationCharacter();
    }

    void MovementCharacter()
    {
        Vector3 direction = characControl.transform.forward * move.y + characControl.transform.right * move.x;

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
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit hit, 10000000f, ground))
        {
            Vector3 targetPosition = hit.point;

            Vector3 direction = targetPosition - transform.position;
            direction.y = 0f;

            if(direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                perso.transform.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);
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
                perso.transform.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);
            }
        }
    }

    public void InputMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }
}
