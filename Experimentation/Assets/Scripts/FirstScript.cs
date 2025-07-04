using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class FirstScript : MonoBehaviour
{

    float Speed;
    private CharacterController controller;
    private Animator animator;
    private Vector3 playerDirection;
    public AnimationCurve t;

    private Vector3 a;
    private Vector3 b;
    private Vector3 origin;


    public float rotationFactor = 0.2f;

 

    private Quaternion currentRotation;


    // Start is called before the first frame update
    void Start()
    {
        Speed = 10f;

        //controller = GetComponent<CharacterController>();
        //animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //Inputs
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = Quaternion.Euler(0, 45, 0) * move;
        move = Vector3.ClampMagnitude(move, 1f);

        if (move != Vector3.zero)
        {
            //walking process
            transform.forward = move;
            //Quaternion targetRotation = Quaternion.LookRotation(move);
            //transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactor * Time.deltaTime);

            if (move.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(move.normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationFactor * Time.deltaTime);
            }


            currentRotation = transform.rotation;


            Vector3 origin = transform.position;

            //Normalize Vectors
            Vector3 a = Vector3.forward;
            Vector3 b = transform.forward;

            //Ensure Length Preserved
            Vector3 c = ((a + b) / 2f).normalized;


            //Vector3 PlayerDirection = new Vector3(HorizontalInput, 0, VerticalInput);
            //PlayerDirection.Normalize();

            Vector3 pos = transform.position;
            if (Input.GetKey(KeyCode.A))
            {
                pos.z += Speed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.D))
            {
                pos.z -= Speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.W))
            {
                pos.x += Speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                pos.x -= Speed * Time.deltaTime;
            }

            transform.position = pos;


        }
    }
}
