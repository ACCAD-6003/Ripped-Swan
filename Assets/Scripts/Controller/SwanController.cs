using System.Collections;
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
  //  [SerializeField] private SpecialAttackControl specialAttackControl;
    [SerializeField] private ItemController itemController;
    
    private PlayerInput pScheme;
    


    // calls handler scripts and passes them input actions and needed components
    private void Awake()
    {
        pScheme = new PlayerInput();
        movementController.Initialize(pScheme.Base.Movement);
        jumpController.Initialize(pScheme.Base.Jump);
        attackController.Initialize(pScheme.Base.Attack);
        heavyAttackControl.Initialize(pScheme.Base.HeavyAttack);
        //specialAttackControl.Initialize(pScheme.Base.SpecialAttack);
        blockController.Initialize(pScheme.Base.Block);
        pScheme.Base.Pause.performed += PauseAction_performed;
        itemController.Initialize(pScheme.Base.ItemNorth, pScheme.Base.ItemSouth, pScheme.Base.ItemWest, pScheme.Base.ItemEast);
        pScheme.Base.Hold.performed += _ => held = true;
        pScheme.Base.Hold.canceled += _ => held = false;


    }
    private void OnEnable()
    {
        pScheme.Enable();
        pScheme.Base.Pause.Enable();
    }

    private void PauseAction_performed(InputAction.CallbackContext obj)
    {
        PauseMenu.pauseToggle?.Invoke();
      
    }

}

