using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwanAttackState : ISwanState
{
    private Swan swan;
    float cooldown = 0.2f;
    float next;
    public SwanAttackState(Swan swan)
    {
        this.swan = swan;
        next = Time.time + cooldown;
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
    public void Update()
    {
        // If swan is in attack state, set attack anim to be true
        if (swan.state is SwanAttackState)
        {
            swan.arm_1.SetBool("attack", true);
            // If swan stops attacking, switch to move state
            if (Time.time > next)
            {
                swan.arm_1.SetBool("attack", false);
                swan.arm_2.SetBool("attack", false);
                swan.state = new SwanMoveState(swan);
            }
        }
    }
}
