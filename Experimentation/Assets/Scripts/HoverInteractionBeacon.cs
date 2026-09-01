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
    public AudioClip Targetselect;

    private GameObject SelectedIcon;
    public GameObject BeaconGen;
    public StarshipNavManager StarshipNavManager;

    public int BeaconIndex;


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

        AudioSource.clip = BeaconHover;
        AudioSource.Play();
    }

    public void HoverInteractionExit()
    {
        ImageComp.sprite = UnhoveredIcon;
        transform.localScale /= scaleupfactor;
    }

    public void BeaconSelected()
    {

        //communicate with beacon manager... send target to this location -Target locked-
        StarshipNavManager.SelectedBeacon = BeaconIndex;
        StarshipNavManager.PerformJumpCalculations(BeaconIndex);

        //sfx
        AudioSource.clip = Targetselect;
        AudioSource.Play();

        //functions
        BeaconGen.GetComponent<BeaconGenerator>().MoveTarget(transform.position);

    }



}
