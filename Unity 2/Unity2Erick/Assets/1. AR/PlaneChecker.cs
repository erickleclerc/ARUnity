using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneChecker : MonoBehaviour
{
    public Camera mainCam;


    void Update()
    {
        Vector2 spot;


#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0) == false)
        {
            return;
        }

        spot = Input.mousePosition;
#else
    if (Input.touchCount == 0)
        {
            return;
        }

        spot = Input.GetTouch(0).position;
#endif

        Ray ray = mainCam.ScreenPointToRay(spot);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {

            Debug.DrawLine(spot, hitInfo.point, Color.green, 1);
            Debug.Log(hitInfo.collider.gameObject.transform.eulerAngles);
        }
    }
}
