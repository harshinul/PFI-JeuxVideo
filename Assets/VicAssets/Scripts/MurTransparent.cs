using UnityEditor.SpeedTree.Importer;
using UnityEngine;

public class MurTransparent : MonoBehaviour
{
    [SerializeField] float fadeSpeed;
    [SerializeField] float fadeAmount;

    Camera cam;
    Material[] materials;

    float originalOpacity;
    public bool doFade = false;

    void Start()
    {
        cam = GetComponent<Camera>();
        materials = GetComponent<Renderer>().materials;
        foreach (Material mat in materials)
            originalOpacity = mat.color.a;
    }

    void Update()
    {
        if (doFade)
            FadeNow();
        else
            ResetFade();
    }

    void FadeNow()
    {
        foreach(Material mat in materials)
        {
            Color currentColor = mat.color;
            Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, fadeAmount, fadeSpeed * Time.deltaTime));
            mat.color = smoothColor;
        }
    }

    void ResetFade()
    {
        foreach (Material mat in materials)
        {
            Color currentColor = mat.color;
            Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, originalOpacity, fadeSpeed * Time.deltaTime));
            mat.color = smoothColor;
        }
    }
}
