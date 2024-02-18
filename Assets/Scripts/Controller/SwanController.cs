using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwanController : MonoBehaviour
{
    public bool held;
    [SerializeField] private MovementControl movementController;
    [SerializeField] private JumpControl jumpController;
    [SerializeField] private AttackControl attackController;
    [SerializeField] private BlockControl blockController;
    [SerializeField] private HeavyAttackControl heavyAttackControl;
    [SerializeField] private SpecialAttackControl specialAttackControl;
    private PlayerInput pScheme;
    


    // calls handler scripts and passes them input actions and needed components
    private void Awake()
    {
        pScheme = new PlayerInput();
        movementController.Initialize(pScheme.Base.Movement);
        jumpController.Initialize(pScheme.Base.Jump);
        attackController.Initialize(pScheme.Base.Attack);
        heavyAttackControl.Initialize(pScheme.Base.HeavyAttack);
        specialAttackControl.Initialize(pScheme.Base.SpecialAttack);
        blockController.Initialize(pScheme.Base.Block);

        pScheme.Base.Hold.performed += _ => held = true;
        pScheme.Base.Hold.canceled += _ => held = false;


    }
    private void OnEnable()
    {
        pScheme.Enable();
    }

}

