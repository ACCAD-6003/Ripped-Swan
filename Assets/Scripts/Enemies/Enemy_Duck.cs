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


    Rigidbody rb;
    public Transform spriteTransform;

    public bool isKockedOut;
    public float knockoutStart;

    void Awake() { 
        behavior = GetComponent<EnemyBehavior>();
    }

    public void Start()
    {
        damage = 0.5;
        hitPoints = 3;
        rb = GetComponent<Rigidbody>();
        spriteTransform = this.gameObject.transform.GetChild(0);
        isKockedOut = false;
    }

    public void Attack()
    {
        // The attack for a basic duck is just moving towards you
    }

    private void Update()
    {
        if (isKnockoutOver()) UnfreezeObject();
    }

    public void TakeDamage(int damage, bool isPoweredUp)
    {
        Debug.Log("Enemy Hit!");
        hitPoints -= damage;
        KnockbackEnemy();
        if (HitPoints <= 0)
        {
            Die();
            Swan.enemiesKilled++;
        }
        if (isPoweredUp && !isKockedOut)
            KnockedOut();
    }

    public void KnockedOut() {
        Debug.Log("ENEMY KNOCKED OUT");
        knockoutStart = Time.time;
        isKockedOut = true;

        spriteTransform.Rotate(new Vector3(0,0,1), 180);
        Vector3 position = spriteTransform.position;
        position.y -= 1;
        spriteTransform.position = position;

        FreezeObject(gameObject);
    }

    // Method to freeze a GameObject's position and rotation
    public void FreezeObject(GameObject obj)
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void UnfreezeObject()
    {
        isKockedOut = false;
        rb.constraints = RigidbodyConstraints.None; 
        spriteTransform.Rotate(new Vector3(0, 0, 1), 180);
        Vector3 position = spriteTransform.position;
        position.y += 1;
        spriteTransform.position = position;
    }

    private bool isKnockoutOver()
    {
        return isKockedOut && Time.time - knockoutStart > 3;
    }

    public void Die()
    {
        Debug.Log("Enemy Dead!");
        Destroy(gameObject);
    }

    private void KnockbackEnemy()
    {
        // TODO: change to use rigidbody instead of transform
        // rb.AddForce(MovementControl.direction * 30, ForceMode.Impulse);
        Vector3 position = gameObject.transform.position;
        position.x = position.x + (3 * MovementControl.direction.x);
        gameObject.transform.position = position;
    }
}
