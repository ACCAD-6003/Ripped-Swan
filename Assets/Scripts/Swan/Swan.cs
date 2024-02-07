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
    public float powerUpStart;

    [Tooltip("Health Points")]
    [Range(0, 100)]
    public int healthPoints;
    public ISwanState state;

    public bool swanPoweredUp;

    public AudioSource punch_hit;
    public AudioSource punch_miss;
    public AudioSource powerUp_Sound;
    public AudioSource hurt;

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
       // if (Input.GetMouseButtonDown(0))
         //   state = new SwanAttackState(this);
        state.Update();
        checkPowerUp();
     } 

    private void checkPowerUp()
    {
        if (swanPoweredUp && Time.time - powerUpStart > 8) // Powerup lasts for 10 seconds
        {
            swanPoweredUp = false;
            transform.localScale = Vector3.one;
        }
    }


    public void attack()
    {
        state = new SwanAttackState(this);
    }

    public void hit()
    {
        Debug.Log("Player hit!");
        healthPoints--;
        hurt.Play();
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
            punch_hit.Play();  
            IEnemy enemy = other.gameObject.GetComponent<IEnemy>();
            enemy.TakeDamage(damage, swanPoweredUp);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bread")
        {
            swanPoweredUp = true;
            transform.localScale = Vector3.one * 2;
            powerUpStart = Time.time;
            powerUp_Sound.Play();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "health_pickup")
        {
            healthPoints++;
            Destroy(collision.gameObject);
        }
    }
}
