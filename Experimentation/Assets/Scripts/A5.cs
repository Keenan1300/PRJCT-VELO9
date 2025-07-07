using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class A5 : MonoBehaviour
{

    public CharacterController CharacterController;

    private InputAction _moveAction, _lookAction;

    // Start is called before the first frame update
    void Start()
    {
        //_moveAction = InputSystem.actions.FindAction("Move");
        //_moveAction = InputSystem.actions.FindAction("Look");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 MovementVector = _moveAction.ReadValue<Vector2>();
        CharacterController.Move(MovementVector);

        Vector2 LookVector = _lookAction.ReadValue<Vector2>();
        CharacterController.Rotate(LookVector);
    }
}
