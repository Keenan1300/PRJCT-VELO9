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


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        controller = GetComponent<CharacterController>();

        //float Step = Time.deltaTime * rotationSpeed;

        currentRotation = transform.rotation;

        //Control inputs WASD
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        controller.Move(move);

        //rotation bs
        //move = Quaternion.Euler(0, 45, 0) * move;
        //move = move.normalized;

        move = Vector3.ClampMagnitude(move, 1f);



        //Vector3 moveDirection = new Vector3(move.x, 0, move.z).normalized;


        //if youre moving, aka when youre not still
        if (move != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);

            // Smoothly rotate towards target
            //transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime * 100);

            transform.Rotate(0, Input.GetAxisRaw("Horizontal"), 0);
        }

       }
}
