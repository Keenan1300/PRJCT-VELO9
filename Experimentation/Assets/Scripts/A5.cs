using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class A5 : MonoBehaviour
{

    public PlayerController PlayerController;

    public InputAction _moveAction, _lookAction;

   
    private void OnEnable()
    {
        _moveAction.Enable();
        _lookAction.Enable();
    }

    private void OnDisable()
    {
        _moveAction.Disable();
        _lookAction.Disable();
    }

   

    // Start is called before the first frame update
    void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _lookAction = InputSystem.actions.FindAction("Look");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 MovementVector = _moveAction.ReadValue<Vector2>();
        PlayerController.Move(MovementVector);

        Vector2 LookVector = _lookAction.ReadValue<Vector2>();
        PlayerController.Rotate(LookVector);
    }
}
