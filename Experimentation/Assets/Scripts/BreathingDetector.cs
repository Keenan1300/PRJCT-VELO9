using StarterAssets;
using UnityEngine;

public class BreathingDetector : MonoBehaviour
{
    Scrolling scrolling;
    //float lastbreathtime = 0f;
    //fix filler values later
    [Header("Breath Deadzones")]
    public float lightBreath = 10f;
    public float mediumBreath = 200f;
    public float heavyBreath = 1000f;

    [Header("Decay Rate")]
    public float decaySpeed = 1.5f;

    [Header("Active Values")]
    public float breathStrength = 0f;
    public float timeBetweenBreaths = 0f;
    public float BreathingIntensity = 0f;

    [Header("Breathing State")]
    public bool NoAir = false;
    public bool SlowAir = false;
    public bool MediumAir = false;
    public bool HeavyAir = false;

    float inOrOutLast = 0f;
    float lastProperBreath = 0f;
    //float noAirTimer = 0f;
    //public float noAirDelay = 2f;
    void Start()
    {
        scrolling = GetComponent<Scrolling>();
    }

    void Update()
    {
        float velocity = scrolling.scrollvelocity;

        if (velocity != 0f)
        {
            breathStrength = Mathf.Abs(velocity) / lightBreath;

            float inOrOutNow = Mathf.Sign(velocity);
            if (inOrOutNow != inOrOutLast && inOrOutLast != 0f)
            {
                timeBetweenBreaths = Time.time - lastProperBreath;
                float intervalScore = Mathf.Clamp(5f / timeBetweenBreaths, 1f, 10f);
                float strengthScore = Mathf.Clamp(breathStrength, 1f, 10f);
                BreathingIntensity = (intervalScore + strengthScore) / 2f;
                lastProperBreath = Time.time;
                //Debug.Log($"time between breaths{timeBetweenBreaths} breat  h strength {breathStrength}");
            }
            //THIS IS HERE ON PURPOSE
            inOrOutLast = inOrOutNow;
        }
        else
        {
            breathStrength = Mathf.MoveTowards(breathStrength, 0f, Time.deltaTime * decaySpeed);
        }

        BreathingIntensity = Mathf.MoveTowards(BreathingIntensity, 0f, Time.deltaTime * decaySpeed);


        if (BreathingIntensity <= 0f)
        {
            NoAir = true; SlowAir = false; MediumAir = false; HeavyAir = false;
            // noAirTimer += Time.deltaTime;
            //timer incase we want delay n stuff :3
            // if (noAirTimer >= noAirDelay) { NoAir = true; SlowAir = false; MediumAir = false; HeavyAir = false; }
        }
        else if (BreathingIntensity < 3f)
        {
            NoAir = false; SlowAir = true; MediumAir = false; HeavyAir = false;
        }
        else if (BreathingIntensity >= 3f && BreathingIntensity < 7f)
        {
            NoAir = false; SlowAir = false; MediumAir = true; HeavyAir = false;
        }
        else if (BreathingIntensity >= 6f)
        {
            NoAir = false; SlowAir = false; MediumAir = false; HeavyAir = true;
        }
    }

    public bool IsBreathing()
    {
        if (scrolling.scrollvelocity != 0)
            return true;
        else return false;
    }
}