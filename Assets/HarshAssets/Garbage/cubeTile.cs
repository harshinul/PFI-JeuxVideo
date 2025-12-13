using UnityEngine;

public class cubeTile : MonoBehaviour
{
    [SerializeField] int index;

    public void SetDestination()
    {
        GraphMap.Instance.SetDestinationTile(index);
    }
}
