using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_Duck : MonoBehaviour, IEnemy
{
    private double hitPoints;
    public double HitPoints { get { return hitPoints; } set { hitPoints = value; } }

    private double damage;
    public double Damage { get { return damage; } }

    public void Start()
    {
        damage = 0.5;
        hitPoints = 3;
    }

    public void Attack()
    {
        // The attack for a basic duck is just moving towards you
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Enemy Hit!");
        hitPoints -=damage;
        KnockbackEnemy();
        if (HitPoints <= 0)
        {
            Die();
            Swan.enemiesKilled++;
        }
    }

    public void Die()
    {
        Debug.Log("Enemy Dead!");
        Destroy(gameObject);
    }

    void KnockbackEnemy()
    {
    }
}
