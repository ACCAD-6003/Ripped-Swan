using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwanAttackState : MonoBehaviour, ISwanState
{
    private Swan swan;
    float cooldown = 0.3f;
    public SwanAttackState(Swan swan)
    {
        this.swan = swan;
        //next = Time.time + cooldown;
    }

    public void Attack()
    {
        // already attacking
    }

    public void Die()
    {
        swan.state = new SwanDeathState(swan);
    }

    // TODO: Implement
    void Update()
    {
        // If swan is attacking, enable bounding box collision
        if (swan.state is SwanAttackState)
        {

        }
        // If swan stops attacking, switch to move state
        swan.state = new SwanMoveState(swan);
    }
}
