using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.EventSystems;
using static UnityEditor.PlayerSettings;

public class InteractToTalk : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public DialogueTreeController DialogueTreeCont;
    

    public Texture2D cursor;
    public float stopDistance;

    //Used in dialogue interaction
    public float Trustworthiness = 100f;
    public Transform talkpos;
    public GameObject Player;
    public Transform Lookrot;

    public enum LookOption { None, LookAtTarget, MatchTargetFacing }
    public LookOption lookOption = LookOption.LookAtTarget;

    private bool ishovering = false;



    private void OnEnable()
    {
        IBlackboard blackboard = DialogueTreeCont.blackboard;
        DialogueTree.OnDialogueStarted += HandledDialogueStarted;
        DialogueTree.OnDialogueFinished += HandledDialogueFinished;
    }

    private void OnDisable()
    {
        DialogueTree.OnDialogueStarted -= HandledDialogueStarted;
        DialogueTree.OnDialogueFinished -= HandledDialogueFinished;
    }

 
    private void HandledDialogueStarted(DialogueTree dialogue)
    {
        //look at where player is
        //Player = GameObject.FindWithTag("Player");
        //Quaternion Rot = Player.transform.rotation;

        //Vector3 direction = Player.transform.position - transform.position;
        //Vector3 targetPosition = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);
        //transform.rotation = Quaternion.LookRotation(targetPosition * -1, Vector3.up);
        

        ChangeToDefaultCursor();
        //Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    private void HandledDialogueFinished(DialogueTree dialogue)
    {
        if (ishovering) ChangeToSpeechCursor();
        // Return cursor if still hovering, otherwise standard default
        //Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        ishovering = true; 

        // If a dialogue is currently active, ignore hover effects
        if (DialogueTree.currentDialogue != null) return;
        ChangeToSpeechCursor();
        //Cursor.SetCursor(speechCursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ChangeToDefaultCursor();
        ishovering = false;

        //if (DialogueTree.currentDialogue != null) return;
        //Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Don't register a new click if a narrative is already running
        if (DialogueTree.currentDialogue != null) return;

        var playerHandler = FindFirstObjectByType<interactor>();

        if (playerHandler == null) { Debug.Log($"No {nameof(interactor)} found in scene"); return; }

        Transform interacttransf = talkpos;
        if (interacttransf == null) { interacttransf = this.transform; }
        

        //playerHandler.MoveToAndTalk(DialogueTreeCont, this.gameObject);
        
    }

    public void ChangeToSpeechCursor()
    {

    }

    public void ChangeToDefaultCursor()
    {

    }
}
