using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwanAttackState : ISwanState
{
    private Swan swan;
    float cooldown; // cooldown based on animation length, this needs to change when we get the final sprites
    float next;
    private string attackType;

    public SwanAttackState(Swan swan, string type)
    {
        // Cooldown lengths obtained from animation length
        if (type == "punch") cooldown = 0.35f;
        if (type == "heavy") cooldown = 0.4f;
        if (type == "special") { 
            cooldown = 0.8f;
            Time.timeScale = 0.25f;
        }
        this.swan = swan;
        next = Time.time + cooldown;
        this.attackType = type;
    }

    public void Update()
    {
        // If swan is in attack state, set attack anim to be true
        if (swan.state is SwanAttackState)
        {
            swan.boxCollider.enabled = true;
            swan.animator.SetBool(attackType, true);
            // If swan stops attacking, switch to move state
            if (Time.time > next)
            {
                swan.boxCollider.enabled = false;
                swan.animator.SetBool(attackType, false);
                if (attackType == "special") Time.timeScale = 1.0f;
                swan.state = new SwanMoveState(swan);
            }
        }
    }
}
