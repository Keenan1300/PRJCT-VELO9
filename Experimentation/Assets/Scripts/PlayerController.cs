using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    private CharacterController CharacterController;
    private Vector3 velocity;

    public float characterspeed = 5, rotatespeed = 5;

    private float _rotationY;

    public float moveSpeed = 5;
    public float gravity = -0;
    public float groundedGravity = 1;

    // Start is called before the first frame update
    void Start()
    {
        Input.ResetInputAxes();
        CharacterController = GetComponent<CharacterController>();
    }

    private void Update()
    {

        bool isGrounded = CharacterController.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            // Keep character pinned to the ground without sliding
            velocity.y = groundedGravity;
        }

    }
 

// Update is called once per frame
public void Move( Vector2 movementVector)
    {
        Vector3 move = transform.forward * movementVector.y + transform.right * movementVector.x;
        move = move * characterspeed * Time.deltaTime;
        CharacterController.Move(move);
    }


    public void Rotate(Vector2 rotationVector)
    {
        _rotationY += rotationVector.x * rotatespeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0, _rotationY, 0);
    }
}
