using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NpcComponent : MonoBehaviour
{
    Transform[] targets;
    GameObject[] POI;

    GameObject npc;
    GameObject player;

    float distance;

    public bool isAfraid;

    private void OnEnable()
    {
        isAfraid = false;
        npc = this.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
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
        if (npc != null)
        {
            distance = Vector3.Distance(npc.transform.position, player.transform.position);
        }

        if (distance <= 250)
        {
            isAfraid = true;
        }
    }
}
