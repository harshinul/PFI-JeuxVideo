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
        initialHeight = transform.position.y + 0.5f;
    }

    private void OnEnable()
    {
    }
    void Update()
    {
        time += Time.deltaTime;
        height = (0.3f * Mathf.Sin(time * 0.5f)) + initialHeight;
        height -= lastHeight;
        transform.Translate(new Vector3(0, height, 0));
        transform.Rotate(new Vector3(0, -100, 0) * Time.deltaTime, Space.Self);
        lastHeight = transform.position.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerAttackComponent>().AddAmmo();
            gameObject.SetActive(false);
        }
    }
}
