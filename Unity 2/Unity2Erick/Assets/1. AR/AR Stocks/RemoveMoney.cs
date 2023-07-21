using UnityEngine;
using UnityEngine.UI;

public class RemoveMoney : MonoBehaviour
{
    public Camera mainCam;
    [SerializeField] private Toggle deleteToggle;
    [SerializeField] private ClickPlaceMoney clickPlaceMoneyScript;

    void Update()
    {
        Vector2 spot;


        //Mouse Clicks in Unity Editor
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0) == false)
        {
            return;
        }

        spot = Input.mousePosition;
#else
          //Phone Taps
    if (Input.touchCount == 0)
        {
            return;
        }

        spot = Input.GetTouch(0).position;
#endif

        Ray ray = mainCam.ScreenPointToRay(spot);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (deleteToggle.isOn && hitInfo.collider.CompareTag("Coin"))
            {
                Destroy(hitInfo.collider.gameObject);
                clickPlaceMoneyScript.coinsInSceneValuein1990 -= 50;
            }
        }
    }
}
