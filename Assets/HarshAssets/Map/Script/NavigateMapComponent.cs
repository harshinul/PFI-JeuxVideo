using UnityEngine;

public class NavigateMapComponent : MonoBehaviour
{
    [SerializeField] Camera mapCamera;
    [SerializeField] LayerMask groundMask;

    bool canNavigate = false;

    void Update()
    {
        if (!canNavigate) return;

        SelectDestination();
    }

    void SelectDestination()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mapCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
            {
                MapManager.Instance.SetDestination(hit.point);
                //MapManager.Instance.test(hit.point);
            }
        }
    }

    public void EnableNavigation()
    {
        canNavigate = true;
    }

    public void DisableNavigation()
    {
        canNavigate = false;
    }

}
