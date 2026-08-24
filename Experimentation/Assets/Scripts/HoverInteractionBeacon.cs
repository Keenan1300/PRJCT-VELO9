using UnityEngine;
using UnityEngine.UI;

public class HoverInteractionBeacon : MonoBehaviour
{
    public Sprite UnhoveredIcon;
    public Sprite HoveredIcon;
    public Image ImageComp;
    public float scaleupfactor;
    //public float defaultscale;

    public void Start()
    {
        ImageComp = GetComponent<Image>();
    }

    public void HoverInteractionEnter() 
    {
        ImageComp.sprite = HoveredIcon;
        transform.localScale *= scaleupfactor;
    }

    public void HoverInteractionExit()
    {
        ImageComp.sprite = UnhoveredIcon;
        transform.localScale /= scaleupfactor;
    }

}
