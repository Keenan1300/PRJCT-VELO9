using UnityEngine;

public class PilotStation : MonoBehaviour
{

    // popups
    public GameObject Popup;
    public Collider Collider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Collider = GetComponent<Collider>();
        Popup.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        
        Popup.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        Popup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
