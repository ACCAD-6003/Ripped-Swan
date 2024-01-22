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
    void Start()
    {
        state = new SwanMoveState(this);
        healthPoints = 5;
    }

    void Update()
    {
        
    }

    public void hit()
    {
        healthPoints--;
    }
}
