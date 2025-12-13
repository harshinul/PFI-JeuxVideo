using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    private float damage = 10;
    private HealthComponent health;
    private void Start()
    {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthComponent>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            health.TakeDamage(damage);
        }
    }
}
