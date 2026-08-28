using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;

public class StarshipInventoryTracker : MonoBehaviour
{
    
    public List<CargoData> StarshipInventory;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void AddCargoData(CargoData NewCargo)
    {
        StarshipInventory.Add(NewCargo);

    }


    //Function that should tell if cargo is added, or swapped. (If lmb is down and hovering over x)
    //Find cargo index(where it should go) - Find data(including gameobject) -  Determine if this is a swap or generation(bool)
    void ConfigureCargo(int CargoIndex, CargoData CargoData, bool IsSwapping)
    {

    }
}
