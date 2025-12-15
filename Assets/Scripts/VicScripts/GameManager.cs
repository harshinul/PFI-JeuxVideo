using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] float checkInterval = 0.5f;

    public float wastedCount;
    public static GameManager Instance;
    public GameObject meleeCop;
    public GameObject pistolCop;
    public GameObject rifleCop;

    bool doOnce = true;

    Vector3 copsPos;
    Vector3 npcPos;

    GameObject player;

    int posMelee;
    int posPistol;
    int posRifle;

    float distance;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        posMelee = 5;
        posPistol = 6;
        posRifle = 7;
        StartCoroutine(loopSpawn());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            foreach (var npc in GameObject.FindGameObjectsWithTag("NPC"))
                AfraidNpc(npc);

            foreach (var cop in GameObject.FindGameObjectsWithTag("MallCop"))
                AfraidCops(cop);
        }
    }
    public void WantedLevel(GameObject npc)
    {
        Debug.Log(wastedCount);
        if (npc.CompareTag("NPC"))
        {
            wastedCount += 2;
        }
        else if (npc.CompareTag("MallCop"))
        {
            wastedCount += 5;
        }
        else if (npc.CompareTag("CopsFirstWave"))
        {
            wastedCount += 10;
        }
        else if (npc.CompareTag("CopsSecondWave"))
        {
            wastedCount += 15;
        }
    }

    public void AfraidCops(GameObject cop)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        copsPos = cop.transform.position;
        distance = Vector3.Distance(copsPos, player.transform.position);

        //Fix pour la scale
        if (distance <= 25)
        {
            if (doOnce)
            {
                wastedCount += 1;
                player.GetComponent<Movement>().isWanted = true;
                doOnce = false;
            }
            Debug.Log(wastedCount);
            cop.GetComponent<CopsComponent>().isAfraidCops = true;
        }
    }

    public void AfraidNpc(GameObject npc)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        npcPos = npc.transform.position;
        distance = Vector3.Distance(npcPos, player.transform.position);

        //Fix pour la scale
        if (distance <= 25)
        {
            npc.GetComponent<NpcComponent>().isAfraidNpc = true;
        }
    }

    IEnumerator loopSpawn()
    {
        Debug.Log($"LoopSpawn running. wastedCount={wastedCount}");
        while (true)
        {
            if (wastedCount >= 1 && wastedCount < 10)
                ObjectPool.objectPoolInstance.ActivateFromPool(meleeCop, 1);
            if (wastedCount >= 10)
                ObjectPool.objectPoolInstance.ActivateFromPool(meleeCop, ObjectPool.objectPoolInstance.quantityToPool[posMelee]);
            if(wastedCount >= 20)
                ObjectPool.objectPoolInstance.ActivateFromPool(pistolCop, ObjectPool.objectPoolInstance.quantityToPool[posPistol]);
            if(wastedCount >= 50)
                ObjectPool.objectPoolInstance.ActivateFromPool(rifleCop, ObjectPool.objectPoolInstance.quantityToPool[posRifle]);

            yield return new WaitForSeconds(checkInterval);
        }
    }
}
