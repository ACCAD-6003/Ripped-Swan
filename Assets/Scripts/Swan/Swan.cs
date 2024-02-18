using Assets.Scripts.Interfaces;
using System;
using UnityEngine;

public class Swan : MonoBehaviour
{
    public static int enemiesKilled;
    public Animator spriteAnimator;
    public Animator specialMovementAnimator;
    public BoxCollider boxCollider;
    public int damage;
    public float powerUpStart;
    [SerializeField] private float scaleFactor = 2f;
    [SerializeField] private float powerUpDuration = 10f;

    [Tooltip("Health Points")]
    [Range(0, 100)]
    public int healthPoints;
    public ISwanState state;

    public bool swanPoweredUp;

    public AudioSource punch_hit;
    public AudioSource punch_miss;
    public AudioSource powerUp_Sound;
    public AudioSource hurt;
    public AudioSource special;

    public ParticleSystem Explosion;

    enum Attacks
    {
        NORMAL,
        HEAVY,
        SPECIAL
    }

    void Start()
    {
        state = new SwanMoveState(this);
        enemiesKilled = 0;
        damage = 1;

        spriteAnimator = gameObject.transform.Find("SwanSprite").GetComponent<Animator>();
        specialMovementAnimator = gameObject.GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;

        swanPoweredUp = false;
    }

     void Update()
     {
       // if (Input.GetMouseButtonDown(0))
         //   state = new SwanAttackState(this);
        state.Update();
        
        if (swanPoweredUp)
        {
            checkPowerUp();
        }
     } 

    private void checkPowerUp()
    {
        if (Time.time - powerUpStart > powerUpDuration) // Powerup lasts for 10 seconds
        {
            swanPoweredUp = false;
            transform.localScale *= 1/scaleFactor;
        }
    }


    public void attack(String attackType)
    {
        if (state is not SwanAttackState)
            state = new SwanAttackState(this, attackType);
    }

    public void hit()
    {
        //Debug.Log("Player hit!");
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
            transform.localScale *= scaleFactor;
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
