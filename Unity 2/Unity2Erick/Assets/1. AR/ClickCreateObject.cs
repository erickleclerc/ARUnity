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
    [SerializeField] private TMP_Dropdown gameStatesDropdown;
    [SerializeField] private Toggle deleteToggle;

    private Vector3 wallOrFloor;

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

        if (Physics.Raycast(ray, out RaycastHit hitInfo) && deleteToggle.isOn == false && gameStatesDropdown.value == 0)
        {
            Debug.Log(hitInfo.collider.gameObject.transform.eulerAngles);

            wallOrFloor = hitInfo.collider.gameObject.transform.localEulerAngles;
            //Check if the hitspot is not a wall
            if (wallOrFloor.z < 1 && wallOrFloor.y > 260)
            {
                Instantiate(furniturePrefabs[currentFurnitureIndex], hitInfo.point + new Vector3(0, 0.3f, 0), Quaternion.identity);

            }
            else
            {
                Debug.Log("NOT A FLOOR");
            }

            //Debug.DrawLine(spot, hitInfo.point, Color.green, 1);
        }
    }


    public void ChangeFurniture()
    {
        int fetchedIndex = dropdownMenu.value;
        currentFurnitureIndex = fetchedIndex;
    }
}

