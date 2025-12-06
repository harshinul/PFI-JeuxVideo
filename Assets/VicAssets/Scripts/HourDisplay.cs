using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class HourDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hourText;
    [SerializeField] private Light cycleJour;

    private int currentHour;
    private int currentMinutes;
    private bool isCounting = true;

    private void Start()
    {
        DateTime now = DateTime.Now;
        currentHour = now.Hour;
        currentMinutes = now.Minute;
        StartCoroutine(UpdateTime());
    }

    void Update()
    {
        hourText.text = $"{currentHour:00}:{currentMinutes:00}";

        float totalHours = currentHour + currentMinutes / 60;
        float rotation = (totalHours / 24) * 360;

        cycleJour.transform.rotation = Quaternion.Euler(rotation - 90, 170, 0);

        if (totalHours >= 6f && totalHours <= 18f)
        {
            cycleJour.gameObject.SetActive(true);
        }
        else
        {
            cycleJour.gameObject.SetActive(false);
        }
    }

    IEnumerator UpdateTime()
    {
        while (isCounting)
        {
            yield return new WaitForSeconds(1);

            currentMinutes++;

            if (currentMinutes >= 60)
            {
                currentMinutes = 0;
                currentHour++;

                if (currentHour >= 24)
                    currentHour = 0;
            }
        }
    }
}
