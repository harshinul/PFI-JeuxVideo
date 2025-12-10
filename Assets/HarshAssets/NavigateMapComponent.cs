using UnityEngine;

public class NavigateMapComponent : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] LayerMask groundMask;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
            {
                //MapManager.Instance.SetDestination(hit.point);
                MapManager.Instance.test(hit.point);
            }
        }
    }

}
