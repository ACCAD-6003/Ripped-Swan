using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwanJump : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float jumpForce;
    [SerializeField] private float flapForce;
    [SerializeField] private int totalFlaps;
    [SerializeField] private int flaps;
    enum JumpStates { GROUNDED, AIRBORN}

    JumpStates jumpState;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
       jumpState = JumpStates.GROUNDED;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jump()
    {
        if(jumpState == JumpStates.GROUNDED)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
        }

        else
        {
            flap();
        }
    }

    private void flap()
    {
        rb.AddForce(transform.up * flapForce, ForceMode.VelocityChange);
    }


    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
