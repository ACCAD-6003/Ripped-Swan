using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_Duck : MonoBehaviour, IEnemy
{
    EnemyBehavior behavior;
    private double hitPoints;
    public double HitPoints { get { return hitPoints; } set { hitPoints = value; } }

    private double damage;
    public double Damage { get { return damage; } }

    public SpriteRenderer spriteRenderer; // Now public

    Rigidbody rb;

    public Color damageFlashColor = Color.red;
    public float damageFlashDuration = 0.1f;

    void Awake()
    {
        behavior = GetComponent<EnemyBehavior>();
        rb = GetComponent<Rigidbody>();
    }

    public void Start()
    {
        damage = 0.5;
        hitPoints = 3;

        // If spriteRenderer is not assigned, try to get it from the GameObject
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    public void Attack()
    {
        // The attack for a basic duck is just moving towards you
    }

    public void TakeDamage(int damage)
    {
        StartCoroutine(FlashDamage());
        hitPoints -= damage;
        KnockbackEnemy();
        if (HitPoints <= 0)
        {
            Die();
            Swan.enemiesKilled++;
        }
    }

    private IEnumerator FlashDamage()
    {
        // Change sprite color to damageFlashColor
        spriteRenderer.color = damageFlashColor;

        // Wait for the specified duration
        yield return new WaitForSeconds(damageFlashDuration);

        // Revert back to the original color
        spriteRenderer.color = Color.white; // You can use the originalColor variable if you have it defined
    }

    public void Die()
    {
        Debug.Log("Enemy Dead!");
        Destroy(gameObject);
    }

    private void KnockbackEnemy()
    {
        rb.AddForce(MovementControl.direction * 10, ForceMode.Impulse);
    }
}
