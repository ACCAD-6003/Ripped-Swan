using Assets.Scripts.Interfaces;
using UnityEngine;

public class SwanMoveState : ISwanState
{
    private Swan swan;
    
    public SwanMoveState(Swan swan)
    {
        this.swan = swan;
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.S))
        {
            //swan.animator.SetBool("isWalking", true);
        }
        else
        {
            //swan.animator.SetBool("isWalking", false);
        }
    }
}