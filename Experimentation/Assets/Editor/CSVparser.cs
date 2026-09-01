using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class CSVparser
{
    public GameObject StarshipInventoryTracker;

    private static string CargoDataPath = "/Editor/CSVs/CargoDataValues.csv";
    private static string CrewDataPath = "/Editor/CSVs/CrewDataValues.csv";


    //[MenuItem("Utilities/GenerateCargo")]

    [MenuItem("Utilities/RefreshCargoDatainFiles")]


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

            //Try not to touch where cargo file is held
            AssetDatabase.CreateAsset(Cargo, $"Assets/Resources/Cargo/{Cargo.CargoName}.asset");

        }

        AssetDatabase.SaveAssets();
    }


    [MenuItem("Utilities/RefreshCargoDatainFiles")]


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


            Crew.CrewID = int.Parse(Splitdata[0]);
            Crew.CrewName = Splitdata[1];
            Crew.Description = Splitdata[2];
            Crew.Talent = Splitdata[3];
            Crew.EngineSkill = bool.Parse(Splitdata[4]);
            Crew.LSSkill = bool.Parse(Splitdata[5]);
            Crew.CargoSkill = bool.Parse(Splitdata[6]);
            Crew.O2Drain = float.Parse(Splitdata[7]);
            Crew.StartingTrust = float.Parse(Splitdata[8]);
            Crew.TrustBar = float.Parse(Splitdata[9]);
            Crew.Value = float.Parse(Splitdata[11]);
            Crew.Cost = float.Parse(Splitdata[12]);

            //Try not to touch where cargo file is held
            AssetDatabase.CreateAsset(Crew, $"Assets/Resources/Crew/{Crew.Race}.asset");

        }

        AssetDatabase.SaveAssets();
    }

}
