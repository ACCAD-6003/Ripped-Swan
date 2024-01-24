using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwanController : MonoBehaviour
{

    [SerializeField] private MovementControl movementController;
    [SerializeField] private JumpControl jumpController;
  
    private PlayerInput pScheme;
    


    // calls handler scripts and passes them input actions and needed components
    private void Awake()
    {
        pScheme = new PlayerInput();
        movementController.Initialize(pScheme.Base.Movement);
        jumpController.Initialize(pScheme.Base.Jump);

     
    }
    private void OnEnable()
    {
       
    }
}

