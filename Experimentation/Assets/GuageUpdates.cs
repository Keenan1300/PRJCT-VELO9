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

    public GameObject TrustBar;

    public List<Sprite> TraitsIcons;
    public List<GameObject> Icons;

    public Text CrewName;



    public void UpdateUItoMakeNPCData(List<TraitData> Traits, CrewData crew)
    {
        //Update Talents Icons
        for (int i = 0; i < Traits.Count; i++)
        {
            Icons[i].GetComponent<Image>().enabled = true;
            Icons[i].GetComponent<TraitDescriptionHover>().Tdata = Traits[i];
            Icons[i].GetComponent<Image>().sprite = Traits[i].Icon;
        }

        //Update Crew Name
        CrewName.GetComponent<Text>().text = crew.CrewName;

    }

    public void TurnOffTraitsIcon(List<TraitData> Traits, List<GameObject> Icons)
    {
        for (int i = 0; i < Traits.Count; i++)
        {
            Icons[i].SetActive(false);


        }

    }

    private void OnEnable()
    {
        DialogueUI.GetComponent<StorageMenu>().UpdateCargoData();
    }

}
