using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "CargoData", menuName = "Scriptable Objects/Starship/CargoData")]
public class CargoData : ScriptableObject
{
    [Header("Cargo Data")]
    public int CargoID;
    public string CargoName;
    public string Description;
    public string SPeffects;
    public float O2Refill;
    public float FuelRefill;
    public float MoraleRefill;

    [Header("Store Data")]
    public float Value;
    public float Cost;
}
