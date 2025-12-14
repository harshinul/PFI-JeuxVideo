using UnityEngine;

public class EnnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;


    public bool isDead;
    private float health;
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;
        health -= damage;
        health = Mathf.Clamp(health, 0f, maxHealth);


        if (health <= 0)
        {
            isDead = true;
            Debug.Log("AI is Dead!");
        }
        Debug.Log($"Player Health: {health}");
    }
}
