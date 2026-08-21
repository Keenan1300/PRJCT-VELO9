using NodeCanvas.DialogueTrees;
using StarterAssets;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class TalktoNPC : MonoBehaviour
{

    [Header("Interaction Settings")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float talkRange = 3.5f;
    [SerializeField] private LayerMask interactable;
    [SerializeField] private KeyCode talkKey = KeyCode.E;

    private DialogueActor playerActor;
    public FirstPersonController FPSControl;

    private void Awake()
    {
        FPSControl = GetComponent<FirstPersonController>();
        
        // Automatically caches the player's DialogueActor component if it exists
        playerActor = GetComponent<DialogueActor>();
        if (playerCamera == null) playerCamera = Camera.main;
    }

    private void Update()
    {
        // 1. If a dialogue tree is already running, block input so you can't double-trigger it
        if (DialogueTree.currentDialogue != null) return;

        // 2. Listen for the interaction keypress
        if (Input.GetKeyDown(talkKey))
        {
            CheckAndTalk();
        }
    }

    private void CheckAndTalk()
    {
        // Shoot a ray directly forward out of the center of your screen view
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, talkRange, interactable))
        {
            // Try to find the NodeCanvas controller on the looked-at object
            DialogueTreeController npcTree = hit.collider.GetComponent<DialogueTreeController>();

            if (npcTree != null)
            {
                StartFirstPersonDialogue(npcTree);
            }
        }
    }

    private void StartFirstPersonDialogue(DialogueTreeController controller)
    {
        FPSControl.Talking = true;
        // Release the mouse cursor so you can freely select dialogue options on-screen
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;

        // Trigger NodeCanvas tree and pass a callback to run code when the dialogue ends
        if (playerActor != null)
        {
            controller.StartDialogue(playerActor, OnDialogueFinished);
        }
        else
        {
            controller.StartDialogue(OnDialogueFinished);
        }
    }

    private void OnDialogueFinished(bool success)
    {
        FPSControl.Talking = false;
        // Re-lock the cursor back into your first-person camera center view
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }
}

