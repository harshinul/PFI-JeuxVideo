using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 10f;

    [SerializeField] int damage = 10;

    [SerializeField] bool addBulletSpread = true;

    private Vector3 BulletSpread = new Vector3(0.1f, 0.1f, 0.1f);

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += GetDirection() * speed * Time.deltaTime;
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = transform.forward;
        if (addBulletSpread)
        {
            direction += new Vector3(
                Random.Range(-BulletSpread.x, BulletSpread.x),
                Random.Range(-BulletSpread.y, BulletSpread.y),
                Random.Range(-BulletSpread.z, BulletSpread.z)
            );
            direction.Normalize();
        }
        return direction;
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit)
    {
        float time = 0;
        Vector3 startPosition = trail.transform.position;

        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
            time += Time.deltaTime / trail.time;
            yield return null;
        }
        trail.transform.position = hit.point;

        Destroy(trail.gameObject, trail.time);
    }
}
