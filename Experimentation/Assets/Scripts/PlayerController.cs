using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController CharacterController;

    public float characterspeed = 5, rotatespeed = 5;

    private float _rotationY;
        
        
    // Start is called before the first frame update
    void Start()
    {
        CharacterController = GetComponent<CharacterController>();
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
