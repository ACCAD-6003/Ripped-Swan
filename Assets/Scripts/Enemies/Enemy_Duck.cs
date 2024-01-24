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
        hitPoints = 1;
    }

    public void Attack()
    {
        // The attack for a basic duck is just moving towards you
    }

    public void TakeDamage()
    {
        hitPoints--;
        if (hitPoints < 0) Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }

}
