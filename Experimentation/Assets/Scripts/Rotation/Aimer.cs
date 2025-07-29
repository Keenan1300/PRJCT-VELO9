using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Aimer : MonoBehaviour
{

    bool equipped;
    private Transform aimTargetALT;


    //ResetDirectionForSwitch
    public Vector3 Reset = new Vector3(0, 52, 0);


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        aimTargetALT = transform.Find("AimerALT");
    }

    // Update is called once per frame
    void Update()
    {

        //Toggle Between Movement Modes
        if ((Input.GetKeyDown(KeyCode.E)))
        {


            if (equipped)
            {

                equipped = false;

                //Vector3 lookTarget = aimTargetALT.position;

                //Vector3 direction = lookTarget - transform.position;
                //transform.LookAt(lookTarget);

                //Quaternion lookRotation = Quaternion.LookRotation(Reset);
                //transform.rotation = Quaternion.Euler(0, 52, 0);
            }

            else if (!equipped)
            {
                equipped = true;


            }

        }
    }
}
