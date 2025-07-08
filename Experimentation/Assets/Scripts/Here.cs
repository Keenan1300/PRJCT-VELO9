using UnityEngine;

public class Here : MonoBehaviour
{
    private float moveSpeed = 30;
    private float gravity = 0;
    private float groundedGravity = 0;

    private CharacterController controller;
    private Vector3 velocity;

    void Start()
    {
        Input.ResetInputAxes();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        bool isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            // Keep character pinned to the ground without sliding
            velocity.y = groundedGravity;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
