using UnityEngine;
using UnityEngine.UI;

public class Cursor : MonoBehaviour
{
    public RawImage CursorImage;

    //Cursor Images
    public Texture HuntingCursor;
    public Texture DefaultCursor;

    //Camera Settings
    public Camera mainCamera;

    //Adjust Mouse
    public float CursorX;
    public float CursorY;


    Vector3 mousepoint;

    public float followSpeed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CursorImage.texture = HuntingCursor;
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 mouseScreenPosition = Input.mousePosition;

        mouseScreenPosition.z = mainCamera.nearClipPlane;

        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);

        mouseWorldPosition.x += CursorX;
        mouseWorldPosition.y += CursorY;

        transform.position = mouseWorldPosition;

    }

    //Change Cursor Function

    public void SwitchToHuntingMode() 
    {
       CursorImage.texture = HuntingCursor;
    }

    public void SwitchToDefaultMode()
    {
        CursorImage.texture = DefaultCursor;
    }

}
