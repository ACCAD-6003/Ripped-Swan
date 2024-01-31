using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swan : MonoBehaviour
{
    [Tooltip("Health Points")]
    [Range(0, 10)]
    public int healthPoints;
    public ISwanState state;

    public Animator animator;

    void Start()
    {
        state = new SwanMoveState(this);
        healthPoints = 5;

        animator = gameObject.transform.Find("SwanSprite").GetComponent<Animator>();
    }

     void Update()
    {
        if (Input.GetMouseButtonDown(0))
            state = new SwanAttackState(this);
        state.Update();
    } 


    public void attack()
    {
       
            state = new SwanAttackState(this);
       
    }

    public void hit()
    {
        Debug.Log("Player hit!");
        healthPoints--;
        if (healthPoints <= 0)
        {
            state = new SwanDeathState(this);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            if (state is SwanAttackState)
            {
                Debug.Log("Enemy Hit!");
                IEnemy enemy = other.gameObject.GetComponent<IEnemy>();
                enemy.TakeDamage();
            }
        }
    }
}
