using NodeCanvas.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using static UnityEngine.Rendering.GPUSort;

public class CSVparser
{
    public GameObject StarshipInventoryTracker;

    private static string CargoDataPath = "/Editor/CSVs/CargoDataValues.csv";
    private static string CrewDataPath = "/Editor/CSVs/CrewDataValues.csv";
    private static string BeaconDataPath = "Editor/CSVs/BeaconDataValues.csv";

    //Handle Cargo Data type

    [MenuItem("Utilities/Parser/RefreshCargoDatainFiles")]


    public static void RefreshCargoDatainFiles()
    {
    
        string[] alllines = File.ReadAllLines(Application.dataPath + CargoDataPath);
        Debug.Log("Generating Cargo...");
        foreach (string s in alllines)
        {
            string[] Splitdata = s.Split(',');


            //Safety net
            if (Splitdata.Length != 9) 
            {
                Debug.Log(s +" has incorrect data values");
                return;
            }

            CargoData Cargo = ScriptableObject.CreateInstance<CargoData>();


            Cargo.CargoID = int.Parse(Splitdata[0]);
            Cargo.CargoName = Splitdata[1];
            Cargo.Description = Splitdata[2];
            Cargo.SPeffects = Splitdata[3];
            Cargo.O2Refill = float.Parse(Splitdata[4]);
            Cargo.FuelRefill = float.Parse(Splitdata[5]);
            Cargo.MoraleRefill = float.Parse(Splitdata[6]);
            Cargo.Value = float.Parse(Splitdata[7]);
            Cargo.Cost = float.Parse(Splitdata[8]);

            //For 3D layer... only using Resource finder for parsing..
            Cargo.Icon = Resources.Load<Sprite>("Icons/"+Cargo.CargoName+"_Icon");
            Cargo.Material = Resources.Load<Material>("Materials/" + Cargo.CargoName + "_material");
            Cargo.Mesh = Resources.Load<Mesh>("Meshes/"+Cargo.CargoName + "_mesh");

            //Consider automating mesh, materials, and Icon via a name convention.. use Resource.Find


            //Try not to touch where cargo file is held
            AssetDatabase.CreateAsset(Cargo, $"Assets/Resources/Cargo/{Cargo.CargoName}.asset");

        }

        AssetDatabase.SaveAssets();
    }


    //HANDLE CREW DATA TYPE

    [MenuItem("Utilities/Parser/RefreshCrewDatainFiles")]
    public static void RefreshCrewDatainFiles()
    {

        string[] alllines = File.ReadAllLines(Application.dataPath + CrewDataPath);
        Debug.Log("Generating Cargo...");
        foreach (string s in alllines)
        {
            string[] Splitdata = s.Split(',');


            //Safety net
            if (Splitdata.Length != 12)
            {
                Debug.Log(s + " has incorrect data values");
                return;
            }

            CrewData Crew = ScriptableObject.CreateInstance<CrewData>();


       
           
            


            //For 3D layer... only using Resource finder for parsing..
            //Note that mesh and gameobjects should be handled differently for crew... consider random generation over strict finder based on names.



            //Visual Aspect

            //Find all icons from resource.. the number of these is the max in range
            //random num, for consistency, will need to be used accross other aspects for visual continuity (aka making sure icon isnt too dissimilar to 3D mesh)
            int RandomGen = Random.Range(0, Resources.LoadAll<Sprite>($"Crew/Species/{Crew.Species}/Icons").Length);

            Crew.Icon = Resources.Load<Sprite>($"Crew/Species/{Crew.Species}/Icons/{Crew.Species}{RandomGen}_Icon");
            Crew.CrewMaterial = Resources.Load<Material>($"Crew/Species/{Crew.Species}/Materials/{Crew.Species}{RandomGen}_Icon");
            Crew.CrewMesh = Resources.Load<GameObject>($"Crew/Species/{Crew.Species}/Meshes/{Crew.Species}{RandomGen}_Icon");

            // ie Human7_Icon.png
            // Drucoid3_Icon.png

            //Dialogue Aspect
            //Crew.NPCDialgoue
            //Crew.NPCDialgoueBB = Resources.Load<GameObject>(



            //Attributes and Skill
            Crew.Talent = Splitdata[3];
            Crew.EngineSkill = bool.Parse(Splitdata[4]);
            Crew.LSSkill = bool.Parse(Splitdata[5]);
            Crew.CargoSkill = bool.Parse(Splitdata[6]);
            Crew.O2Drain = float.Parse(Splitdata[7]);

            Crew.StartingTrust = float.Parse(Splitdata[8]);
            Crew.TrustBar = float.Parse(Splitdata[9]);

            //Passive Data
            Crew.CrewID = int.Parse(Splitdata[0]);
            Crew.CrewName = Splitdata[1];
            Crew.Description = Splitdata[2];
    
            Crew.Value = float.Parse(Splitdata[11]);
            Crew.Cost = float.Parse(Splitdata[12]);



            //Try not to touch where cargo file is held
            AssetDatabase.CreateAsset(Crew, $"Assets/Resources/Crew/CrewAssets/{Crew.Species}.asset");

        }

        AssetDatabase.SaveAssets();
    }


    //HANDLE BEACON DATA TYPE

    [MenuItem("Utilities/Parser/RefreshBeaconDatainFiles")]
    public static void RefreshBeaconDatainFiles()
    {

        string[] alllines = File.ReadAllLines(Application.dataPath + BeaconDataPath);
        Debug.Log("Generating BeaconEvents...");

        foreach (string s in alllines)
        {
            string[] Splitdata = s.Split(',');


            //Safety net
            if (Splitdata.Length != 12)
            {
                Debug.Log(s + " has incorrect data values");
                return;
            }

            BeaconData Beacon = ScriptableObject.CreateInstance<BeaconData>();







            //For 3D layer... only using Resource finder for parsing..
            //Note that mesh and gameobjects should be handled differently for crew... consider random generation over strict finder based on names.


            //Find all icons from resource.. the number of these is the max in range
            //random num, for consistency, will need to be used accross other aspects for visual continuity (aka making sure icon isnt too dissimilar to 3D mesh)
            //int RandomGen = Random.Range(0, Resources.LoadAll<Sprite>($"Crew/Species/{Beacon.Species}/Icons").Length);

            Beacon.Icon = Resources.Load<Sprite>($"Navigation/Sectors/{Beacon.SectorofSpawn}/{Beacon.EventName}_Icon");
            Beacon.BeaconEvent = Resources.Load<AssetBlackboard>($"Navigation/Sectors/{Beacon.SectorofSpawn}/{Beacon.EventName}_EventTree");
            Beacon.BeaconEventBB = Resources.Load<Blackboard>($"Navigation/Sectors/{Beacon.SectorofSpawn}/{Beacon.EventName}/{Beacon.EventName}_BB");

            // ie Human7_Icon.png
            // Drucoid3_Icon.png

            //Dialogue Aspect
            //Crew.NPCDialgoue
            //Crew.NPCDialgoueBB = Resources.Load<GameObject>(



            //Attributes and Skill
            Beacon.EventName = Splitdata[3];
            Beacon.IsPartOfQuest = bool.Parse(Splitdata[4]);
            Beacon.SectorofSpawn = Splitdata[8];

            //Passive Data
            Beacon.EventID = int.Parse(Splitdata[0]);
           



            //Try not to touch where cargo file is held
            AssetDatabase.CreateAsset(Beacon, $"Assets/Resources/Crew/CrewAssets/{Beacon.EventName}.asset");

        }

        AssetDatabase.SaveAssets();
    }
}
