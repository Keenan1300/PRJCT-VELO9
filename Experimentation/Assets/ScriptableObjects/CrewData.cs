using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "CrewData", menuName = "Scriptable Objects/CrewData")]
public class CrewData : ScriptableObject
{
    [Header("Crew Data")]
    public int CrewID;
    public string Description;
    public string CrewName;
    public string Race;
    public string Talent;
    public bool EngineSkill;
    public bool LSSkill;
    public bool CargoSkill;
    public float O2Drain;
    public float StartingTrust;
    public float TrustBar;

    //Crew Appearance in ship
    public GameObject CrewMesh;
    public Material CrewMaterial;
    public Sprite Icon;

    //NPC Dialogues
    public DialogueTreeController NPCDialogueSkeleton;
    public Blackboard NPCDialgoueBB;
    public AssetBlackboard NPCDialgoue;

    //blackmarket store prices
    [Header("Store Data")]
    public float Value;
    public float Cost;

}
