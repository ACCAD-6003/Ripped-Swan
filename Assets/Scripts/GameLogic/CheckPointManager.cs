using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    [SerializeField] private Transform startPos;
    public static Vector3 lastCheckPointPos = new Vector3(-6, 0, -2);

    private void Awake()
    {
        if (startPos != null)
        {
            lastCheckPointPos = startPos.position;
            
            Debug.Log("StartPos: " + lastCheckPointPos);
        }
    }

    private void Start()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = lastCheckPointPos;
    }

}
