using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

internal class SwanBlockState : ISwanState
{
    private Swan swan;

    public SwanBlockState(Swan swan)
    {
        this.swan = swan;
    }


    public void Update()
    {
        if (swan.spriteAnimator.GetBool("block") == false)
            swan.spriteAnimator.SetBool("block", true);
    }
}