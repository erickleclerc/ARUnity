using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RemoveFurniture : MonoBehaviour
{
    public Camera mainCam;
    [SerializeField] private Toggle deleteToggle;
    [SerializeField] private TMP_Dropdown gameStatesDropdown;

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

        if (Physics.Raycast(ray, out RaycastHit hitInfo))// && gameStatesDropdown.value == 0)
        {
            if (deleteToggle.isOn && hitInfo.collider.CompareTag("Furniture"))
            {
                Destroy(hitInfo.collider.gameObject);
            }
            //Debug.DrawLine(spot, hitInfo.point, Color.red, 1);
        }
    }
}



