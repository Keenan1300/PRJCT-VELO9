using UnityEngine;
using NodeCanvas.DialogueTrees;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Assertions.Comparers;

public class InteractToTalk : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public DialogueTreeController DialogueTreeCont;
    public Texture2D cursor;
    public float stopDistance;
    public Transform talkpos;

    public enum LookOption { None, LookAtTarget, MatchTargetFacing }
    public LookOption lookOption = LookOption.LookAtTarget;

    private bool ishovering = false;



    private void OnEnable()
    {
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
