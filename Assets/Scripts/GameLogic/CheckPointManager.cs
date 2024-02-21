using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public static Vector3 lastCheckPointPos = new Vector3(-9.5f, 1, 0.5f);

    private void Awake()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = lastCheckPointPos;
    }

}
