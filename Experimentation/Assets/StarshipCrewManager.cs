using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;
using NUnit.Framework;
//using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using NodeCanvas;

public class StarshipCrewManager : MonoBehaviour
{
    //To be given to crew
    public GameObject TrustBar;
    public GameObject Player;


    //TraitClass
    public List<TraitData> HumanTraits;
    public List<TraitData> DrucoidTraits;
    public List<TraitData> AnthropodaTraits;
    public List<TraitData> NaalketekTraits;

    //public ScriptableObject DefaultNPCDialogue;

    public DialogueTree SourceDefault;

    [System.Serializable]

    //for one station.. one crew
    public class CrewPositions
    {
        public string station;
        public bool Occupied;
        public Transform StationSpot;
        public CrewData Crew;
    }

    public Transform Spawnlocation;

    public CrewPositions[] StarshipPositions;

    [Header("Visual Trait Icons - Dialogue")]
    public List<Image> TraitsIcons;


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
        //Make space for crew!! INITIAL PLACEMENT
        for (int i = 0; i < MaxCrewOccupancy; i++)
        {
            if (StarshipPositions[i].Occupied == false)
            {
                //Save their spot
                StarshipPositions[i].Occupied = true;
                StarshipPositions[i].Crew = crew;

                //how much traits a crewmate gets
                crew.NumOfTalents = Random.Range(1, 3);

                //what talents does this person have?
                for(int e = 0; e < crew.NumOfTalents; e++)
                {
                    GenerateTrait(crew.Species, out TraitData TraitName);
                    crew.Talents.Add(TraitName);
                }

                //Will make crew.. place them in appropriate spot
                crew.locationIndex = i;

                Spawnlocation = StarshipPositions[i].StationSpot.transform;
                GameObject Crewmate = Instantiate(crew.CrewMesh);
                Crewmate.transform.SetParent(Spawnlocation,false);
                //Crewmate.transform.position = Spawnlocation.position;

                //Here we populate Blackboard, Dialogue Tree,


                DialogueTree Default = Instantiate(SourceDefault);
                Default.name = SourceDefault.name;
                //DialogueTree Default = DefaultNPCDialogue.GetComponent<DialogueTree>();
                //DialogueTree clonedGraph = Instantiate(Default);

                Crewmate.GetComponent<DialogueTreeController>().graph = Default;
                Crewmate.GetComponent<DialogueTreeController>().SetActorReference("Player", Player.GetComponent<DialogueActor>());
                Crewmate.GetComponent<DialogueTreeController>().blackboard = Crewmate.GetComponent<Blackboard>();

                //Give crewmate scene context
                Crewmate.GetComponent<Blackboard>().SetVariableValue("StarshipManager", gameObject.GetComponent<StarshipInventoryTracker>());
                Crewmate.GetComponent<Blackboard>().SetVariableValue("Player", Player);
                Crewmate.GetComponent<Blackboard>().SetVariableValue("TrustBar", TrustBar);
                Crewmate.GetComponent<Blackboard>().SetVariableValue("CrewData", crew);
                Crewmate.GetComponent<Blackboard>().SetVariableValue("Talents",crew.Talents);
                Crewmate.GetComponent<Blackboard>().SetVariableValue("TraitIconList", TraitsIcons);

         




                //Debug.Log($"Found spot at {StarshipPositions[i].station}.. Which is index {StarshipPositions[i].LocationIndex}");
                break;
            }
            else
            {

                Debug.Log("location occupied... finding available spot");
            }

        }



      
    }

    //have this on instance enable
    public void GenerateTrait(in string Species, out TraitData Trait)
    {
        //Take this crew's species, see what applies based on it

        //What traits can humans access?
        if (Species == "Human")
        {
            //TraitData ;
            int RandomTraitPull = Random.Range(0, HumanTraits.Count);
            Trait = HumanTraits[RandomTraitPull];
            return;

        }

        //What traits can Drucoids access?
        else if (Species == "Drucoid")
        {
            //TraitData ;
            int RandomTraitPull = Random.Range(0, DrucoidTraits.Count);
            Trait = DrucoidTraits[RandomTraitPull];
            return;
        }

        //What traits can Anthropoda access?
        else if (Species == "Anthropoda")
        {
            //TraitData ;
            int RandomTraitPull = Random.Range(0, AnthropodaTraits.Count);
            Trait = AnthropodaTraits[RandomTraitPull];
            return;
        }

        //What traits can Naalketek access?
        else if (Species == "Naalketek") 
        {
            //TraitData ;
            int RandomTraitPull = Random.Range(0, NaalketekTraits.Count);
            Trait = NaalketekTraits[RandomTraitPull];
            return;
        }

        Trait = NaalketekTraits[0];
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
