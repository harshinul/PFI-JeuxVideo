using TMPro;
using UnityEngine;

public class MoneyScript : MonoBehaviour
{
    public static MoneyScript Instance;

    [SerializeField] TextMeshProUGUI moneyText;
    public int moneyAmount { get; private set; } = 0;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        moneyText.text = "$" + moneyAmount.ToString("D9");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddMoney(int amount)
    {
        moneyAmount += amount;
        UpdateUI();
    }

    public void UpdateUI()
    {
        moneyText.text = "$" + moneyAmount.ToString("D9");
    }
}
