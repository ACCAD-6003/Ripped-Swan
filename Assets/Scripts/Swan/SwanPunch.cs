using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwanPunch : MonoBehaviour
{
    [Tooltip("Health Points")]
    [Range(0, 10)]
    public int healthPoints;
    public ISwanState state;

    public Animator arm_1;
    public Animator arm_2;

    // Used to switch arms during attack
    public enum Arm
    {
        Left,
        Right
    }
    public Arm arm;

    void Start()
    {
        arm = Arm.Left;

        state = new SwanMoveState(this);
        healthPoints = 5;

        arm_1 = gameObject.transform.Find("Arm_1/Arm").GetComponent<Animator>();
        arm_2 = gameObject.transform.Find("Arm_2/Arm").GetComponent<Animator>();
    }

     void Update()
    {
       
        state.Update();
    } 


    public void attack()
    {
       
            state = new SwanAttackState(this);
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state is SwanAttackState && collision.gameObject.tag == "enemy")
        {
            Debug.Log("Enemy Hit!");
            IEnemy enemy = collision.gameObject.GetComponent<IEnemy>();
            enemy.TakeDamage();
        } 
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
}
