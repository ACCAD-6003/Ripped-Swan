using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    // The beginning of the level
    public static Vector3 lastCheckPointPos = new Vector3(-9.5f, 1, 0.5f);
    
    //Next to the train station
    //public static Vector3 lastCheckPointPos = new Vector3(140, 10, 1);

    private void Awake()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = lastCheckPointPos;
    }

}
