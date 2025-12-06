using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ScriptArmes : MonoBehaviour
{
    public RawImage imageUI;

    int posX = 0;
    int posY = 0;

    const int nbreDeColonne = 4;
    const int nbreDeRows = 10;

    void Start()
    {
        UpdateUV();
    }

    void UpdateUV()
    {
        float tileW = 1f / nbreDeColonne;
        float tileH = 1f / nbreDeRows;

        float x = posX * tileW;
        float y = 1f - tileH - posY * tileH;

        imageUI.uvRect = new Rect(
            x,
            y,
            tileW,
            tileH
        );
    }

    public void OnScroll(InputAction.CallbackContext context)
    {
        Vector2 scroll = context.ReadValue<Vector2>();

        if (!context.performed) 
            return;

        if (scroll.y > 0)
        {
            posX++;
            if (posX >= nbreDeColonne)
            {
                posX = 0;
                posY = (posY + 1) % nbreDeRows;
            }
        }
        else if (scroll.y < 0)
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