using JetBrains.Annotations;
using NodeCanvas.DialogueTrees.UI.Examples;
using StarterAssets;
using System;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class GuageUpdates : MonoBehaviour
{
    public TextMeshProUGUI FuelGauge;
    public TextMeshProUGUI O2Guage;
    //Displayed resources
    public float fuelvalue;
    public float O2Value;
    public GameObject StarshipManager;
    public GameObject DialogueUI;

  


    private void OnEnable()
    {
        DialogueUI.GetComponent<StorageMenu>().UpdateCargoData();
    }

}
