using StarterAssets;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class NavMenu : MonoBehaviour

{

    public Button EscapeButton;
    public Button JumpButton;
    public GameObject Menu;
    public GameObject Player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void lockedmousemode()
    {
      
        
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    public void unlockdmousemode()
    {

        UnityEngine.Cursor.lockState = CursorLockMode.None;
    }
}
