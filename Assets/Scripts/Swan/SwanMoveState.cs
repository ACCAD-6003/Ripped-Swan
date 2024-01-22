using Assets.Scripts.Interfaces;
using UnityEngine;

internal class SwanMoveState : MonoBehaviour, ISwanState
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
}