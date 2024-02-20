using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpControl : MonoBehaviour
{
    [SerializeField] private SwanJump sj;

    public void Initialize(InputAction jumpAction)
    {
        jumpAction.performed += JumpAction_performed;
        jumpAction.Enable();

    }
    
    private void JumpAction_performed(InputAction.CallbackContext obj)
    {
        //Debug.Log("Jump");
        sj.Jump();
    }


}
