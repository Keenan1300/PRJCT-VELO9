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

    // Keeps track of already spawned positions
    public List<Vector3> spawnedBeaconPositions = new List<Vector3>();
    
    public Vector3 SelectedBeaconLoc;

    //Jump costs
    public float MinimalJumpCost;
    public float MaximalJumpCost;

    //Jump cost multiplier influenced by wheather or not engine is staffed.
    public float JumpCostMultiplier;

    //O2 costs
    public float MinimalO2Cost;
    public float MaximalO2Cost;
    public float O2CostMultiplier;


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

    }

    public void PerformJumpCalculations(int index)
    {
        //Gather Vectors
        SelectedBeaconLoc = spawnedBeaconPositions[index];

        //fuel
        Vector3 Direction =  ShipIcon.transform.position - SelectedBeaconLoc;
        //float TravelDistance = Direction.magnitude;
        float TravelDistance = Vector3.Distance(SelectedBeaconLoc.normalized, ShipIcon.transform.position.normalized);
        //Here is where jump calc is had
        Mathf.Clamp(TravelDistance,MinimalJumpCost, MaximalJumpCost);
       
        float TravelFuelCost = TravelDistance * JumpCostMultiplier;


        //life support
        //float O2ConsumptionDistance = Vector3.Distance(ShipIcon.transform.position, SelectedBeaconLoc);
        float O2ConsumptionDistance = Direction.magnitude;
        Mathf.Clamp(O2ConsumptionDistance,MinimalO2Cost, MaximalO2Cost);
        float TravelO2Cost = O2ConsumptionDistance * TotalCrewMates;

        Debug.Log("This trip would cost" + TravelFuelCost + " fuel units.. and " + TravelO2Cost + " O2 units");
    }

    //Consider 'inhabited beacon' over transform.position...
}
