using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;

public class PilotStation : MonoBehaviour
{

    // popups
    public GameObject Popup;
    public GameObject Menu;
    public Collider Collider;
    public  bool inrange;
    public UnityEvent FreezeLook;
    public AudioSource ConsoleOpenNoise;
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Collider = GetComponent<Collider>();
        ConsoleOpenNoise = GetComponent<AudioSource>();
        Popup.SetActive(false);
        inrange = false;
        Menu.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        
        Popup.SetActive(true);
        inrange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        Popup.SetActive(false);
        inrange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if( inrange == true && Input.GetKey(KeyCode.E))
        {
            FreezeLook.Invoke();
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            ConsoleOpenNoise.Play();
            Menu.SetActive(true);
           

        }
    }
}
