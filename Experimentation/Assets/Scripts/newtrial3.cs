using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class newtrial3 : MonoBehaviour
{
    private CharacterController controller;
    public Vector3 rotation;
    public float rotationSpeed = 0.01f;
    public Vector3 velocity;
    private Quaternion currentRotation;
    
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        

        //float Step = Time.deltaTime * rotationSpeed;

        currentRotation = transform.rotation;

        //Control inputs WASD
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        

        //rotation bs
        //move = Quaternion.Euler(0, 45, 0) * move;
        //move = move.normalized;

        move = Vector3.ClampMagnitude(move, 1f);




        //Vector3 targetForwardDirection = velocity;

        //get the rotation that corresponds to facing in the direction of the velocity
        //Quaternion targetRotation = Quaternion.LookRotation(targetForwardDirection);

        //explicity set the rotation of the rigidbody
        //rb.MoveRotation(targetRotation);




        //Vector3 moveDirection = new Vector3(move.x, 0, move.z).normalized;


        //if youre moving, aka when youre not still
        if (move != Vector3.zero)
        {
            controller.Move(move);
            Quaternion targetRotation = Quaternion.LookRotation(move);
            //rb.MoveRotation(targetRotation);

            // Smoothly rotate towards target
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime * 100);

            transform.Rotate(0, Input.GetAxisRaw("Horizontal"), 0);
        }

       }
}
