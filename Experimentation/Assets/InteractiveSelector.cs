using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugManager;
using static UnityEngine.UI.Image;

public class InteractiveSelector : MonoBehaviour
{


    //Currently selected object;
    [Header("Selected Object")]
    public string CurrentSelection;

    //Selectable Objects
    private string NPC;
    private string Console;

    public GameObject CrossH;
    public float Distance;

    [Header("Cursor Data")]
    //SpriteChanger
    private Image CursorImage;

    //Talking Sprite
    public Sprite TalkingSprite;

    //Default Sprite
    public Sprite DefualtSprite;

    [Header("Game Mode Data")]
    //Game Mode
    public bool UImode;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UImode = false;
        CurrentSelection = null;

    }

    // Update is called once per frame
    void Update()
    {
        if (UImode == false)
        {


            CursorImage = CrossH.GetComponent<Image>();

            Vector3 Crosshairspot = CrossH.transform.position;

            //define hit variable
            RaycastHit hit;

            //shoot laser, see whats hit
            if (Physics.Raycast(CrossH.transform.position, CrossH.transform.forward, out hit, Distance))
            {

                string hitTag = hit.collider.tag;

                if (hitTag != null)
                {

                    Debug.Log($"Cursor is Hitting: {hit.collider.tag}");
                    CurrentSelection = hitTag;

                    if (CurrentSelection == "NPC")
                    {
                        CursorImage.sprite = TalkingSprite;
                    }
                    else
                    {
                        CursorImage.sprite = DefualtSprite;
                    }
                }
             
            }
            else
            {
                CursorImage.sprite = DefualtSprite;
            }
        } 
    }
}
