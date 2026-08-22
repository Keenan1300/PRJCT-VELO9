using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class BeaconGen : MonoBehaviour
{


    //public List<int> beacons = new List<int>();

    public List<Vector2> beacons = new List<Vector2>();
    
    public int BeaconCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BeaconCount = Random.Range(12, 20);
        for (int i = 0; i <= BeaconCount; i++)
        {
            Vector2 randompos = new Vector2(Random.Range(0, 100), Random.Range(0, 100));

            //take these random coordinates, filter them for acceptable spot placement
            GeneratePos(in beacons, in i, in randompos, out Vector2 BeaconPos);

            beacons.Add(BeaconPos);
            
           
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
            float dist = (FirstRandompos - beacon[BeaconIndex - 1]).sqrMagnitude;



            // If present beacon is within X distance of previous beacon regenerate present beacon. Repeat this as long as there are now 2 becons too close to each other.

            if (dist < 0.2f)
            {

                //if beacon is too close, we want the NEW beacon to go to a new spot
                Vector2 NewRandompos = new Vector2 (Random.Range(0,100), Random.Range(0,100));
                GeneratePos(in beacons, in BeaconIndex, in NewRandompos, out Vector2 FixedBeaconPos);
                
            }
            else 
            {
              BeaconPos = FirstRandompos; 
                return;
            }


        }
        else 
        { 
            BeaconPos = new Vector2(Random.Range(0,100),Random.Range(0,100));
            return; 
        }


        BeaconPos = FirstRandompos;
        return ;
    }
}
