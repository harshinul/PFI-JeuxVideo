using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 10f;

    [SerializeField] int damage = 10;

    [SerializeField] bool addBulletSpread = true;

    [SerializeField] AudioClip hitSound;

    public Vector3 BulletSpread = new Vector3(0.01f, 0.01f, 0.1f);

    Vector3 direction;

    TrailRenderer trail;

    void Start()
    {

    }

    private void OnEnable()
    {
        direction = GetDirection();
        trail = GetComponent<TrailRenderer>();
        if (trail != null)
        {
            trail.Clear();
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

    }

    private Vector3 GetDirection()
    {
        Vector3 direction = transform.forward;
        if (addBulletSpread)
        {
            direction += new Vector3(
                Random.Range(-BulletSpread.x, BulletSpread.x),
                direction.y, // on ne modifie pas la hauteur pour éviter de tirer vers le sol ou le ciel
                Random.Range(-BulletSpread.z, BulletSpread.z)
            );
            direction.Normalize();
        }
        return direction;
    }

    private void OnTriggerEnter(Collider other)
   {
        if (other.CompareTag("Player"))
        {
            var health = other.GetComponent<HealthComponent>();
            if (health != null)
            {
                health.TakeDamage(damage);
                SFXManager.Instance.PlaySFX(hitSound, transform, 1.0f);
            }
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Untagged"))
        {
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("MallCop") || other.CompareTag("CopsFirstWave") || other.CompareTag("CopsSecondWave") || other.CompareTag("CopsThirdWave"))
        {
            var health = other.GetComponent<NPCHealthComponent>();
            if (health != null)
            {
                health.TakeDamage(damage, Vector3.zero);
                SFXManager.Instance.PlaySFX(hitSound, transform, 1.0f);
            }

        }
        else if (other.CompareTag("NPC"))
        {
            var health = other.GetComponent<NPCHealthComponent>();
            if (health != null)
            {
                health.TakeDamage(damage, Vector3.zero);
                SFXManager.Instance.PlaySFX(hitSound, transform, 1.0f);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

}
