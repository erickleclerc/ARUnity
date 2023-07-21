using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlaceEquivalentSO : MonoBehaviour
{
    /// <summary>
    /// This will check the total value today and return the equivalent amount of the objects selected in the ObjectEquivalent dropdown list
    /// </summary>

    [SerializeField] private TMP_Dropdown objectDropdown;
    [SerializeField] private ClickPlaceMoney clickPlaceMoneyScript;
    [SerializeField] private List<ObjectEquivalentSO> objectsSO = new List<ObjectEquivalentSO>();
    [SerializeField] private Toggle displaySOToggle;
    

    [SerializeField] private int currentObjectIndex = 0;
    int objectIndex => objectDropdown.value;

    private float totalValueToday;

    [SerializeField] private int ableToInstantiate;

    void Update()
    {
        totalValueToday = clickPlaceMoneyScript.valueToday;

        ChangeUseableObject();

        //How many cars can be instantiated
        ableToInstantiate = (int)(totalValueToday / objectsSO[currentObjectIndex].cost);

        //Will remove the objects in scene when changing the dropdown list. Ex: remove all jeeps when changing to iphones
        objectDropdown.onValueChanged.AddListener(delegate { RemoveCurrentObjects(); });
    }

    void ChangeUseableObject()
    {
        currentObjectIndex = objectIndex;
    }


    public void InstantiateEquivalent()
    {
        if (displaySOToggle.isOn == true)
        {
            int offset = 0;

            for (int i = 0; i < ableToInstantiate; i++)
            {
                GameObject obj = Instantiate(objectsSO[currentObjectIndex].objectEquivalent, transform.position + new Vector3(offset,0,10), Quaternion.identity);
                obj.tag = "EquivalentModel";

                offset += 3;
            }
        }
        if (displaySOToggle.isOn == false)
        {
            RemoveCurrentObjects();
            return;
        }
    }


    private void RemoveCurrentObjects()
    {
       GameObject[] foundObject = GameObject.FindGameObjectsWithTag("EquivalentModel");
        foreach (GameObject obj in foundObject)
        {
            Destroy(obj);
        }

        Debug.Log("Removed all objects");
    }
}
