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

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 200f))
        {
            Vector3 targetPos = hitInfo.point;
            targetPos.y = transform.position.y; // Keep only horizontal rotation

            Vector3 direction = targetPos - transform.position;

            if (direction.sqrMagnitude > 0.01f) // Avoid NaN when standing still
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
            }
        }
    }
}
