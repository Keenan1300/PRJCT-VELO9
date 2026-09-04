using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "BeaconData", menuName = "Scriptable Objects/BeaconData")]
public class BeaconData : ScriptableObject
{
    [Header("Beacon Event Data")]
    public int EventID;
    public string EventName;
    public bool IsPartOfQuest;
   
    //Needed for creating the libraries
    public string SectorofSpawn;

    //For UI popup
    public Sprite Icon;

    //NPC Dialogues
    public DialogueTreeController BeaconEventSkeleton;
    public Blackboard BeaconEventBB;
    public AssetBlackboard BeaconEvent;

}
