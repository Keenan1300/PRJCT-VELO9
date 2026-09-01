using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using static UnityEngine.Rendering.GPUSort;

public class CargoManager : MonoBehaviour
{
    public int Inventoryslots = 6;
    public GameObject StarshipManager;
    public List<GameObject> CargoBoxes = new List<GameObject>();
    public CargoData CargoData;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Refresh so that visuals represent data
        UpdateCargoData();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void UpdateCargoData()
    {
        for (int i = 0; i < Inventoryslots; i++)
        {
            if (StarshipManager.GetComponent<StarshipInventoryTracker>().StarshipInventory[i] == null)
            {
                CargoBoxes[i].SetActive(false);
                CargoBoxes[i].name = "CargoBoxIndex" + i;
            }
            else
            {
                CargoData = StarshipManager.GetComponent<StarshipInventoryTracker>().StarshipInventory[i];
                CargoBoxes[i].SetActive(true);

                //Index searching name
                String CargoName = CargoData.CargoName;

                //Establish new mesh and materials
                Mesh Cargomesh = CargoData.Mesh;
                Material CargoMaterial = CargoData.Material;

                //Mesh Cargomesh = Resources.Load<Mesh>("Meshes/" + (CargoName + "_mesh"));
                //Material CargoMaterial = Resources.Load<Material>("Materials/" + (CargoName + "_material"));

                //Swap out mesh and materials
                CargoBoxes[i].GetComponent<MeshFilter>().mesh = Cargomesh;

                if(Cargomesh = null)
                {
                    Debug.Log("Couldnt find mesh!");
                }

                CargoBoxes[i].GetComponent<MeshRenderer>().material = CargoMaterial;

                CargoBoxes[i].name = CargoName;

            }
        }
    }
}

