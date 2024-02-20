using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpControl : MonoBehaviour
{
    [SerializeField] private SwanJump sj;
    [SerializeField] private Swan s;

    public void Initialize(InputAction jumpAction)
    {
        jumpAction.performed += JumpAction_performed;
        jumpAction.Enable();

    }
    
    private void JumpAction_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Jump");
        s.jump.Play();
        sj.Jump();
    }
}
