using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpControl : MonoBehaviour
{


    public void Initialize(InputAction jumpAction)
    {
        jumpAction.performed += jumpAction_performed;
        jumpAction.Enable();
    }
    // Start is called before the first frame update
    private void jumpAction_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Jump");
    }
}
