using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CargoDatabase", menuName = "Scriptable Objects/CargoDatabase")]
public class CargoDatabase : ScriptableObject
{
    [SerializeField] private List<CargoData> allCargoItems = new List<CargoData>();

    // Quick lookup dictionary built at runtime
    private Dictionary<string, CargoData> cargoLookup;

    public void Initialize()
    {
        cargoLookup = new Dictionary<string, CargoData>();
        foreach (var item in allCargoItems)
        {
            if (!cargoLookup.ContainsKey(item.CargoName))
            {
                cargoLookup.Add(item.CargoName, item);
            }
        }
    }

    //public CargoData GetCargoByID(string id)
    //{
    //    if (cargoLookup == null) Initialize();

    //    if (cargoLookup.TryGetValue(id, out var item))
    //    {
    //        return item;
    //    }

    //    Debug.LogWarning($"Cargo ID '{id}' not found in database.");
    //    return Car;
    //}

    public List<CargoData> GetAllCargo() => allCargoItems;
}
