using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Newhope : MonoBehaviour
{

    private Quaternion currentRotation;
    public float rotationFactorPerFrame = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        currentRotation = transform.rotation;

        // Horizontal input
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = Quaternion.Euler(0, 45, 0) * move;
        move = Vector3.ClampMagnitude(move, 1f); 
    

        if (move != Vector3.zero)
        {
            //walking
            //animator.SetBool("isWalking", true);
            transform.forward = move;
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }

    }
}
