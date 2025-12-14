using UnityEngine;

public class AmmoInteractable : MonoBehaviour
{
    [SerializeField] GameObject ammoPrefab;

    float time = 0;
    float height;
    float lastHeight = 0;
    float initialHeight;
    void Start()
    {
        initialHeight = transform.position.y + 4f;
    }

    private void OnEnable()
    {
    }
    void Update()
    {
        time += Time.deltaTime;
        height = (4f * Mathf.Sin(time * 7)) + initialHeight;
        height -= lastHeight;
        transform.Translate(new Vector3(0, height, 0));
        transform.Rotate(new Vector3(0, -100, 0) * Time.deltaTime, Space.Self);
        lastHeight = transform.position.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Ammo picked up! ");
        }
    }
}
