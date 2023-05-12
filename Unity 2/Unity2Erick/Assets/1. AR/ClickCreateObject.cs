using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickCreateObject : MonoBehaviour
{
    public Camera mainCam;

    [SerializeField] private GameObject sofaPrefab, chairPrefabs, tablePrefab;

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
            // Instantiate object 

            Instantiate(sofaPrefab, hitInfo.point + new Vector3(0,0.3f,0), Quaternion.identity);

            Debug.DrawLine(spot, hitInfo.point, Color.green, 1);
        }
    }
}
