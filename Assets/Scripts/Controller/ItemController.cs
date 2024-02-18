using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemController : MonoBehaviour
{
    [SerializeField] private Swan s;

    public void Initialize(InputAction northAction, InputAction southAction, InputAction westAction, InputAction eastAction)
    {
        northAction.performed += ItemNorthAction_performed;
        northAction.Enable();
        southAction.performed += ItemSouthAction_performed;
        southAction.Enable();
        westAction.performed += ItemWestAction_performed;
        westAction.Enable();
        eastAction.performed += ItemEastAction_performed;
        eastAction.Enable();



    }

    private void ItemNorthAction_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("NorthItem");
    
    }

    private void ItemSouthAction_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("SouthItem");

    }

    private void ItemWestAction_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("WestItem");

    }

    private void ItemEastAction_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("EastItem");

    }
}
