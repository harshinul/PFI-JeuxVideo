using UnityEngine;

public class NavigateMapComponent : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] LayerMask locationMask;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, locationMask))
            {
                hit.collider.GetComponent<cubeTile>()?.SetDestination();
            }
        }
    }
}
