using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private List<GameObject> pool = new List<GameObject>();

    [SerializeField] public GameObject[] objectsToPool;
    [SerializeField] public int[] quantityToPool;

    public static ObjectPool objectPoolInstance;

    private void Awake()
    {
        if (objectPoolInstance == null)
        {
            objectPoolInstance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        for (int i = 0; i < Mathf.Min(objectsToPool.Length, quantityToPool.Length); i++)
        {
            for (int j = 0; j < quantityToPool[i]; j++)
            {
                GameObject obj = GameObject.Instantiate(objectsToPool[i]);
                obj.transform.parent = this.transform;
                obj.transform.position = this.transform.position;
                obj.name = objectsToPool[i].name;
                obj.SetActive(false);
                pool.Add(obj);
                if (obj.CompareTag("NPC"))
                    obj.SetActive(true);
                if (obj.CompareTag("MallCop"))
                    obj.SetActive(true);
                if (obj.CompareTag("CopsFirstWave"))
                    GameManager.Instance.meleeCop = obj.gameObject;
                if(obj.CompareTag("CopsSecondWave"))
                    GameManager.Instance.pistolCop = obj.gameObject;
                if(obj.CompareTag("CopsThirdWave"))
                    GameManager.Instance.rifleCop = obj.gameObject;
            }
        }
    }

    public void ActivateFromPool(GameObject prefab, int amount)
    {
        int activated = 0;

        for (int i = 0; i < pool.Count && activated < amount; i++)
        {
            if (pool[i].name == prefab.name && !pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                activated++;
            }
        }
    }

    public void RecreateObjects()
    {
        pool.Clear();
        Start();
    }

    public GameObject GetPooledObject(GameObject typeObject)
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (pool[i].name == typeObject.name && !pool[i].activeInHierarchy)
                return pool[i];
        }
        return null;
    }
}
