using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    void Awake()
    {
 #if ENABLE_INPUT_SYSTEM
        //InputSystem.DisableDevice(Keyboard.current);
        //InputSystem.DisableDevice(Gamepad.current);
 #endif
    }
}


