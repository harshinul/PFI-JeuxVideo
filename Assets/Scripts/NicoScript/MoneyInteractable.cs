using UnityEngine;
using UnityEngine.UI;

public class MoneyInteractable : MonoBehaviour
{

    [SerializeField] GameObject moneyPrefab;

    float time = 0;
    float height;
    float lastHeight = 0;
    float initialHeight;

    int randomMoneyAmount;
    void Start()
    {
        initialHeight = transform.position.y + 4f;
    }

    private void OnEnable()
    {
        randomMoneyAmount = Random.Range(20, 101);
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
            MoneyScript.Instance.AddMoney(randomMoneyAmount);
            gameObject.SetActive(false);
        }
    }
}
