using UnityEngine;

public class CamRaycast : MonoBehaviour
{
    private MurTransparent scriptTransp;
    GameObject player;
    Camera cam;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        sendRaycastCam();
    }

    void sendRaycastCam()
    {
        if (player != null)
        {
            Vector3 dir = player.transform.position - cam.transform.position;
            Ray ray = new Ray(cam.transform.position, dir);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider == null)
                    return;

                if (hit.collider.gameObject == player)
                {
                    scriptTransp.doFade = false;
                }
                else
                {
                    scriptTransp = hit.collider.gameObject.GetComponent<MurTransparent>();
                    if (scriptTransp != null)
                    {
                        scriptTransp.doFade = true;
                    }
                }
            }
        }
    }
}
