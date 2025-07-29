using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Here : MonoBehaviour
{
    //separate rotation from movement
    private Transform aimTarget;

    private float moveSpeed = 17;
    private float gravity = 0;
    private float groundedGravity = 0;
    private Vector3 moveDir;
    bool equipped;

    //move2 variables
    private float smoothtime = 0.05f;
    private float currentVelocity;

    //character controller setup
    private CharacterController controller;
    private Vector3 velocity;

    //bugfixing
    private bool snapOverride = false;

    void Start()
    {   
        //establish first move mode
        equipped = true;


        Input.ResetInputAxes();

        controller = GetComponent<CharacterController>();

        //rotation stuff
        aimTarget = transform.Find("Aimer");

        if (aimTarget == null)
            Debug.LogWarning("Aimer child not found!");
    }

    void Update()
    {

       


        bool isGrounded = controller.isGrounded;

        //Movement Equipped 1
        if (equipped)
        {
            if (isGrounded && velocity.y < 0)
            {
                // Keep character pinned to the ground without sliding
                velocity.y = groundedGravity;
            }

            float x = 0;
            if (Input.GetKey(KeyCode.A)) x = -1;
            if (Input.GetKey(KeyCode.D)) x = 1;

            float z = 0;
            if (Input.GetKey(KeyCode.W)) z = 1;
            if (Input.GetKey(KeyCode.S)) z = -1;


            Vector3 move = transform.right * x + transform.forward * z;

            //no wierd Diagonal crap
            if (move.magnitude > 1f)
                move = move.normalized;


            controller.Move(move * moveSpeed * Time.deltaTime);




            // Apply gravity
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);



            //Try move rotate towards mouse

            if (aimTarget != null)
            {
                //once aim target is found, perform physics ray cast operation, which connects mouse position to camera screenpoint
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


                if (Physics.Raycast(ray, out RaycastHit hit, 100f))
                {
                    Vector3 lookPoint = hit.point;
                    lookPoint.y = aimTarget.position.y;

                    Vector3 direction = lookPoint - aimTarget.position;

                    if (direction.sqrMagnitude > 0.01f)
                    {
                        Quaternion lookRotation = Quaternion.LookRotation(direction);
                        aimTarget.rotation = Quaternion.Slerp(aimTarget.rotation, lookRotation, 10f * Time.deltaTime);
                    }
                }

            }
        }


        //Swap To MovementMode 2

        if (!equipped)
        {
            print("mode2!!");


            if (isGrounded && velocity.y < 0)
            {
                // Keep character pinned to the ground without sliding
                velocity.y = groundedGravity;
            }


            //Input Detection
            float x = 0;
            if (Input.GetKey(KeyCode.A)) x = -1;
            if (Input.GetKey(KeyCode.D)) x = 1;

            float z = 0;
            if (Input.GetKey(KeyCode.W)) z = 1;
            if (Input.GetKey(KeyCode.S)) z = -1;


            Vector3 move = transform.right * x + transform.forward * z;

            //no weird Diagonal crap
            if (move.magnitude > 1f)
                move = move.normalized;


            // Apply gravity
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
            


            //calculate direction
            Vector3 moveDir = new Vector3(x, 0, z);
            moveDir.Normalize();


            if (moveDir != Vector3.zero)
            {
                var targetangle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
                var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref currentVelocity, smoothtime);

                transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
            }

               //process movement calculations
                controller.Move(moveDir * moveSpeed * Time.deltaTime);
            

        }


        //Toggle Between Movement Modes
        if ((Input.GetKeyDown(KeyCode.E)))
        {
            if (equipped)
            {

                equipped = false;
                transform.LookAt(new Vector3(52, 0, 0));
                //transform.rotation = Quaternion.Euler(0, 52, 0);
            }

            else if (!equipped)
            {
                transform.rotation = Quaternion.identity;
                equipped = true;
                

            }

        }

    }
            


    
}