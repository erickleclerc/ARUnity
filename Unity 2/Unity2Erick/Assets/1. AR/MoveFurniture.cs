using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoveFurniture : MonoBehaviour
{
    public Camera mainCam;

    [SerializeField] private TMP_Dropdown gameStatesDropdown;
    [SerializeField] private Button rightButton;
    [SerializeField] private Button leftButton; 
    private GameObject selectedObject;

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

        if (Physics.Raycast(ray, out RaycastHit hitInfo)  && gameStatesDropdown.value == 1)
        {
            if (hitInfo.collider.CompareTag("Furniture"))
            {
                selectedObject = hitInfo.collider.gameObject;
            }
            //var itemRenderer = hitInfo.collider.gameObject.GetComponent<Renderer>();
            //itemRenderer.material.SetColor("_red", Color.red);
        }

        if (Physics.Raycast(ray, out RaycastHit hit) && gameStatesDropdown.value == 2)
        {
            if (hit.collider.CompareTag("Furniture"))
            {
                selectedObject = hit.collider.gameObject;
            }
            //var itemRenderer = hitInfo.collider.gameObject.GetComponent<Renderer>();
            //itemRenderer.material.SetColor("_red", Color.red);
        }
    }

    public void MoveRight()
    {
        if (gameStatesDropdown.value == 1)
        {
            selectedObject.transform.Translate(transform.right * 0.5f);
        }
    }
    public void MoveLeft()
    {
        if (gameStatesDropdown.value == 1)
        {
            selectedObject.transform.Translate(transform.right * -0.5f);
        }
    }

    public void MoveForward()
    {
        if (gameStatesDropdown.value == 1)
        {
            selectedObject.transform.Translate(transform.forward * 0.5f);
        }
    }

    public void MoveBack()
    {
        if (gameStatesDropdown.value == 1)
        {
            selectedObject.transform.Translate(transform.forward * -0.5f);
        }
    }

    public void RotateRight()
    {
        if (gameStatesDropdown.value == 2)
        {
            selectedObject.transform.Rotate(0, 45, 0);
        }
    }

    public void RotateLeft()
    {
        if (gameStatesDropdown.value == 2)
        {
            selectedObject.transform.Rotate(0, -45, 0);
        }
    }
}
