using TMPro;
using UnityEngine;

public class TextContainer : MonoBehaviour
{
    //public GameObject popup;
    public TextMeshProUGUI DescriptiveText;



    public void ChangeText(TraitData trait)
    {
      
        DescriptiveText.text = trait.name;
    }
    // Update is called once per frame

}
