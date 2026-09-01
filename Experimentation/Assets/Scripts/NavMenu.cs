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

    //ShowResources
    public TextMeshProUGUI FuelGauge;
    public TextMeshProUGUI O2Guage;

    public GameObject StarshipManager;
    public float fuelvalue;
    public float O2Value;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateCargoData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCargoData()
    {

        //Ensure resource display remains accurate on screen
        StarshipInventoryTracker StarshipManagerScript = StarshipManager.GetComponent<StarshipInventoryTracker>();
        fuelvalue = StarshipManagerScript.fuelValue;
        O2Value = StarshipManagerScript.O2Value;

        //connect to text
        FuelGauge.text = fuelvalue.ToString();
        O2Guage.text = O2Value.ToString();



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
