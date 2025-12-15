using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCHealthComponent : MonoBehaviour
{
    [SerializeField] bool canDropCollectables;
    [SerializeField] float startHealth;
    [SerializeField] GameObject collectableMoneyPrefab;
    [SerializeField] GameObject collectableAmmoPrefab;
    [SerializeField] int chanceToDropMoney;
    [SerializeField] int chanceToDropAmmo;
    float currentHealth;

    RagdollController ragdollController;
    NavMeshAgent agent;
    BehaviorTree behaviorTree;

    void Awake()
    {
        ragdollController = GetComponent<RagdollController>();
        agent = GetComponent<NavMeshAgent>();
        behaviorTree = GetComponent<BehaviorTree>();
    }

    private void OnEnable()
    {
        currentHealth = startHealth;
    }
    void Start()
    {
        currentHealth = startHealth;

        chanceToDropMoney = 100 - chanceToDropMoney;
        chanceToDropAmmo = 100 - chanceToDropAmmo;
    }

    public void TakeDamage(float damage, Vector3 force)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            GameManager.Instance.WantedLevel(this.gameObject);
            Die(force,transform.position);
        }
    }

    void Die(Vector3 force, Vector3 lastPosition)
    {
        StartCoroutine(DieCoroutine(force,lastPosition));
    }

    IEnumerator DieCoroutine(Vector3 force, Vector3 lastPosition)
    {
        int randomNumber = Random.Range(0, 100);
        GameObject objectTodrop;
        agent.enabled = false;
        if (behaviorTree != null)
            behaviorTree.enabled = false;
        ragdollController.EnableRagdoll(force);

        if (randomNumber > chanceToDropMoney && canDropCollectables)
        {
            if (randomNumber > chanceToDropAmmo)
            {
                objectTodrop = ObjectPool.objectPoolInstance.GetPooledObject(collectableAmmoPrefab);
            }
            else
            {
                objectTodrop = ObjectPool.objectPoolInstance.GetPooledObject(collectableMoneyPrefab);
            }

            yield return new WaitForSeconds(0.1f);
            objectTodrop.transform.position = lastPosition + Vector3.up * 0.5f; 
            objectTodrop.SetActive(true);
        }

        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    //private void OnTriggerEnter(Collider other)
    //{ 
    //    Debug.Log("Trigger entered");
    //    if (other.CompareTag("Bullet"))
    //    {
    //        Debug.Log("Hit by bullet");
    //    }
    //}
}
