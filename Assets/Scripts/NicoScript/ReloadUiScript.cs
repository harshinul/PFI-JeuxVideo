using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ProBuilder.AutoUnwrapSettings;

public class ReloadUiScript : MonoBehaviour
{
    [SerializeField] Image reloadBar;


    public IEnumerator FillReloadBar(float chipSpeed)
    {
        float elapsed = 0f;
        while (elapsed < chipSpeed)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / chipSpeed);
            reloadBar.fillAmount = Mathf.Lerp(0, 1, t);
            yield return null;
        }
        reloadBar.fillAmount = 0;
    }
}