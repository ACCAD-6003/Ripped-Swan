using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemController : MonoBehaviour
{
    [SerializeField] private Swan s;
   /* [SerializeField] private int northItemCost = 5;
    [SerializeField] private int southItemCost = 5;
    [SerializeField] private int eastItemCost = 5;
    [SerializeField] private int westItemCost = 5;*/
    [SerializeField] private int healPower = 25;



    public void Initialize(InputAction northAction, InputAction southAction, InputAction westAction, InputAction eastAction)
    {
        northAction.performed += ItemNorthAction_performed;
        northAction.Enable();
       // southAction.performed += ItemSouthAction_performed;
       // southAction.Enable();
        westAction.performed += ItemWestAction_performed;
        westAction.Enable();
        eastAction.performed += ItemEastAction_performed;
        eastAction.Enable();



    }

    private void ItemNorthAction_performed(InputAction.CallbackContext obj)
    {
        if (s.state is SwanMoveState && Swan.SpendFeathers(Swan.specialCap))
        {
            s.attack("special");
        }


    }

  /*  private void ItemSouthAction_performed(InputAction.CallbackContext obj)
    {
        if (!s.Attacking && Swan.SpendFeathers(southItemCost))
        {

        }

    }*/

    private void ItemWestAction_performed(InputAction.CallbackContext obj)
    {
        if (s.state is SwanMoveState && Swan.SpendFeathers(Swan.healCap))
        {
            s.Heal(healPower);
        }

    }

    private void ItemEastAction_performed(InputAction.CallbackContext obj)
    {
        if (s.state is SwanMoveState && Swan.SpendFeathers(Swan.growCap))
        {
            s.powerUp();
        }

    }
}
