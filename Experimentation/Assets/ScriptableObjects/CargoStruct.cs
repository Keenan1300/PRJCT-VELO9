using System;
using UnityEngine;

[Serializable] // This allows the struct to be displayed and edited in the Unity Inspector
public struct CargoManifest
{
    // 1. Fields / Variables
    public string destinationStation;
    public int deliveryDeadlineTurns;
    public bool isInsured;

    // 2. Constructor (Optional: Used to quickly build a new instance via code)
    public CargoManifest(string destination, int deadline, bool insured)
    {
        destinationStation = destination;
        deliveryDeadlineTurns = deadline;
        isInsured = insured;
    }

    // 3. Methods (Optional: Structs can have functions just like classes)
    public void PrintManifestDetails()
    {
        Debug.Log($"Destination: {destinationStation} | Safe: {isInsured}");
    }
}
