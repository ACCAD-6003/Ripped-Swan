using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwanAnimations : MonoBehaviour
{
    private Animator swanAnimator;
    void Start()
    {
        swanAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || 
            Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.S))
        {
            swanAnimator.SetBool("isWalking",true);
        } else
        {
            swanAnimator.SetBool("isWalking", false);
        }
    }
}