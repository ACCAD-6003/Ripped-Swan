using Assets.Scripts.Interfaces;
using UnityEngine;

internal class SwanMoveState : ISwanState
{
    private Swan swan;
    
    public SwanMoveState(Swan swan)
    {
        this.swan = swan;
    }

    public void Attack()
    {
        swan.state = new SwanAttackState(swan);
    }

    public void Die()
    {
        swan.state = new SwanDeathState(swan);
    }

    public void Update()
    {
        /* TODO: Change sprite animation based on direction player is facing */
    }
}