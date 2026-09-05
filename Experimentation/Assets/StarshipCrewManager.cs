using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;
using NUnit.Framework;
//using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StarshipCrewManager : MonoBehaviour
{
    //To be given to crew
    public GameObject TrustBar;
    public GameObject Player;


    [System.Serializable]

    //for one station.. one crew
    public class CrewPositions
    {
        public string station;
        public bool Occupied;
        public Transform StationSpot;
        public CrewData Crew;
    }


    public CrewPositions[] StarshipPositions;



    //public List<(string station, bool Occupied, int LocationIndex, Transform stationposition)> StarshipPositions = new List<(string, bool, int, Transform)>();


    public class PlayerData { public int Score; }

   
    public int MaxCrewOccupancy;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        //number to spot association
        StarshipPositions[0].station = "Engine Station";
        StarshipPositions[1].station = "LS Station";
        StarshipPositions[2].station = "Cargo Station";
        StarshipPositions[3].station = "Empty1";
        StarshipPositions[4].station = "Empty2";
        StarshipPositions[5].station = "Empty3";
    

    }


    //I was here, about to go there
    public void CrewPositionChange()
    {

    }



    public void GenerateRandomCrew()
    {
        //Simply pick a random crew file from folder
        CrewData[] AllItems = Resources.LoadAll<CrewData>("Crew/CrewAssets");

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

        for (int i = 0; i < MaxCrewOccupancy; i++)
        {
            if (StarshipPositions[i].Occupied == false)
            {
                //Save their spot
                StarshipPositions[i].Occupied = true;
                StarshipPositions[i].Crew = crew;

                //Will make crew.. place them in appropriate spot
                crew.locationIndex = i;
                GameObject Crewmate = Instantiate(crew.CrewMesh, StarshipPositions[i].StationSpot);

                //Here we populate Blackboard, Dialogue Tree,

                //Give crewmate scene context
                Crewmate.GetComponent<Blackboard>().SetVariableValue("StarshipManager", gameObject.GetComponent<StarshipInventoryTracker>());
                Crewmate.GetComponent<Blackboard>().SetVariableValue("Player", Player);
                Crewmate.GetComponent<Blackboard>().SetVariableValue("TrustBar", TrustBar);
                Crewmate.GetComponent<DialogueTreeController>().SetActorReference("Player", Player.GetComponent<DialogueActor>());

                //Debug.Log($"Found spot at {StarshipPositions[i].station}.. Which is index {StarshipPositions[i].LocationIndex}");
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
        //Make Crew no longer exist, and clear their spot
        for (int i = 0; i < MaxCrewOccupancy; i++)
        {
            StarshipPositions[i].Occupied = false;
            StarshipPositions[i].Crew = null;
            GameObject crew = GameObject.FindWithTag("NPC");
            DestroyImmediate(crew);
        }


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
            Debug.LogWarning("Could not find a crewsystem script in the current scene!");
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
