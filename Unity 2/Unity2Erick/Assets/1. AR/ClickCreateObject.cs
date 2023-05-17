using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClickCreateObject : MonoBehaviour
{
    public Camera mainCam;

    [SerializeField] private List<GameObject> furniturePrefabs = new List<GameObject>();
    [SerializeField] private int currentFurnitureIndex = 0;
    [SerializeField] private TMP_Dropdown dropdownMenu;

    void Update()
    {
        Vector2 spot;

        ChangeFurniture();

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
            Instantiate(furniturePrefabs[currentFurnitureIndex], hitInfo.point + new Vector3(0,0.3f,0), Quaternion.identity);

            Debug.DrawLine(spot, hitInfo.point, Color.green, 1);
        }
    }


    public void ChangeFurniture()
    {
        int fetchedIndex = dropdownMenu.value;
        currentFurnitureIndex = fetchedIndex;
    }
}

