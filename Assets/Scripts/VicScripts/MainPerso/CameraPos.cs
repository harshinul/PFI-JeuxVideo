using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;      
    [SerializeField] Vector3 offset = new Vector3(0, 10, -10);
    [SerializeField] float followSpeed = 10f;

    void LateUpdate()
    {
        if (!target) return;

        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(45f, -45f, 0f);
    }
}
