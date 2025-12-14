using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NpcComponent : MonoBehaviour
{
    Transform[] targets;
    GameObject[] POI;

    Vector3 npcPos;
    GameObject player;

    float distance;

    public bool isAfraid;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            OnClickAfraid();
        }
    }

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
        npcPos = transform.position;
        Debug.Log(npcPos);
        distance = Vector3.Distance(npcPos, player.transform.position);

        if (distance <= 100)
        {
            isAfraid = true;
        }
    }
}
