using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class FirstScript : MonoBehaviour
{
    float Speed;


    // Start is called before the first frame update
    void Start()
    {
        Speed = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        if (Input.GetKey(KeyCode.A))
        {
            pos.x -= Speed * Time.deltaTime; 
        }

        if (Input.GetKey(KeyCode.D))
        {
            pos.x += Speed * Time.deltaTime; 
        }
        if (Input.GetKey(KeyCode.W))
        {
            pos.z += Speed * Time.deltaTime; 
        }
        if (Input.GetKey(KeyCode.S))
        {
            pos.z -= Speed * Time.deltaTime; 
        }

        transform.position = pos;
        

    }
}
