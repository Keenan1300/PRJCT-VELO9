using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class BeaconGen : MonoBehaviour
{


    //public List<int> beacons = new List<int>();

    public List<int> beacons = new List<int>();
    
    public int BeaconCount = Random.Range(12,20);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i <= BeaconCount; i++)
        {
            beacons.Add(1);
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
