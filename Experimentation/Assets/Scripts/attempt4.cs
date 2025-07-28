using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attempt4 : MonoBehaviour
{
    private Transform aimTarget;

    //Custom Vars
    private float moveSpeed = 17;
    private float gravity = 0;
    private float groundedGravity = 0;
    private Vector3 moveDir;

    //Char controllers
    private CharacterController controller;
    private Vector3 velocity;


    // Start is called before the first frame update
    void Start()
    {
        Input.ResetInputAxes();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isGrounded = controller.isGrounded;


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
;
        //calculate direction
        Vector3 moveDir = new Vector3(x, 0, z);
        moveDir.Normalize();

        var targetangle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, targetangle, 0.0f);

        controller.Move(moveDir * moveSpeed * Time.deltaTime);


    }


}
