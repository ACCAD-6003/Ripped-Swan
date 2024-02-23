using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BlockControl : MonoBehaviour
{
    [SerializeField] private Swan s;

    public void Initialize(InputAction blockAction)
    {
        blockAction.performed += BlockAction_performed;
        blockAction.canceled += BlockAction_cancelled;

        blockAction.Enable();

    }

    private void BlockAction_cancelled(InputAction.CallbackContext context)
    {
       s.EndBlock();
    }

    private void BlockAction_performed(InputAction.CallbackContext obj)
    {
        if (s.state is SwanMoveState)
        {
            s.StartBlock();
        }
       
    
    }


}
