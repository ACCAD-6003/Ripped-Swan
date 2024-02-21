using Assets.Scripts.Interfaces;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Swan : MonoBehaviour
{
    public static int feathers;
    public Animator spriteAnimator;
    public Animator specialMovementAnimator;
    public BoxCollider boxCollider;
    public int damage;
    public float powerUpStart;
    [SerializeField] private float scaleFactor = 2f;
    [SerializeField] private float powerUpDuration = 10f;
    
    [Tooltip("Damage taken")]
    [Range(0, 10)]
    [SerializeField] private int damageTake = 1; // how much damage the player takes from a hit

    [Tooltip("Health Points")]
    [Range(0, 100)]
    public int healthPoints;
    private int maxHealth;
    public int blockPoints;
    public ISwanState state;

    public bool swanPoweredUp;

    public AudioSource punch_hit;
    public AudioSource punch_miss;
    public AudioSource powerUp_Sound;
    public AudioSource hurt;
    public AudioSource special;

    public AudioSource bite;
    public AudioSource walk;
    public AudioSource jump;
    public AudioSource lowhp;
    public AudioSource swanDeath;

    public ParticleSystem Explosion;
    public bool Attacking;// This is so you can't start attacking when already attacking

    public AudioSource[] PunchSound;
    
    enum Attacks
    {
        NORMAL,
        HEAVY,
        SPECIAL
    }

    void Start()
    {
        spriteAnimator = gameObject.transform.Find("SwanSprite").GetComponent<Animator>();
        specialMovementAnimator = gameObject.GetComponent<Animator>();
        state = new SwanMoveState(this);

        Attacking = false;
        maxHealth = healthPoints;
        feathers = 0;
        damage = 1;
        blockPoints = 5;

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
        checkHP();
     }
    
    public void TurnOffAnimations()
    {
        foreach (AnimatorControllerParameter parameter in spriteAnimator.parameters)
        {
            spriteAnimator.SetBool(parameter.name, false);
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

    private void checkHP()
    {
        if (healthPoints <= 15 && !lowhp.isPlaying)
            lowhp.Play();
        else if (healthPoints > 15)
            lowhp.Stop();
    }


    public void attack(String attackType)
    {
        if (state is not SwanAttackState)
            state = new SwanAttackState(this, attackType);
    }

    public void block()
    {
        if (state is not SwanBlockState)
            state = new SwanBlockState(this);
        else
        {
            spriteAnimator.SetBool("block", false);
            state = new SwanMoveState(this);
        }
    }

    private void breakBlock()
    {
        blockPoints = 5; // reset block points
        spriteAnimator.SetBool("block", false);
        state = new SwanMoveState(this);
        // TODO: play break block sound effect
    }

    public void hit()
    {
        // Swan can only get hurt if it is not blocking
        if (state is not SwanBlockState)
        {
            //Debug.Log("Player hit!");
            healthPoints -= damageTake;
            hurt.Play();
            if (healthPoints <= 0 && state is not SwanDeathState)
                state = new SwanDeathState(this);
            else
            {
                // changes to swan hurt state
                if (state is not SwanHurtState) state = new SwanHurtState(this);
            }
        } 
        else
        {
            blockPoints--;
            if (blockPoints <= 0)
                breakBlock();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy" &&
            state is SwanAttackState)
        {
            PunchSound[Random.Range(0, 29)].Play();
            IEnemy enemy = other.gameObject.GetComponent<IEnemy>();
            enemy.TakeDamage(damage, swanPoweredUp);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bread")
        {
            powerUp();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "health_pickup")
        {
            healthPoints++;
            Destroy(collision.gameObject);
        }
    }

    public void powerUp()
    {
        swanPoweredUp = true;
        transform.localScale *= scaleFactor;
        powerUpStart = Time.time;
        powerUp_Sound.Play();
    }

    public void Heal(int healPower)
    {
        if (healthPoints + healPower > 100)
            healthPoints = maxHealth;
        else
            healthPoints += healPower;
    }

    public static bool SpendFeathers(int cost)
    {
        if (cost < feathers)
        {
            if (feathers - cost < 0)
                feathers = 0;
            else
                feathers -= cost;
            return true;
        }
        return false;
    }
}
