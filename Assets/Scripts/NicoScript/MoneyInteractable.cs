using UnityEngine;
using UnityEngine.UI;

public class MoneyInteractable : MonoBehaviour
{

    [SerializeField] GameObject moneyPrefab;
    [SerializeField] AudioClip collectSound;
    [SerializeField] float collectVolume = 1.0f;

    float time = 0;
    float height;
    float lastHeight = 0;
    float initialHeight;

    int randomMoneyAmount;
    void Start()
    {
        initialHeight = transform.position.y + 0.5f;
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
        randomMoneyAmount = Random.Range(20, 101);
        if (other.gameObject.CompareTag("Player"))
        {
            SFXManager.Instance.PlaySFX(collectSound,transform, collectVolume);
            MoneyScript.Instance.AddMoney(randomMoneyAmount);
            gameObject.SetActive(false);
        }
    }
}
