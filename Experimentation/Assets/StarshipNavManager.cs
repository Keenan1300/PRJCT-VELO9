using JetBrains.Annotations;
using StarterAssets;
using System;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StarshipNavManager : MonoBehaviour
{


    //Going to need crew tracker gameobject so proper O2 calculations can apply to jump... Make sure it has a list of scriptable crew data... rerference this list..
    //its count multiplies 02 consumption
    public GameObject CrewManager;

    public int TotalCrewMates;
    public float MaxRange = 100f;

    //nav holders
    public int SelectedBeacon;
    public GameObject BeaconGenerator;
    public GameObject ShipIcon;
    public GameObject TargettingIcon;
    public GameObject NavMenuUI;

    // Keeps track of already spawned positions
    public List<Vector3> spawnedBeaconPositions = new List<Vector3>();
    
    public Vector3 SelectedBeaconLoc;
    public Vector3 InhabitedBeaconLoc;
    public Transform StarOrigin;

    //Jump costs
    public float MinimalJumpCost;
    public float MaximalJumpCost;

    //Jump cost multiplier influenced by wheather or not engine is staffed.
    public float JumpCostMultiplier;

    //O2 costs
    public float MinimalO2Cost;
    public float MaximalO2Cost;
    public float O2CostMultiplier;

    public int CurrentShipIndex;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RefreshData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeselectedBeacon(int BeaconIndex)
    {
        SelectedBeacon = BeaconIndex;
    }

    public void RefreshData()
    {
        spawnedBeaconPositions = BeaconGenerator.GetComponent<BeaconGenerator>().spawnedBeaconPositions;
        CurrentShipIndex = BeaconGenerator.GetComponent<BeaconGenerator>().EntryBeaconIndex;
        InhabitedBeaconLoc = spawnedBeaconPositions[CurrentShipIndex];
    }

    public void PerformJumpCalculations(int index)
    {
        //Gather Vectors
        SelectedBeaconLoc = spawnedBeaconPositions[index];
        InhabitedBeaconLoc = spawnedBeaconPositions[CurrentShipIndex];

        //fuel
        float TravelDistance = Vector3.Distance(InhabitedBeaconLoc, SelectedBeaconLoc);

        //Here is where jump calc is had
        float TravelFuelCost = TravelDistance * JumpCostMultiplier;
        Mathf.Clamp(TravelFuelCost, MinimalJumpCost, MaximalJumpCost);

        //life support
        float O2ConsumptionDistance = Vector3.Distance(ShipIcon.transform.position, SelectedBeaconLoc);
       
        float TravelO2Cost = (O2ConsumptionDistance/2) * TotalCrewMates * O2CostMultiplier;
        Mathf.Clamp(TravelO2Cost, MinimalO2Cost, MaximalO2Cost);

        //For nice round... dont have to deal with annoying long numbers
        TravelFuelCost = Mathf.Ceil(TravelFuelCost * 10f) / 10f;
        TravelO2Cost = Mathf.Ceil(TravelO2Cost * 10f) / 10f;


        Debug.Log("This trip would cost" + TravelFuelCost + " fuel units.. and " + TravelO2Cost + " O2 units");
        NavMenuUI.GetComponent<NavMenu>().ShowJumpCosts(TravelO2Cost, TravelFuelCost);
    }

    //Consider 'inhabited beacon' over transform.position...
}
