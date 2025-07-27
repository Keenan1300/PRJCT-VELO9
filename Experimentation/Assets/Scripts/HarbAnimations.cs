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
        anim = GetComponent<Animator>();
        anim.SetBool("Idle", true);
        anim.SetBool("RightStrafe", false);
        anim.SetBool("LeftStrafe", false);
        anim.SetBool("BackStrafe", false);
        anim.SetBool("Run", false);
        anim.SetBool("Grab", false);
        anim.SetBool("GunFire", false);
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
