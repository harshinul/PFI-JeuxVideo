using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CopsComponent : MonoBehaviour
{
    Transform[] targets;
    GameObject[] POI;

    public bool isAfraidCops;

    private void OnEnable()
    {
        POI = GameObject.FindGameObjectsWithTag("POI");
        List<Transform> poiList = new List<Transform>();

        foreach (GameObject poi in POI)
        {
            poiList.Add(poi.transform);
        }
        targets = poiList.ToArray();

        transform.position = targets[Random.Range(0, targets.Length)].transform.position;
    }
}
