using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    //private GameObject startPos;
    public static Vector3 lastCheckPointPos = new Vector3(-6, 0, -2);

    private void Start()
    {
        lastCheckPointPos = transform.position;
    }

    private void Awake()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = lastCheckPointPos;
    }

}
