using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

internal class SwanHurtState : ISwanState
{
    private Swan swan;
    float cooldown, next;
    public SwanHurtState(Swan swan)
    {
        this.swan = swan;
        cooldown = 0.25f;
        next = Time.time + cooldown;
        swan.TurnOffAnimations();
    }


    public void Update()
    {
        swan.spriteAnimator.SetBool("hurt", true);
        if (Time.time > next)
        {
            swan.spriteAnimator.SetBool("hurt", false);
            swan.state = new SwanMoveState(swan);
        }
    }
}