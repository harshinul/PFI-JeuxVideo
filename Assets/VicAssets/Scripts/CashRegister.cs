using TMPro;
using UnityEngine;

public class CashRegister : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI cashText;
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("cashRegister"))
        {

        }
    }
}
