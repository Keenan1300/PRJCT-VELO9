using UnityEngine;

public class HarbAnimations : MonoBehaviour
{
    //separate rotation from movement
    private Transform aimTarget;

    private float moveSpeed = 17;
    private float gravity = 0;
    private float groundedGravity = 0;

    private CharacterController controller;
    private Vector3 velocity;

    public Animator Anim; 

    void Start()
    {
      
    }

    void Update()
    {
        bool isGrounded = controller.isGrounded;

        if ((Input.GetKey(KeyCode.W))
        {
            // Keep character pinned to the ground without sliding
            velocity.y = groundedGravity;
        }

        if ((Input.GetKey(KeyCode.D))
        {
            // Keep character pinned to the ground without sliding
            velocity.y = groundedGravity;
        }

        if ((Input.GetKey(KeyCode.A))
        {
            // Keep character pinned to the ground without sliding
            velocity.y = groundedGravity;
        }

        if ((Input.GetKey(KeyCode.S))
        {
            // Keep character pinned to the ground without sliding
            velocity.y = groundedGravity;
        }



    }
    }
}
