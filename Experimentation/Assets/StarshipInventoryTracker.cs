using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;
using static UnityEngine.Rendering.GPUSort;

public class StarshipInventoryTracker : MonoBehaviour
{
    [Header("Cargo Logistics")]
    public int CargoCapacity = 6;
    public List<CargoData> StarshipInventory;
    public GameObject CargoVisuals;
    public GameObject CargoUIVisuals;

    //manages what can and cant be done
    public bool ItemSelected;

    //Tell me whats currently selected
    public int SelectionIndex;

    public float StartingO2Value;
    public float StartingFuel;

    [Header("Critical Resource Logistics")]
    public float O2Value;
    public float fuelValue;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Debug purposes
        O2Value = StartingO2Value;
        fuelValue = StartingFuel;


        SelectionIndex = CargoCapacity + 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //shouldnt be neccessary
    public void AddCargoData(CargoData NewCargo)
    {
        StarshipInventory.Add(NewCargo);

    }



    public void GenerateRandomCargo()
    {
        //Search folder 'cargo' for cargo data
        CargoData[] AllItems = Resources.LoadAll<CargoData>("Cargo");

        if (AllItems.Length > 0)
        {
            int RandomIndex = Random.Range(0, AllItems.Length);

            CargoData randomitem = AllItems[RandomIndex];
            GenerateCargo(randomitem);
            Debug.Log("Selected: " + randomitem);


        }
        else
        {
            Debug.LogWarning("No items found in the Resources folder.");
        }
    }

    //what happens when a cargo unit it 'used'
    public void ConsumeCargo()
    {
        //Make sure player cant 'use' non-existent cargo
        if (ItemSelected)
        {
            CargoData Cargo = CargoUIVisuals.GetComponent<StorageMenu>().SelectedCargoItem;

            //Check what this cargo 'does'

            fuelValue += Cargo.FuelRefill;
            O2Value += Cargo.O2Refill;

            CargoUIVisuals.GetComponent<StorageMenu>().UpdateCargoData();
        }
        else 
        {
            Debug.Log("No item selected!");
        }

        //Add section that clears effect text!

    }


    public void GenerateCargo(CargoData item)
    {
        //Test to see which cargo spot isnt taken
        for (int i = 0; i < CargoCapacity; i++)
        {
            if (StarshipInventory[i] == null)
            {
                StarshipInventory[i] = item;

                //visualize this change
                CargoVisuals.GetComponent<CargoManager>().UpdateCargoData();
                CargoUIVisuals.GetComponent<StorageMenu>().UpdateCargoData();

                break;
            }
            else
            {
                Debug.Log("Cant Generate! inventory is full!");
            }
        }
    }


    public void ClearSelectedCargo()
    {
        // Cant do this -> StarshipInventory[SelectionIndex] = null;
        ItemSelected = false;
        //visualize this change
        CargoVisuals.GetComponent<CargoManager>().UpdateCargoData();
        CargoUIVisuals.GetComponent<StorageMenu>().UpdateCargoData();
        CargoUIVisuals.GetComponent<StorageMenu>().ClearSelection();
    }

    public void ClearAllCargo()
    {

        for (int i = 0; i < CargoCapacity; i++)
        {
           StarshipInventory[i] = null;
        }


        //visualize this change
        CargoVisuals.GetComponent<CargoManager>().UpdateCargoData();
    }

    //For adding or removing Resources from events... or Cargo, crew, etc
    public void AdjustO2(float O2Change)
    {
        O2Value += O2Change;
        Mathf.Clamp(fuelValue, 0, 100);
    }

    public void AdjustFuel(float FuelChange)
    {
        fuelValue += FuelChange;
        Mathf.Clamp(fuelValue, 0, 100);
    }




}


//DEBUG/TOOLS OPTIONS BELOW

public class MyCustomMenu
{
    StarshipInventoryTracker StarshipTracker;

    // This creates a new top-level menu named "Utilities" with an item named "Perform Task"



    [MenuItem("Utilities/GenerateRandomCargo")]
    public static void GenerateRandomCargo()
    {
        //Find Cargo script
        StarshipInventoryTracker CargoSystem = Object.FindFirstObjectByType<StarshipInventoryTracker>();

        //Find UI Cargo script
        StorageMenu UICargoSystem = Object.FindFirstObjectByType<StorageMenu>();

        if (CargoSystem != null && UICargoSystem != null)
        {
            //Execute the function on it
            CargoSystem.GenerateRandomCargo();
            UICargoSystem.UpdateCargoData();

            // Tell Unity that the scene changed so it saves properly
            EditorUtility.SetDirty(CargoSystem);
            EditorUtility.SetDirty(UICargoSystem);
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(CargoSystem.gameObject.scene);
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UICargoSystem.gameObject.scene);
        }
        else
        {
            Debug.LogWarning("Could not find a InventorySystem script in the current scene!");
        }
    }



    [MenuItem("Utilities/ClearAllCargo")]
    public static void ClearAllCargo()
    {

        StarshipInventoryTracker CargoSystem = Object.FindFirstObjectByType<StarshipInventoryTracker>();


        //Find UI Cargo script
        StorageMenu UICargoSystem = Object.FindFirstObjectByType<StorageMenu>();


        if (CargoSystem != null)
        {
            CargoSystem.ClearAllCargo();
            UICargoSystem.ClearAllCargo();

            EditorUtility.SetDirty(CargoSystem);
            EditorUtility.SetDirty(UICargoSystem);
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(CargoSystem.gameObject.scene);
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UICargoSystem.gameObject.scene);
        }
        else
        {
            Debug.LogWarning("Could not find a InventorySystem script in the current scene!");
        }


    }

}