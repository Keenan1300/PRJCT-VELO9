using StarterAssets;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    float lastscrolltime = 0f;
    public  float scrollvelocity = 0f;
    StarterAssetsInputs StarterAssetsInputs;

    void Start()
    {
        StarterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }
    void Update()
    {
        if (StarterAssetsInputs.scroll != 0f)
        {
            float timesincelast = Time.time - lastscrolltime;
                scrollvelocity = StarterAssetsInputs.scroll / timesincelast;
            lastscrolltime = Time.time;
            //chud debug script cause im so lost -.-
            //Debug.Log(scrollvelocity);
        }
    }
}

