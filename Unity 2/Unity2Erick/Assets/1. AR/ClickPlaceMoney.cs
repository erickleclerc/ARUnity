using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickPlaceMoney : MonoBehaviour
{
    [SerializeField] private List<StockSO> stocks = new List<StockSO>();


    [SerializeField] private Camera mainCam;
    [SerializeField] private int currentStockIndex = 0;

    [SerializeField] private GameObject money;
    [SerializeField] private TMP_Dropdown dropdownStocks;
    int stockIndex => dropdownStocks.value;

    //World Space UI Canvas Details
    [SerializeField] private Canvas worldSpaceCanvas;
    [SerializeField] private Image stockIcon;
    [SerializeField] private Image stockGraph;
    [SerializeField] private TextMeshProUGUI oldPriceText;
    [SerializeField] private TextMeshProUGUI currentPriceText;
    [SerializeField] private TextMeshProUGUI peRatio;
    [SerializeField] private TextMeshProUGUI dividendYield;
    [SerializeField] private TextMeshProUGUI eps;


    //Screen Space UI Canvas Details
    public float coinsInSceneValuein1990 = 0;
    [SerializeField] private TextMeshProUGUI moneyInvestedText;
    [SerializeField] private TextMeshProUGUI valueTodayText;
    public float valueToday;

    private Vector3 wallOrFloor;

    // Update is called once per frame
    void Update()
    {
        ChangeStock();

        Raycasting();

        moneyInvestedText.text = "In 1990: $" + coinsInSceneValuein1990.ToString();

        CheckValueToday();

        //When changing the stock in the dropdown menu, reset the text
        dropdownStocks.onValueChanged.AddListener(delegate { ResetDisplayText(); });
    }

    public void Raycasting()
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


        if (Physics.Raycast(ray, out RaycastHit hitInfo) && dropdownStocks.value == 0)
        {
            Debug.Log(hitInfo.collider.gameObject.transform.eulerAngles);

            //Stops from Raycasting through the UI
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
        }

        wallOrFloor = hitInfo.collider.gameObject.transform.localEulerAngles;
        //Check if the hitspot is not a wall
        if (wallOrFloor.z < 1 && wallOrFloor.y > 260)
        {
            Instantiate(money, hitInfo.point + new Vector3(0, 0.3f, 0), Quaternion.identity);
            coinsInSceneValuein1990 += 50;
        }
        else
        {
            Debug.Log("NOT A FLOOR");
        }

        //place the logo and stats canvas panel on a wall
        if (wallOrFloor.z > 260)
        {
            worldSpaceCanvas.transform.position = hitInfo.point + new Vector3(0, 0f, 0);
            worldSpaceCanvas.transform.LookAt(mainCam.transform);
            worldSpaceCanvas.transform.Rotate(0, 180, 0);
        }
    }


    public void ChangeStock()
    {
        currentStockIndex = stockIndex;


        stockIcon.sprite = stocks[stockIndex].icon; 
        stockGraph.sprite = stocks[stockIndex].graph;
        oldPriceText.text = "Price in 1990: $" + stocks[stockIndex].oldPrice.ToString();
        currentPriceText.text = "Price Today: $"  + stocks[stockIndex].currentPrice.ToString();
        peRatio.text = "P/E Ratio: " + stocks[stockIndex].peRatio.ToString();
        dividendYield.text = "Dividend Yield: " + stocks[stockIndex].dividendYield.ToString() + "%";
        eps.text = "EPS: " + stocks[stockIndex].eps.ToString();
    }
    

    public void CheckValueToday()
    {
        valueToday = coinsInSceneValuein1990 * stocks[stockIndex].currentPrice;
        //check the value of the coins in the scene and add it to the value today
        valueTodayText.text = "Today: $" + valueToday;

        if (valueToday > coinsInSceneValuein1990)
        valueTodayText.color = Color.green;
    }

    public void ResetDisplayText()
    {
        coinsInSceneValuein1990 = 0;
        moneyInvestedText.text = "In 1990: $0";
        valueTodayText.text = "Today: $0";

        //REFERENCE PLACEEQUIVALENTSO
        GameObject[] placeEquivalents = GameObject.FindGameObjectsWithTag("EquivalentModel");
        foreach (GameObject placeEquivalent in placeEquivalents)
        {
            Destroy(placeEquivalent);
        }




        //RESET ABLE TO INSTANTIATE TO 0

        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        foreach (GameObject coin in coins)
        {
            Destroy(coin);
        }
    }
}
