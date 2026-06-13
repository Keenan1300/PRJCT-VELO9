using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBreathing : MonoBehaviour
{
    // input references
    Scrolling ScrollInputScript;
    [SerializeField] float breathVelocity;
    float lastScrollUpOrDown;

    [Header("Breathing Values")]
    public bool breathingIn;
    public bool breathingOut;
    public int breathCycle;
    [SerializeField] float breathCapacity;
    public cycleState currentState;

    [Header("Breath Conditions")]
    int breathMin = 0;
    public int breathMax;
    public int breathFillRate;
    public enum cycleState
    {
        None,
        Inhaling,
        HoldingIn,
        Exhaling,
        HoldingOut
    }

    [Header("Plant Activation")]
    public float PlantActivationRange = 5;
    public Color gizmoColorIfFound;
    public Color gizmoColorIfNotFound;
    Color activeGizmoColor;


    private void Start()
    {
        ScrollInputScript = GetComponent<Scrolling>();

        if (ScrollInputScript == null)
            Debug.LogError("input not found");

        breathCycle = 0;
        currentState = cycleState.None;
    }
    private void Update()
    {
        switch (currentState)
        {
            case cycleState.Inhaling:
                Inhale();
                break;
            case cycleState.HoldingIn:
                Holding();
                break;
            case cycleState.Exhaling:
                Exhale();
                break;
            case cycleState.HoldingOut:
                Holding();
                break;
            case cycleState.None:
                // if they havent inhaled yet, what do we do>?
                break;
        }
        BreathCheck();
        //Debug.Log("breath is at: " +  breathCapacity);
    }

    void ListenForScrolling()
    {
        float scrollUpOrDown = Mathf.Sign(ScrollInputScript.scrollvelocity); // positive if up, negative if down
        if (lastScrollUpOrDown == scrollUpOrDown)
        {
            breathingIn = false;
            breathingOut = false;
            return;
        }

        if (scrollUpOrDown > 0) // if up
        {
            breathingIn = true;
        }
        else
            breathingIn = false;

        if (scrollUpOrDown < 0) // if down
            breathingOut = true;
        else breathingOut = false;
        lastScrollUpOrDown = scrollUpOrDown;
    }

    void Inhale()
    {
        //Debug.Log("breathing in");
        if (breathCapacity < breathMax)
            breathCapacity += Time.deltaTime * breathFillRate;
        breathCapacity = Mathf.Clamp(breathCapacity, breathMin, breathMax);
        return;
    }
    void Exhale()
    {
        //Debug.Log("breathing out");
        if (breathCapacity >= breathMin)
            breathCapacity -= Time.deltaTime * breathFillRate;
        breathCapacity = Mathf.Clamp(breathCapacity, breathMin, breathMax);
        return;
    }

    void Holding()
    {
        float breathCapPercent = (breathCapacity / breathMax); // 0-1 where 0 is no air and 1 is full lungs

    }

    void BreathCheck()
    {
        ListenForScrolling();

        //Debug.Log("breath in is "+breathingIn+ " breath out is "+ breathingOut);

        cycleState lastCycle = currentState;
        //// exit clause in case player is pressing both. keeps the last updated state as a result.
        //if (currentState == cycleState.None && !breathingIn)
        //    return;

        if (!breathingIn && !breathingOut) // holds breath if not actively breathing in or out, unless they're exhaling
        {
            if (lastCycle == cycleState.Inhaling)
                currentState = cycleState.HoldingIn;
            if (lastCycle == cycleState.Exhaling)
                currentState = cycleState.HoldingOut;
        }

        if (breathingIn && !breathingOut) // if breathing in and not breathing out (Inhale)
            currentState = cycleState.Inhaling;

        else if (breathingOut && currentState == cycleState.HoldingIn) //if breathing out and not breathing in (Exhale)
            currentState = cycleState.Exhaling;

        //Debug.Log("current state is "+ currentState);

        if (lastCycle == cycleState.HoldingOut && currentState == cycleState.Inhaling) // trigger new cycle
            IncrementBreathCycle();
    }

  

    private void OnDrawGizmos()
    {
        Gizmos.color = Application.isPlaying ? activeGizmoColor : new Color(1f, 1f, 1f, .5f);
        Gizmos.DrawSphere(transform.position, PlantActivationRange);
    }

    void IncrementBreathCycle()
    {
        breathCycle++;
        //Debug.Log("breath cycle: " + breathCycle);
    }
}
