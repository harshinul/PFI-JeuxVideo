using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CopsComponent : MonoBehaviour
{
    Transform[] targets;
    GameObject[] POI;

    Vector3 copsPos;
    GameObject player;

    public bool isAfraid;

    float distance;
    private void OnEnable()
    {
        isAfraid = false;
        POI = GameObject.FindGameObjectsWithTag("POI");
        List<Transform> poiList = new List<Transform>();

        foreach (GameObject poi in POI)
        {
            poiList.Add(poi.transform);
        }
        targets = poiList.ToArray();

        transform.position = targets[Random.Range(0, targets.Length)].transform.position;
    }

    public void OnClickAfraid()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        copsPos = transform.position;
        Debug.Log(copsPos);
        distance = Vector3.Distance(copsPos, player.transform.position);

        if (distance <= 100)
        {
            GetComponent<Movement>().isWanted = true;
            isAfraid = true;
        }
    }
}
