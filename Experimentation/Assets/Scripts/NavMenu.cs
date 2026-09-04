using StarterAssets;
using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class NavMenu : MonoBehaviour

{

    //public Button EscapeButton;
    //public Button JumpButton;
    public GameObject Menu;
    public GameObject Player;
    public GameObject JumpButton;

    //Show Current Resources
    public TextMeshProUGUI FuelGauge;
    public TextMeshProUGUI O2Guage;

    //CostBar - For preview jump costs before theyre made
    public GameObject CostBar;
    public TextMeshProUGUI FuelCost;
    public TextMeshProUGUI O2Cost;

    //Updates current resource data
    public GameObject StarshipManager;
    public float Displayfuelvalue;
    public float DisplayO2Value;


    //needed for consistency actions
    private void OnEnable()
    {
        CostBar.SetActive(false);
        JumpButton.SetActive(false);
        UpdateCargoData();
    }


    private void OnDisable()
    {
        //ensures player cant do 'blind jump' bug
        StarshipManager.GetComponent<StarshipNavManager>().beaconselected = false;
    }

    //for UX signifier only
    public void ShowJumpCosts(float O2costnum, float fuelcostnum)
    {
        //flare
        if (O2costnum > 0 && fuelcostnum > 0)
        {
            O2Cost.color = Color.red;
            FuelCost.color = Color.red;
        }
        else
        {
            O2Cost.color = Color.grey;
            FuelCost.color = Color.grey;
        }


        //show cost to player
            JumpButton.SetActive(true);
            CostBar.SetActive(true);
            O2Cost.text = "-" + O2costnum.ToString();
            FuelCost.text = "-" + fuelcostnum.ToString();
    }
    public void HideJumpCosts()
    {
        //hide cost to player
        CostBar.SetActive(false);
        JumpButton.SetActive(false);
    }


    public void UpdateCargoData()
    {

        //Ensure resource display remains accurate on screen
        StarshipInventoryTracker StarshipManagerScript = StarshipManager.GetComponent<StarshipInventoryTracker>();
        Displayfuelvalue = StarshipManagerScript.fuelValue;
        DisplayO2Value = StarshipManagerScript.O2Value;


        //For nice round... dont have to deal with annoying long numbers
        Displayfuelvalue = Mathf.Ceil(Displayfuelvalue * 10f) / 10f;
        DisplayO2Value = Mathf.Ceil(DisplayO2Value * 10f) / 10f;

        //connect to text
        FuelGauge.text = Displayfuelvalue.ToString();
        O2Guage.text = DisplayO2Value.ToString();

        //ShowJumpCosts(0,0);

    }

    public void lockedmousemode()
    {
      
        
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    public void unlockdmousemode()
    {

        UnityEngine.Cursor.lockState = CursorLockMode.None;
    }
}
