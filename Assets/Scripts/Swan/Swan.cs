using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Swan : MonoBehaviour
{
    public static int enemiesKilled;
    public Animator animator;
    public BoxCollider boxCollider;
    private int damage;

    [Tooltip("Health Points")]
    [Range(0, 10)]
    public int healthPoints;
    public ISwanState state;

    public bool swanPoweredUp;

    void Start()
    {
        state = new SwanMoveState(this);
        enemiesKilled = 0;
        damage = 1;

        animator = gameObject.transform.Find("SwanSprite").GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;

        swanPoweredUp = false;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy" &&
            state is SwanAttackState)
        {
            IEnemy enemy = other.gameObject.GetComponent<IEnemy>();
            enemy.TakeDamage(damage);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bread")
        {
            swanPoweredUp = true; // TODO: implement power up mechanic
        }
    }
}
