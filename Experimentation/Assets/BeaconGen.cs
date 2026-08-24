using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class BeaconGen : MonoBehaviour
{

    /// <summary>
    /// 
    /// 
    /// DO NOT USE THIS. SCRIPT CAUSES STACK OVERFLOW
    /// IT REMAINS HERE FOR DATA PURPOSES ONLY
    /// </summary>
    //public List<int> beacons = new List<int>();

    public List<Vector2> beacons = new List<Vector2>();
    public GameObject BeaconIcon;
    public int BeaconCount;
    public Transform BeaconOrigin;
    public float SpaceBetweenBeacons;
    public int SafetyCount = 3;
    public float squaredMinGap;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //create Random amount of beacons
        BeaconCount = Random.Range(12, 20);

        for (int i = 0; i <= BeaconCount; i++)
        {
            //Generate randomposition
            Vector2 randompos = new Vector2(Random.Range(0, 70), Random.Range(0, 70));

            //take the random pos, filter them for acceptable spot placement... Output beacon position
            GeneratePos(in beacons, in i, in randompos, out Vector2 BeaconPos);

            beacons.Add(BeaconPos);

            Vector3 spawnPosition = BeaconOrigin.transform.position + new Vector3(BeaconPos.x, BeaconPos.y, 0);
            GameObject clonedObject = Instantiate(BeaconIcon, spawnPosition, Quaternion.identity);

            //Instantiate(BeaconIcon, BeaconOrigin.transform.position + new Vector3(BeaconPos.x, BeaconPos.y, 0), Quaternion.identity);

            //fix parent issue
            clonedObject.transform.SetParent(this.gameObject.transform, false);
            clonedObject.transform.localPosition = BeaconOrigin.transform.localPosition + new Vector3(BeaconPos.x, BeaconPos.y, 0);

            //clonedObject.transform.SetParent(targetParent);

        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    void GeneratePos(in List<Vector2> beacon, in int BeaconIndex, in Vector2 FirstRandompos, out Vector2 BeaconPos)
    {
        //If there are more than one beacon. . .
        if (beacon.Count > 0 && SafetyCount > 0)
        {

            // find distance difference from previous and current beacon
            //for loop runs to ensure current beacon isnt made too close to any previous beacons.

            foreach (Vector2 ExistingPos in beacons)
            {
                float Sqrdist = (FirstRandompos - ExistingPos).sqrMagnitude;

                if (Sqrdist < squaredMinGap)
                {
                    //isValidPosition = false; // Too close! Break out and generate a new point
                    break;
                }


            }

        }
        //there isnt more than 1 beacon so far, allow it to keep its random start.
        BeaconPos = FirstRandompos;
        return;

    }
}

//Problem in current script
//Stack overflow... for loop runs potentially infinite times. Will need safety net
//Potential issue can be caused by oversized 'beacon boundary' radius. Thus, reducing available spots for spawn
//Getting data overflow errors, must reduce.