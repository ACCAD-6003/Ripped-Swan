using Assets.Scripts.Interfaces;
using UnityEngine;

public class SwanMoveState : ISwanState
{
    private Swan swan;
    
    public SwanMoveState(Swan swan)
    {
        this.swan = swan;
        swan.TurnOffAnimations();
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.S))
        {
            swan.spriteAnimator.SetBool("isWalking", true);
            if (!swan.walk.isPlaying)
                swan.walk.Play();
        }
        else
        {
            swan.spriteAnimator.SetBool("isWalking", false);
            swan.walk.Stop();
        }
    }
}