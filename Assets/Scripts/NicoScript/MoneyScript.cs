using TMPro;
using UnityEngine;

public class MoneyScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;
    private int moneyAmount = 0;
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
        moneyText.text = "$" + moneyAmount.ToString("D9");
    }
}
