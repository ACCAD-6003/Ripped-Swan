using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwanJump : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] private float jumpForce;
    [SerializeField] private float flapForce;
    [SerializeField] private int totalFlaps;
     private int flaps;
    enum JumpStates { GROUNDED, AIRBORN}

    JumpStates jumpState;

    void Start()
    {
       rb = GetComponent<Rigidbody>();
       jumpState = JumpStates.GROUNDED;
        flaps = totalFlaps;
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
            jumpState = JumpStates.AIRBORN;
        }

        else
        {
            flap();
        }
    }

    private void flap()
    {
        if (flaps > 0)
        {
            rb.AddForce(transform.up * flapForce, ForceMode.VelocityChange);
            flaps--;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            jumpState = JumpStates.GROUNDED;
            flaps = totalFlaps;
        }
    }
}
