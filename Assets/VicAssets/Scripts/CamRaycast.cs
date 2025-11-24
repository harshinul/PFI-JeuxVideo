using System.Collections.Generic;
using UnityEngine;

public class CamRaycast : MonoBehaviour
{
    GameObject player;
    Camera cam;

    //Mur qui sont fade pendant le frame
    List<MurTransparent> fadedWalls = new List<MurTransparent>();

    //Mur qui ce font toucher dans le frame
    List<MurTransparent> wallsHitThisFrame = new List<MurTransparent>();

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
        if (player == null)
            return;

        wallsHitThisFrame.Clear();

        float offset = 0.5f;
        Vector3 leftOffset = cam.transform.right * -offset;
        Vector3 rightOffset = cam.transform.right * offset;
        Vector3 upOffset = cam.transform.up * (offset * 2);
        Vector3 downOffset = cam.transform.up * (-offset * 2);

        Vector3 topLeftOffset = (leftOffset + upOffset * 1.5f);
        Vector3 topRightOffset = (rightOffset + upOffset * 1.5f);
        Vector3 bottomLeftOffset = (leftOffset + downOffset * 1.5f);
        Vector3 bottomRightOffset = (rightOffset + downOffset * 1.5f);

        //Champ de raycast qui entoure le joueur pour voir si il y a quelque chose qui obstrue la vue et le fade si c'est le cas
        CastRay(cam.transform.position);
        CastRay(cam.transform.position + leftOffset);
        CastRay(cam.transform.position + rightOffset);
        CastRay(cam.transform.position + upOffset);
        CastRay(cam.transform.position + downOffset);
        CastRay(cam.transform.position + topLeftOffset);
        CastRay(cam.transform.position + topRightOffset);
        CastRay(cam.transform.position + bottomLeftOffset);
        CastRay(cam.transform.position + bottomRightOffset);

        foreach (var wall in fadedWalls)
        {
            if (!wallsHitThisFrame.Contains(wall))
                wall.doFade = false;
        }

        foreach (var wall in wallsHitThisFrame)
        {
            wall.doFade = true;
        }

        fadedWalls = new List<MurTransparent>(wallsHitThisFrame);
    }

    //Créer les spheresCast pour entourer le joueur plus efficace qu'un raycast vu que le raycast est trop mince et peut ne pas enregistrer une collision
    void CastRay(Vector3 origin)
    {
        Vector3 dir = player.transform.position - origin;
        RaycastHit hit;

        float radius = 0.5f; // thickness of the ray

        if (Physics.SphereCast(origin, radius, dir, out hit))
        {
            if (hit.collider.gameObject == player)
                return;

            MurTransparent wall = hit.collider.GetComponent<MurTransparent>();

            if (wall != null && !wallsHitThisFrame.Contains(wall))
            {
                wallsHitThisFrame.Add(wall);
            }
        }
    }
}