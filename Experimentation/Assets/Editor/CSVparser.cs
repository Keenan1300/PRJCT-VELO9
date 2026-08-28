using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class CSVparser
{
    public GameObject StarshipInventoryTracker;

    private static string CargoDataPath = "/Editor/CSVs/CargoDataValues.csv";

    [MenuItem("Utilities/GenerateCargo")]

    [MenuItem("Utilities/GenerateCargoInStarship")]

    //Will Generate Random Cargo Item in starship
    public static void GenerateCargoInStarship()
    {
        int Index = Random.Range(0, 1);

    }


    public static void GenerateCargo()
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

            AssetDatabase.CreateAsset(Cargo, $"Assets/Cargo/{Cargo.CargoName}.asset");

        }

        AssetDatabase.SaveAssets();
    }

}
