using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    [SerializeField] float regenDelay = 4f;
    [SerializeField] float regenDuration = 3f;
    [SerializeField] float regenAmount = 100f;
    [SerializeField] float chipSpeed = 4f;

    [SerializeField] Image frontHealthBar;
    [SerializeField] Image backHealthBar;

    public bool isDead;
    private float health;
    private float elapsedSinceHit;
    private float regenRate;
    private Coroutine backBarCoroutine;

    void Start()
    {
        health = maxHealth;
        regenRate = regenAmount / regenDuration;

        frontHealthBar.fillAmount = 1f;
        backHealthBar.fillAmount = 1f;
    }

    void Update()
    {
        if (isDead) return; // plus aucune logique une fois mort
        // timer avant regen
        elapsedSinceHit += Time.deltaTime;

        if (elapsedSinceHit >= regenDelay && health < maxHealth)
        {
            health += regenRate * Time.deltaTime;
            if (health > maxHealth) health = maxHealth;
            UpdateHealthUI();
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;
        health -= damage;
        health = Mathf.Clamp(health, 0f, maxHealth);
        elapsedSinceHit = 0f;

        if (health <= 0)
        {
            isDead = true;

        }
        Debug.Log($"Player Health: {health}");
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        float targetFill = health / maxHealth;

        frontHealthBar.fillAmount = targetFill;

        if (backBarCoroutine != null)
            StopCoroutine(backBarCoroutine);
        backBarCoroutine = StartCoroutine(AnimateBackBar(backHealthBar.fillAmount, targetFill));
    }

    private IEnumerator AnimateBackBar(float fromFill, float toFill)
    {
        float elapsed = 0f;
        while (elapsed < chipSpeed)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / chipSpeed);
            backHealthBar.fillAmount = Mathf.Lerp(fromFill, toFill, t);
            yield return null;
        }
        backHealthBar.fillAmount = toFill;
    }
}
