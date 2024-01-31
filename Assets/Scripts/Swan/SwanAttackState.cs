using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwanAttackState : ISwanState
{
    private Swan swan;
    float cooldown = 0.3f; // cooldown based on animation length, this needs to change when we get the final sprites
    float next;

    public SwanAttackState(Swan swan)
    {
        this.swan = swan;
        next = Time.time + cooldown;
    }

    public void Update()
    {
        // If swan is in attack state, set attack anim to be true
        if (swan.state is SwanAttackState)
        {
            swan.animator.SetBool("swanAttack1",true);
            // If swan stops attacking, switch to move state
            if (Time.time > next)
            {
                swan.animator.SetBool("swanAttack1", false);
                swan.state = new SwanMoveState(swan);
            }
        }
    }
}
