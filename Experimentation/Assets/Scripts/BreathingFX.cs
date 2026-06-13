using UnityEngine;
using UnityEngine.Rendering;

public class BreathingFX : MonoBehaviour
{
    BreathingDetector BreathingDetector;
    public Volume Vignette;
    public Volume GlobalFX;
    
    public GameObject blackoutCube;

    bool NoAir;
    bool SlowAir;
    bool MedAir;
    bool HeavyAir;


    float currentVignetteVal;
    float currentBlurVal;
    public float shiftRate;
    public bool isBlackedOut;

    public enum BreathForce
    {
        Slow,
        Med,
        Heavy,
        None
    }
    public BreathForce currentState;

    private void Start()
    {
        BreathingDetector = GetComponent<BreathingDetector>();
        if(BreathingDetector == null)
            Debug.LogError("cannot find breathing detector");
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Vignette WEIGHT "+Vignette.weight);
        NoAir = BreathingDetector.NoAir;
        SlowAir = BreathingDetector.SlowAir;
        MedAir = BreathingDetector.MediumAir;
        HeavyAir = BreathingDetector.HeavyAir;



        if (SlowAir)
        {
            //Debug.Log("Slow Air FX");
            currentVignetteVal = 1;   //0.8-1
            currentBlurVal = 5f;
            currentState = BreathForce.Slow;
        }
        else
            if (MedAir)
            {
                //Debug.Log("Med Air FX");
                currentVignetteVal = 0.1f; //0.4 - 0.6
                currentBlurVal = 0f;
                currentState = BreathForce.Med;
            }
            else
                if (HeavyAir)
                {
                    //Debug.Log("Heavy Air FX");
                    currentVignetteVal = 0.1f; // 0-0.2
                    currentBlurVal = 2f;
                    currentState = BreathForce.Heavy;
                }
                else
                    if (BreathingDetector.NoAir) // if not breathing
                    {
                        Vignette.weight = Mathf.MoveTowards(Vignette.weight, 1, shiftRate);
                    }
        if (Vignette.weight >= 1)
            IsBlackedOut(true);
        else IsBlackedOut(false);

        Vignette.weight = Mathf.MoveTowards(Vignette.weight, currentVignetteVal, shiftRate);
        //ChangeBlur.BlurFromFX(currentBlurVal, shiftRate);
    }
    public bool IsBlackedOut(bool value)
    {
        blackoutCube.SetActive(value);
        isBlackedOut = value;
        return value;
    }
}
