using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CrewData", menuName = "Scriptable Objects/CrewData")]
public class CrewData : ScriptableObject
{
    [Header("Crew Data")]
    public int CrewID;

    public string Description;
    public string CrewName;
    public string Species;

    //Rare crew will have more talents...
    public int NumOfTalents;
    public string TraitRarity;
    public List<string> Talents = new List<string>();

    public float O2Drain;

    public float StartingTrust;
    public float TrustBar;

    //Crew Appearance in ship
    public GameObject CrewMesh;
    public Material CrewMaterial;
    public Sprite Icon;

    //used for station purposes
    public int locationIndex;

    //NPC Dialogues
    public DialogueTreeController NPCDialogueSkeleton;
    public Blackboard NPCDialgoueBB;
    public AssetBlackboard NPCDialgoue;

    //blackmarket store prices
    [Header("Store Data")]
    public float Value;
    public float Cost;

}
