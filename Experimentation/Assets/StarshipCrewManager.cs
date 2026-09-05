using NUnit.Framework;
//using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StarshipCrewManager : MonoBehaviour
{

    public List<(string station, bool Occupied, int LocationIndex, Transform stationposition)> StarshipPositions = new List<(string, bool, int, Transform)>();

    public List<CrewData> CrewList;
    public int MaxCrewOccupancy;

    public List<Transform> StationSpots;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        //Station Spots - Unity doesnt show Duble Lists in inspector...

        //0
        StarshipPositions.Add(("EngineStation", false,0, StationSpots[0]));
        //1
        StarshipPositions.Add(("LSStation", false, 1, StationSpots[1]));
        //2
        StarshipPositions.Add(("CargoStation", false,2, StationSpots[2]));
        //3
        //Non-Station Spots
        StarshipPositions.Add(("EmptySpot1", false,3, StationSpots[3]));
        //4
        StarshipPositions.Add(("EmptySpot2", false,4, StationSpots[4]));
        //5 
        StarshipPositions.Add(("EmptySpot3", false, 5,StationSpots[5]));

    }


    //I was here, about to go there
    public void CrewPositionChange()
    {

    }



    public void GenerateRandomCrew()
    {
        //Simply pick a random crew file from folder
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
        //Make space for crew!!

        for (int i = 0; i < StarshipPositions.Count; i++)
        {
            if (StarshipPositions[i].Occupied == false)
            {
                //Will make crew.. place them in appropriate spot
                crew.locationIndex = StarshipPositions[i].LocationIndex;
                GameObject Crewmate = Instantiate(crew.CrewMesh);
                Crewmate.transform.position = StationSpots[i].position;

                Debug.Log($"Found spot at {StarshipPositions[i].station}.. Which is index {StarshipPositions[i].LocationIndex}");
                break;
            }
            else
            {
                Debug.Log("location occupied... finding available spot");
            }

        }



      
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
