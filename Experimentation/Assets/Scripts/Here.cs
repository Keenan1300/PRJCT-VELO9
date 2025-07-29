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

    //ResetDirectionForSwitch
    public Vector3 Reset = new Vector3(0, 52, 0);

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
                velocity.y = groundedGravity;
            }

            //Rotation: rotate aimer toward mouse
            if (aimTarget != null)
            {
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

                    

                    //Movement relative to mouse
                    Vector3 toMouse = (lookPoint - transform.position).normalized;
                    toMouse.y = 0f;
                    Vector3 rightDir = Vector3.Cross(Vector3.up, toMouse);


                    //its important to not normalize distance to mouse detection
                    float distanceToMouse = (lookPoint - transform.position).magnitude;

                    //bugfix
                    print("Distance to mouse: " + distanceToMouse);

                    //AutoExit Mode if player places cursor ontop of avatar
                    if (distanceToMouse < 5f)
                    {
                        equipped = false;
                    }

                        // Input
                        float x = 0;
                    if (Input.GetKey(KeyCode.A)) x = -1;
                    if (Input.GetKey(KeyCode.D)) x = 1;

                    //W key only works if magnitude between mouse and player reaches certain minimum
                    float z = 0;
                    if (Input.GetKey(KeyCode.W)) z = 1;
                    if (Input.GetKey(KeyCode.S)) z = -1;

                    // Movement direction relative to mouse position
                    Vector3 moveDir = (toMouse * z + rightDir * x);

                    if (moveDir.magnitude > 1f)
                        moveDir = moveDir.normalized;

                    controller.Move(moveDir * moveSpeed * Time.deltaTime);
                }

            }

            // Apply gravity separately
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
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
            


            //calculate direction
            Vector3 moveDir = new Vector3(x, 0, z);
            moveDir.Normalize();


            
            
            if (!equipped && moveDir != Vector3.zero)
            {
                float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;

                // Snap to nearest 45 instead of 90
                float snappedAngle = Mathf.Round(targetAngle / 45f) * 45f;

                // Create target rotation
                Quaternion targetRotation = Quaternion.Euler(0f, snappedAngle, 0f);

                // Smoothly rotate toward the snapped direction
                aimTarget.rotation = Quaternion.RotateTowards(aimTarget.rotation, targetRotation, 360f * Time.deltaTime);

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


                // Reset aimer's rotation to face world north
                //Vector3 lookTarget = aimTarget.position + Vector3.forward;
                //aimTarget.LookAt(lookTarget);

                print("Switched to Mode 2, using LookAt on Aimer!");

              
            }

            else if (!equipped)
            {
                equipped = true;
                

            }

        }

    }
            


    
}