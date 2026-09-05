using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "TraitData", menuName = "Scriptable Objects/TraitData")]
public class TraitData : ScriptableObject
{
    [Header("Trait Data")]

    public int TraitID;
    public string TraitName;
    public string Rarity;
    public string Effect;

    //For UI popup
    public Sprite Icon;
}
