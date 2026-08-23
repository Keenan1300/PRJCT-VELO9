using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class BeaconGen : MonoBehaviour
{


    //public List<int> beacons = new List<int>();

    public List<Vector2> beacons = new List<Vector2>();
    public GameObject BeaconIcon;
    public int BeaconCount;
    public Transform BeaconOrigin;
    public float SpaceBetweenBeacons;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BeaconCount = Random.Range(12, 20);
        for (int i = 0; i <= BeaconCount; i++)
        {
            Vector2 randompos = new Vector2(Random.Range(0, 70), Random.Range(0, 70));

            //take these random coordinates, filter them for acceptable spot placement
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
        if (beacon.Count > 0)
        {

            // find distance difference from previous and current beacon
            //need for loop here so that present beacon doesnt go too close to any other beacon before it (current logic only accounts for 2nd most recent beacon)

            for (int i = 1; i < beacon.Count; i++)
            {
                float dist = (FirstRandompos - beacon[BeaconIndex - i]).magnitude;

                if (dist < SpaceBetweenBeacons)
                {

                    //if beacon is too close, we want the NEW beacon to go to a new spot
                    Vector2 NewRandompos = new Vector2(Random.Range(0, 70), Random.Range(0, 70));
                    GeneratePos(in beacons, in BeaconIndex, in NewRandompos, out Vector2 FixedBeaconPos);
                    BeaconPos = FixedBeaconPos;

                }
                BeaconPos = FirstRandompos;
            }

        }
        // If present beacon is within X distance of previous beacon regenerate present beacon. Repeat this as long as there are now 2 becons too close to each other.
        else
        {
            BeaconPos = FirstRandompos;
            return;
        }

            BeaconPos = FirstRandompos;
            return;
        }
    }

