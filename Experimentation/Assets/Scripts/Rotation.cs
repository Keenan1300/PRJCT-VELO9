using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    Vector3 rot;
    float Speed;
    float inc;
    float armspeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        Speed = 0.09f;
        inc = 25;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rot = transform.rotation;

        if (Input.GetKey(KeyCode.A))
        {
            if (rot.y < 0)
            {
                rot.y += armspeed;
            }
            else
            {
                rot.y = 0;
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (rot.y < 180)
            {
                rot.y -= armspeed;
            }
            else
            {
                rot.y = 180;
            }


            
        }
        if (Input.GetKey(KeyCode.W))
        {
            if (rot.y < 90)
            {
                rot.y -= armspeed;
            }
         
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (rot.y == -90)
            {
                rot.y = rot.y * -1;
            }

            rot.y -= armspeed;
        }

        transform.rotation = rot;
    }
}
