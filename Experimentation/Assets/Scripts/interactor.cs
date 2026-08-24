using NodeCanvas.DialogueTrees;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class interactor : MonoBehaviour
{
   
    [Header("Raycast Settings")]
    [SerializeField] private Transform playerCamera;
    [SerializeField] private float interactRange = 3.5f;
    [SerializeField] private LayerMask interactableLayers;

    [Header("Input Settings")]
    [SerializeField] private KeyCode interactKey = KeyCode.E; // Or mouse click via KeyCode.Mouse0

    private DialogueActor playerActor;

    private void Awake()
    {
        playerActor = GetComponentInParent<DialogueActor>();
    }

    private void Update()
    {
        // Block interaction attempts if a dialogue is already actively running
        if (DialogueTree.currentDialogue != null) return;

        if (Input.GetKeyDown(interactKey))
        {
            TryInteract();
        }
    }

    private void TryInteract()
    {
        // Shoot a ray directly forward from the camera's center view
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange, interactableLayers))
        {
            // Look for the Dialogue Controller on the object we hit
            DialogueTreeController npcDialogue = hit.collider.GetComponent<DialogueTreeController>();

            if (npcDialogue != null)
            {
                TriggerDialogue(npcDialogue);
            }
        }
    }

    private void TriggerDialogue(DialogueTreeController controller)
    {
        // Optional: Unlock/show the mouse cursor so the player can click dialogue choices
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;

        if (playerActor != null)
        {
            controller.StartDialogue(playerActor, (success) => { OnDialogueEnd(); });
        }
        else
        {
            controller.StartDialogue((success) => { OnDialogueEnd(); });
        }
    }

    private void OnDialogueEnd()
    {
        // Re-lock the mouse cursor back to the center of the screen for FPS movement
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }
}