using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Newhope : MonoBehaviour
{

    private CharacterController controller;

    public float rotationSpeed = 720f; // degrees per second
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Raw keyboard input (WASD or arrows)
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 inputDirection = new Vector3(h, 0f, v).normalized;

        // Only move if there's input
        if (inputDirection.sqrMagnitude > 0.01f)
        {
            // Rotate toward movement direction (world-relative)
            Quaternion targetRotation = Quaternion.LookRotation(inputDirection);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        // Movement
        Vector3 move = inputDirection * moveSpeed;
        velocity.y += gravity * Time.deltaTime;

        controller.Move((move + velocity) * Time.deltaTime);

        // Reset gravity when grounded
        if (controller.isGrounded)
        {
            velocity.y = 0f;
        }
    }
}

