using Unity.VisualScripting;
using UnityEngine;

public class TraitDescriptionHover : MonoBehaviour
{

    public TraitData Tdata;
    public GameObject DescriptionBox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void HoverBox()
    {

        DescriptionBox.SetActive(true);
        DescriptionBox.GetComponent<TextContainer>().ChangeText(Tdata);
    
    }

    public void HideHoverBox()
    {


        DescriptionBox.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
