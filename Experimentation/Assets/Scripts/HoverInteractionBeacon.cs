using UnityEngine;
using UnityEngine.UI;

public class HoverInteractionBeacon : MonoBehaviour
{
    public Sprite UnhoveredIcon;
    public Sprite HoveredIcon;
    public Image ImageComp;
    public float scaleupfactor;
    private AudioSource AudioSource;
    public AudioClip BeaconHover;
    //public float defaultscale;

    public void Start()
    {
        ImageComp = GetComponent<Image>();
        AudioSource = GetComponent<AudioSource>();  
    }

    public void HoverInteractionEnter() 
    {
        ImageComp.sprite = HoveredIcon;
        transform.localScale *= scaleupfactor;
        AudioSource.Play();
    }

    public void HoverInteractionExit()
    {
        ImageComp.sprite = UnhoveredIcon;
        transform.localScale /= scaleupfactor;
    }

    public void BeaconSelected()
    {
        //Target locked

    }

}
