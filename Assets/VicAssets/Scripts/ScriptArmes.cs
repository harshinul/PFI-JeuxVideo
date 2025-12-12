using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ScriptArmes : MonoBehaviour
{
    public RawImage imageUI;
    [SerializeField] float zoom = 1.1f;

    float posX = -0.02f;
    float posY = -0.04f;

    const int nbreDeColonne = 4;
    const int nbreDeRows = 10;
    bool doOnce = true;

    void Start()
    {
        UpdateUV();
    }

    void UpdateUV()
    {
        if (doOnce)
        {
            posX = 0;
            posY = 0;
            doOnce = false;
        }

        float tileW = 1f / nbreDeColonne;
        float tileH = 1f / nbreDeRows;

        float x = posX * tileW;
        float y = 1f - tileH - posY * tileH;

        float w = tileW * zoom;
        float h = tileH * zoom;

        x -= (w - tileW) * 0.5f;
        y -= (h - tileH) * 0.5f;

        imageUI.uvRect = new Rect(x, y, tileW, tileH);
    }

    public void OnScroll(InputAction.CallbackContext context)
    {
        Vector2 scroll = context.ReadValue<Vector2>();

        if (!context.performed) 
            return;

        if (scroll.y < 0)
        {
            posX++;
            if (posX >= nbreDeColonne)
            {
                posX = 0;
                posY = (posY + 1) % nbreDeRows;
            }
        }
        else if (scroll.y > 0)
        {
            posX--;
            if (posX < 0)
            {
                posX = nbreDeColonne - 1;
                posY--;
                if (posY < 0) 
                    posY = nbreDeRows - 1;
            }
        }

        UpdateUV();
    }
}