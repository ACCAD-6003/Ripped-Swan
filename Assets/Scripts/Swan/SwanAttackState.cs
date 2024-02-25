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
        if (type == "punch") 
        { 
            cooldown = 0.35f;
            swan.ArmSwingSound[Random.Range(0,9)].Play();
        }
        if (type == "heavy")
        {
            cooldown = 0.4f;
            swan.bite.Play();
        }
        if (type == "special") {
            swan.superArmor = true;
            //   swan.Attacking = true;
            EnemyWaveController.specialZoom?.Invoke();
            cooldown = 0.8f;
            Time.timeScale = 0.55f;
            swan.damage = 5;
            swan.special.Play();
        }
        this.swan = swan;
        next = Time.time + cooldown;
        this.attackType = type;
        swan.TurnOffAnimations();
    }

    public void Update()
    {

        if (attackType == "punch")
            Attack();
        else if (attackType == "heavy")
            HeavyAttack();
        else if(attackType == "special")
            SpecialAttack();
        
        
        
        
        /*  // If swan is in attack state, set attack anim to be true
        if (swan.state is SwanAttackState)
        {
            swan.boxCollider.enabled = true;
            swan.spriteAnimator.SetBool(attackType, true);
            // If swan stops attacking, switch to move state
            if (Time.time > next)
            {
                swan.boxCollider.enabled = false;
                swan.spriteAnimator.SetBool(attackType, false);
                if (attackType == "special")
                {
                    Time.timeScale = 1.0f;
                    swan.damage = 1;
                    //swan.Explosion.Play();
                }
              //  swan.Attacking = false;
                swan.state = new SwanMoveState(swan);
            }
        } */
    }

    private void Attack()
    {
        swan.boxCollider.enabled = true;
        swan.spriteAnimator.SetBool(attackType, true);

        if (Time.time > next)
        {
            swan.boxCollider.enabled = false;
            swan.spriteAnimator.SetBool(attackType, false);
           
            swan.state = new SwanMoveState(swan);
        }

    }

    private void HeavyAttack()
    {
        swan.boxCollider.enabled = true;
        swan.spriteAnimator.SetBool(attackType, true);

        if (Time.time > next)
        {
            swan.boxCollider.enabled = false;
            swan.spriteAnimator.SetBool(attackType, false);
           
            //  swan.Attacking = false;
            swan.state = new SwanMoveState(swan);
        }
    }

    private void SpecialAttack()
    {
        
        swan.spriteAnimator.SetBool(attackType, true);
        if (Time.time < next && Time.time > next -0.1f)
        {
            swan.boxCollider.enabled = true;
            
            swan.explosionSound.Play();
            swan.explosionParticleSystem.Play();
        }
        if (Time.time > next)
        {
            swan.boxCollider.enabled = false;
            swan.spriteAnimator.SetBool(attackType, false);
           
                Time.timeScale = 1.0f;
                swan.damage = 1;

            swan.superArmor = false;
            swan.state = new SwanMoveState(swan);
        }
    }
}
