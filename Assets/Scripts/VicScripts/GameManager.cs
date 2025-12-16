using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    [SerializeField] float checkInterval = 0.5f;

    public float wastedCount;
    public static GameManager Instance;

    [SerializeField] GameObject npc;
    [SerializeField] GameObject mallCop;
    [SerializeField] GameObject meleeCop;
    [SerializeField] GameObject pistolCop;
    [SerializeField] GameObject rifleCop;

    Transform[] targets;
    GameObject[] POI;

    public bool isAfraidCops;
    public bool isAfraidNpc;
    bool doOnce = true;

    Vector3 copsPos;
    Vector3 npcPos;

    GameObject player;

    float distance;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        POI = GameObject.FindGameObjectsWithTag("POI");
        List<Transform> poiList = new List<Transform>();

        foreach (GameObject poi in POI)
        {
            poiList.Add(poi.transform);
        }
        targets = poiList.ToArray();

        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(loopSpawn());

        for (int i = 0; i < 20; i++)
        {
            GameObject npcInstance = ObjectPool.objectPoolInstance.GetPooledObject(npc);
            npcInstance.SetActive(true);
            npcInstance.transform.position = GenerateRandomPos();
            Debug.Log(npcInstance.transform.position);
        }

        for(int i = 0; i < 3; i++)
        {
            GameObject mallCopInstance = ObjectPool.objectPoolInstance.GetPooledObject(mallCop);
            mallCopInstance.SetActive(true);
            mallCopInstance.transform.position = GenerateRandomPos();
        }
    }

    public void WantedLevel(GameObject npc)
    {
        //Debug.Log(wastedCount);
        if (npc.CompareTag("NPC"))
        {
            wastedCount += 1;
        }
        else if (npc.CompareTag("MallCop"))
        {
            wastedCount += 3;
        }
        else if (npc.CompareTag("CopsFirstWave"))
        {
            wastedCount += 8;
        }
        else if (npc.CompareTag("CopsSecondWave"))
        {
            wastedCount += 10;
        }
    }

    public void AfraidCops(GameObject cop)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        copsPos = cop.transform.position;
        distance = Vector3.Distance(copsPos, player.transform.position);

        if (distance <= 25)
        {
            if (doOnce)
            {
                wastedCount += 1;
                player.GetComponent<Movement>().isWanted = true;
                doOnce = false;
            }
            Debug.Log(wastedCount);
            isAfraidCops = true;
        }
    }

    public void AfraidEveryone()
    {
        foreach (var npc in GameObject.FindGameObjectsWithTag("NPC"))
            AfraidNpc(npc);

        foreach (var cop in GameObject.FindGameObjectsWithTag("MallCop"))
            AfraidCops(cop);
    }

    public void AfraidNpc(GameObject npc)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        npcPos = npc.transform.position;
        distance = Vector3.Distance(npcPos, player.transform.position);

        if (distance <= 25)
        {
            isAfraidNpc = true;
        }
    }

    Vector3 GenerateRandomPos()
    {
        return targets[Random.Range(0, targets.Length)].transform.position;
    }

    IEnumerator loopSpawn()
    {
        while (true)
        {
            if (wastedCount >= 1)
            {
                for(int i = 0; i < 1; i++)
                {
                    GameObject copsMeleeInstance = ObjectPool.objectPoolInstance.GetPooledObject(meleeCop);
                    copsMeleeInstance.SetActive(true);
                    copsMeleeInstance.transform.position = GenerateRandomPos();
                }
            }
            if (wastedCount >= 75)
            {
                for (int i = 0; i < 3; i++)
                {
                    GameObject copsPistolInstance = ObjectPool.objectPoolInstance.GetPooledObject(pistolCop);
                    copsPistolInstance.SetActive(true);
                    copsPistolInstance.transform.position = GenerateRandomPos();
                }
            }
            if (wastedCount >= 150)
            {
                for (int i = 0; i < 1; i++)
                {
                    GameObject copsRifleInstance = ObjectPool.objectPoolInstance.GetPooledObject(rifleCop);
                    copsRifleInstance.SetActive(true);
                    copsRifleInstance.transform.position = GenerateRandomPos();
                }
            }

            yield return new WaitForSeconds(checkInterval);
        }
    }
}
