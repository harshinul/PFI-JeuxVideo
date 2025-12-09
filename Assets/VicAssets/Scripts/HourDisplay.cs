using System;
using System.Collections;
using TMPro;
using UnityEngine;
public class HourDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hourText;
    [SerializeField] private Light cycleJour;
    [SerializeField] Color dayLightColor = Color.white;
    [SerializeField] Color dawnDuskColor = new Color(1f, 0.55f, 0.25f);

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

        if (totalHours < 8f)
        {
            float t = Mathf.InverseLerp(6f, 8f, totalHours);
            cycleJour.color = Color.Lerp(dawnDuskColor, dayLightColor, t);
        }
        else if (totalHours > 16f)
        {
            float t = Mathf.InverseLerp(16f, 18f, totalHours);
            cycleJour.color = Color.Lerp(dayLightColor, dawnDuskColor, t);
        }
        else
        {
            cycleJour.color = dayLightColor;
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
