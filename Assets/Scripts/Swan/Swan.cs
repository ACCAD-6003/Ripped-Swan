using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swan : MonoBehaviour
{
    [Tooltip("Health Points")]
    [Range(0, 10)]
    public int healthPoints;
    public ISwanState state;

    public Animator arm_1;
    public Animator arm_2;

    // I know this is janky, but it should work for now - Musab
    // Used to switch arms during attack
    public string arm;

    void Start()
    {
        arm = "left";

        state = new SwanMoveState(this);
        healthPoints = 5;

        arm_1 = gameObject.transform.Find("Arm_1/Arm").GetComponent<Animator>();
        arm_2 = gameObject.transform.Find("Arm_2/Arm").GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            state = new SwanAttackState(this);
        }
        state.Update();
    }

    public void hit()
    {
        healthPoints--;
        if (healthPoints <= 0)
        {
            state = new SwanDeathState(this);
        }
    }
}
