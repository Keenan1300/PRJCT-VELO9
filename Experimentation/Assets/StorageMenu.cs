using JetBrains.Annotations;
using StarterAssets;
using System;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StorageMenu : MonoBehaviour
{


    public Button EscapeButton;
    public Button Eject;
    public GameObject Menu;
    public GameObject Player;
    public CargoData CargoData;

    //UI symbols
    public List<GameObject> StorageIcons;

    public GameObject StarshipManager;

    public int Inventoryslots = 6;

    //UI
    public TextMeshProUGUI CargoEffects;
    public TextMeshProUGUI CargoDescription;
    public TextMeshProUGUI CargoName;
    public TextMeshProUGUI FuelGauge;
    public TextMeshProUGUI O2Guage;

    public Sprite DefaultCargoSprite;

    //Needed for lock on selection
    public CargoData SelectedCargoItem;

    //Displayed resources
    private float fuelvalue;
    private float O2Value;

    //SFX
    public AudioClip SelectSound;
    public AudioClip EjectSound;
    private AudioSource SFX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        SFX = GetComponent<AudioSource>();
        UpdateCargoData();

        if (CargoDescription != null) 
        { 
            CargoDescription.text = null;
        }

        if (CargoName != null)
        {
            CargoName.text = null;
        }

        if (CargoEffects != null)
        {
            CargoEffects.text = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void lockedmousemode()
    {


        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    public void unlockdmousemode()
    {

        UnityEngine.Cursor.lockState = CursorLockMode.None;
    }


    //Talk to data value selected, display in text
    public void DataValuesToText(CargoData CargoData)
    {
        //Debug if fail to grab cargo data
        if (CargoData == null)
        {
            CargoName.text = "No Object data";
            CargoDescription.text = "No Object description";
            CargoEffects.text = "No Effects";
            return;
        }



        //ExtraDetails
        CargoName.text = CargoData.CargoName;
        CargoDescription.text = CargoData.Description;

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




        for (int i = 0; i < Inventoryslots; i++)
        {
            if (StarshipManager.GetComponent<StarshipInventoryTracker>().StarshipInventory[i] == null)
            {
                StorageIcons[i].GetComponent<Image>().sprite = DefaultCargoSprite;
                StorageIcons[i].name = "CargoBoxIndex" + i;
            }
            else
            {
                CargoData = StarshipManager.GetComponent<StarshipInventoryTracker>().StarshipInventory[i];
               

                //Index searching name
                String CargoName = CargoData.CargoName;

                //Establish new Icon and Descriptions
                Sprite CargoIcon = CargoData.Icon;

                //Sprite CargoIcon = Resources.Load<Sprite>("Icons/" + (CargoName + "_Icon")); <--- avoid searching through folders in runtime

                StorageIcons[i].GetComponent<Image>().sprite = CargoIcon;

                if (CargoIcon == null)
                {
                    Debug.Log("Couldnt find Icon!");
                }


                StorageIcons[i].name = CargoName;

            }
        }
    }
        //USE THIS FOR TEXT CHANGES
        public void CurrentSelectedCargodata(int Index)
        {
            ClearSelection();
            
            //Refer back to manager
            SelectedCargoItem = StarshipManager.GetComponent<StarshipInventoryTracker>().StarshipInventory[Index];
            StarshipManager.GetComponent<StarshipInventoryTracker>().SelectionIndex = Index;

            StarshipManager.GetComponent<StarshipInventoryTracker>().ItemSelected = true;


            //Check what effects to include
            if (SelectedCargoItem.O2Refill > 0)
            {
                CargoEffects.text = "Refills 02 +" + SelectedCargoItem.O2Refill.ToString();
            }

            //Check what effects to include
            if (SelectedCargoItem.FuelRefill > 0)
            {
                CargoEffects.text = "Refills Fuel +" + SelectedCargoItem.FuelRefill.ToString();
            }

            //Check what effects to include
            if (SelectedCargoItem.O2Refill > 0 && SelectedCargoItem.FuelRefill > 0)
            {
                CargoEffects.text = $"Refills Fuel +{SelectedCargoItem.FuelRefill} And 02 +{SelectedCargoItem.O2Refill}";
            }

            //Other Data
                CargoDescription.text = SelectedCargoItem.Description;
                CargoName.text = SelectedCargoItem.CargoName;
                SFX.clip = SelectSound;
                SFX.Play();

        }
        
            //Clear Pre-existing selection
            public void ClearSelection()
             {
            StarshipManager.GetComponent<StarshipInventoryTracker>().SelectionIndex = Inventoryslots + 1;
            SelectedCargoItem = null;
            CargoDescription.text = null;
            CargoName.text = null;
            CargoEffects.text = null;
    }

    public void ClearAllCargo() 
    {
        for (int i = 0; i < Inventoryslots; i++)
        {
            StorageIcons[i].GetComponent<Image>().sprite = DefaultCargoSprite;
            CargoDescription.text = null;
            CargoName.text = null;
            StorageIcons[i].name = "CargoBoxIndex" + i;

        }
    
    }

    public void EjectSFX() 
    {
        SFX.clip = EjectSound;
        SFX.Play();
    }
}

