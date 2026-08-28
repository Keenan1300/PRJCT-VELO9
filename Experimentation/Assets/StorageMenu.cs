using StarterAssets;
using UnityEditor;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StorageMenu : MonoBehaviour
{

    public Button EscapeButton;
    public Button Eject;
    public GameObject Menu;
    public GameObject Player;
    public CargoData CargoData;

    //UI
    public TextMeshProUGUI CargoDescription;
    public TextMeshProUGUI CargoName;


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


    //Talk to data value selected, display in text
    public void DataValuesToText(CargoData CargoData)
    {
        //Debug if fail to grab cargo data
        if (CargoData == null)
        {
            CargoName.text = "No Object data";
            CargoDescription.text = "No Object description";
            return;
        }

        CargoName.text = CargoData.CargoName;
        CargoName.text = CargoData.Description;

    }

}
