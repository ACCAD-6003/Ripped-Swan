using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    double HitPoints { get; set; }
    double Damage { get; }

    void Attack();
    void Die();
    void TakeDamage(int damage);
}