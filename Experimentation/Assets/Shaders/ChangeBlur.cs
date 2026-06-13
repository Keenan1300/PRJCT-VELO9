using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeBlur : MonoBehaviour
{

    public Material blurMaterial;
    public float blurIncrement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        blurIncrement = 0;
        blurMaterial.SetFloat("_BlurMultiplier", 0);

    }

    private void OnDestroy()
    {
        blurMaterial.SetFloat("_BlurMultiplier", 0);
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            blurIncrement += 0.2f;
            blurMaterial.SetFloat("_BlurMultiplier", blurIncrement );
            Debug.Log("beans");
        }

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            blurIncrement -= 0.2f;
            blurMaterial.SetFloat("_BlurMultiplier", blurIncrement);
           
        }
    }
    public void BlurFromFX(float increment, float rate)
    {
        blurIncrement = Mathf.MoveTowards(blurIncrement,increment, rate);
        blurMaterial.SetFloat("_BlurMultiplier", blurIncrement);
    }
}
