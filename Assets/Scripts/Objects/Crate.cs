using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class Crate : MonoBehaviour
{
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            rb.AddForce(MovementControl.direction * 10, ForceMode.Impulse);
        }
    }
}
