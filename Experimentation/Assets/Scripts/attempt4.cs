using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attempt4 : MonoBehaviour
{
    float Speed = 0.02f;


    // Start is called before the first frame update
    void Start()
    {    

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 movementDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        movementDir.Normalize();
     
        transform.Translate(movementDir * Speed * Time.deltaTime, Space.World);


        if (movementDir != Vector3.zero)
        {
            transform.forward = movementDir;
        }
        
    }


}
