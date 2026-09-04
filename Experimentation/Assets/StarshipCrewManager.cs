using NUnit.Framework;
//using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StarshipCrewManager : MonoBehaviour
{
    private Dictionary<string, bool> StarshipPositions = new Dictionary<string, bool>();

    public List<CrewData> CrewList;
    public int MaxCrewOccupancy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Station Spots
        StarshipPositions.Add("EngineStation", false);
        StarshipPositions.Add("LSStation", false);
        StarshipPositions.Add("CargoStation", false);

        //Non-Station Spots
        StarshipPositions.Add("EmptySpot1", false);
        StarshipPositions.Add("EmptySpot2", false);
        StarshipPositions.Add("EmptySpot3", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateRandomCrew()
    {
        //Search folder 'cargo' for cargo data
        CrewData[] AllItems = Resources.LoadAll<CrewData>("Crew");

        if (AllItems.Length > 0)
        {
            int RandomIndex = Random.Range(0, AllItems.Length);

            CrewData randomitem = AllItems[RandomIndex];
            GenerateCrew(randomitem);
            Debug.Log("Selected: " + randomitem);


        }
        else
        {
            Debug.LogWarning("No crewdata found in the Resources folder.");
        }
    }

    //Needed for debug reasons
    public void GenerateCrew(CrewData crew)
    {

    }


    //Eliminate all crew

    public void ClearAllCrew()
    {

    }
}


public class DebugCrewMenu
{
    [MenuItem("Utilities/Crew/GenerateRandomCrew")]
    public static void GenerateRandomCrew()
    {
        //Find Cargo script
        StarshipCrewManager CrewSystem = Object.FindFirstObjectByType<StarshipCrewManager>();


        if (CrewSystem != null)
        {
            //Execute the function on it
            CrewSystem.GenerateRandomCrew();
           

            // Tell Unity that the scene changed so it saves properly
            EditorUtility.SetDirty(CrewSystem);
            
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(CrewSystem.gameObject.scene);
            
        }
        else
        {
            Debug.LogWarning("Could not find a InventorySystem script in the current scene!");
        }
    }

    [MenuItem("Utilities/Crew/ClearAllCrew")]
    public static void ClearAllCrew()
    {

        StarshipCrewManager CrewSystem = Object.FindFirstObjectByType<StarshipCrewManager>();



        if (CrewSystem != null)
        {
            CrewSystem.ClearAllCrew();
        
            EditorUtility.SetDirty(CrewSystem);
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(CrewSystem.gameObject.scene);
        }
        else
        {
            Debug.LogWarning("Could not find a InventorySystem script in the current scene!");
        }


    }

}
