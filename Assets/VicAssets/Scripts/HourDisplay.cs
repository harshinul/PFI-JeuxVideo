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
    [SerializeField] private Light flashLight;

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

        float totalHours = currentHour + currentMinutes / 60f;

        cycleJour.transform.rotation = Quaternion.Euler((totalHours / 24) * 360 - 90, 170, 0);

        if (totalHours < 8f && totalHours >= 6f)
        {
            float t = Mathf.InverseLerp(6f, 8f, totalHours);
            cycleJour.color = Color.Lerp(dawnDuskColor, dayLightColor, t);
        }
        else if (totalHours >= 16f && totalHours < 20f)
        {
            float t = Mathf.InverseLerp(16f, 20f, totalHours);
            cycleJour.color = Color.Lerp(dayLightColor, dawnDuskColor, t);
        }
        else if (totalHours >= 20f || totalHours < 6f)
        {
            cycleJour.gameObject.SetActive(false);
            flashLight.gameObject.SetActive(true);
        }
        else
        {
            flashLight.gameObject.SetActive(false);
            cycleJour.gameObject.SetActive(true);
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
