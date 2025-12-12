using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NpcComponent : MonoBehaviour
{
    Transform[] targets;
    GameObject[] POI;

    GameObject player;

    float distance;

    public bool isAfraid;

    private void OnEnable()
    {
        isAfraid = false;
        POI = GameObject.FindGameObjectsWithTag("POI");
        List<Transform> poiList = new List<Transform>();

        foreach(GameObject poi in POI)
        {
            poiList.Add(poi.transform);
        }

        targets = poiList.ToArray();

        transform.position = targets[Random.Range(0, targets.Length)].transform.position;
    }

    public void OnClickAfraid()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= 100)
        {
            isAfraid = true;
        }
    }
}
